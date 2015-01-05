using UnityEngine;
using System.Collections;

public class QuestObjectives{
	public bool ObjectiveComplete = false;
	public string ObjectiveName,Tag;
    public int Q_Amount;
	public GameObject Objective;

	public QuestObjectives(GameObject objective, int amount, string name, string tag)
	{
		ObjectiveName = name;
		Objective = objective;
		Tag = tag;
	}

	public string GetObjectiveName()
	{
		return ObjectiveName;
	}
}
