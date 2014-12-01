using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvanceMovement))]
public class PlayerInput : MonoBehaviour {

	private float lastSynchronizationTime = 0f;
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPosition = Vector3.zero;
	private Vector3 syncEndPosition = Vector3.zero;
	private Combat combat;
	
	// Use this for initialization
	void Start () {
		this.combat = this.gameObject.GetComponent<Combat> ();
	}
	
	// Update is called once per frame
	void Update () {

				if (networkView.isMine) {

						if (Input.GetButton ("Move Forward/Backward")) {
								if (Input.GetAxis ("Move Forward/Backward") > 0) {
										SendMessage ("MoveMeForward", AdvanceMovement.Forward.forward);
								} else {
										SendMessage ("MoveMeForward", AdvanceMovement.Forward.back);
								}
						}
						if (Input.GetButtonUp ("Move Forward/Backward")) {
								SendMessage ("MoveMeForward", AdvanceMovement.Forward.none);
						}
						if (Input.GetButton ("Rotate Player")) {
								if (Input.GetAxis ("Rotate Player") > 0) {
										SendMessage ("RotateMe", AdvanceMovement.Turn.right);
								} else {
										SendMessage ("RotateMe", AdvanceMovement.Turn.left);
								}
						}
						if (Input.GetButtonUp ("Rotate Player")) {
								SendMessage ("RotateMe", AdvanceMovement.Turn.none);
						}
						if (Input.GetButton ("Move Strafe")) {
								if (Input.GetAxis ("Move Strafe") > 0) {
										SendMessage ("StrafeMe", AdvanceMovement.Turn.right);
								} else {
										SendMessage ("StrafeMe", AdvanceMovement.Turn.left);
								}
						}
						if (Input.GetButtonUp ("Move Strafe")) {
								SendMessage ("StrafeMe", AdvanceMovement.Turn.none);
						}
						if (Input.GetButtonDown ("Jump")) {
								SendMessage ("JumpMe");
						}
						if (Input.GetButtonDown ("Running")) {			
								SendMessage ("RunMode");
						}
						if(Input.GetMouseButtonUp(1))
						{
							combat.strike(combat.targeting.target.gameObject,10,5);
						}
			}
			else {
			SyncedMovement();
				}
		}

	void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		Vector3 syncPosition = Vector3.zero;
		if (stream.isWriting)
		{
			syncPosition = rigidbody.position;
			stream.Serialize(ref syncPosition);
		}
		else
		{
			stream.Serialize(ref syncPosition);
			syncTime = 0f;
			syncDelay = Time.time - lastSynchronizationTime;
			lastSynchronizationTime = Time.time;
			
			syncStartPosition = rigidbody.position;
			syncEndPosition = syncPosition;
		}
	}

	private void SyncedMovement()
	{
		syncTime += Time.deltaTime;
		rigidbody.position = Vector3.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
	}
}
