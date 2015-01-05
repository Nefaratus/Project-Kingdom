using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;

public class SaveManager {
	string dir = @"s:\saves\";
	public Dictionary<string, CharacterSettings> saves;
	private string EXT = "pkc";
	// Use this for initialization

	public void create(CharacterSettings cs, string name){
		if(cs != null){
			string fileName = dir + name + "." + EXT;
			string file = cs.ToString();
			try{
				File.WriteAllText(fileName, file);
			} catch(Exception e) {
				Debug.LogError("File failed to save! | " + e.Message);
			}
		}
	}

	public void refresh(){
		saves = new Dictionary<string, CharacterSettings>();
		//Debug.Log(""+Application.persistentDataPath);
		string[] filePaths = Directory.GetFiles(dir);
		foreach(string s in filePaths){
			if(Path.GetExtension(s) == EXT){
				
				try{
					String file = File.ReadAllText(s);
					CharacterSettings cs = new CharacterSettings();
					cs.fillFields(s);
					saves.Add(Path.GetFileNameWithoutExtension(s), cs);
				}catch(Exception e){
					Debug.LogError("Save corrupted!: " + s + " | " + e.Message);
				}
				
			}
		}
	}
}
