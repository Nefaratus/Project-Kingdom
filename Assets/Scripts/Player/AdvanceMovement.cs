using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]

public class AdvanceMovement : MonoBehaviour {
	public enum State{
		Idle,
		Init,
		Setup,
		Action
	}
	public enum Turn {
		left = -1,
		none = 0,
		right = 1
	}
	public enum Forward{
		back = -1,
		none = 0,
		forward = 1
	}

	public float rotateSpeed = 10f;
	public float walkSpeed = 2f;
	public float runSpeed = 3f;
	public float strafeSpeed = 1f;
	public float gravity = 20f;
	public float airTime = 0;
	public float fallTime = 0.5f;
	public float jumpHeight = 12f;
	public float jumpTime = 0.5f;

	private Turn turning;
	private Turn strafing;
	private Forward forward;
	private bool running;
	private bool jumping;
	private State statement;


	private CollisionFlags collisionFlag;
	private Vector3 moveDirection;
	private Transform myTransform;
	private CharacterController charController;

	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	
	void Awake(){

		anim = GetComponent<Animator>();
		if (anim.layerCount == 2) {
			anim.SetLayerWeight(1, 1);
		}

		myTransform = transform;
		charController = GetComponent<CharacterController>();
		statement = AdvanceMovement.State.Init;
	}


	void Update()
	{
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if (!charController.isGrounded) {
			charController.Move(Vector3.down * Time.deltaTime);
		}
	}


	// Use this for initialization
	IEnumerator Start () {
		while (true){
			switch(statement){
			case State.Init:
				Init ();
				break;
			case State.Setup:
				SetUp();
				break;
			case State.Action:
				ActionPicker();
				break;
			}
			yield return null;
		}

	}

	private void Init(){
		if(!GetComponent<CharacterController>())return;
		if(!GetComponent<Animator>())return;

		statement = AdvanceMovement.State.Setup;
	}


	private void SetUp(){
		turning = AdvanceMovement.Turn.none;
		strafing = AdvanceMovement.Turn.none;
		forward = AdvanceMovement.Forward.none;
		running = true;
		jumping = false;

		moveDirection = Vector3.zero;
		statement = AdvanceMovement.State.Action;
	}

	private void ActionPicker(){
		
		//Rotates the Player
		myTransform.Rotate(0, (int)turning * Time.deltaTime * rotateSpeed, 0);
		//Code voor RotatePlayer Animatie

		if(charController.isGrounded){
			airTime = 0;
			
			float v = Input.GetAxis("Move Forward/Backward");
			anim.SetFloat("Speed", v);
			
			
			float h = Input.GetAxis("Rotate Player");
			anim.SetFloat("Direction", h);
					
			moveDirection = new Vector3 ((int)strafing,0,(int)forward);
			moveDirection = myTransform.TransformDirection(moveDirection).normalized;
			moveDirection *= walkSpeed;
						
			//Move player Forward and Backward
			//Code van Forward/Backward Animatie
			if(forward != AdvanceMovement.Forward.none){
				if(running){
					moveDirection *= runSpeed;
					// Code voor run animatie
				}
				else{
					//Code voor walk animatie
				}
			}
			else if(strafing != AdvanceMovement.Turn.none){
				//Code voor strafe animatie
			}
			else{
				//Code voor Idle animatie
			}
			if(jumping){
				if(airTime < jumpTime){
					moveDirection.y += jumpHeight;
					//Code voor jump animatie
					jumping = false;
				}

			}
		}
		else{
			if((collisionFlag & CollisionFlags.CollidedBelow) == 0){
				airTime += Time.deltaTime;
				// Code voor fall animatie
			}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		collisionFlag = charController.Move (moveDirection * Time.deltaTime);

	}
	public void MoveMeForward(Forward z){
		forward = z;
	}
	public void RunMode(){
		running = !running;
	}
	public void RotateMe(Turn y){
		turning = y;
	}
	public void StrafeMe(Turn x){
		strafing = x;
	}
	public void JumpMe(){
		jumping = true;
	}
}
