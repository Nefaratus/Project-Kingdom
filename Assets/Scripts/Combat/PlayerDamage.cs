using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	public GameObject target;
	float distance,direction,cooldown,attackTimer;
	public int damage;
	Vector3 dir;
	EnemyStatus enemy;
	// Use this for initialization
	void Start () {
		//damage = -10;
		attackTimer = 0;
		cooldown = 2.0f;
//		enemy = (EnemyStatus)target.GetComponent ("EnemyStatus");
	}
	
	// Update is called once per frame
	void Update () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;

		if (attackTimer < 0)
			attackTimer = 0;
		//If the default button for fire is pressed do an attack.
		if (Input.GetButton ("Fire1")) 
		{
			if(attackTimer == 0)
			{
				Attack();	
				attackTimer = cooldown;
			}
		}
	}

	public void Attack()
	{   //Deal damage to current enemy
		distance = Vector3.Distance (target.transform.position, transform.position);
		//When normalized, a vector keeps the same direction but its length is 1.0.
		dir = (target.transform.position - transform.position).normalized;

		direction = Vector3.Dot (dir, transform.forward);

		if (distance < 1.2) 
		{
			/*This will manage the direction of the attack so that if the player his/her distance is close enough but not facing the enemy that they wont deal damage
			 * the 0.5 is determinated by the first steps creating this so in future design can be changed.
			 */ 
			if(direction > 0.5)
			{
			enemy.HealthChange(damage);;
			Debug.Log("BOOOOOM");
			}
		}

	}
}
