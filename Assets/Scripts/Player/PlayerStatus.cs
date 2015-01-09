using UnityEngine;
using System.Collections;

public class PlayerStatus : Photon.MonoBehaviour{

	//test
	public int P_Level, P_Health,P_MaxHealth, P_Mana ,P_MaxMana,P_Stamina,P_MaxStamina , P_Experience;
	public int x, y, z;
	private int P_Attributes;
	public float HealthBarLength;
	public string playerName;
	bool possible = true;
	public CharacterSettings cs;
	public Transform P_Transform;	
	public Audio DeathSound;
	public bool Enemy = false;

	void Start()
	{
		P_Health = 100;
		P_MaxHealth = 100;
		cs = new CharacterSettings();
		DeathSound = GetComponentInChildren<Audio>();
	}

	void FixedUpdate()
	{
		HealthChange(0);

			//If Health == 0 then respawn the player and set his health back to max Health
			if(P_Health == 0)
			{
				DeathSound.PlayDeath();
				Respawn(x,y,z);
				setHealth(100);
				
			}


	}

	void OnGUI()
	{
		if(!Enemy)
		{
			if(photonView.isMine)
			{
				//GUI.backgroundColor = Color.red;
				//Health Bar
				GUI.Box(new Rect (10, 10, HealthBarLength, 20), P_Health + "/" + P_MaxHealth);


				if(possible == true)
				{
				playerName = GUI.TextField (new Rect (10, Screen.height / 2, Screen.width /12, 20), playerName, 15);
				if(GUI.Button(new Rect (10, Screen.height / 2 + 20, Screen.width /12, 20),"Set Name"))
				{				
					photonView.RPC("SetName", PhotonTargets.AllBuffered, playerName);
						possible = false;
				}
				}

			}}

	}
		


	#region Health
	public void setHealth(int Health)
	{
		P_Health = Health;
	}

	[RPC]
	public void DealDamage(int Damage)
	{
		P_Health -= Damage;
	}

	public int getHealth()
	{
		return P_Health;
	}

	public void HealthChange(int change)
	{
		P_Health += change;

		
		//If Health droppes beneath 0 set is back to 0
		if(P_Health < 0)
		{
			P_Health = 0;
		}

		if (P_Health > P_MaxHealth) 
		{
			P_Health = P_MaxHealth;
		}

		HealthBarLength = (Screen.width / 4) * (P_Health / (float)P_MaxHealth);

		if (HealthBarLength < 50) 
		{
			HealthBarLength = 50;
		}
	}

	#endregion

	#region Mana
	public void setMana(int Mana)
	{
		P_Mana = Mana;
	}

	public int getMana()
	{
		return P_Mana;
	}	
	#endregion

	#region Stamina
	public void setStamina(int Stamina)
	{
		P_Stamina = Stamina;
	}
	
	public int getStamina()
	{
		return P_Stamina;
	}	
	#endregion

	#region Position
	public void setPlayerPosition(float x, float y, float z)
	{
		//In Position we temporarily store the x y an z that the user or systems gives so that after assigning the x y and z the P_Position.position can be set.
		Vector3 Position;
		Position.x = x;
		Position.y = y;
		Position.z = z;

		P_Transform.position = Position;
	}
	
	public Vector3 getPlayersPosition()
	{
		Vector3 position = P_Transform.position;
		return position;
	}
	#endregion

	public void LevelUp()
	{
		if(P_Experience == 100)
		{
			P_Level += 1;
		}
	}

	public void Respawn(float x,float y, float z)
	{

		setPlayerPosition (x, y, z);
		setHealth (P_MaxHealth);

	}

	[RPC]
	public void SetName(string name)
	{
			this.gameObject.name = name;
	}
	                   
}
