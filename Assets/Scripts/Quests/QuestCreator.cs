using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestCreator : Photon.MonoBehaviour {

	public string Q_Name, Q_Description;
	public int Q_Objective,s_Type;
	Quests N_Quest;
	public bool showCreator;
	GameObject[] places,players;
	int p = 0;	
	public List<Quests> Q_List = new List<Quests>(); 

	float Border_width, Border_height, G_width, G_height;

	// Use this for initialization
	void Start () {
		places = GameObject.FindGameObjectsWithTag ("Places");
	}
	
	// Update is called once per frame
	void Update () {
	
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

			if(GUI.Button(new Rect(10,Border_height + Border_height /3,G_width /5,G_height / 15),"<-"))
			{
				if(p > 0)
				{
					p--;
				}
			}

			GUI.Label(new Rect(Border_width / 3f,Border_height + Border_height /3 ,G_width /5,G_height / 15),"" + places[p].name,"box");

			if(GUI.Button(new Rect(Border_width / 1.5f ,Border_height + Border_height /3,G_width /5,G_height / 15),"->"))
			{
				if(p < places.Length -1)
				{
					p++;
				}
			}

			if(GUI.Button (new Rect (Border_width / 3.5f,Border_height + Border_height /1.5f ,G_width /3,G_height / 10), "Set Quest"))
			{
				photonView.RPC("CreateQuest", PhotonTargets.AllBuffered, Q_Name,Q_Description,places[p].name,gameObject.name);
			}

			GUI.EndGroup();

		}
	}
		
	public void AddQuest(string Q_name,string Q_Descr, string Q_desti,string Q_author)
	{
		N_Quest = new Quests();
		N_Quest.Q_Name = Q_name;
		N_Quest.Q_Description = Q_Descr;
		N_Quest.Q_Destination = Q_desti;
		N_Quest.Q_Author = Q_author;
		Q_List.Add(N_Quest);
		Q_Name = "";
		Q_Description = "";
	}

	[RPC]
	void CreateQuest(string Q_name,string Q_Descr, string Q_desti,string Q_author, PhotonMessageInfo info)
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{
			//This is so that on every player with this script on it will invoke the method AddQuest
			player.GetComponent<QuestCreator>().AddQuest(Q_name, Q_Descr, Q_desti,Q_author);
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


