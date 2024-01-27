using System.Collections;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerState currentPlayerState;

    PlayerController _player;
    PlayerAttack _attackPlayer;
    Animator _anim;
    private SpriteRenderer _spriteRenderer;

    string playerDirection;
    [SerializeField] public float _attackAnimTime = 3f;
    private Color OriginalColour;
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
        OriginalColour = _spriteRenderer.color;
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
        if (isPlayerAttacking)
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
        else if (isPlayerRunning)
        {
            currentPlayerState = PlayerState.Running;
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
            StartCoroutine(_player.SlowSpeed());
            _anim.CrossFade(animationHash, 0);
            
            animationLock = true;

            yield return new WaitForSeconds(_attackAnimTime);

            animationLock = false;

            isPlayerAttacking = false;

        }
    }

    private IEnumerator OnHit()
    {
        
      //  _spriteRenderer.color = color.red;
        yield return new WaitForSeconds(0.2f);
        //_spriteRenderer.sprite = OriginalColour;
    }
}
