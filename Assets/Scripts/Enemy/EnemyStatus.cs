using UnityEngine;
using System.Collections;

public class EnemyStatus : MonoBehaviour {

	//test
	public int E_Level, E_Health,E_MaxHealth, E_Mana ,E_MaxMana,E_Stamina,E_MaxStamina , E_Experience;
	int E_Attributes;
	public int x, y, z;
	public Transform E_Transform;
	

	void Start()
	{
		setHealth(100);
	}
	
	void FixedUpdate()
	{
		if(E_Health == 0)
		{
			Respawn(x,y,z);
			setHealth(100);
			
		}
	}	

	#region Health
	public void setHealth(int Health)
	{
		E_Health = Health;
	}
	
	public int getHealth()
	{
		return E_Health;
	}

	public void HealthChange(int change)
	{
		E_Health += change;
	}
	
	#endregion
	
	#region Mana
	public void setMana(int Mana)
	{
		E_Mana = Mana;
	}
	
	public int getMana()
	{
		return E_Mana;
	}	
	#endregion
	
	#region Stamina
	public void setStamina(int Stamina)
	{
		E_Stamina = Stamina;
	}
	
	public int getStamina()
	{
		return E_Stamina;
	}	
	#endregion
	
	#region Position
	public void setEnemyPosition(float x, float y, float z)
	{
		//In Position we temporarily store the x y an z that the user or systems gives so that after assigning the x y and z the P_Position.position can be set.
		Vector3 Position;
		Position.x = x;
		Position.y = y;
		Position.z = z;
		
		E_Transform.position = Position;
	}
	
	public Vector3 getEnemyPosition()
	{
		Vector3 position = E_Transform.position;
		return position;
	}
	#endregion
	
	public void LevelUp()
	{
		if(E_Experience == 100)
		{
			E_Level += 1;
		}
	}
	
	public void Respawn(float x,float y, float z)
	{
		setEnemyPosition (x, y, z);
		
	}

}
