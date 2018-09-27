using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefMemCardClick : MonoBehaviour {
	//public PreferencesField PrefField;
	public GameObject wd;
	public GameObject rck;
	public GameObject brdr;
	public GameObject body_s;
	public GameObject tail;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnMouseDown() {
		GameData GD = GameData.getInstance();
		int MemcardNumber = PlayerPrefs.GetInt ("MemcardNumber");
		PreferencesField PrefField = GameObject.FindObjectOfType(typeof(PreferencesField)) as PreferencesField;
		int sel = PrefField.selected;
//		Debug.Log (sel);
//		Debug.Log (PrefField.memcard[3,1]);
		//GameObject TObj = new GameObject();
		//PrefField.selected = NameSel;
		//GameObject selector = new GameObject ();
		//selector = GameObject.Find ("selector");
		//selector.transform.Translate (new Vector3(this.transform.position.x-selector.transform.position.x,this.transform.position.y-selector.transform.position.y,1));
		//PrefField.selected = NameSel;
		//mathf.roundtoint
		if (GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] != 0 && GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] != 99) {
			Destroy (PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)]);
			//Debug.Log ("Object destroyed " + this.transform.position.x + " " + this.transform.position.y);
			GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] = 0;
		}
		else
			if (GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] == 0 && sel !=0 && GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] != 99) {
//				0 - grass
//				1 - border
//				2 - rock
//				3 - wood
//				98 - body_s
//				97 - tail
				//Debug.Log (Mathf.RoundToInt (this.transform.position.x) + " " + Mathf.RoundToInt (this.transform.position.y));
				if (sel == 1) {PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)] = Instantiate (brdr,new Vector3(this.transform.position.x,this.transform.position.y,0),Quaternion.identity);}
				if (sel == 2) {PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)] = Instantiate (rck,new Vector3(this.transform.position.x,this.transform.position.y,0),Quaternion.identity);}
				if (sel == 3) {PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)] = Instantiate (wd,new Vector3(this.transform.position.x,this.transform.position.y,0),Quaternion.identity);}
				if (sel == 97) {PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)] = Instantiate (tail,new Vector3(this.transform.position.x,this.transform.position.y,0),Quaternion.identity);}
				if (sel == 98) {PrefField.memcard_obj[Mathf.RoundToInt (this.transform.position.x), Mathf.RoundToInt (this.transform.position.y)] = Instantiate (body_s,new Vector3(this.transform.position.x,this.transform.position.y,0),Quaternion.identity);}
				GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)] = sel;
				//Debug.Log ("x= "+Mathf.RoundToInt (this.transform.position.x-1)+" y= "+Mathf.RoundToInt (this.transform.position.y-1)+" MCN= "+MemcardNumber+" result =" +GD.MemCards [MemcardNumber,0,Mathf.RoundToInt (this.transform.position.x-1), Mathf.RoundToInt (this.transform.position.y-1)]);
		}
		ExpandMaps ();
	}
	void ExpandMaps() 
	//предполагаем что у нас есть массив GD.MemCards 
	//карточки[номер_карты,номер_поворота,кол-во_х,кол-во_у]
	//размер квадрата, который видит змея задается переменной (range_vision_snake (rv_s)), в принципе ее можно добавить в класс змей
	//которая в свою очередь должна зависеть от прокачанности змеи
	//предполагаем что повороты
	//0 - вверх
	//1 - вниз
	//2 - влево
	//3 - вправо
	{
		int rv_s = 5;
		GameData GD = GameData.getInstance();
		int pow;
		for (int i = 0;i<10;i++)
		{
			pow = 1; //отражаем сверху-вниз
			for (int xc = 0;xc<rv_s;xc++)
			{
				for (int yc = 0;yc<rv_s;yc++)
				{
					GD.MemCards[i,pow,xc,yc] = GD.MemCards[i,0,rv_s-1-xc,rv_s-1-yc];
				}
			}
			pow = 2; //отражаем сверху-налево
			for (int xc = 0;xc<rv_s;xc++)
			{
				for (int yc = 0;yc<rv_s;yc++)
				{
					GD.MemCards[i,pow,xc,yc] = GD.MemCards[i,0,yc,rv_s-1-xc];
				}
			}
			pow = 3; //отражаем сверху-направо
			for (int xc = 0;xc<rv_s;xc++)
			{
				for (int yc = 0;yc<rv_s;yc++)
				{
					GD.MemCards[i,pow,xc,yc] = GD.MemCards[i,0,rv_s-1-yc,xc];
				}
			}		


		}

	}
}






