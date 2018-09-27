using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreferencesField : MonoBehaviour
{
	public GameObject MemTile;
	public GameObject Head;
	public GameObject brdr;
	public GameObject rck;
	public GameObject wd;
	public GameObject body_s;
	public GameObject tail;
	public int selected;
	public Text number;
	public GameObject[,] memcard_obj = new GameObject[6, 6];
	public GameObject SelectDirection;

	// Use this for initialization
	void Start ()
	{
		
		//GameSettings GS = GameSettings.getInstance ();
		//canva_main.scaleFactor = GS.Scale;
		//int MemcardNumber = new int();
		int MemcardNumber = PlayerPrefs.GetInt ("MemcardNumber");
		GameData GD = GameData.getInstance();
		number.text = MemcardNumber.ToString ();
		//0 toleft
		//1 toforward
		//2 toright
		//int mcm = new int();

		//Debug.Log ("Direction - "+GD.MemCardsMove[MemcardNumber].ToString ());
		if (GD.MemCardsMove [MemcardNumber] == 0) {
			SelectDirection.transform.Rotate (0, 0, 90);
			//Debug.Log ("90");
		}
		if (GD.MemCardsMove [MemcardNumber] == 1) {
			SelectDirection.transform.Rotate (0, 0, 0);
			//Debug.Log ("-90");
		}
		if (GD.MemCardsMove [MemcardNumber] == 2) {
			SelectDirection.transform.Rotate (0, 0, -90);
			//Debug.Log ("0");
		}
		selected = 0;
	
		//Создаем поле настроек
		for (int i = 1; i < 6; i++) {
			for (int j = 1; j < 6; j++) {				
					//GameObject go = 
				Instantiate (MemTile, new Vector3 (i, j, 0), Quaternion.identity);			


				//go.transform.SetParent(canva_main.transform);
				//go.transform.localScale = new Vector3 (0, 300, 1);
				}
			}
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				//Debug.Log ("MemcardNumber="+MemcardNumber+" i="+i+" j="+j);
				if (GD.MemCards [MemcardNumber, 0, i, j] == 1) {
					memcard_obj[i+1,j+1] = Instantiate (brdr, new Vector3 (i+1, j+1, 0), Quaternion.identity);
				}
				if (GD.MemCards [MemcardNumber, 0, i, j] == 2) {
					memcard_obj[i+1,j+1] = Instantiate (rck, new Vector3 (i+1, j+1, 0), Quaternion.identity);
				}
				if (GD.MemCards [MemcardNumber, 0, i, j] == 3) {
					memcard_obj[i+1,j+1] = Instantiate (wd, new Vector3 (i+1, j+1, 0), Quaternion.identity);
				}
			}
		}
		memcard_obj[3,1]= Instantiate (Head,new Vector3(3, 2, 0),Quaternion.identity);
		memcard_obj[3,1].name = "HOF";
		if (GD.MemCards [MemcardNumber, 0, 2, 1] != 99) {
			GD.MemCards [MemcardNumber, 0, 2, 1] = 99;
		}


//		wd = Instantiate (wd, new Vector3 (7, 1, 0), Quaternion.identity);
//		rck = Instantiate (rck, new Vector3 (7, 2, 0), Quaternion.identity);
//		brdr = Instantiate (brdr, new Vector3 (7, 3, 0), Quaternion.identity);
//		tail = Instantiate (tail, new Vector3 (7, 4, 0), Quaternion.identity);
//		body_s = Instantiate (body_s, new Vector3 (7, 5, 0), Quaternion.identity);
	}
	// Update is called once per frame
	void Update ()
	{
		
	}
}
