using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	private Transform player;
    private NavMeshAgent navigation;

	PlayerHealth playerHealth;
	EnemyHealth enemyHealth;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
		navigation = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0) {
			navigation.SetDestination(player.position);
        } else {
		    navigation.enabled = false;
        }
    }
}
