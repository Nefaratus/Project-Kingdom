using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestsList : MonoBehaviour {

	
	public List<Quests> Q_List = new List<Quests>(); 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void AddQuest(Quests quest)
	{
		Q_List.Add (quest);
	}

	public void RemoveQuest(Quests quest)
	{
		Q_List.Remove (quest);
	}
	
}
