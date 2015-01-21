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
	}

	public float perceptionRadius = 20f;

	public float meleeRange = 5f;
	public Transform target;
	public bool combat;
	private Transform myTransform;

	private const float rotationDamp = 0.03f;
	private const float forwardDamp = 0.9f;

	private GameObject home;
	private State statement;
	private bool targetAlive = true;
	private SphereCollider sphereCol;

	private Vector3 startpos;

	private Animator anim;
	Combat fight;
	int cooldown = 2;
	float nextPunch;

	void Start(){
		statement = EnemyAI.State.Init;
		StartCoroutine ("FSM");		
		anim = GetComponent<Animator>();
		startpos = gameObject.transform.position;
		home = GameObject.FindGameObjectWithTag ("home");
		fight = GetComponent<Combat> ();
	}

	private IEnumerator FSM(){
		while(targetAlive){
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
		targetAlive = false;

	}

	private void Search(){

		Movement ();

		myTransform.LookAt (target);
		anim.SetBool ("Follow", true);
		anim.SetBool ("Battle", false);
		anim.StopPlayback ();
		Debug.Log ("Searching ");
		statement = EnemyAI.State.Action;
	}

	private void Action(){
		Debug.Log ("Fight");
		anim.SetBool ("Follow", false);
		anim.SetBool ("Battle", true);	
		statement = EnemyAI.State.Search;
		//if(Vector3.Distance(this.gameObject.transform.position, target.position) < 2)
		//{
			if(Time.time > nextPunch)
			{
				fight.strike(target.gameObject,10,4);
				nextPunch = Time.time + cooldown;
			}
		//}
		else
		{
		statement = EnemyAI.State.Search;
		}
	}
	/// <summary>
	/// Volgt de speler binnend het spel.
	/// </summary>
	private void Retreat(){
		 // moet nog gefixed worden
		//Movement ();
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
			target = other.gameObject.transform;
			targetAlive = true;
			StartCoroutine("FSM");
		}
	}

	public void OnTriggerExit(Collider other){
		if(other.CompareTag ("Player")){
			target = gameObject.transform;
			StopCoroutine("FSM");			
			anim.SetBool ("Follow", false);
			anim.SetBool ("Battle", false);
			Movement();

		}
	}
}
