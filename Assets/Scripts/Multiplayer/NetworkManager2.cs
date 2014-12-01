using UnityEngine;
using System.Collections;

public class NetworkManager2 : MonoBehaviour {

	
	public GameObject standbyCamera;
	public int x , y ,z ;

	// Use this for initialization
	void Start () {
		Connect ();
	}

	void Connect()
	{

		PhotonNetwork.ConnectUsingSettings("0.22");

		//If offline is necessary uncomment this
		//PhotonNetwork.offlineMode = true;	
		//PhotonNetwork.CreateRoom("some name");
	}

	void OnGUI()
	{
		if (!PhotonNetwork.connected)
		{
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString(),"box");
		}

		//if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
		//{

//		}

	}
	

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
		Debug.Log ("Joined Lobby 1");
	}
	
	void OnPhotonRandomJoinFailed() // Get called when joining a room failed
	{
		Debug.Log ("Joining room failed");

		Debug.Log ("Creating Room");
		PhotonNetwork.CreateRoom (null); // Due to the failed connection we have to setup our own room.
	}

	void OnJoinedRoom()
	{
		Debug.Log ("Joined Room");

		SpawnPlayer ();
	}

	void SpawnPlayer()
	{
		GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate ("Player", //PlayerPrefab
		                                                             new Vector3 (-256.9889f,10f,353.546f), //Position
											                         Quaternion.identity, //Rotation
											                         0);
		standbyCamera.SetActive (false);

		//Setting on the objects of the player so that only the player has them in his client
		myPlayer.GetComponent<PlayerStatus>().enabled = true;
		myPlayer.GetComponent<PlayerInput>().enabled = true;
		myPlayer.GetComponent<AdvanceMovement> ().enabled = true;
		myPlayer.GetComponent<ChatBox> ().enabled = true;
		myPlayer.GetComponent<Destination> ().enabled = true;
		myPlayer.GetComponent<QuestCreator> ().enabled = true;
		myPlayer.GetComponent<QuestLog> ().enabled = true;

		myPlayer.transform.FindChild ("Player Camera").gameObject.SetActive (true);
		myPlayer.transform.FindChild ("UI").gameObject.SetActive (true);
		myPlayer.transform.FindChild ("MinimapCamera").gameObject.SetActive (true);

	}

}
