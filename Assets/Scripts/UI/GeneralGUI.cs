using UnityEngine;
using System.Collections;

public class GeneralGUI : MonoBehaviour {
	

	public GameObject target;
	PlayerStatus player;
	public int x,y,z;
	bool menu,options,multiplayer,video = false;
	private int MasterVolume,Music,Sound;
	QuestLog Q_logUI;
	QuestCreator Q_createUI;
	ChatBox C_Box;
	public Vector3 Village1Cor, Village2Cor;
	public string des;
	public Audio audio;
	// Use this for initialization
	void Start () {
		MasterVolume = 50;
		Music = 50;
		Sound = 50;

		player = (PlayerStatus)target.GetComponent ("PlayerStatus");
		Q_logUI = player.GetComponent<QuestLog> ();
		Q_createUI = player.GetComponent<QuestCreator>();
		C_Box = player.GetComponent<ChatBox> ();
		audio = player.GetComponentInChildren<Audio> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Menu ();
			Q_logUI.showLog = false;
			Q_createUI.showCreator = false;
			C_Box.usable = true;
			Console.NetWrite("ok");
		}
		if(Input.GetKeyDown(KeyCode.L))
		{
			QuestLog ();
		}		
		if(Input.GetKeyDown(KeyCode.K))
		{
			QuestCreator ();
		}
	}

	public void Menu()
	{
		menu = !menu;
		options = false;
	}

	public void QuestLog()
	{
		Q_logUI.showLog = !Q_logUI.showLog;
		menu = false;	
	}

	public void QuestCreator()
	{
		Q_createUI.showCreator = !Q_createUI.showCreator;	
		C_Box.usable = !C_Box.usable;
	}

	public void Options()
	{		
		options = !options;
	}

	public void Multiplayer()
	{		
		multiplayer = !multiplayer;	
	}

	void OnGUI()
	{ 
		int left = Screen.width / 120;
		int top = Screen.height /40;
		int width = Screen.width / 25; 
		int height = Screen.height / 15;
		/*
		GUI.BeginGroup (new Rect (Screen.width /3.5f ,Screen.height *0.75f , Screen.width/2.3f, Screen.height/5 ),"","box");

		if(GUI.Button (new Rect (left , top, width, height), "F1"))
		{

		}
		if (GUI.Button (new Rect (left * 6, top, width, height), "F2")) 
		{

		}
		GUI.Button (new Rect (left * 11, top, width, height), "F3");
		GUI.Button (new Rect (left * 16, top, width, height), "F4");
		GUI.Button (new Rect (left * 21, top, width, height), "F5");
		GUI.Button (new Rect (left * 26, top, width, height), "F6");
		GUI.Button (new Rect (left * 31, top, width, height), "F7");
		GUI.Button (new Rect (left * 36, top, width, height), "F8");
		GUI.Button (new Rect (left * 41, top, width, height), "F9");
		GUI.Button (new Rect (left * 46, top, width, height), "F10");		
		GUI.Button (new Rect (left, top * 5, width, height),  "1");
		GUI.Button (new Rect (left * 6, top * 5, width, height),  "2");
		GUI.Button (new Rect (left * 11, top * 5, width, height), "3");
		GUI.Button (new Rect (left * 16, top * 5, width, height), "4");
		GUI.Button (new Rect (left * 21, top * 5, width, height), "5");
		GUI.Button (new Rect (left * 26, top * 5, width, height), "6");
		GUI.Button (new Rect (left * 31, top * 5, width, height), "7");
		GUI.Button (new Rect (left * 36, top * 5, width, height), "8");
		GUI.Button (new Rect (left * 41, top * 5, width, height), "9");
		GUI.Button (new Rect (left * 46, top * 5, width, height), "0");
		GUI.EndGroup ();
		*/

		if(GUI.Button (new Rect (Screen.width - Screen.width / 6.7f, Screen.height - Screen.height / 14, Screen.width /9, Screen.height / 15), "Menu"))
		{
			Menu();
		}

		if (menu == true) 
		{
			GUI.BeginGroup(new Rect (Screen.width- Screen.width/5.5f, Screen.height- Screen.height/2f, Screen.width/5, Screen.height/2), "");

			if (GUI.Button (new Rect (1, Screen.height/7.1f,  Screen.width / 12, Screen.height / 15), "Quest Creator")) 
			{
				QuestCreator();
			}
			if(GUI.Button(new Rect(Screen.width/11 , Screen.height/7.1f, Screen.width / 12, Screen.height / 15),"Quest Log"))
			{
				QuestLog();
			}

			//Respawn button
			if (GUI.Button (new Rect (1, Screen.height/4.7f,  Screen.width / 12, Screen.height / 15), "Respawn")) 
			{
				player.Respawn (x, y, z);
			}
			if(GUI.Button(new Rect (1 ,Screen.height/3.5f, Screen.width / 12, Screen.height / 15),"Death"))	
			{
				player.P_Health = 0;
			}
			if(GUI.Button(new Rect(1, Screen.height/2.8f, Screen.width / 12, Screen.height / 15),"Options"))
			{
				Options ();
			}
			if(GUI.Button(new Rect(Screen.width/11 , Screen.height/4.7f, Screen.width / 12, Screen.height / 15),"Village 1"))
			{
				player.setPlayerPosition(Village1Cor.x,Village1Cor.y,Village1Cor.z);
				audio.PlayTeleport();
			}
			if(GUI.Button(new Rect(Screen.width/11,Screen.height/3.5f, Screen.width / 12, Screen.height / 15), "Village 2"))
			{
				player.setPlayerPosition(Village2Cor.x,Village2Cor.y,Village2Cor.z);
				audio.PlayTeleport();
			}
			if(GUI.Button(new Rect(Screen.width/11, Screen.height/2.8f, Screen.width / 12, Screen.height / 15),"Exit Game"))
			{
				Debug.Log("Not here Yet");
				Application.Quit();
			}

			GUI.EndGroup();
		}
		/*
		if(options == true)
		{
			GUI.BeginGroup(new Rect (Screen.width/3.5f, Screen.height/3.5f, Screen.width/2 + 10, Screen.height/3),"Options","box");

			if(GUI.Button(new Rect(Screen.width/30, Screen.height/30, Screen.width/9, Screen.height/40),"Video Settings"))
			{

			}
			if(GUI.Button(new Rect(Screen.width/30, Screen.height/11, Screen.width/9, Screen.height/40),"Interface"))
			{

			}
			if(GUI.Button(new Rect(Screen.width/30, Screen.height/6.8f, Screen.width/9, Screen.height/40),"KeyBindings"))
			{

			}

			MasterVolume = (int)GUI.HorizontalScrollbar (new Rect (Screen.width/3, Screen.height/30, Screen.width/9, Screen.height/40), MasterVolume, 1f, 0.0f, 101.0f);	
			GUI.Label(new Rect(Screen.width/3, Screen.height/15, Screen.width/9, 50),"Master Volume: " + MasterVolume.ToString());

			
			Sound = (int)GUI.HorizontalScrollbar (new Rect (Screen.width/3, Screen.height/9, Screen.width/9, Screen.height/40), Sound, 1.0f, 0.0f, 101.0f);	
			GUI.Label(new Rect(Screen.width/3, Screen.height/7, Screen.width/9, 50),"Sound: " + Sound.ToString());
			
			Music = (int)GUI.HorizontalScrollbar (new Rect (Screen.width/3, Screen.height/5, Screen.width/9, Screen.height/40), Music, 1.0f, 0.0f, 101.0f);	
			GUI.Label(new Rect(Screen.width/3, Screen.height/4.5f, Screen.width/9, 50),"Music: " + Music.ToString());


			GUI.EndGroup();
		}*/

		
		des = gameObject.GetComponentInParent<Destination> ().destination;
		GUI.Label (new Rect (Screen.width - Screen.width/7.5f, Screen.height - Screen.height + 5, 
		                     Screen.width/10, Screen.height/30), des == null ? "Location : Somewhere" : "Location : " + des );



	}
}
