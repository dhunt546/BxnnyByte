using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float playerHealth;

    public static int Score { get; set; }

   [SerializeField]
    private static float playerLevel;

    public static float PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = Mathf.Clamp(value, 1, 8); }
    }
}
