using UnityEngine;
using System.Collections;

public class TerrainNeighbors : MonoBehaviour {
	public Terrain top;
	public Terrain right;
	public Terrain bottom;
	public Terrain left;
	// Use this for initialization
	void Start () {
		Terrain t = (Terrain)this.gameObject.GetComponent(typeof(Terrain));
		if(t != null){
			t.SetNeighbors(left,top,right,bottom);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
