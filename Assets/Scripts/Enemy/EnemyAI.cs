using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvanceMovement))]
[RequireComponent (typeof(SphereCollider))]

public class EnemyAI : MonoBehaviour {
	private enum State{
		Idle,
		Init,
		Search,
		Setup,
		Action,
		Retreat,
		Flee
	}

	public float perceptionRadius = 5f;
	public float meleeRange = 1f;
	public Transform target;
	private Transform myTransform;

	private const float rotationDamp = 0.03f;
	private const float forwardDamp = 0.9f;

	private Transform home;
	private State statement;
	private bool alive = true;
	private SphereCollider sphereCol;

	private Animator anim;

	void Start(){
		statement = EnemyAI.State.Init;
		StartCoroutine ("FSM");		
		anim = GetComponent<Animator>();
	}

	private IEnumerator FSM(){
		while(alive){
			switch(statement){
			case State.Init:
				Init ();
				break;
			case State.Setup:
				Setup ();
				break;
			case State.Search:
				Search ();
				break;
			case State.Action:
				Action ();
				break;
			case State.Retreat:
				Retreat ();
				break;
			case State.Flee:
				Flee ();
				break;
			}
			yield return null;
		}
	}

	private void Init(){
		myTransform = transform;
//		home = transform.parent.transform;
		sphereCol = GetComponent<SphereCollider>();
		
		if(sphereCol == null){
			Debug.LogError("No Sphere Collider!!");
		}
		statement = EnemyAI.State.Setup;
	}

	private void Setup(){
		sphereCol.center = GetComponent<CharacterController> ().center;
		sphereCol.radius = perceptionRadius;
		sphereCol.isTrigger = true;
		statement = EnemyAI.State.Search;
		alive = false;
	}

	private void Search(){

		Movement ();
		statement = EnemyAI.State.Action;
	}

	private void Action(){

		Movement ();
		statement = EnemyAI.State.Retreat;
	}

	private void Retreat(){
		myTransform.LookAt (target); // moet nog gefixed worden
		Movement ();
		statement = EnemyAI.State.Search;
	}

	private void Flee(){

		Movement ();
		statement = EnemyAI.State.Search;
	}


	private void Movement(){
		if (target) {
			Vector3 dir = (target.position - myTransform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);
			
			float dist = Vector3.Distance(target.position, myTransform.position);
			
			if (direction > forwardDamp && dist > meleeRange){
				SendMessage("MoveMeForward", AdvanceMovement.Forward.forward);
			}
			else{
				SendMessage("MoveMeForward", AdvanceMovement.Forward.none);
			}
			dir = (target.position - myTransform.position).normalized;
			direction = Vector3.Dot (dir, transform.right);
			
			if(direction > rotationDamp){
				SendMessage("RotateMe", AdvanceMovement.Turn.right);
			}
			else if(direction < -rotationDamp){
				SendMessage("RotateMe", AdvanceMovement.Turn.left);
			}
			else{
				SendMessage("RotateMe", AdvanceMovement.Turn.none);
			}
		}
		else{
			SendMessage("MoveMeForward", AdvanceMovement.Forward.none);
			SendMessage("RotateMe", AdvanceMovement.Turn.none);
		}
	}

	public void OnTriggerEnter(Collider other){
		if(other.CompareTag ("Player")){
			target = other.transform;
			alive = true;
			StartCoroutine("FSM");
			anim.SetBool("Follow",true);
		}
	}

	public void OnTriggerExit(Collider other){
		if(other.CompareTag ("Player")){
			target = home;
		}
	}
}
