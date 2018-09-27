using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CreateLevelsList : MonoBehaviour {
	public GameObject button;
	public Transform panel;
	public int luft;
	public int storona;
	public int storona_back;
	public Canvas canva;

	// Use this for initialization
public	void Start () {
		GameSettings GS = GameSettings.getInstance ();
		canva.scaleFactor = GS.Scale;
		int x = Mathf.RoundToInt (Screen.width/2-3*(storona+luft+luft)*Screen.width/1920);
		//Debug.Log (Screen.height);
		int y = Mathf.RoundToInt(Screen.height/2+2*(storona+luft)*Screen.width/1920);
		//Debug.Log (Screen.width);
		int num = 1;
		GameObject back = GameObject.Find ("back");
		//Debug.Log (y+(storona-storona_back)/2);
		back.transform.SetPositionAndRotation (new Vector3 (x-(storona+storona_back+4*luft)/2*Screen.width/1920, y+(storona-storona_back)/2*Screen.width/1920, 0), Quaternion.identity);
		for (int i=0;i<5;i++) 
		{
			for (int j=0;j<8;j++)
			{
				GameObject	btn = Instantiate (button, new Vector3 (x+Mathf.RoundToInt (j*(storona+luft)*Screen.width/1920), y, 0), Quaternion.identity,panel);
				btn.name = "button" + num;
				btn.GetComponentInChildren<Text>().text = num.ToString();
				num += 1;
		}
			y = y - Mathf.RoundToInt((storona+luft)*Screen.width/1920);

		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void back()
	{
		SceneManager.LoadScene (0);
	}
}
