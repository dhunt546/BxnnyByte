using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //Cooldown by Regan Ly

    RaycastHit2D[] hits;

    [SerializeField] private Transform attackTransform;
    [SerializeField] private LayerMask attackableLayer;
   
    public LayerMask layer1;
    public LayerMask layer2;

    //range of abilities
    [SerializeField] private float basicAttackRange = 1.5f;
    [SerializeField] private float powerAttackRange = 2.0f;
    [SerializeField] private float spinAttackRange = 0.5f;
    //dodge doesnt need a range. just degate incoming damage

    [SerializeField] private AudioSource attackSource;

    //damage amount
    [SerializeField] private float basicAttackDmg = 1.0f;
    [SerializeField] private float powerAttackDmg = 3.0f;
    [SerializeField] private float spinAttackDmg = 0.0f;

    public GameObject PlayerDirection;

    //public Text countdownText;
    //probably make this a protextmesh


    private Dictionary<KeyCode, string> KeyInputMappings = new Dictionary<KeyCode, string>();
    private Dictionary<string, bool> attackCooldownFinished = new Dictionary<string, bool>();

    public bool _isPlayerAttacking;

    //maybe change add to this if any animations take longer.
    private float AttackAnimationCooldown = 0.2f;

    //Cooldown times
    private float basicCooldown = 1.0f;
    private float spinCooldown = 5.0f;
    private float powerCooldown = 10.0f;
    private float dodgeCooldown = 2.0f;

    public string TypeOfAttack;
    void Awake()
    {
        //keybinds
        KeyInputMappings[KeyCode.E] = "BasicAttack";
        KeyInputMappings[KeyCode.X] = "Spin";
        KeyInputMappings[KeyCode.Q] = "PowerAttack";
        KeyInputMappings[KeyCode.Space] = "Dodge";      
    }

    void Start()
    {
       InitializeCooldowns();
        _isPlayerAttacking = false;
        attackableLayer = layer1 | layer2;
    }
 
    void Update()
    {
        //check key inputs for attacking
        foreach (var kvp in KeyInputMappings)
        {
            string attackName = kvp.Value;
            if (Input.GetKeyUp(kvp.Key) && IsCooldownFinished(attackName) && !_isPlayerAttacking)
            {
                if (CanAttack(attackName))
                {
                    Attacking(attackName);
                    
                }
            }

        }
    }

    void Attacking(string attackName)
    {
        TypeOfAttack = attackName;
        SetAttacking(true);
        Attack(attackName);
        StartCooldown(attackName, GetCooldownTime(attackName));
        Invoke("SetAttackingToFalse", AttackAnimationCooldown);
    }
    void SetAttackingToFalse()
    {
        SetAttacking(false);
    }
    void SetAttacking(bool isAttacking)
    {
        _isPlayerAttacking = isAttacking;
    }
    void PreformAttack(float attackDamage)
    {
        hits = Physics2D.CircleCastAll(attackTransform.position, basicAttackRange, Vector2.up, 0f, attackableLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.CompareTag("Enemy"))
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    //apply the pain
                    iDamageable.Damage(attackDamage);
                    attackSource.Play();
                }
                //apply knockback
                Rigidbody2D enemyRb = hits[i].collider.gameObject.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    //Debug.Log("got the rigitbody of the enemy");
                    // call the Knockback function on the enemy Rigidbody
                    enemyRb.GetComponent<KnockBack>().Knockback();
                }
            }
            else if (hits[i].collider.gameObject.CompareTag("Debris"))
            {
                Rigidbody2D debrisRb = hits[i].collider.gameObject.GetComponent<Rigidbody2D>();
                if (debrisRb != null)
                {
                    // Debug.Log("debris detected");
                    // Destroy the debris object
                    debrisRb.GetComponent<debris>().CleanDebris();
                }
            }
            else if (hits[i].collider.gameObject.CompareTag("Spawner"))
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();
                if (iDamageable != null)
                {
                    iDamageable.Damage(attackDamage);
                    //Change sounds
                    attackSource.Play();
                }
            }
        }
    }
    void Attack(string attackName)
    {
        if (attackName == "BasicAttack")
        {
            //basic attack info goes here
            PreformAttack(basicAttackDmg);
        }

        if (attackName == "Spin")
        {
            PreformAttack(spinAttackDmg);
        }
        if (attackName == "PowerAttack")
        {
            PreformAttack(powerAttackDmg);
        }
        if(attackName == "Dodge")
        {
            //dodge here

           // Debug.Log(attackName);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackTransform.position, basicAttackRange);   
    }

    //cooldown math
    //checks if they have a cooldown of if their cooldown is not done yet
    bool CanAttack(string attackName)
    {
      if (!IsCooldownFinished(attackName)) {
            //Debug.Log(CanAttack(attackName) + "false return");
          return false;           
      }
      return true;
    }

    bool IsCooldownFinished(string attackName)
    {
        if (!attackCooldownFinished.ContainsKey(attackName))
        {
           // Debug.LogWarning("Attack cooldown state not found for " + attackName);
            return true;
        }
        return attackCooldownFinished[attackName];
    }

    //sets attackcooldown name and gets cooldown time
   void StartCooldown(string attackName, float cooldown)
   {
        attackCooldownFinished[attackName] = false; // Cooldown started

        // Start a coroutine to count down the cooldown
        StartCoroutine(CountDownCooldown(attackName, GetCooldownTime(attackName)));
   }
    IEnumerator CountDownCooldown(string attackName, float cooldown)
    {
        float remainingCooldown = cooldown;

        while (remainingCooldown > 0.0f)
        {
            remainingCooldown -= Time.deltaTime;
           // Debug.Log("Remaining cooldown for " + attackName + " is " + remainingCooldown);
            yield return null;
        }
        attackCooldownFinished[attackName] = true; // Cooldown finished
    }

    //declares cooldown time for each attack name
    float GetCooldownTime(string attackName)
    {
       switch (attackName)
       {
           case "BasicAttack":
               return basicCooldown;         
           case "Spin":
               return spinCooldown;         
           case "PowerAttack":
               return powerCooldown;
           case "Dodge":
               return dodgeCooldown;
           //add more cases here if needed
           default:
               return 0f;
       }
    }
    void InitializeCooldowns()
    {
        foreach (var kvp in KeyInputMappings)
        {
            attackCooldownFinished[kvp.Value] = true;
        }
    }
   
}
