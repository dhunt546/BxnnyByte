using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    //By Regan Ly

    [SerializeField] public float _attackAnimTime = 3f;
    //declares what "type" these will be
    public PlayerMovement _player;
    public PlayerAttack _attackPlayer;
    public Animator _anim;
    private SpriteRenderer _renderer;
    bool isCurrentlyAttacking = false;
    private bool isAttackAnimationPlaying;
    //player bool states



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

        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();

    }
    // Start is called before the first frame update
    void Start()
    {
        isCurrentlyAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_player != null)
        {
            String direction = _player.playerDirection;
            bool isMoving = _player.isMoving;


            if (isCurrentlyAttacking)
            {
               // Debug.Log("Currently Animating");
                // Don't start a new animation if one is already playing
                return;
            }
             else if (isMoving)
            {
                //Debug.Log("moving");
                //Running + direction
                if (direction == "Forward")
                {
                    _anim.CrossFade(MoveF, 0);
                }
                else if (direction == "Backward")
                {
                    _anim.CrossFade(MoveB, 0);
                }
                else if (direction == "Right")
                {
                    _anim.CrossFade(MoveR, 0);
                }
                else if (direction == "Left")
                {
                    _anim.CrossFade(MoveL, 0);
                }

            }
            //idle + direction
            else if (!isMoving)
            {
                if (direction == "Forward")
                {
                    _anim.CrossFade(IdleF, 0);
                }
                else if (direction == "Backward")
                {
                    _anim.CrossFade(IdleB, 0);
                }
                else if (direction == "Right")
                {
                    _anim.CrossFade(IdleR, 0);
                }
                else if (direction == "Left")
                {
                    _anim.CrossFade(IdleL, 0);
                }
            }
            if (_attackPlayer != null)
            {
                bool _isAttacking = _attackPlayer._isAttacking;

                if (_isAttacking && !IsAttackAnimationInProgress())
                {
                    //Debug.Log("attacking or something");
                    if (direction == "Forward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackF));
                        //Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Backward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackB));
                       // Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Right")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackR));
                       // Debug.Log(direction + "attacking");
                    }
                    else if (direction == "Left")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackL));
                        //Debug.Log(direction + "attacking");
                    }

                }
            }
        }
    }
    private bool IsAttackAnimationInProgress()
    {
        //Debug.Log("returning in progress animation");
        return isAttackAnimationPlaying;
    }
    private IEnumerator PlayAnimationAndLock(int animationHash)
        {
        isCurrentlyAttacking = true;

        // Crossfade the attack animation
        _anim.CrossFade(animationHash, 0);
        isAttackAnimationPlaying = true; // Set the animation flag to true

        // Wait for the animation to finish
        yield return new WaitForSeconds(_attackAnimTime);

        isAttackAnimationPlaying = false; // Set the animation flag back to false
        isCurrentlyAttacking = false;
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
