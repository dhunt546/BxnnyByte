using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    RaycastHit2D[] hits;
    [SerializeField] private Transform attackTransform;
    [SerializeField] private float attack1Range = 1.5f;
    [SerializeField] private float attack2Range = 2.0f;
    [SerializeField] private float attack3Range = 0.5f;
    [SerializeField] private LayerMask attackableLayer;
    [SerializeField] private float attack1Damage = 1.0f;
    [SerializeField] private float attack2Damage = 3.0f;
    [SerializeField] private float attack3Damage = 0.0f;

    public GameObject PlayerDirection;
    //     public Text countdownText;
    //probably make this a protextmesh

    private Dictionary<KeyCode, string> KeyInputMappings = new Dictionary<KeyCode, string>();
    private Dictionary<string, bool> attackCooldownFinished = new Dictionary<string, bool>();
    public bool _isAttacking;
    public float AttackAnimationCooldown;



    public float basicCooldown = 1.0f;
    public float spinCooldown = 5.0f;
    public float powerCooldown = 10.0f;
    public float dodgeCooldown = 2.0f;

    void Awake()
    {
        KeyInputMappings[KeyCode.E] = "BasicAttack";
        KeyInputMappings[KeyCode.Space] = "Spin";
        KeyInputMappings[KeyCode.Q] = "PowerAttack";
        KeyInputMappings[KeyCode.LeftShift] = "Dodge";
       
        
    }

    // Start is called before the first frame update
    void Start()
    {
       InitializeCooldowns(); 
        _isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        //check key inputs for attacking
        foreach (var kvp in KeyInputMappings)
        {
            string attackName = kvp.Value;
            if (Input.GetKeyUp(kvp.Key) && IsCooldownFinished(attackName) && !_isAttacking)
            {
                
                if (CanAttack(attackName))
                {
                    //Debug.Log("Attack button released");
                    SetAttacking(true);
                    Attack(attackName);
                    StartCooldown(attackName, GetCooldownTime(attackName));
                    Invoke("SetAttackingToFalse", AttackAnimationCooldown);
                    
                }
            }

        }
    }
    void SetAttackingToFalse()
    {
        SetAttacking(false);
    }
    void SetAttacking(bool isAttacking)
    {
        _isAttacking = isAttacking;
    }
    void Attack(string attackName)
    {
        if (attackName == "BasicAttack")
        {
            
            //basic attack info goes here
            hits = Physics2D.CircleCastAll(attackTransform.position, attack1Range, Vector2.up, 0f, attackableLayer);
            
            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    //apply the pain
                    iDamageable.Damage(attack1Damage);
                    Debug.Log(attack1Damage);
                }
            }
            Debug.Log(attackName);


         //   void OnDrawGizmosSelected()
         //   {
         //      Gizmos.DrawWireSphere(attackTransform.position, attack1Range);
         //   }

}
        if (attackName == "Spin")
        {
            //attack information here
            hits = Physics2D.CircleCastAll(attackTransform.position, attack2Range, Vector2.up, 0f, attackableLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    //apply the pain
                    iDamageable.Damage(attack1Damage);
                }
            }
            Debug.Log(attackName);

        }
        if (attackName == "PowerAttack")
        {
            //attack information
            hits = Physics2D.CircleCastAll(attackTransform.position, attack1Range, Vector2.up, 0f, attackableLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    //apply the pain
                    iDamageable.Damage(attack2Damage);
                }
            }
            Debug.Log(attackName);

        }
        if(attackName == "Dodge")
        {
            //dodge here
            hits = Physics2D.CircleCastAll(attackTransform.position, attack3Range, Vector2.up, 0f, attackableLayer);

            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable iDamageable = hits[i].collider.gameObject.GetComponent<IDamageable>();

                if (iDamageable != null)
                {
                    //apply the pain
                    iDamageable.Damage(attack3Damage);
                }
            }
            Debug.Log(attackName);

        }

    }

  //cooldown math
  //checks if they have a cooldown of if their cooldown is not done yet
  bool CanAttack(string attackName)
  {
      if (!IsCooldownFinished(attackName)) {
            Debug.Log(CanAttack(attackName) + "false return");
          return false;
            
      }
      return true;
  }

    bool IsCooldownFinished(string attackName)
    {
        if (!attackCooldownFinished.ContainsKey(attackName))
        {
            Debug.LogWarning("Attack cooldown state not found for " + attackName);
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
