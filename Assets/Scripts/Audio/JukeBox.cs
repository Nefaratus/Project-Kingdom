using UnityEngine;
using System.Collections;

public class JukeBox : MonoBehaviour {

	AudioSource source;
	public AudioClip sailor,mordu,database,csharp;
	bool showMusic;
	
	// Use this for initialization
	void Start () {
		source = gameObject.GetComponentInParent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider col)
	{
		showMusic = true;
	}
	
	void OnTriggerExit(Collider col)
	{
		showMusic = false;
	}
	
	void OnGUI()
	{

		if(showMusic)
		{
			GUI.BeginGroup (new Rect (Screen.width/2,Screen.height/2,Screen.width/10,Screen.width/10), "");
			
			if(GUILayout.Button("Sailor"))
			{
				source.clip = sailor;
				source.enabled = true;
				source.Play();
			}

			if(GUILayout.Button("Mor'du"))
			{
				source.clip = mordu;
				source.enabled = true;
				source.Play();
			}

			if(GUILayout.Button("Database"))
			{
				source.clip = database;
				source.enabled = true;
				source.Play();
			}
			
			if(GUILayout.Button("Nocturne C# Minor"))
			{
				source.clip = csharp;
				source.enabled = true;
				source.Play();
			}
			GUI.EndGroup();
		}
	}
}

