using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SaveLoadUI : MonoBehaviour {
	private Vector2 scrollViewVector = Vector2.zero;
	public Rect dropDownRect = new Rect(125,50,125,300);
	public static string[] list = {"Drop_Down_Menu"};
	public string fileName = "Character";
	public PlayerStatus ps;

	private SaveManager sm;
	
	int indexNumber;
	bool show = false;

	void Start(){
		sm = new SaveManager();
		sm.refresh();
		List<string> strings = new List<string>();
		foreach(KeyValuePair<string, CharacterSettings> kvp in sm.saves){
			strings.Add(kvp.Key);
		}
		list = strings.ToArray();
		if(list.Length == 0){
			list = new string[]{"NO SAVES"};
		}
	}
	
	void OnGUI()
	{  
		if(GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
		{
			if(!show)
			{
				show = true;
			}
			else
			{
				show = false;
			}
		}
		
		if(show)
		{
			scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height),scrollViewVector,new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length*25))));
			
			GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (list.Length*25))), "");
			
			for(int index = 0; index < list.Length; index++)
			{
				
				if(GUI.Button(new Rect(0, (index*25), dropDownRect.height, 25), ""))
				{
					show = false;
					indexNumber = index;
				}
				
				GUI.Label(new Rect(5, (index*25), dropDownRect.height, 25), list[index]);
				
			}
			
			GUI.EndScrollView();   
		}
		else
		{
			GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), list[indexNumber]);
		}
	}
}
