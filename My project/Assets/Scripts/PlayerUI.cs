using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textObject; // Asegúrate de que esto esté asignado en el Inspector
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private MonoBehaviour firstPersonMovement; // Referencia al script de movimiento
    [SerializeField] private CharacterController characterController; // Referencia al CharacterController
    [SerializeField] private MonoBehaviour cameraController; // Referencia al script de control de la cámara
    private int scoreNumber; // Inicializa scoreNumber

    private bool isInPause = false;
    private void Start()
    {
        scoreNumber = 300; // Inicializa el puntaje en 300
        UpdateScoreText(); // Actualiza el texto al inicio
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al inicio
        Cursor.visible = false; // Oculta el cursor al inicio
    }

    private void Update()
    {
        // Alterna la pausa al presionar V
        if (Input.GetKeyDown(KeyCode.V))
        {
            TogglePause();
        }

        // Aumenta el puntaje al presionar E, solo si no está pausado
        if (!isInPause && Input.GetKeyDown(KeyCode.E))
        {
            IncreaseScore(1); // Aumenta el puntaje en 1
        }
    }

    private void TogglePause()
    {
        isInPause = !isInPause;
        pauseMenu.SetActive(isInPause);
        Time.timeScale = isInPause ? 0 : 1; // Ajusta la escala del tiempo

        // Habilita o deshabilita el script de movimiento y el CharacterController
        firstPersonMovement.enabled = !isInPause;
        if (characterController != null)
        {
            characterController.enabled = !isInPause; // Desactiva el CharacterController si está presente
        }

        // Habilita o deshabilita el script de control de la cámara
        if (cameraController != null)
        {
            cameraController.enabled = !isInPause; // Desactiva el script de la cámara si está presente
        }

        // Muestra u oculta el cursor
        Cursor.visible = isInPause; // El cursor es visible si está en pausa
        Cursor.lockState = isInPause ? CursorLockMode.None : CursorLockMode.Locked; // Desbloquea o bloquea el cursor
    }
    //public void Resumir()
   // {
     //   pauseMenu.SetActive(false); 
     //   Time.timeScale = 1f;      
        //isInPause = false;
   // }
    public void IncreaseScore(int amount)
    {
        scoreNumber += amount; // Aumenta el puntaje
        UpdateScoreText(); // Actualiza el texto
    }

    private void UpdateScoreText()
    {
        textObject.text = "Salud: " + scoreNumber; // Muestra el puntaje
    }


}