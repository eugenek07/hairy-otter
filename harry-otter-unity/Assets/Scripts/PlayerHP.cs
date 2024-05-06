using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    bool shielded = false; 

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Hit by " + other.name); 
        if (other.tag == "enemyAttack" && !shielded)
        {
            Hit();
        }
    }

    private void Hit()
    {
        StartCoroutine(FlickerRed()); 
    }

    [SerializeField] DisplayUI redScreenUI; 

    IEnumerator FlickerRed()
    {
        redScreenUI.Show();

        yield return new WaitForSeconds(0.2f);

        redScreenUI.Hide(); 
    }

    public void EnableShield()
    {
        shielded = true; 
    }

    public void DisableShield()
    {
        shielded = false; 
    }
}
