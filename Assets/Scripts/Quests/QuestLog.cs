using UnityEngine;
using System.Collections;

public class QuestLog : Photon.MonoBehaviour {

	public bool showLog,showDes;
	private Vector2 scrollPos = Vector2.zero;
	public GUIStyle buttonStyle;
	QuestCreator questCreator;
	int counter;	
	public Audio audio;

	// Use this for initialization
	void Start () {
		questCreator = GetComponent<QuestCreator> ();
		audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<Audio> ();
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = questCreator.Q_List.Count - 1; i >= 0; i--)
		{
			for (int j = 0; j < questCreator.Q_List[i].Q_Objectives.Count; j++) 
			{			
				if(questCreator.Q_List[i].Q_Objectives[j].Tag == "Places")
				{
					if(questCreator.Q_List[i].Q_Objectives[j].ObjectiveName == gameObject.GetComponent<Destination>().destination)
					{			
						questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete = true;
						Debug.Log(questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete);
					}
				}
				if(questCreator.Q_List[i].Q_Objectives[j].Tag == "Player")
				{
					GameObject target = GameObject.Find(questCreator.Q_List[i].Q_Objectives[j].ObjectiveName);

					if(target.GetComponent<PlayerStatus>().P_Health < 1)
					{			
						questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete = true;
						Debug.Log(questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete);
					}
				}
				if(questCreator.Q_List[i].Q_Objectives[j].Tag == "Enemy")
				{
					GameObject target = GameObject.Find(questCreator.Q_List[i].Q_Objectives[j].ObjectiveName);
					
					if(target.GetComponent<EnemyStatus>().E_Health < 1)
					{			
						questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete = true;
						Debug.Log(questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete);
					}
				}
				if(questCreator.Q_List[i].Q_Objectives[j].ObjectiveComplete == true)
				{
					questCreator.Q_List[i].ObjectiveComplete += 1;	
				}
				if(questCreator.Q_List[i].ObjectiveComplete == questCreator.Q_List[i].Q_Objectives.Count)
				{					
					Debug.Log("The quest is now done..");
					questCreator.Q_List[i].Q_Completed = true;
					audio.PlayDing();
				}
			}
		}
					

	}

	void OnGUI()
	{
		buttonStyle = new GUIStyle();
		buttonStyle.fixedWidth = Screen.width/4.5f;
		buttonStyle.wordWrap = true;
		buttonStyle.normal.textColor = Color.white;	

		if(showLog == true)
		{
			GUI.BeginGroup (new Rect (Screen.width - Screen.width/3,Screen.height / 4f,Screen.width/3.5f,Screen.height/2), "","box");

			if(GUILayout.Button("Hide Log"))
			{
				showLog = false;
			}

			scrollPos = GUILayout.BeginScrollView(scrollPos,GUILayout.Width(Screen.width/4), GUILayout.Height(Screen.height/2));   

			counter = questCreator.Q_List.Count;
			
			for (int i = questCreator.Q_List.Count - 1; i >= 0; i--)
			{

				if(GUILayout.Button("Quest: " + questCreator.Q_List[i].Q_Name + "\n",buttonStyle))
				{
					showDes = !showDes;	
				}

				if(showDes == true)
				{
					
					GUILayout.Label("Description: \n" + questCreator.Q_List[i].Q_Description + "\n");
					GUILayout.Label("Objective: " + questCreator.Q_List[i].Q_Objectives[0].GetObjectiveName() + "\n");

					GUILayout.Label("Created by: " + questCreator.Q_List[i].Q_Author + "\n");

					if(GUILayout.Button("Remove Quest"))
					{	
						photonView.RPC("RemoveQuest",PhotonTargets.All,i);
					}

					if(questCreator.Q_List.Count > 0)
					{
						if(questCreator.Q_List[i].Q_Completed == true)
						{
							Debug.Log("Removed");
							photonView.RPC("RemoveQuest",PhotonTargets.All,i);
								//Award Player etc..
						}
					}
					else
					{
						Debug.Log ("Index to low");
					}
					if(GUILayout.Button("Complete"))
					{
						questCreator.Q_List[i].Q_Completed = true;
					}
				}
			}
			
			GUILayout.EndScrollView();


			
			GUI.EndGroup ();
		}

	}
}
