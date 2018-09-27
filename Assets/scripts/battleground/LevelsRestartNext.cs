using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class LevelsRestartNext : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Levels(){
		SceneManager.LoadScene (1);
	}
	public void Restart(){
		SceneManager.LoadScene (2);
	}
	public void Next(){
		int actual = PlayerPrefs.GetInt ("actual");
		GameData GD = GameData.getInstance ();
		CurrLevel CL = CurrLevel.getInstance ();
		for (int i=0;i<101;i++){
			for (int j=0;j<101;j++){
				CL.Map[i,j] = GD.LevelMaps [actual+1,i,j];//считываем какой номер уровня был актуальный (текущий), записываем карту текущего уровня из текущий_уровень+1 и прогружаем сцену BattleGround по-новой
			}
		}
		SceneManager.LoadScene (2);
	}
}
