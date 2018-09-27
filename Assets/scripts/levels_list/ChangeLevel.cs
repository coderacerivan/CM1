using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class ChangeLevel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LoadLevel(){
		PlayerPrefs.SetInt ("actual", Convert.ToInt32 (GetComponentInChildren<Text> ().text));

		GameData GD = GameData.getInstance ();
		CurrLevel CL = CurrLevel.getInstance ();
		for (int i=0;i<101;i++){
			for (int j=0;j<101;j++){
				CL.Map[i,j] = GD.LevelMaps [Convert.ToInt32 (GetComponentInChildren<Text> ().text),i,j];
			}
		}
		SceneManager.LoadScene (2);
	}
}
