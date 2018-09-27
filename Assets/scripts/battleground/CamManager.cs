using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {
	public Camera cam;
	public int delta;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void plus90(){
		cam.transform.Rotate (0, 0, 90);
	}
	public void minus90(){
		cam.transform.Rotate (0, 0, -90);
	}
	public void plus(){
		cam.orthographicSize=cam.orthographicSize-delta;	
	}
	public void minus(){
		cam.orthographicSize=cam.orthographicSize+delta;	
	}
}
