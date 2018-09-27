using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class back_setup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void loadBG(){
		int rvs = 5;
		int stat = 0;
		GameData GD = GameData.getInstance ();
		for (int a=0;a<GD.QMemCards;a++){//Нормализуем мемкарты: в случае если на карте нет ничего, то делаем выход -1
			for(int i=0;i<rvs;i++){
				for(int j=0;j<rvs;j++){
					if (GD.MemCards [a, 0, i, j] != 0&&GD.MemCards [a, 0, i, j] !=99) {
						stat++;
					}
				}
			}
			if (stat == 0) {
				GD.MemCardsMove [a] = -1;
			}
		}
		GD.SaveGame ();
		SceneManager.LoadScene (2);
	}
}
