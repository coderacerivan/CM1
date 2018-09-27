using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class arrow_click : MonoBehaviour {
	public GameObject pressed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnMouseDown() {
		if (this.name == "left") {
			int num = PlayerPrefs.GetInt ("MemcardNumber");
			PlayerPrefs.SetInt ("MemcardNumber", num - 1);
			GameData GD = GameData.getInstance();
			GD.SaveGame ();
			SceneManager.LoadScene (3);
		}
		if (this.name == "right") {
			int num = PlayerPrefs.GetInt ("MemcardNumber");
			PlayerPrefs.SetInt ("MemcardNumber", num + 1);
			GameData GD = GameData.getInstance();
			GD.SaveGame ();
			SceneManager.LoadScene (3);
		}
		//GameObject p = Instantiate (pressed, this.transform.position, this.transform.rotation);
		//if (this.name == "right") {p.name = "rightp";}
		//if (this.name == "left") {p.name = "leftp";}
	}
	//public void OnMouseUp() {
	//	if (this.name == "right") { Destroy (GameObject.Find ("rightp")); }
	//	if (this.name == "left") { Destroy (GameObject.Find ("leftp")); }
	//}
}
