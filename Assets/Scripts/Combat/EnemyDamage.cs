using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

	public GameObject target;
	float distance,direction,cooldown,attackTimer;
	public int damage;
	Vector3 dir;
	PlayerStatus player;

	// Use this for initialization
	void Start () {
		damage = -10;
		attackTimer = 0;
		cooldown = 2.0f;
		player = (PlayerStatus)target.GetComponent ("PlayerStatus");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (attackTimer > 0)
			attackTimer -= Time.deltaTime;
		
		if (attackTimer < 0)
			attackTimer = 0;


		if(attackTimer == 0)
			{
				Attack();	
				attackTimer = cooldown;
			}
	}

	
	public void Attack()
	{   //Deal damage to current enemy
		distance = Vector3.Distance (target.transform.position, transform.position);
		//When normalized, a vector keeps the same direction but its length is 1.0.
		dir = (target.transform.position - transform.position).normalized;
		
		direction = Vector3.Dot (dir, transform.forward);
		//Debug.Log (direction);
		
		if (distance < 1.2) 
		{
			/*This will manage the direction of the attack so that if the player his/her distance is close enough but not facing the enemy that they wont deal damage
			 * the 0.5 is determinated by the first steps creating this so in future design can be changed.
			 */ 
			if(direction > 0.5)
			{
				player.HealthChange(damage);
				Debug.Log(player.P_Health);
				Debug.Log("OOOUUUUCH");
			}
		}
		
	}
}
