using UnityEngine;
using System.Collections;

public class DoneMenu : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		void OnGUI ()
		{
				if (GUI.Button (new Rect (Screen.width - 85, 15, 70, 20), "Done")) {
						Application.LoadLevel ("Test Terrain 1-JanVersie");
				}
		}
}
