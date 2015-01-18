using UnityEngine;
using System.Collections;

public class ActivateEnemy : MonoBehaviour {

	public GameObject enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter()
	{
		enemies.SetActive (true);
	}

	void OnTriggerExit()
	{
		enemies.SetActive (false);
	}
}
