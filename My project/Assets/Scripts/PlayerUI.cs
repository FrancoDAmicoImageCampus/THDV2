using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public int scoreNumber;
    [SerializeField] private TMP_Text textObject; 
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private MonoBehaviour firstPersonMovement; 
    [SerializeField] private CharacterController characterController; 
    [SerializeField] private MonoBehaviour cameraController; 
    [SerializeField] private ScoreData scoreData; 

    private bool isInPause = false;

    private void Start()
    {
        //scoreData.scoreNumber = 300; 
        UpdateScoreText();
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false; 
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        
        if (!isInPause && Input.GetKeyDown(KeyCode.E))
        {
            IncreaseScore(1); 
        }
    }

    private void TogglePause()
    {
        isInPause = !isInPause;
        pauseMenu.SetActive(isInPause);
        Time.timeScale = isInPause ? 0 : 1; 

        
        firstPersonMovement.enabled = !isInPause;
        if (characterController != null)
        {
            characterController.enabled = !isInPause; 
        }

        
        if (cameraController != null)
        {
            cameraController.enabled = !isInPause; 
        }

        
        Cursor.visible = isInPause; 
        Cursor.lockState = isInPause ? CursorLockMode.None : CursorLockMode.Locked; 
    }

    public void IncreaseScore(int amount)
    {
        scoreNumber += amount; 
        UpdateScoreText(); 
    }

    private void UpdateScoreText()
    {
        textObject.text = "Salud: " + scoreNumber;
    }
}
