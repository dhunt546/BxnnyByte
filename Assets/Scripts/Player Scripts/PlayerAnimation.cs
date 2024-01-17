using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerState currentPlayerState;

    PlayerController _player;
    PlayerAttack _attackPlayer;
    Animator _anim;

    string playerDirection;
    [SerializeField] private float _attackAnimTime = 3f;

    public bool isPlayerRunning;
    public bool isPlayerAttacking;

    bool isCurrentlyAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        isCurrentlyAttacking = false;
        //Get Components
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
        _attackPlayer = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = _player.playerDirection;
        if (isCurrentlyAttacking)
        {
            return;
        }
        else
        {
            SetPlayerState();
            PlayAnimation(playerDirection);
        }

    }

    public enum PlayerState
    {
        Idle,
        Running,
        BasicAttacking,
        PowerAttacking,
        SpinAttacking,
        Stunned,
        Dodging
    }

    void SetPlayerState()
    {
        if (isPlayerRunning)
        {
            currentPlayerState = PlayerState.Running;
        }
        else if (isPlayerAttacking)
        {
            switch (_attackPlayer.TypeOfAttack)
            {
                case "BasicAttack":
                    currentPlayerState = PlayerState.BasicAttacking;

                    break;
                case "PowerAttack":
                    currentPlayerState = PlayerState.PowerAttacking;

                    break;
                case "Spin":
                    currentPlayerState = PlayerState.SpinAttacking;

                    break;
            }
        }
        else
        {
            currentPlayerState = PlayerState.Idle;
        }

    }


    private int AnimationLibrary(string playerDirection)
    {
        string animationName = $"{currentPlayerState}{playerDirection}";
        Debug.Log(animationName);

        int animationHash = Animator.StringToHash(animationName);

        return animationHash;
    }

    void PlayAnimation(string playerDirection)
    {
        _anim.CrossFade(AnimationLibrary(playerDirection), 0);
        Debug.Log
            ("If no animation plays, animationName not found for: " + AnimationLibrary(playerDirection));
    }
}
