using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerAnimator : MonoBehaviour
{
    //By Regan Ly

    [SerializeField] public float _attackAnimTime = 3f;

    PlayerMovement _player;
    PlayerAttack _attackPlayer;
    Animator _anim;
    SpriteRenderer _renderer;
    bool isCurrentlyAttacking = false;
    private bool isAttackAnimationPlaying;

    void Start()
    {
        _player = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _attackPlayer = GetComponent<PlayerAttack>();
        isCurrentlyAttacking = false;
    }

    void Update()
    {      
        if (_player != null)
        {
            String direction = _player.playerDirection;
            bool isMoving = _player.isMoving;

            if (isCurrentlyAttacking)
            {
                return;
            }
            else if (isMoving)
            {            
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
                bool _isPlayerAttacking = _attackPlayer._isPlayerAttacking;

                if (_isPlayerAttacking && !IsAttackAnimationInProgress())
                {                 
                    if (direction == "Forward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackF));;
                    }
                    else if (direction == "Backward")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackB));
                    }
                    else if (direction == "Right")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackR));
                    }
                    else if (direction == "Left")
                    {
                        StartCoroutine(PlayAnimationAndLock(AttackL));
                    }

                }
            }
        }
    }
    private bool IsAttackAnimationInProgress()
    {
        return isAttackAnimationPlaying;
    }
    private IEnumerator PlayAnimationAndLock(int animationHash)
    {
        isCurrentlyAttacking = true;

        _anim.CrossFade(animationHash, 0);
        isAttackAnimationPlaying = true;

        yield return new WaitForSeconds(_attackAnimTime);

        isAttackAnimationPlaying = false; 
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
