using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public float playerHealth;

    [SerializeField]
    private float playerLevel;

    public float PlayerLevel
    {
        get { return playerLevel; }
        set { playerLevel = Mathf.Clamp(value, 1, 8); }
    }
}
