using System.Collections;
using UnityEngine;
using TMPro;

public class ZonaSinSombra : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject; // Asigna esto en el Inspector
    [SerializeField] private ScoreData scoreData; // Referencia al Scriptable Object que almacena el puntaje
    private bool isInZone = false; // Verifica si está en la zona

    private void Start()
    {
        UpdateHealthDisplay(); // Muestra la salud inicial
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objetos en colisión: " + other.gameObject.name); // Mensaje para verificar colisión
        if (other.CompareTag("Player")) // Verifica si es el jugador
        {
            isInZone = true; // Está en la zona
            Debug.Log("Entró en la zona"); // Mensaje de depuración
            StartCoroutine(DecreaseScoreCoroutine()); // Inicia la Coroutine
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false; // Sale de la zona
            Debug.Log("Salió de la zona"); // Mensaje de depuración
            StopAllCoroutines(); // Detiene la Coroutine
        }
    }






    private IEnumerator DecreaseScoreCoroutine()
    {
        while (isInZone && scoreData.scoreNumber > 0)
        {
            yield return new WaitForSeconds(1f); // Espera un segundo
            DecreaseScore(); // Reduce la salud
            UpdateHealthDisplay(); // Actualiza el texto
        }
    }

    private void DecreaseScore()
    {
        if (scoreData.scoreNumber > 0)
        {
            scoreData.scoreNumber--; // Reduce la salud
            Debug.Log("Salud reducida: " + scoreData.scoreNumber); // Mensaje de depuración
        }
    }

    private void UpdateHealthDisplay()
    {
        textObject.text = "Salud: " + scoreData.scoreNumber; // Actualiza el texto
    }
}
