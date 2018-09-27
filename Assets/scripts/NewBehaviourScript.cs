using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class TableSnake:ScriptableObject
{
	public int body_length = 0;
	public GameObject[] parts = new GameObject[200];
	public string[] partsdir = new string[200];
	public string direction = "";

	public string CreateSnake (GameObject head, GameObject body_s, GameObject tail, string capt, int x, int y, int length, string dest)//имя , положение по осям, длина, расположение создания
	{  
		name = capt;
		body_length = length;
		int max = length - 1;
		//расположение: слева хвост, справа голова (змея смотрит перед собой)
		if (dest == "toright") {
			direction = "toright";
			Debug.Log (direction);
			parts [0] = new GameObject ();
			parts [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			parts [0].transform.Rotate (new Vector3(0,0,-90));
			partsdir[0] = "toright";
			for (int i = 1; i < max; i++) {
				parts [i] = Instantiate (body_s, new Vector3 (x - i, y, 0), Quaternion.identity);
				parts [i].transform.Rotate (new Vector3(0,0,-90));
				partsdir[i] = "toright";
			}
			parts [max] = new GameObject ();
			parts [max] = Instantiate (tail, new Vector3 (x - max, y, 0), Quaternion.identity);
			parts [max].transform.Rotate (new Vector3(0,0,-90));
			partsdir[max] = "toright";
		}
		//расположение: слева голова, справа хвост (змея смотрит перед собой)
		if (dest == "toleft") {
			direction = "toleft";
			parts [0] = new GameObject ();
			parts [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			parts [0].transform.Rotate (new Vector3(0,0,90));
			partsdir[0] = "toleft";
			for (int i = 1; i < max; i++) {
				parts [i] = new GameObject ();
				parts [i] = Instantiate (body_s, new Vector3 (x + i, y, 0), Quaternion.identity);
				parts [i].transform.Rotate (new Vector3(0,0,90));
				partsdir[i] = "toleft";
			}
			parts [max] = new GameObject ();
			parts [max] = Instantiate (tail, new Vector3 (x + max, y, 0), Quaternion.identity);
			parts [max].transform.Rotate (new Vector3(0,0,90));
			partsdir[max] = "toleft";
		}
		//расположение: снизу хвост, сверху голова (змея смотрит перед собой)
		if (dest == "toup") {
			direction = "toup";
			parts [0] = new GameObject ();
			parts [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			partsdir[0] = "toup";
			for (int i = 1; i < max; i++) {
				parts [i] = new GameObject ();
				parts [i] = Instantiate (body_s, new Vector3 (x, y - i, 0), Quaternion.identity);
				partsdir[i] = "toup";
			}
			parts [max] = new GameObject ();
			parts [max] = Instantiate (tail, new Vector3 (x, y - max, 0), Quaternion.identity);
			partsdir[max] = "toup";
		}
		//расположение: снизу голова, справа хвост (змея смотрит перед собой)
		if (dest == "todown") {
			direction = "todown";
			parts [0] = new GameObject ();
			parts [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			parts [0].transform.Rotate (new Vector3(0,0,180));
			partsdir[0] = "todown";
			for (int i = 1; i < max; i++) {
				parts [i] = new GameObject ();
				parts [i] = Instantiate (body_s, new Vector3 (x, y + i, 0), Quaternion.identity);
				parts [i].transform.Rotate (new Vector3(0,0,180));
				partsdir[i] = "todown";
			}
			parts [max] = new GameObject ();
			parts [max] = Instantiate (tail, new Vector3 (x, y + max, 0), Quaternion.identity);
			parts [max].transform.Rotate (new Vector3(0,0,180));
			partsdir[max] = "todown";
		}
		return "create snake "+capt+" sucessfull";
	}

	void MoveSnake (string dest, int qtt) //dest куда движемся, qtt количество метров
	{
		int x_old, y_old, x, y;
		x_old = Mathf.RoundToInt (parts [0].transform.position.x);
		y_old = Mathf.RoundToInt (parts [0].transform.position.y);
		string olddir = partsdir [0];
		//система координат относительно положения змеи, т.к. концепция карт памяти с алгоритмами предполагает именно такую систему координат
		//dest - куда змея должна передвинуться
		//direction - абсолютное положение готовы змеи

		//начинаем с головы
		if (dest == "toright") {
			if (partsdir[0] == "toright") {
				parts [0].transform.Translate (new Vector3(x_old, y_old - qtt,0));
				partsdir[0] = "todown";
			}
			if (partsdir[0] == "toleft") {
				parts [0].transform.Translate (new Vector3(x_old, y_old + qtt,0));
				partsdir[0] = "toup";
			}
			if (partsdir[0] == "toup") {
				parts [0].transform.Translate (new Vector3(x_old + qtt, y_old,0));
				partsdir[0] = "toright";
			}
			if (partsdir[0] == "todown") {
				parts [0].transform.Translate (new Vector3(x_old - qtt, y_old,0));
				partsdir[0] = "toleft";
			}
			parts [0].transform.Rotate (new Vector3(0,0,-90));
		}
		if (dest == "toleft") {
			if (partsdir[0] == "toright") {
				parts [0].transform.Translate (new Vector3 (x_old, y_old + qtt, 0));
				partsdir[0] = "toup";
			}
			if (partsdir[0] == "toleft") {
				parts [0].transform.Translate (new Vector3 (x_old, y_old - qtt, 0));
				partsdir[0] = "todown";
			}
			if (partsdir[0] == "toup") {
				parts [0].transform.Translate (new Vector3 (x_old - qtt, y_old, 0));
				partsdir[0] = "toleft";
			}
			if (partsdir[0] == "todown") {
				parts [0].transform.Translate (new Vector3 (x_old + qtt, y_old, 0));
				partsdir[0] = "toright";
			}
			parts [0].transform.Rotate (new Vector3(0,0,90));
		}
		if (dest == "toforward") {
			if (partsdir[0] == "toright") {
				parts [0].transform.Translate (new Vector3 (x_old + qtt, y_old, 0));
			}
			if (partsdir[0] == "toleft") {
				parts [0].transform.Translate (new Vector3 (x_old - qtt, y_old, 0));
			}
			if (partsdir[0] == "toup") {
				parts [0].transform.Translate (new Vector3 (x_old, y_old + qtt, 0));
			}
			if (partsdir[0] == "todown") {
				parts [0].transform.Translate (new Vector3 (x_old, y_old - qtt, 0));
			}
		}


		//наметки на прыжки и кусание

		if (dest == "tojumpright") {

		}
		if (dest == "tojumpleft") {

		}
		if (dest == "tojumpforward") {

		}
		if (dest == "toeat") {

		}

		//за головой подтягиваем остальное тело
		string old2dir;
		for (int i = 1; i < (body_length - 1); i++) {
			x = Mathf.RoundToInt (parts [i].transform.position.x);
			y = Mathf.RoundToInt (parts [i].transform.position.y);
			parts [i].transform.Translate (new Vector3 (x_old, y_old, 0));
			x_old = x;
			y_old = y;
			old2dir = partsdir [i];
			partsdir [i] = olddir;
			olddir = old2dir;
		}

	}
}

public class NewBehaviourScript : MonoBehaviour
{

	public int qrck = 5;
	public int qwd = 10;

	public GameObject brdr;
	public GameObject grn;
	public GameObject rck;
	public GameObject wd;
	public GameObject head;
	// Голова
	public GameObject body_s;
	//Тело
	public GameObject tail;
	//Хвост
	public TableSnake[] snakes = new TableSnake[10]; 

	public int[,] StartPoints = new int[10, 2];

	void Start ()
	{
		CreateField ();

		StartPoints [0, 0] = 3;
		StartPoints [0, 1] = 5;
		snakes [0] = new TableSnake ();
		Debug.Log (snakes [0].CreateSnake (head, body_s, tail, "FirstSnake", 0, 7, 10, "toright"));

	}

	// Update is called once per frame
	void Update ()
	{

	}

	void CreateField ()
	{ //Создаем игровое поле
		int qqw = 0;
		int qqr = 0;
		for (int i = 1; i < 20; i++) {
			for (int j = 1; j < 10; j++) {
				if (qqr < qrck && UnityEngine.Random.Range (0, 30) == 1) {
					Instantiate (rck, new Vector3 (i, j, 0), Quaternion.identity);
					qqr++;
				}
				if (qqw < qwd && UnityEngine.Random.Range (0, 15) == 1) {
					Instantiate (wd, new Vector3 (i, j, 0), Quaternion.identity);
					qqw++;
				}
				Instantiate (grn, new Vector3 (i, j, 0), Quaternion.identity);
			}
		}
		for (int i = 0; i < 21; i++) {
			Instantiate (brdr, new Vector3 (i, 0, 0), Quaternion.identity);
			Instantiate (brdr, new Vector3 (i, 10, 0), Quaternion.identity);
		}
		for (int i = 0; i < 11; i++) {
			Instantiate (brdr, new Vector3 (0, i, 0), Quaternion.identity);
			Instantiate (brdr, new Vector3 (20, i, 0), Quaternion.identity);
		}
	}
}

