﻿using UnityEngine;
using System.Collections;

public class Enemypatrol : MonoBehaviour {
	public Transform[] patrolPoints;
	public float moveSpeed;
	private int currentPoint;

	private Animator anim;
	
	// Use this for initialization
	void Start () {		
		anim = GetComponent<Animator>();
		transform.position = patrolPoints [0].position;
		currentPoint = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (transform.position == patrolPoints[currentPoint].position)
		{
			currentPoint++;
		}
		if (currentPoint >= patrolPoints.Length) 
		{
			currentPoint = 0;
		}
		anim.SetBool ("Walk", true);
		transform.position = Vector3.MoveTowards (transform.position, patrolPoints [currentPoint].position, moveSpeed * Time.deltaTime);
	}
}
