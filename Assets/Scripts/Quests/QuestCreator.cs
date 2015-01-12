using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestCreator : Photon.MonoBehaviour {

	public string Q_Name, Q_Description;
	public int Q_Objective,s_Type;
	Quest N_Quest;
	public bool showCreator;
	GameObject[] places,players,enemies,objectives;
	int p = 0;	
	public List<Quest> Q_List = new List<Quest>(); 
	public Audio audio;

	float Border_width, Border_height, G_width, G_height;

	// Use this for initialization
	void Start () {
		places = GameObject.FindGameObjectsWithTag ("Places");
		audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio> ();
	}
	
	// Update is called once per frame
	void Update () {
		players = GameObject.FindGameObjectsWithTag ("Player");
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		Border_width = Screen.width/3f;
		Border_height = Screen.height / 4f;
		G_width = Screen.width/3.5f;
		G_height = Screen.height/2;

	}

	void OnGUI()
	{
		if(showCreator == true)
		{
			GUI.BeginGroup (new Rect (Border_width,Border_height,G_width,G_height), "","box");

			Q_Name = GUI.TextField (new Rect (10, Border_height / 20, G_width - 20, G_height / 15), Q_Name, 25);


			Q_Description = GUI.TextArea (new Rect (10, Border_height / 4.5f, G_width - 20, G_height / 2), Q_Description);

			if(GUI.Button(new Rect(10,Border_height + Border_height /3,G_width /5,G_height / 15),"Places"))
			{
				objectives = places;
			}

			if(GUI.Button(new Rect(Border_width / 2.9f,Border_height + Border_height /3,G_width /5,G_height / 15),"Players"))
			{
				objectives = players;
			}

			if(GUI.Button(new Rect(Border_width / 1.5f,Border_height + Border_height /3,G_width /5,G_height / 15),"Enemies"))
			{
				objectives = enemies;
			}

			if(objectives != null)
			{
				if(GUI.Button(new Rect(10,Border_height + Border_height /2,G_width /5,G_height / 15),"<-"))
				{
					if(p > 0)
					{
						p--;
					}
				}

				GUI.Label(new Rect(Border_width /4.5f,Border_height + Border_height /2 ,G_width /2,G_height / 15),"" + objectives[p].name,"box");

				if(GUI.Button(new Rect(Border_width / 1.5f ,Border_height + Border_height /2,G_width /5,G_height / 15),"->"))
				{
					if(p < objectives.Length - 1)
					{
						p++;
					}
				}

				if(GUI.Button (new Rect (Border_width / 3.5f,Border_height + Border_height /1.5f ,G_width /3,G_height / 10), "Set Quest"))
				{
					photonView.RPC("CreateQuest", PhotonTargets.AllBuffered, Q_Name,Q_Description,objectives[p].name,gameObject.name,1,objectives[p].tag);
				}
			}
			GUI.EndGroup();

		}
	}
		
	public void AddQuest(string Q_name,string Q_descr, string Q_objec,string Q_author,int Q_amount,string tag)
	{
		N_Quest = new Quest();
		N_Quest.Q_Name = Q_name;
		N_Quest.Q_Description = Q_descr;
		N_Quest.NewObjective(Q_objec,Q_amount,tag);
		N_Quest.Q_Author = Q_author;
		Q_List.Add(N_Quest);
		Q_Name = "";
		Q_Description = "";
		audio.PlayClosingBook ();
	}

	[RPC]
	void CreateQuest(string Q_name,string Q_descr, string Q_objec,string Q_author,int Q_amount,string tag, PhotonMessageInfo info)
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{
			//This is so that on every player with this script on it will invoke the method AddQuest
			player.GetComponent<QuestCreator>().AddQuest(Q_name, Q_descr, Q_objec,Q_author,Q_amount,tag);
		}
	}

	[RPC]
	void RemoveQuest(int q, PhotonMessageInfo info)
	{	
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{
		player.GetComponent<QuestCreator>().Q_List.RemoveAt (q);
		}
	}
}


