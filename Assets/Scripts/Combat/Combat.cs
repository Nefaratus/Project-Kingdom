using UnityEngine;
using System.Collections;

public class Combat : Photon.MonoBehaviour {

	public Targeting targeting;
	private GameObject target;
	private Audio punch;

	// Use this for initialization
	void Start () {
		//target =  targeting.target;
		punch = GetComponentInChildren<Audio>();

	}
	/// <summary>
	/// Strike this for the specified damage.
	/// </summary>
	/// <returns><c>true</c>, damage was done, <c>false</c> if not damage was done.</returns>
	/// <param name="damage">Damage done.</param>
	public bool struck(int damage){
		punch.PlayPunch ();
		Debug.Log("Struck " + this.name + " for " + damage + " damage.");
		//this.gameObject.GetComponent<PlayerStatus>().P_Health -= damage;
			photonView.RPC("DealDamage",PhotonTargets.AllBuffered,damage);
		/* Damage code here */return false;/* << Remove that << */

	}

	/*---------------------------------*\
     | \/ Do not USE! fix it FIRST! \/ | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - |
	\*---------------------------------*/
	/// <summary>
	/// !-DO NOT USE-!
	/// </summary>
	/// <returns><c>false</c>always. Also throws some exceptions.</returns>
	/// <param name="other">!-DO NOT USE THIS METHOD-!.</param>
	public bool lineOfSight(Combat other){
		RaycastHit hit;
		Physics.Linecast(this.gameObject.transform.position, other.gameObject.transform.position, out hit);
		if(hit.collider.gameObject == other.gameObject){
			return true;
		} else {
			Debug.LogError(hit.collider.gameObject.name);
		}
		if(hit.collider.gameObject == this.gameObject){
			Debug.LogError("Self collision!");
		}
		return false;
	}
	/*---------------------------------*\  ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^   ^
     | /\ Do not USE! fix it FIRST! /\ | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - | - |
	\*---------------------------------*/

	/// <summary>
	/// This strikes the specified target for specified damage.
	/// </summary>
	/// <param name="target">Target.</param>
	/// <param name="damage">Damage dealt.</param>
	public bool strike (Combat target, int damage){
		return target.struck(damage);
	}

	/// <summary>
	/// Strike the specified target, for specified damage if within specified range.
	/// </summary>
	/// <returns><c>true</c>, damage was dealt, <c>false</c> otherwise.</returns>
	/// <param name="target">Target.</param>
	/// <param name="damage">Damage.</param>
	/// <param name="range">Range.</param>
	public bool strike (GameObject target, int damage, int range){
		Combat other = target.GetComponent<Combat>();
		if(other != null && other.enabled == true){
			if(range > Vector3.Distance(this.gameObject.transform.position, target.transform.position)/* && lineOfSight(other)*/){
				other.struck(damage);
				return true;
			}
		}
		return false;
	}
}
