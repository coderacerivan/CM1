using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SelectDirection : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void OnMouseDown ()
	//0 toleft
	//1 toforward
	//2 toright
	{
		int MemcardNumber = PlayerPrefs.GetInt ("MemcardNumber");
		GameData GD = GameData.getInstance ();
		if (Mathf.RoundToInt (this.transform.rotation.eulerAngles.z) == 0) {
			this.transform.Rotate (0, 0, -90);
			GD.MemCardsMove [MemcardNumber] = 2;
			//Debug.Log ("toright "+MemcardNumber);
		} else if (Mathf.RoundToInt (this.transform.rotation.eulerAngles.z) == 270) {
			this.transform.Rotate (0, 0, -180);
			GD.MemCardsMove [MemcardNumber] = 0;
			//Debug.Log ("toleft "+MemcardNumber);
		} else if (Mathf.RoundToInt (this.transform.rotation.eulerAngles.z) == 90) {
			this.transform.Rotate (0, 0, -90);
			GD.MemCardsMove [MemcardNumber] = 1;
			//Debug.Log ("toforward "+MemcardNumber);
		}
			
	}
}
