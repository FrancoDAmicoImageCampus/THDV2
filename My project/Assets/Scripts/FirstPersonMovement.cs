using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [Header("Running")]
    [SerializeField] private GameObject vampiro;
    [SerializeField] private GameObject murcielago;

    private Rigidbody rigidbody;
    private bool transformado;
    private bool enCooldown; // Variable para controlar el cooldown de transformación
    private float health; // Variable para la salud
    private Coroutine revertirTransformacionCoroutine; // Corrutina para revertir la transformación

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        health = playerData.StartHealth; // Inicializa la salud desde ScoreData
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Lógica para cuando el jugador muere
            SceneManager.LoadScene("Nivel1"); // O cualquier otra acción que quieras
        }
    }

    void Update()
    {
        if (!transformado && !enCooldown && Input.GetKeyDown(KeyCode.R))
        {
            Transformarse();
        }
        else if (transformado && Input.GetKeyDown(KeyCode.R))
        {
            Destransformarse();
        }
        if (transformado && Input.GetKey(KeyCode.Space))
        {       
            transform.position += Vector3.up * 35 * Time.deltaTime;
        }
        if (transformado && Input.GetKey(KeyCode.C))
        {
            transform.position += Vector3.down * 35 * Time.deltaTime;
        }
    }

    private void Transformarse()
    {
        vampiro.SetActive(false);
        murcielago.SetActive(true);
        transformado = true;
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;

        // Inicia la corrutina para revertir la transformación después de 5 segundos
        if (revertirTransformacionCoroutine != null)
        {
            StopCoroutine(revertirTransformacionCoroutine);
        }
        revertirTransformacionCoroutine = StartCoroutine(RevertirTransformacion());
    }

    private void Destransformarse()
    {
        vampiro.SetActive(true);
        murcielago.SetActive(false);
        transformado = false;
        rigidbody.useGravity = true;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, -10f, rigidbody.velocity.z);

        // Detiene la corrutina si se destransforma manualmente
        if (revertirTransformacionCoroutine != null)
        {
            StopCoroutine(revertirTransformacionCoroutine);
            revertirTransformacionCoroutine = null;
        }

        // Inicia el cooldown de 2 segundos
        StartCoroutine(Cooldown());
    }

    private IEnumerator RevertirTransformacion()
    {
        yield return new WaitForSeconds(5f);
        Destransformarse();
    }

    private IEnumerator Cooldown()
    {
        enCooldown = true; // Activa el cooldown
        yield return new WaitForSeconds(2f); // Espera 2 segundos
        enCooldown = false; // Desactiva el cooldown
    }

    public float GetCurrentHealth()
    {
        return health; // Devuelve la salud actual
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        playerData.IsRunning = playerData.canRun && Input.GetKey(playerData.runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = playerData.IsRunning ? playerData.runSpeed : playerData.speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            SceneManager.LoadScene("Nivel1");
        }
    }
}