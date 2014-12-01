using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

	public float deadZone = 5f;

	private Transform player;
	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Animator anim;
	private HashID hash;
	private EnemyAnimatorSetup animSetup;


	void Awake() {
		player = GameObject.FindGameObjectWithTag(TagsManagement.player).transform;
		enemySight = GetComponent <EnemySight>();
		nav = GetComponent <NavMeshAgent>();
		anim = GetComponent <Animator>();
		hash = GameObject.FindGameObjectWithTag(TagsManagement.gameController).GetComponent<HashID>();

		nav.updateRotation = false;
		animSetup = new EnemyAnimatorSetup(anim, hash);

		deadZone *= Mathf.Deg2Rad;
	}

	void Update() {
		navAnimSetup ();
	}

	void navAnimSetup() {
		float speed;
		float angle;

		if(enemySight.playerInSight){
			speed = 0f;
			angle = findAngle(transform.forward, player.position - transform.position, transform.up);
		}
		else{
			speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
			angle = findAngle(transform.forward, nav.desiredVelocity, transform.up);

			if(Mathf.Abs(angle) < deadZone){
				transform.LookAt(transform.position + nav.desiredVelocity);
				angle = 0f;
			}
		}
		animSetup.Setup(speed, angle);
	}

	void AnimatorMove(){
		nav.velocity = anim.deltaPosition;
		transform.rotation = anim.rootRotation;
	}

	float findAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector){
		if(toVector == Vector3.zero){
			return 0f;
		}
		float angle = Vector3.Angle(fromVector, toVector);
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		angle *= Mathf.Deg2Rad;
		return angle;
	}
}
