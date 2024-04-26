using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private GameObject player;
    private int maxHealth = 2;
    private int currHealth;

    private NavMeshAgent navMeshAgent;

    private Animator animator;
    private float speed;
    private float attackRange = 4f;

    private float timeUntilMove = 0f;

    // Use this for initialization
    void Start()
    {
        player = Camera.main.gameObject; 
        navMeshAgent = transform.parent.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        currHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        Vector3 distanceVector = transform.position - player.transform.position;
        float distance = distanceVector.magnitude;

        if (distance <= attackRange)
        {
            navMeshAgent.SetDestination(transform.position);
            animator.SetTrigger("Attack");

            timeUntilMove = Time.time + 2f;
        }
        else if (Time.time > timeUntilMove)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerAttack")
        {
            Hit(other.transform); 
        }
    }

    public GameObject explosionPrefab;

    private void Hit(Transform hitObject)
    {
        currHealth -= 1; 

        GameObject explosion = Instantiate(explosionPrefab, hitObject.position, Quaternion.identity);
        StartCoroutine(Spells.DelayDestroy(explosion, 3f)); 

        Destroy(hitObject.gameObject);

        if (currHealth <= 0)
        {
            Death(); 
        }
        else
        {
            animator.SetTrigger("TakeDamage");
        }
    }

    private void Death()
    {
        animator.SetTrigger("Dead");

        StartCoroutine(Spells.DelayDestroy(transform.parent.gameObject, 3f));
    }
}
