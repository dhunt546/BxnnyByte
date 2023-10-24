using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //declares what "type" these will be
    private PlayerMovement _player;
    private Animator _anim;
    private SpriteRenderer _renderer;

    //player bool states
    private bool _attackL;
    private bool _attackR;
    private bool _attackF;
    private bool _attackB;

    private bool _spin;

    private void Awake()
    {
        //Getting movement funtions from playermovement script
        if (!TryGetComponent(out _player))
        {
            Destroy(this);
            return;
        }
        //Getting the components. Previously declared what the type of thing it will be at the top.
        _player = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
