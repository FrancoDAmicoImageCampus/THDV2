using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    [SerializeField] private GameObject vampiro;
    [SerializeField] private GameObject murcielago;
    private Vector3 playerposition;
    bool transformado;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();



    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!transformado && Input.GetKeyDown(KeyCode.R)){
        vampiro.SetActive(false);
        murcielago.SetActive(true);
        transformado = true; 
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;

        //agregar que aca desactive la gravedad y el salto
        }
    else if (transformado && Input.GetKeyDown(KeyCode.R)){
        vampiro.SetActive(true);
        murcielago.SetActive(false);
        transformado = false; 
        rigidbody.useGravity = true;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, -10f, rigidbody.velocity.z);
        //agregar que aca active la gravedad y el salto
        }
    if (transformado && Input.GetKey(KeyCode.Space)){       
       transform.position += Vector3.up*35*Time.deltaTime;
      }
    if (transformado && Input.GetKey(KeyCode.C)){
        transform.position += Vector3.down*35*Time.deltaTime;
    }
}
   

 
 


    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            SceneManager.LoadScene("Nivel1"); 
        }
    }
}