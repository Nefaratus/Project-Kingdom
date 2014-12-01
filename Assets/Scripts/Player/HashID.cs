using UnityEngine;
using System.Collections;

public class HashID : MonoBehaviour {

	// Here we store the hash tags for various strings used in our animators.
	public int dyingState;
	public int movementState;
	public int deadBool;
	public int speedFloat;
	public int playerInSightBool;
	public int angularSpeedFloat;
	public int openBool;
	
	
	void Start ()	{
				//dyingState = Animator.StringToHash ("Base Layer.Dying");
				movementState = Animator.StringToHash ("Base Layer.Movement");
				deadBool = Animator.StringToHash ("Dead");
				speedFloat = Animator.StringToHash ("Speed");
				playerInSightBool = Animator.StringToHash ("PlayerInSight");
				angularSpeedFloat = Animator.StringToHash ("AngularSpeed");
				openBool = Animator.StringToHash ("Open");
		}
}
