using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneJump : MonoBehaviour {
	public void loadback(){
		SceneManager.LoadScene (1);
	}
	public void loadsetup(){
		SceneManager.LoadScene (3);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
