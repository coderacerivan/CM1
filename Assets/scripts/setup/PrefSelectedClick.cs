using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefSelectedClick : MonoBehaviour {
	public PreferencesField PrefField;
	public int NameSel;
	public GameObject selector;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
	public void OnMouseDown() {
		
		//PrefField.selected = NameSel;
		//GameObject selector = new GameObject ();
		//selector = GameObject.Find ("selector");
		selector.transform.Translate (new Vector3(this.transform.position.x-selector.transform.position.x,this.transform.position.y-selector.transform.position.y,1));
		PrefField.selected = NameSel;


		//GameObject cam = GameObject.Find ("camera1");
		//Debug.Log (cam.);
		//Debug.Log("x = "+this.transform.position.x+" y = "+this.transform.position.y);
		//Destroy (GameObject.Find ("HeadOnField"));
	}
}
