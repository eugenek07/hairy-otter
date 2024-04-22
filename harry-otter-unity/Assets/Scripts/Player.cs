using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private string restartLevel;
    [SerializeField]
    private float minHeightFallDamage;
    [SerializeField]
    private float damageMultiplyer; 
    private float fallStartHeight;
    private CharacterController characterController; 

    [HideInInspector]
    public float curHealth;
    [HideInInspector]
    public bool isFalling; 
    #endregion 
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>(); 
        curHealth = startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        TakeFallDamage(); 
    }

    #region Methods
    public void TakeDamage(float damage)
    {
        curHealth -= damage; 
        if (curHealth <= 0)
        {
            SceneManager.LoadScene(restartLevel);
        }
    }

    public void Heal(float health)
    {
        curHealth += health;
        if (curHealth > startHealth)
        {
            curHealth = startHealth; 
        }
    }

    public void TakeFallDamage()
    {
        if (characterController.isGrounded)
        {
            if (isFalling)
            {
                isFalling = false;
                //where fall occured - grounded level y 
                float fallDistance = fallStartHeight - transform.position.y;
                if (fallDistance > minHeightFallDamage)
                {
                    float damage = (fallDistance - minHeightFallDamage) * damageMultiplyer;
                    TakeDamage(damage); 
                }
            }
        }
        else if (!isFalling)
        {
            isFalling = true; 
            fallStartHeight = transform.position.y; 
        }
    }
    #endregion 
}
