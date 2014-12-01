using UnityEngine;
using System.Collections;

public class PlayerTargeting : Targeting
{
	// Use this for initialization
	private GameObject _target;
	public override GameObject target {
		get {
			return this._target;
		}
		set {
			this._target = target;
		}
	}

	string hp_max;
	string hp;
	
	void Start ()
	{
		
	}
	
	void OnGUI ()
	{
		if (_target != null) {
			try {
				hp_max = "" + _target.GetComponent<PlayerStatus> ().P_MaxHealth;
				hp = "" + _target.GetComponent<PlayerStatus> ().P_Health;
			} catch (UnassignedReferenceException e) {
				hp_max = "-";
				hp = "-";
			}
			GUI.Box (new Rect (10, 35, 100, 100), "");
			GUI.Label (new Rect (15, 40, 100, 30), _target != null ? _target.name : "");
			GUI.Label (new Rect (15, 60, 100, 30), hp + "/" + hp_max);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {

					_target = hit.collider.gameObject;
				if (_target.GetComponent<Combat>() != null) {
				} else {
					_target = null;
				}
			}
		}
	}
}