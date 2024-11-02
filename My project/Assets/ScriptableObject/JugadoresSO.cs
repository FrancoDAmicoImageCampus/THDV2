using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData", order = 1)]
public class ScoreData : ScriptableObject
{
    public float speed = 5;
    public bool IsRunning { get; set; }
    [Header("Running")]
    public bool canRun = true;
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;
    public float StartHealth;
}