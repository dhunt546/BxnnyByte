using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimator : MonoBehaviour
{

    [SerializeField] private float _attackAnimTime = 0.25f;
    //declares what "type" these will be
    public PlayerMovement _player;
    public PlayerAttack _attackPlayer;
    public Animator _anim;
    private SpriteRenderer _renderer;

    //player bool states
    private bool isAnimating;
    private bool _isAttacked;
    private float _lockedTill;
   
    

    private void Awake()
    {
        //Getting movement funtions from playermovement script
        if (!TryGetComponent(out PlayerMovement player))
        {
            Destroy(this);
            return;
        }
        if (!TryGetComponent(out PlayerAttack attackPlayer))
        {
            Destroy(this);
            return;
        }

        //Getting the components. Previously declared what the type of thing it will be at the top.
        _player = player;
        _attackPlayer = attackPlayer;
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
        
        if (_attackPlayer != null && _player != null)
        {
            String direction = _player.playerDirection;
            bool isMoving = _player.isMoving;
            bool _isAttacking = _attackPlayer._isAttacking;

            if (isAnimating)
            {
                // Don't start a new animation if one is already playing
                return;
            }
                if (isMoving)
            {
                //Running + direction
                if (direction == "Forward")
                {
                    StartCoroutine(PlayAnimationAndLock(MoveF));
                }
                else if (direction == "Backward")
                {
                    StartCoroutine(PlayAnimationAndLock(MoveB));
                }
                else if (direction == "Right")
                {
                    StartCoroutine(PlayAnimationAndLock(MoveR));
                }
                else if (direction == "Left")
                {
                    StartCoroutine(PlayAnimationAndLock(MoveL));
                }

            }
            //idle + direction
            else if (!isMoving)
            {
                if (direction == "Forward")
                {
                    StartCoroutine(PlayAnimationAndLock(IdleF));  
                }
                else if (direction == "Backward")
                {
                    StartCoroutine(PlayAnimationAndLock(IdleB));
                }
                else if (direction == "Right")
                {
                    StartCoroutine(PlayAnimationAndLock(IdleR));
                }
                else if (direction == "Left")
                {
                    StartCoroutine(PlayAnimationAndLock(IdleL));
                }
            }
            
                if (_isAttacking || _isAttacking && isMoving)
                {
                    if (direction == "Forward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackF));
                        Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Backward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackB));
                        Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Right")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackR));
                        Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Left")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackL));
                        Debug.Log(direction + "attacking");
                    }

                }      
        }
    }
        private IEnumerator PlayAnimationAndLock(int animationHash)
        {
            isAnimating = true; // Lock animation

            // Crossfade the animation
            _anim.CrossFade(animationHash, 0);

            // Wait for the animation to finish
            yield return new WaitForSeconds(_attackAnimTime);

            isAnimating = false; // Unlock animation
        }


    private static readonly int IdleF = Animator.StringToHash("Idle");
    private static readonly int IdleR = Animator.StringToHash("idle_R");
    private static readonly int IdleB = Animator.StringToHash("idle_B");
    private static readonly int IdleL = Animator.StringToHash("idle_L");
    private static readonly int AttackL = Animator.StringToHash("Attack_L");
    private static readonly int AttackR = Animator.StringToHash("Attack_R");
    private static readonly int AttackF = Animator.StringToHash("Attack_F");
    private static readonly int AttackB = Animator.StringToHash("Attack_B");
    private static readonly int Spin = Animator.StringToHash("Spin");
    private static readonly int MoveF = Animator.StringToHash("move_F");
    private static readonly int MoveB = Animator.StringToHash("move_B");
    private static readonly int MoveR = Animator.StringToHash("move_R");
    private static readonly int MoveL = Animator.StringToHash("move_L");

}
