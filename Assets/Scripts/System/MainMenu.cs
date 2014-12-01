using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin skinLabel;

	void OnGUI(){

		GUI.skin = skinLabel;
		GUI.Label (new Rect (Screen.height / 2.8f, 10, Screen.width / 2, 200), "PROJECT KINGDOM");

		if(GUI.Button ( new Rect ( Screen.width / 4, Screen.height / 3.1f, Screen.width / 2, Screen.height/20), "New Game")){
			Application.LoadLevel("CharacterCreation");  // naam van character creation invullen!
		}
		if(GUI.Button ( new Rect ( Screen.width / 4, Screen.height / 2, Screen.width / 2, Screen.height/20), "Options")){
			Application.LoadLevel("Options");
		}
		if(GUI.Button ( new Rect ( Screen.width / 4, Screen.height / 1.5f, Screen.width / 2, Screen.height/20), "Quit")){
			Application.Quit ();
		}
	}
}
