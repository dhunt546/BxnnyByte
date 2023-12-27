using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectiveDisplay : MonoBehaviour
{
    public TextMeshProUGUI debrisText;
    public TextMeshProUGUI enemyText;

    int debrisCount;
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
        debrisCount = GameObject.FindGameObjectsWithTag("Debris").Length;
    }

    void UpdateText()
    {
        if (debrisCount <= 0 )
        {
            debrisText.text = ":Complete";
        }
        if (debrisCount >= 1)
        {
            debrisText.text = ":" + debrisCount.ToString();
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
