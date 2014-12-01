using UnityEngine;
using System.Collections;

public class CharacterCreator : MonoBehaviour
{

		private Attributes att;
		public Texture placeholderHAIR;
		public Texture placeholderFACE;
		public Texture placeholderBODY;

		private enum Tabs
		{
				ATTRIBUTES,
				HAIR,
				FACE,
				BODY
		}
		Tabs tab = Tabs.ATTRIBUTES;


		// Use this for initialization
		void Start ()
		{
				this.att = this.GetComponentInParent<Attributes> ();
		}

		void OnGUI ()
		{
				GUI.Box (new Rect (25, 25, 330, 300), "");
				if (GUI.Button (new Rect (35, 35, 70, 20), "Attributes")) {
						tab = Tabs.ATTRIBUTES;
				}
				if (GUI.Button (new Rect (115, 35, 70, 20), "Hair")) {
						tab = Tabs.HAIR;
				}
				if (GUI.Button (new Rect (195, 35, 70, 20), "Face")) {
						tab = Tabs.FACE;
				}
				if (GUI.Button (new Rect (275, 35, 70, 20), "Body")) {
						tab = Tabs.BODY;
				}
				Vector2 loc;
				switch (tab) {
				case Tabs.ATTRIBUTES:

						// - TRAINABLE - //
						loc = new Vector2 (70, 65);
						GUI.Label (new Rect (loc.x + 95, loc.y - 5, 200, 30), "Trainable Attributes");
						// STAMINA //
						loc = new Vector2 (70, 85);
						att.setStamina ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.stamina, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Stamina");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.stamina + "");
						// TOUGHNESS //
						loc = new Vector2 (70, 105);
						att.setToughness ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.toughness, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Toughness");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.toughness + "");
						// CONSTITUTION //
						loc = new Vector2 (70, 125);
						att.setConstitution ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.constitution, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Constitution");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.constitution + "");
						// AGILITY //
						loc = new Vector2 (70, 145);
						att.setAgility ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.agility, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Agility");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.agility + "");
						// STRENGTH //
						loc = new Vector2 (70, 165);
						att.setStrength ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.strength, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Strength");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.strength + "");

						// - STATIC - //
						loc = new Vector2 (70, 195);
						GUI.Label (new Rect (loc.x + 100, loc.y - 5, 100, 30), "Static Attributes");
						// INTELLIGENCE //
						loc = new Vector2 (70, 215);
						att.setIntelligence ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.intelligence, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Intelligence");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.intelligence + "");
						// STATURE //
						loc = new Vector2 (70, 235);
						att.setStature ((int)GUI.HorizontalSlider (new Rect (loc.x + 100, loc.y, 100, 10), (float)att.stature, 1.0F, 20.0F));
						GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Stature");
						GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.stature + "");

			// POINTS REMAINING //
			loc = new Vector2 (70, 255);
			GUI.Label (new Rect (loc.x, loc.y - 5, 100, 30), "Remaining");
			GUI.Label (new Rect (loc.x + 230, loc.y - 5, 100, 30), att.spare_points + "");


						break;
				case Tabs.BODY:
						GUI.Label (new Rect (35, 65, placeholderBODY.width, placeholderBODY.height), placeholderBODY);
						break;
				case Tabs.FACE:
						GUI.Label (new Rect (35, 65, placeholderFACE.width, placeholderFACE.height), placeholderFACE);
						break;
				case Tabs.HAIR:
						GUI.Label (new Rect (35, 65, placeholderHAIR.width, placeholderHAIR.height), placeholderHAIR);
						break;
				}
		}
	
		// Update is called once per frame
		void Update ()
		{

	
		}
}