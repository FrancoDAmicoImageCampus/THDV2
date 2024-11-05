using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float speed = 5;
    public bool IsRunning { get; set; }
    [Header("Running")]
    public bool canRun = true;
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public float StartHealth = 100; // Ajusta el valor inicial de salud si es necesario
}