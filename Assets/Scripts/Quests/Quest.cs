using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Quest{
	public string Q_Name;
	public string Q_Description;
	public List<QuestObjectives> Q_Objectives = new List<QuestObjectives>();
	public string Q_Author;
	public bool Q_DestReached;
	public bool Q_Completed;
	public int ObjectiveComplete = 0;

	public void NewObjective(string name,int Amount, string tag)
	{
		QuestObjectives N_Objective = new QuestObjectives(GameObject.Find(name), Amount, name, tag);
		Q_Objectives.Add (N_Objective);
	}

	public List<QuestObjectives> GetObjectives()
	{
		return Q_Objectives;
	}
}
