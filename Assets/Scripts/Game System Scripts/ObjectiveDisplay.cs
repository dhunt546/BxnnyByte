using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour
{
    public TextMeshProUGUI debrisText;
    public TextMeshProUGUI enemyText;

    int enemyCount;

    void Start()
    {
        UpdateCounts();
        UpdateText();
    }

    void Update()
    {
        UpdateCounts();
        UpdateText();
    }

    void UpdateCounts()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    void UpdateText()
    {
        if (LevelFinishDoor.debrisCount <= 0 )
        {
            debrisText.text = ":Complete";
        }
        if (LevelFinishDoor.debrisCount >= 1)
        {
            debrisText.text = ":" + LevelFinishDoor.debrisCount.ToString();
        }
        
        if (enemyCount <= 0 )
        {
            enemyText.text = ":Safe";
        }
        if (enemyCount >= 1)
        {
            enemyText.text = ":" + enemyCount.ToString();
        }        
    }
}
