using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playpause : MonoBehaviour {
	public GameObject but;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void play() {
		CurrLevel CL = CurrLevel.getInstance ();
		Crusher crush = Crusher.getInstance ();
		if (CL.play == false) {
			crush.speed = 1;
			CL.play = true;
			but.GetComponentInChildren<Text>().text ="Play";
			//Debug.Log (1);
		}
		else if (CL.play == true&&crush.speed == 1){
			crush.speed = 2;
			but.GetComponentInChildren<Text>().text ="Play x2";
			//Debug.Log (2);
		}
		else if (CL.play == true&&crush.speed == 2){
			crush.speed = 3;
			but.GetComponentInChildren<Text>().text ="Play x3";
			//Debug.Log (3);
		}
		else if (CL.play == true&&crush.speed == 3){
			crush.speed = 1;
			but.GetComponentInChildren<Text>().text ="Play";
			//Debug.Log (1);
		}


	}
	public void pause() {
		CurrLevel CL = CurrLevel.getInstance ();
		CL.play = false;
	}
}
