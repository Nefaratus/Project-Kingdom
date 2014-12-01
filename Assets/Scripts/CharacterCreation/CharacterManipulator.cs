using UnityEngine;
using System.Collections;

public class CharacterManipulator : MonoBehaviour
{


		private float rotation;
		private bool isDragging;
		private Vector3 pos_one;
		private Vector3 pos_two;
		// Use this for initialization
		void Start ()
		{
	
		}

		void OnGUI ()
		{
				bool rotLeft = false;
				bool rotRight = false;
				bool rotStop = false;
				// Create rotation buttons
				if (GUI.Button (new Rect (Screen.width / 2 - 60, Screen.height - 40, 30, 20), "<<")) {
						rotLeft = true;
				}
			
				if (GUI.Button (new Rect (Screen.width / 2 + 20, Screen.height - 40, 30, 20), ">>")) {
						rotRight = true;
				}

				if (GUI.Button (new Rect (Screen.width / 2 - 20, Screen.height - 40, 30, 20), "O")) {
						rotStop = true;
				}
				this.rotation += (rotLeft && this.rotation < 200.0f) ? 100.0f : 0;
				this.rotation += (rotRight && this.rotation > -200.0f) ? -100.0f : 0;
				this.rotation = (rotStop) ? 0.0f : this.rotation;
		}
		// Update is called once per frame
		void Update ()
		{
				if (isDragging) {
						if (Input.GetMouseButton (2)) {
								pos_two = Input.mousePosition;

								rotation = (pos_one - pos_two).x * 10;
								
								pos_one = Input.mousePosition;
						} else {
								isDragging = false;
						}
				} else {
						if (Input.GetMouseButton (2)) {
								isDragging = true;
								pos_one = Input.mousePosition;
						} else {
								if (rotation > 200.0f) {
										rotation = 200.0f;
								}
								if (rotation < -200.0f) {
										rotation = -200.0f;
								}
						}
				}
				transform.Rotate (new Vector3 (0.0f, this.rotation * Time.deltaTime, 0.0f));
		}
}
