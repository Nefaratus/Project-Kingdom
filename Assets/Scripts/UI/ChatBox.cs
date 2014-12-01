using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatBox : Photon.MonoBehaviour {


	public string currentMessage,Chat;
	private bool selectText;
	private Vector2 scrollPos = Vector2.zero;
	public List<string> chatHistory = new List<string>();
	public GUIStyle labelStyle;
	GameObject[] players;
	private float lastUnfocusTime = 0;
	public bool usable;
	
	/*In this class we will be able to communicate with other players by using a Remote Procedure call
	 * The class calls itself so that in every client the chat looks the same and has spaces between each line
	 * 
	 */


	// Use this for initialization
	void Start () {
		usable = true;
	}

	void Awake()
	{
//		chatBox = this;
	}
	// Update is called once per frame
	void Update () 
	{

	}

	void OnGUI()
	{
		labelStyle = new GUIStyle();
		labelStyle.fixedWidth = Screen.width/4.5f;
		labelStyle.wordWrap = true;
		labelStyle.normal.textColor = Color.red;
		
		GUI.SetNextControlName("MyTextField");
		GUI.BeginGroup (new Rect (Screen.width/100,Screen.height - Screen.height/3.9f,Screen.width/3.85f,Screen.height/4), "","box");

		scrollPos = GUILayout.BeginScrollView(scrollPos,GUILayout.Width(Screen.width/4f), GUILayout.Height(Screen.height/5));   

		
		for (int i = chatHistory.Count - 1; i >= 0; i--)
		{
			GUILayout.Label(chatHistory[i],labelStyle);
		}

		GUILayout.EndScrollView();

		currentMessage = GUI.TextField (new Rect (Screen.width / 100, Screen.height /4.9f, Screen.width / 5.2f, Screen.height /25), currentMessage);

		if(GUI.Button (new Rect (Screen.width / 4.9f, Screen.height /4.9f, Screen.width / 20f, Screen.height /25), "Send"))
		{
			Send();
		}



	if (Event.current.type == EventType.keyDown && Event.current.character == '\n') //Event for when enter is pressed
		{		
			if(usable == true)
			{
				if (GUI.GetNameOfFocusedControl() == "MyTextField")
				{
					Send();
					lastUnfocusTime = Time.time;
					GUI.FocusControl("");
					GUI.UnfocusWindow();
				}
				else
				{
					if (lastUnfocusTime < Time.time - 0.1f)
					{
						GUI.FocusControl("MyTextField");
					}
				} 
			}
		}



		GUI.EndGroup ();

	
	}

	void Send()
	{
		if (currentMessage != "")
		{
			photonView.RPC("SendChatMessage", PhotonTargets.AllBuffered, currentMessage);
			currentMessage = "";
		}
	}

	public void AddMessage(string text)
	{

		this.chatHistory.Add(text);
		if (this.chatHistory.Count > 20)
			this.chatHistory.RemoveAt(0);
	}
	
	[RPC]
	void SendChatMessage(string text, PhotonMessageInfo info)
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{			
			player.GetComponent<ChatBox>().AddMessage("[" + info.sender + "] :" + text);
		}
	}

	[RPC]
	void NetWrite(string text, PhotonMessageInfo info)
	{
		AddMessage (text);
	}

}
