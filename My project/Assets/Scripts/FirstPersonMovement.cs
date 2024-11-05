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
    private float health; // Variable para la salud

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
        if (!transformado && Input.GetKeyDown(KeyCode.R))
        {
            vampiro.SetActive(false);
            murcielago.SetActive(true);
            transformado = true; 
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
        }
        else if (transformado && Input.GetKeyDown(KeyCode.R))
        {
            vampiro.SetActive(true);
            murcielago.SetActive(false);
            transformado = false; 
            rigidbody.useGravity = true;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -10f, rigidbody.velocity.z);
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