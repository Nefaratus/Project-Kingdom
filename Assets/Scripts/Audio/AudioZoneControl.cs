using UnityEngine;
using System.Collections;

public class AudioZoneControl : MonoBehaviour {

	AudioSource music;

	// Use this for initialization
	void Start () {
		music = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		music.enabled = true;
		music.Play ();
	}

	void OnTriggerExit(Collider col)
	{
		music.Stop ();
		music.enabled = false;
	}
}
