using UnityEngine;
using System.Collections;

public class Attributes : MonoBehaviour
{

		//spare
		public int spare_points = 40;

		//trainable
		public int stamina = 1;
		public int toughness = 1;
		public int constitution = 1;
		public int agility = 1;
		public int strength = 1;
		//static
		public int intelligence = 1;
		public int stature = 1;
	
		//permanent modifiers
		//trainable
		public int stamina_pmod = 0;
		public int toughness_pmod = 0;
		public int constitution_pmod = 0;
		public int agility_pmod = 0;
		public int strength_pmod = 0;
		//static
		public int intelligence_pmod = 0;
		public int stature_pmod = 0;
	
		//other modifiers
		//trainable
		public int stamina_omod = 0;
		public int toughness_omod = 0;
		public int constitution_omod = 0;
		public int agility_omod = 0;
		public int strength_omod = 0;
		//static
		public int intelligence_omod = 0;
		public int stature_omod = 0;
	
		//other
		public int staminaDepletion = 100; // 0 - 100
		private int stamina_dmod = 0;

	#region statchanging
		public void setStamina (int s)
		{
				if (this.spare_points + (this.stamina - s) >= 0) {
						this.spare_points += this.stamina - s;
						this.stamina = s;
				}
		}

		public void setToughness (int s)
		{
				if (this.spare_points + (this.toughness - s) >= 0) {
						this.spare_points += this.toughness - s;
						this.toughness = s;
				}
		}

		public void setConstitution (int s)
		{
				if (this.spare_points + (this.constitution - s) >= 0) {
						this.spare_points += this.constitution - s;
						this.constitution = s;
				}
		}

		public void setAgility (int s)
		{
				if (this.spare_points + (this.agility - s) >= 0) {
						this.spare_points += this.agility - s;
						this.agility = s;
				}
		}

		public void setStrength (int s)
		{
				if (this.spare_points + (this.strength - s) >= 0) {
						this.spare_points += this.strength - s;
						this.strength = s;
				}
		}

		public void setIntelligence (int s)
		{
				if (this.spare_points + (this.intelligence - s) >= 0) {
						this.spare_points += this.intelligence - s;
						this.intelligence = s;
				}
		}

		public void setStature (int s)
		{
				if (this.spare_points + (this.stature - s) >= 0) {
						this.spare_points += this.stature - s;
						this.stature = s;
				}
		}


	#endregion



		// Use this for initialization
		void Start ()
		{
				stamina = ((stamina > 20) ? 20 : ((stamina <= 1) ? 1 : stamina));
				toughness = ((toughness > 20) ? 20 : ((toughness <= 1) ? 1 : toughness));
				constitution = ((constitution > 20) ? 20 : ((constitution <= 1) ? 1 : constitution));
				agility = ((agility > 20) ? 20 : ((agility <= 1) ? 1 : agility));
				strength = ((strength > 20) ? 20 : ((strength <= 1) ? 1 : strength));
				intelligence = ((intelligence > 20) ? 20 : ((intelligence <= 1) ? 1 : intelligence));
				stature = ((stature > 20) ? 20 : ((stature <= 1) ? 1 : stature));

		}
	
		// Update is called once per frame
		void Update ()
		{

		}

		public int getNextHit ()
		{
				return (int)((Dice.d20 () + this.getAgility () + this.getStature () + this.getStamina ()) / 4.0f);
		}
	
		public int getNextHit (int roll)
		{
				return (int)((roll + this.getAgility () + this.getStature () + this.getStamina ()) / 4.0f);
		}
	
		public int getDamageRoll ()
		{
				return (int)((Dice.d20 () + this.getStrength () + this.getStature () + this.getStamina ()) / 4.0f);
		}
	
		public int getDamageRoll (int roll)
		{
				return (int)((roll + this.getStrength () + this.getStature () + this.getStamina ()) / 4.0f);
		}


	#region getters of attributes
		public int getStamina ()
		{
				this.stamina_dmod = - (20 - (int)(((float)this.staminaDepletion / 100.0f) * 20.0f));
				return (this.stamina + this.stamina_pmod + this.stamina_omod + this.stamina_dmod < 1 ?
		        (1) : (this.stamina + this.stamina_pmod + this.stamina_omod + this.stamina_dmod));
		}

		public int getStamina_unmod ()
		{
				return this.stamina;
		}
	
		public int getToughness ()
		{
				return (this.toughness + this.toughness_pmod + this.toughness_omod);
		}

		public int getToughness_unmod ()
		{
				return this.toughness;
		}
	
		public int getConstitution ()
		{
				return (this.constitution + this.constitution_pmod + this.constitution_omod);
		}

		public int getConstitution_unmod ()
		{
				return this.constitution;
		}
	
		public int getAgility ()
		{
				return (this.agility + this.agility_pmod + this.agility_omod);
		}

		public int getAgility_unmod ()
		{
				return this.agility;
		}
	
		public int getStrength ()
		{
				return (this.strength + this.strength_pmod + this.strength_omod);
		}

		public int getStrength_unmod ()
		{
				return this.strength;
		}
	
		public int getIntelligence ()
		{
				return (this.intelligence + this.intelligence_pmod + this.intelligence_omod);
		}

		public int getIntelligence_unmod ()
		{
				return this.intelligence;
		}
	
		public int getStature ()
		{
				return (this.stature + this.stature_pmod + this.stature_omod);
		}

		public int getStature_unmod ()
		{
				return this.stature;
		}
	#endregion

}
