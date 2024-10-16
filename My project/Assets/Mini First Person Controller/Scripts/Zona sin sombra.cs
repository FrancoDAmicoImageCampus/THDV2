using System.Collections;
using UnityEngine;
using TMPro;

public class ZonaSinSombra : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject; // Asigna esto en el Inspector
    private static int playerScore = 300; // Salud inicial del jugador
    private bool isInZone = false; // Verifica si est� en la zona

    private void Start()
    {
        UpdateHealthDisplay(); // Muestra la salud inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objetos en colisi�n: " + other.gameObject.name); // Mensaje para verificar colisi�n
        if (other.CompareTag("Player")) // Verifica si es el jugador
        {
            isInZone = true; // Est� en la zona
            Debug.Log("Entr� en la zona"); // Mensaje de depuraci�n
            StartCoroutine(DecreaseScoreCoroutine()); // Inicia la Coroutine
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false; // Sale de la zona
            Debug.Log("Sali� de la zona"); // Mensaje de depuraci�n
            StopAllCoroutines(); // Detiene la Coroutine
        }
    }

    private IEnumerator DecreaseScoreCoroutine()
    {
        while (isInZone && playerScore > 0)
        {
            yield return new WaitForSeconds(1f); // Espera un segundo
            DecreaseScore(); // Reduce la salud
            UpdateHealthDisplay(); // Actualiza el texto
        }
    }

    private void DecreaseScore()
    {
        if (playerScore > 0)
        {
            playerScore--; // Reduce la salud
            Debug.Log("Salud reducida: " + playerScore); // Mensaje de depuraci�n
        }
    }

    private void UpdateHealthDisplay()
    {
        textObject.text = "Salud: " + playerScore; // Actualiza el texto
    }
}
