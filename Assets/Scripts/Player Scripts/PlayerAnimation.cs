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

    bool animationLock = false;

    // Start is called before the first frame update
    void Start()
    {
        animationLock = false;
        //Get Components
        _player = GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
        _attackPlayer = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDirection = _player.playerDirection;
        if (animationLock)
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
            animationLock = true;
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
        int animationHash = Animator.StringToHash(animationName);
        //Debug.Log(animationName);
        return animationHash;
    }

    void PlayAnimation(string playerDirection)
    {
        if (isPlayerAttacking)
        {
            StartCoroutine(PlayAnimationAndLock(AnimationLibrary(playerDirection)));
        }
        else
        _anim.CrossFade(AnimationLibrary(playerDirection), 0);
    }

    private IEnumerator PlayAnimationAndLock(int animationHash)
    {
        if (isPlayerAttacking)
        {

            _anim.CrossFade(animationHash, 0);
             animationLock = true;

            yield return new WaitForSeconds(_attackAnimTime);

             animationLock = false;

            isPlayerAttacking = false;
        }
    }
}
