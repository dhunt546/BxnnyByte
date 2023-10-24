using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject PlayerDirection;
    //     public Text countdownText;
    //probably make this a protextmesh

    private Dictionary<KeyCode, string> KeyInputMappings = new Dictionary<KeyCode, string>();
    private Dictionary<string, float> attackCooldowns = new Dictionary<string, float>();

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

    }

    // Update is called once per frame
    void Update()
    {
        //check key inputs for attacking
        foreach (var kvp in KeyInputMappings)
        {
            if (Input.GetKey(kvp.Key))
            {
                string attackName = kvp.Value;
                if (CanAttack(attackName))
             {
                    Attack(attackName);
                  StartCooldown(attackName);
                    
            }
               else
               {
                   Debug.Log("cant use" +  attackName);
                   
               }


            }
        }
       
    }
    void Attack(string attackName)
    {
        if (attackName == "BasicAttack")
        {
            Debug.Log(attackName);
            
        }
        if (attackName == "Spin")
        {
            Debug.Log(attackName);

        }
        if (attackName == "PowerAttack")
        {
            Debug.Log(attackName);

        }
        if(attackName == "Dodge")
        {
            Debug.Log(attackName);

        }

    }

  //cooldown math
  //checks if they have a cooldown of if their cooldown is not done yet
  bool CanAttack(string attackName)
  {
      if (!attackCooldowns.ContainsKey(attackName))
      {
          Debug.LogWarning("Attack cooldown not found for " + attackName);
          return true;
      }
      float cooldown = attackCooldowns[attackName];
      if (cooldown <= 0f) {
          return true;
      }
      return false;
  }


   void StartCooldown(string attackName)
   {
       if (attackCooldowns.ContainsKey(attackName))
       {
           attackCooldowns[attackName] = GetCooldownTime(attackName);
       }
       else
       {
           Debug.LogWarning("attack cooldown not found for" +  attackName);
       }
   }
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

    ////cooldown counter
    ////i was getting really bad errors so this is the fix idk 
    //void UpdateCooldown()
    //{
    //      List<string> keysToRemove = new List<string>();
    //      foreach (var kvp in attackCooldowns)
    //      {
    //          if (kvp.Value > 0.0f)
    //          {
    //              attackCooldowns[kvp.Key] -= Time.deltaTime;
    //              if (kvp.Value <= 0.0f)
    //              {
    //                  keysToRemove.Add(kvp.Key);
    //              }
    //          }
    //      }
    //      // Remove keys with cooldowns that have reached or gone below 0
    //      foreach (var key in keysToRemove)
    //      {
    //          attackCooldowns.Remove(key);
    //      }
    //}
    void UpdateCooldowns()
    {
        foreach (var kvp in attackCooldowns)
        {
            if (kvp.Value > 0.0f)
            {
                attackCooldowns[kvp.Key] -= Time.deltaTime;
                Debug.Log(kvp.Key + " cooldown: " + kvp.Value);
            }
        }
    }
    //starting set cooldowns to 0 
    void InitializeCooldowns()
   {
       foreach (var kvp in KeyInputMappings)
       {
           attackCooldowns[kvp.Value] = 0f;
       }
   }
 
  void FixedUpdate()
  {
     UpdateCooldowns();
  }
 
}
