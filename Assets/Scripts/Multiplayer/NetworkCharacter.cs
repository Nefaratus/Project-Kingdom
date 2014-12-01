using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;



	Animator anim;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		if (anim == null)
		{
			Debug.LogError("Animator is not setted");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(photonView.isMine)
		{
			// do nothing
		}
		else
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);

		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo mess)
	{
		if(stream.isWriting)
		{
			//Our player we send our position to the other clients
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);

			//Syncing the animation
			stream.SendNext(anim != null && anim.GetFloat("Speed") != null ? anim.GetFloat("Speed") : 0f);
			stream.SendNext(anim != null && anim.GetFloat("Direction") != null ? anim.GetFloat("Direction") : 0f);
		}
		else
		{
			//other players their position gets sync. here
			realPosition = (Vector3)stream.ReceiveNext();
			realRotation = (Quaternion)stream.ReceiveNext();

			//Set Animations of other Players
			anim.SetFloat("Speed", (float)stream.ReceiveNext());
			anim.SetFloat("Direction", (float)stream.ReceiveNext());
		}
	}
}
