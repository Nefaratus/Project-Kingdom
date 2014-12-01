using UnityEngine;
using System.Collections;

public class NPCStatus : MonoBehaviour {
	
	//test
	public int NPC_Level, NPC_Health,NPC_MaxHealth, NPC_Mana ,NPC_MaxMana,NPC_Stamina,NPC_MaxStamina , NPC_Experience;
	int NPC_Attributes;
	public int x, y, z;
	public Transform NPC_Transform;
	
	
	void Start()
	{
		setHealth(100);
	}
	
	void FixedUpdate()
	{
		if(NPC_Health == 0)
		{
			Respawn(x,y,z);
			setHealth(100);
			
		}
	}	
	
	#region Health
	public void setHealth(int Health)
	{
		NPC_Health = Health;
	}
	
	public int getHealth()
	{
		return NPC_Health;
	}
	
	public void HealthChange(int change)
	{
		NPC_Health += change;
	}
	
	#endregion
	
	#region Mana
	public void setMana(int Mana)
	{
		NPC_Mana = Mana;
	}
	
	public int getMana()
	{
		return NPC_Mana;
	}	
	#endregion
	
	#region Stamina
	public void setStamina(int Stamina)
	{
		NPC_Stamina = Stamina;
	}
	
	public int getStamina()
	{
		return NPC_Stamina;
	}	
	#endregion
	
	#region Position
	public void setNPCPosition(float x, float y, float z)
	{
		//In Position we temporarily store the x y an z that the user or systems gives so that after assigning the x y and z the P_Position.position can be set.
		Vector3 Position;
		Position.x = x;
		Position.y = y;
		Position.z = z;
		
		NPC_Transform.position = Position;
	}
	
	public Vector3 getNPCPosition()
	{
		Vector3 position = NPC_Transform.position;
		return position;
	}
	#endregion
	
	public void LevelUp()
	{
		if(NPC_Experience == 100)
		{
			NPC_Level += 1;
		}
	}
	
	public void Respawn(float x,float y, float z)
	{
		setNPCPosition (x, y, z);
		
	}
	
}
