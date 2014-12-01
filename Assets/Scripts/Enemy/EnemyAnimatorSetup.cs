using UnityEngine;
using System.Collections;

public class EnemyAnimatorSetup : MonoBehaviour {

	public float speedTime = 0.1f;
	public float angularSpeedTime = 0.7f;
	public float angleResponeTime = 0.6f;

	private Animator anim;
	private HashID hash;

	public EnemyAnimatorSetup(Animator animator, HashID hashID){
		anim = animator;
		hash = hashID;
	}

	public void Setup(float speed, float angle){
		float angularSpeed = angle * angleResponeTime;

//		anim.SetFloat(hash.speedFloat, speed, speedTime, Time.deltaTime);
//		anim.SetFloat(hash.angularSpeedFloat, angularSpeed, angularSpeedTime, Time.deltaTime);
	}

}
