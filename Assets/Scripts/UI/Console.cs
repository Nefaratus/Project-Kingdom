using UnityEngine;
using System.Collections;

public static class Console{

	static GameObject[] players;
	static GameObject player;

	public static void WriteLine(string line)
	{	players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{
			player.GetComponent<ChatBox>().AddMessage(line);
		}
	}

	public static void NetWrite(string line)
	{
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) 
		{			
			player.GetComponent<ChatBox>().photonView.RPC("NetWrite", PhotonTargets.AllBuffered, line);
		}

	}
}
