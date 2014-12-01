using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {
	
	public float rotateSpeed = 10f;
	public float movementSpeed = 5f;
	public float runSpeed = 2f;
	public float strafeSpeed = 0.1f;

	private Transform myTransform;
	private CharacterController charController;

	private Animator anim;
	private AnimatorStateInfo currentBaseState;
	
	void Awake(){
		myTransform = transform;
		charController = GetComponent<CharacterController>();
	}

	void Start(){
		anim = GetComponent<Animator>();
		if (anim.layerCount == 2) {
			anim.SetLayerWeight(1, 1);
		}

//		animation.wrapMode = WrapMode.Loop;
	}

	void Update(){
		currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if (!charController.isGrounded) {
			charController.Move(Vector3.down * Time.deltaTime);
		}
		Turning ();
		Walking ();
		strafeWalking ();
	}
	private void Turning(){
		//Rotates the Player
		float h = Input.GetAxis("Rotate Player");
		anim.SetFloat("Direction", h);

		if(Mathf.Abs(Input.GetAxis("Rotate Player")) > 0){
			myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
		}
	}
	private void Walking(){
		//Move player Forward and Backward
		float v = Input.GetAxis("Move Forward/Backward");
		anim.SetFloat("Speed", v);

		if(Mathf.Abs(Input.GetAxis("Move Forward/Backward")) > 0){
			if(Input.GetButton("Running")){
				//animation.CrossFade ("Run");
				charController.SimpleMove(myTransform.TransformDirection(Vector3.forward) 
				                          * Input.GetAxis("Move Forward/Backward")* movementSpeed * runSpeed);
			}
			else {
				//animation.CrossFade ("WalkForward");
				charController.SimpleMove(myTransform.TransformDirection(Vector3.forward) 
				                          * Input.GetAxis("Move Forward/Backward")* movementSpeed);
			}
		}
		else {
			//animation.CrossFade ("Idle");
		}
	}
	private void strafeWalking(){
		if(Mathf.Abs(Input.GetAxis("Move Strafe")) > 0){
			charController.SimpleMove(myTransform.TransformDirection(Vector3.right) 
			                          * Input.GetAxis("Move Strafe")* strafeSpeed);
		}
	}
}
