using UnityEngine;
using System.Collections;

public class AudioZoneControl : MonoBehaviour {

	AudioSource source;

	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource> ();
	}


	void OnTriggerEnter(Collider col)
	{
		source.enabled = true;
		if(source.clip != null)
		{
			source.Play ();
		}
	}

	void OnTriggerExit(Collider col)
	{
		source.Stop ();
		source.enabled = false;
	}
}
