using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

	public float fieldOfViewAngle = 150f; 	//Aantal graden van gezichtsveld
	public bool playerInSight;				
	public Vector3 personalLastSight;


	private NavMeshAgent nav;
	private Animator anim;
	private SphereCollider col;
	private GameController lastPlayerSight;
	private GameObject player;
	private Animator playerAnim;
	private PlayerStatus playerHealth;
	private Vector3 previousSight;
	private HashID hash;
	
	void Awake() {

		nav = GetComponent <NavMeshAgent>();
		col = GetComponent <SphereCollider>();
		anim = GetComponent <Animator>();
		lastPlayerSight = GameObject.FindGameObjectWithTag(TagsManagement.gameController).GetComponent<GameController>();
		player = GameObject.FindGameObjectWithTag(TagsManagement.player);
		playerAnim = player.GetComponent <Animator>();
		playerHealth = player.GetComponent <PlayerStatus>();
		hash = GameObject.FindGameObjectWithTag(TagsManagement.gameController).GetComponent<HashID>();

		personalLastSight = lastPlayerSight.resetPosition;
		previousSight = lastPlayerSight.resetPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(lastPlayerSight.position != previousSight){
			personalLastSight = lastPlayerSight.position;
		}
		previousSight = lastPlayerSight.position;

//		if(playerHealth.P_Health >= 0f){
//			anim.SetBool(hash.playerInSightBool, playerInSight);
//		}
//		else{
//			anim.SetBool(hash.playerInSightBool, false);
//		}
	}

	void OnTriggerEnter (Collider other){
		if(other.gameObject == player){
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if(angle < fieldOfViewAngle * 0.5f){
				RaycastHit hit;

				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)){

					if(hit.collider.gameObject == player){
						playerInSight = true;
						lastPlayerSight.position = player.transform.position;
					}
				}
			}
		}
		int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
		int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
		
		if(playerLayerZeroStateHash == hash.movementState){
			
			if(CalculatePathLength(player.transform.position) <= col.radius){
				personalLastSight = player.transform.position;
			}
		}

	}
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
			playerInSight = false;
	}
	float CalculatePathLength(Vector3 targetPosition){
		NavMeshPath path = new NavMeshPath();
		if(nav.enabled){
			nav.CalculatePath(targetPosition, path);
		}

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];
		allWayPoints[0] = transform.position;
		allWayPoints[allWayPoints.Length - 1] = targetPosition;
		for(int i = 0; i < path.corners.Length; i++){
			allWayPoints[i + 1] = path.corners[i];
		}
		float pathLength = 0;
		for(int i = 0; i < allWayPoints.Length - 1; i++){
			pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
		}
		return pathLength;
		}
}

