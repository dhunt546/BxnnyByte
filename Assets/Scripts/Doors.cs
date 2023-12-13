using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Sprite openDoorSprite;
    public Sprite closedDoorSprite;
    public float doorOpenTime = 1f;
    public bool is_door_open = false;

    private Animator doorAnimator;
    private SpriteRenderer doorSpriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
    }
    void Start()
    {

        doorSpriteRenderer = GetComponent<SpriteRenderer>();

        SetDoorState(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (is_door_open)
        {
            doorSpriteRenderer.sprite = openDoorSprite;
        }
        else if (!is_door_open)
        {
            doorSpriteRenderer.sprite = closedDoorSprite;
        }

      //  if (Input.GetKeyDown(KeyCode.E))
      //  {
      //      if (Input.GetKeyDown(KeyCode.E) && is_door_open == false)
      //      {
      //          // Play the door opening animation
      //          doorAnimator.SetTrigger("OpenDoor");
      //
      //          // Wait for the door to open before changing the sprite
      //          Invoke("OpenDoor", doorOpenTime);
      //      }
      //      else if (Input.GetKeyDown(KeyCode.E) && is_door_open == true)
      //      {
      //          // Play the door closing animation
      //          doorAnimator.SetTrigger("CloseDoor");
      //
      //          // Wait for the door to close before changing the sprite
      //          Invoke("CloseDoor", doorOpenTime);
      //      }
      //
      //  }
    }

    void OpenDoor()
    {
        SetDoorState(true);
    }

    void CloseDoor()
    {
        SetDoorState(false);
    }

    void SetDoorState(bool doorOpen)
    {
        is_door_open=doorOpen;
        // Set the door sprite based on the state
        doorSpriteRenderer.sprite = doorOpen ? openDoorSprite : closedDoorSprite;
    }
    // Animation Event Functions
    void OnDoorOpened()
    {
        OpenDoor();
    }

    void OnDoorClosed()
    {
        CloseDoor();
    }

    // You may want to use OnTriggerEnter2D or OnTriggerStay2D based on your needs

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Display a prompt or handle player interaction as needed
        }
    }
}
