using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonVisual : MonoBehaviour
{

    public Button button;
    public Sprite normalSprite;
    public Sprite pressedSprite;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ChangeButtonSprite);
    }

    void ChangeButtonSprite()
    {
        // Change the button's sprite to the pressed state sprite
        button.image.sprite = pressedSprite;
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
