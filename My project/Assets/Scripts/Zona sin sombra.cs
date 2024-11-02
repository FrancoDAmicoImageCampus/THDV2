using System.Collections;
using UnityEngine;
using TMPro;

public class ZonaSinSombra : MonoBehaviour
{
    public float scoreNumber;
    [SerializeField] private TMP_Text textObject; 
    [SerializeField] private ScoreData scoreData; 
    private bool isInZone = false; 
    
    private void Awake()
    {
        scoreNumber = scoreData.StartHealth;       
    }
    private void Start()
    {
        UpdateHealthDisplay(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Objetos en colisión: " + other.gameObject.name); 
        if (other.CompareTag("Player")) 
        {
            isInZone = true; 
            Debug.Log("Entró en la zona"); 
            StartCoroutine(DecreaseScoreCoroutine()); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInZone = false; 
            Debug.Log("Salió de la zona"); 
            StopAllCoroutines(); 
        }
    }






    private IEnumerator DecreaseScoreCoroutine()
    {
        while (isInZone && scoreNumber > 0)
        {
            yield return new WaitForSeconds(1f); 
            DecreaseScore(); 
            UpdateHealthDisplay(); 
        }
    }

    private void DecreaseScore()
    {
        if (scoreNumber > 0)
        {
            scoreNumber--; 
            Debug.Log("Salud reducida: " + scoreNumber); 
        }
    }

    private void UpdateHealthDisplay()
    {
        textObject.text = "Salud: " + scoreNumber; 
    }
}