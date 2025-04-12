using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    Transform player;
    public float playerDistanceThreshold = 20f;
    public Vector2 wanderLimits = new Vector2(5,5);
    public float wanderTime = 1f;
    bool finishedWander = true;
    private NavMeshAgent agent;

    Vector3 startCoord;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();

        startCoord = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //check distance
        if (Vector3.Distance(transform.position, player.position) <= playerDistanceThreshold) {
            agent.destination = player.position;
        } else {

            if(finishedWander) {
                //wander within limits of start
                finishedWander = false;

                StartCoroutine(WanderCooldown());

                float randX = Random.Range(-wanderLimits.x, wanderLimits.x);
                float randZ = Random.Range(-wanderLimits.y, wanderLimits.y);
                agent.destination = new Vector3(startCoord.x + randX, transform.position.y, startCoord.z + randZ);
            }
        }
        
    }

    IEnumerator WanderCooldown() {
        yield return new WaitForSeconds(wanderTime);
        finishedWander = true;
    }
}
