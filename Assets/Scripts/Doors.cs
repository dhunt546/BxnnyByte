using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;
    public float doorOpenTime;
    public Animator _anim;

    public bool is_door_open;

    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (is_door_open)
            {
                _anim.CrossFade(closingdoor, 1);
                Invoke("CloseDoor", doorOpenTime);
                Debug.Log("doorclose");
            }
            else if (is_door_open == false)
            {
                _anim.CrossFade(openingdoor, 0);
                Invoke("OpenDoor", doorOpenTime);
            }
            
        }
    }

    public void OpenDoor()
    {
        spriteRenderer.sprite = openDoorSprite;
        is_door_open = true;
        
    }    
    public void CloseDoor()
    {
        
        spriteRenderer.sprite = closedDoorSprite;
        is_door_open = false;
        
    }

    private static readonly int openingdoor = Animator.StringToHash("DoorOpening");
    private static readonly int closingdoor = Animator.StringToHash("DoorClosing");
}
