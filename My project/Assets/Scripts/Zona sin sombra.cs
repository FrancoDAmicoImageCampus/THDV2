using System.Collections;
using UnityEngine;
using TMPro;

public class ZonaSinSombra : MonoBehaviour
{
    public float damageAmount = 1f; // Cantidad de daño por segundo
    [SerializeField] private TMP_Text textObject; 
    private bool isInZone = false; 
    private FirstPersonMovement player; // Referencia al script del jugador

    private void Awake()
    {
        player = FindObjectOfType<FirstPersonMovement>(); // Busca el componente FirstPersonMovement en la escena
        UpdateHealthDisplay(); // Inicializa la visualización de salud
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            isInZone = true; 
            StartCoroutine(DecreaseHealthCoroutine()); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false; 
            StopAllCoroutines(); 
        }
    }

    private IEnumerator DecreaseHealthCoroutine()
    {
        while (isInZone)
        {
            yield return new WaitForSeconds(1f); 
            player.TakeDamage(damageAmount); // Llama al método de daño del jugador
            UpdateHealthDisplay(); // Actualiza la visualización de salud
        }
    }

    private void UpdateHealthDisplay()
    {
        // Aquí asumimos que 'player' tiene un método para obtener la salud actual
        float currentHealth = player.GetCurrentHealth(); // Asegúrate de que este método exista
        textObject.text = "Salud: " + currentHealth; // Actualiza el texto en el UI
    }
}