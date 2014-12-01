using UnityEngine;
using System.Collections;
using System;

public class WorldClock : MonoBehaviour {

	private DateTime date;
	private int day,hour,min,seconds;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		date = DateTime.Now;
		hour = date.Hour;
		min = date.Minute;
		seconds = date.Second;
	}

	void OnGUI()
	{
		GUI.Label (new Rect (Screen.width - Screen.width/7.5f, Screen.height/5 + 10, 
		                     Screen.width/10, Screen.height/30), 
		           "Time " + hour + ":" + min + ":" + seconds,"box");
	}

}
	

	