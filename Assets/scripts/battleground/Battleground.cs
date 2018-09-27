using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Battleground : MonoBehaviour
{
	public GameObject brdr;
	public GameObject rck;
	public GameObject wd;
	public GameObject grass;
	public GameObject[,] level_obj = new GameObject[101, 101];
	public GameObject head;
	public GameObject body_s;
	public GameObject tail;
	public Canvas canva;
	public GameObject WinFail; 
	public Camera cam;
	public Text WF;
	// Use this for initialization
	void Start ()//Разворачиваем поле и рисуем на нем препятствия
	{
		WinFail.SetActive(false);
		GameSettings GS = GameSettings.getInstance ();
		canva.scaleFactor = GS.Scale;
		GameData GD = GameData.getInstance ();
		Crusher crush = Crusher.getInstance ();
		crush.speed = 1;
		crush.Length = GD.CrusherLength;
		CurrLevel CL = CurrLevel.getInstance ();
		crush.Structure = GD.CrusherStructure;
		//int actual = PlayerPrefs.GetInt ("level");
		int[] start = new int[2];  
		int[] finish = new int[2];
		for (int i = 1; i < 101; i++) {
			for (int j = 1; j < 101; j++) {		
				
				////Debug.Log (i+" "+j+" "+CL.Map[i,j]);

				if (CL.Map [i, j] == 0) {
					level_obj [i, j] = Instantiate (grass, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (CL.Map [i, j] == 1) {
					level_obj [i, j] = Instantiate (brdr, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (CL.Map [i, j] == 2) {
					level_obj [i, j] = Instantiate (rck, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (CL.Map [i, j] == 3) {
					level_obj [i, j] = Instantiate (wd, new Vector3 (i, j, 0), Quaternion.identity);
				}
				if (CL.Map [i, j] == 90) { //Start Point
					level_obj [i, j] = Instantiate (grass, new Vector3 (i, j, 0), Quaternion.identity);
					start [0] = i;
					start [1] = j;
					//Debug.Log ("start ("+start[0]+","+start[1]+")");
				}
				if (CL.Map [i, j] == 91) { //Finish Point
					level_obj [i, j] = Instantiate (grass, new Vector3 (i, j, 0), Quaternion.identity);
					finish [0] = i;
					finish [1] = j;
					//Debug.Log ("finish ("+finish[0]+","+finish[1]+")");
				}
			}
		}
		//Debug.Log ("start ("+start[0]+","+start[1]+")");
		//Debug.Log ("finish ("+finish[0]+","+finish[1]+")");
		LoadSnake (start [0], start [1], head, body_s, tail); //Кладем змею на поле
		cam.transform.position = new Vector3 (start [0], start [1], cam.transform.position.z);


//		int wtg = WhereToGo ();
		crush.TargetPosition [0].z = -1000;
//		//Debug.Log ("wtg - "+wtg);
//		MoveSnake (wtg, 1);
		GD.QMemCards = 10;

		//string str = "";
//		for (int a=0;a<10;a++){
//			Debug.Log ("a => "+a);
//			for (int dir=0;dir<4;dir++){
//				Debug.Log ("dir => "+dir);
//				for (int y = 4; y >=0; y--) {
//					for (int x = 0; x < 5; x++) {
//				str += GD.MemCards [a, dir, x, y];						
//			}
//			Debug.Log (str);
//			str = "";
//
//		}
//				Debug.Log ("**********");
//		}
//		}
		for (int x = 0; x < GD.QMemCards; x++) {
//			Debug.Log ("a " + x + " MMM " + GD.MemCardsMove [x]);						
		}
		//for (int i=0;i<10;i++){GD.MemCardsMove[i]=-1;}
	}
	
	// Update is called once per frame
	void Update ()//Здесь зашито движение змеи, ее действия с объектами и подсчет всевозможных текущих значения типа очков
	{
		CurrLevel CL = CurrLevel.getInstance ();
		if (CL.play) {
			int wtg = WhereToGo ();
			MoveSnake (wtg, 1);//двигаем змею на 1 метр туда, куда укажет wheretogo
		}
	}


	void LoadSnake (int x, int y, GameObject head, GameObject body_s, GameObject tail)//кладем змею на карту на точку входа xy
	{ 	

		//int actual = PlayerPrefs.GetInt ("level");
		CurrLevel CL = CurrLevel.getInstance ();
		Crusher crush = Crusher.getInstance ();
		if (crush.Length <= 0) {
			crush.Length = 10;
		}
		int max = crush.Length - 1;
		//расположение: слева хвост, справа голова (змея смотрит перед собой)
				//Debug.Log (" x="+x+" y="+y);
				//Debug.Log ("CL.Map[ x, y ]="+CL.Map[ x, y]);
				//Debug.Log ("CL.Map[ x-1, y ]="+CL.Map[ x-1, y]);
				//Debug.Log ("CL.Map[ x+1, y ]="+CL.Map[ x+1, y]);
				//Debug.Log ("CL.Map[ x, y-1 ]="+CL.Map[ x, y-1]);
				//Debug.Log ("CL.Map[ x, y+1 ]="+CL.Map[ x, y+1]);

		if (CL.Map [x, y - 1] == 1 && CL.Map [x, y + 1] == 1 && CL.Map [x + 1, y] == 0) {
			//Debug.Log ("toright");
			crush.Direction [0] = "toright";
			crush.Objects [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			crush.Objects [0].transform.Rotate (0, 0, -90);
			for (int i = 1; i < max; i++) {
				crush.Objects [i] = Instantiate (body_s, new Vector3 (x - i, y, 0), Quaternion.identity);
				crush.Objects [i].transform.Rotate (0, 0, -90);
				crush.Direction [i] = "toright";
			}
			crush.Objects [max] = Instantiate (tail, new Vector3 (x - max, y, 0), Quaternion.identity);
			crush.Objects [max].transform.Rotate (0, 0, -90);
			crush.Direction [max] = "toright";
		}
		//расположение: слева голова, справа хвост (змея смотрит перед собой)
		if (CL.Map [x, y - 1] == 1 && CL.Map [x, y + 1] == 1 && CL.Map [x - 1, y] == 0) {
			//Debug.Log ("toleft");
			crush.Direction [0] = "toleft";
			crush.Objects [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			crush.Objects [max].transform.Rotate (0, 0, 90);
			for (int i = 1; i < max; i++) {
				crush.Objects [i] = Instantiate (body_s, new Vector3 (x + i, y, 0), Quaternion.identity);
				crush.Objects [max].transform.Rotate (0, 0, 90);
				crush.Direction [i] = "toleft";
			}
			crush.Objects [max] = Instantiate (tail, new Vector3 (x + max, y, 0), Quaternion.identity);
			crush.Objects [max].transform.Rotate (0, 0, 90);
			crush.Direction [max] = "toleft";
		}
		//расположение: снизу хвост, сверху голова (змея смотрит перед собой)
		if (CL.Map [x - 1, y] == 1 && CL.Map [x + 1, y] == 1 && CL.Map [x, y + 1] == 0) {
			//Debug.Log ("toup");
			crush.Direction [0] = "toup";
			crush.Objects [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			for (int i = 1; i < max; i++) {
				crush.Objects [i] = Instantiate (body_s, new Vector3 (x, y - i, 0), Quaternion.identity);
				crush.Direction [i] = "toup";
			}
			crush.Objects [max] = Instantiate (tail, new Vector3 (x, y - max, 0), Quaternion.identity);
			crush.Direction [max] = "toup";
		}
		//расположение: снизу голова, сверху хвост (змея смотрит перед собой)
		if (CL.Map [x - 1, y] == 1 && CL.Map [x + 1, y] == 1 && CL.Map [x, y - 1] == 0) {
			//Debug.Log ("todown");
			crush.Direction [0] = "todown";
			crush.Objects [0] = Instantiate (head, new Vector3 (x, y, 0), Quaternion.identity);
			crush.Objects [max].transform.Rotate (0, 0, 180);
			for (int i = 1; i < max; i++) {
				crush.Objects [i] = Instantiate (body_s, new Vector3 (x, y + i, 0), Quaternion.identity);
				crush.Objects [max].transform.Rotate (0, 0, 180);
				crush.Direction [i] = "todown";
			}
			crush.Objects [max] = Instantiate (tail, new Vector3 (x, y + max, 0), Quaternion.identity);
			crush.Objects [max].transform.Rotate (0, 0, 180);
			crush.Direction [max] = "todown";
		}

	}



	//*************************end of LoadSnake***************************
















	int WhereToGo ()
	{
		Crusher crush = Crusher.getInstance ();
		if (crush.TargetPosition [0].z == -1000) {
			
			CurrLevel CL = CurrLevel.getInstance ();
			GameData GD = GameData.getInstance ();		
			float xhead = crush.Objects [0].transform.position.x; //положения головы по x,y
			float yhead = crush.Objects [0].transform.position.y;
			string drctn = crush.Direction [0]; // в какую сторону смотрит голова
			int rvs = 5;///!!!!!!!!!!!!!!!!!!!!!!!!!!
			int[,] current_vision = new int[rvs, rvs];
			float xcur = new float ();
			float ycur = new float ();
			int dir = new int ();
			int xhead_r_w = 0; //относительное в пространстве rvs положение точки перед лбом змеи по х и у
			int yhead_r_w = 0;
			//смотрим в какую сторону повернута голова и какие ее координаты и исходя из этого
			//считываем картинку слева, справа, сверху или снизу от головы
			if (drctn == "toup") {//0
				dir = 0;
				xcur = Mathf.Round (xhead - (rvs / 2 - 1 / 2));
				ycur = yhead - 1;
				xhead_r_w = 2;
				yhead_r_w = 2;
			}
			if (drctn == "todown") {//1
				dir = 1;
				xcur = Mathf.Round (xhead - (rvs / 2 - 1 / 2));
				ycur = yhead - rvs + 2;
				xhead_r_w = 2;
				yhead_r_w = 2;
			}
			if (drctn == "toleft") {//2
				dir = 2;
				xcur = xhead - rvs + 1;
				ycur = Mathf.Round (yhead - (rvs / 2 - 1 / 2));
				xhead_r_w = 2;
				yhead_r_w = 2;
			}
			if (drctn == "toright") {//3
				dir = 3;
				xcur = xhead - 1;
				ycur = Mathf.Round (yhead - (rvs / 2 - 1 / 2));
				xhead_r_w = 2;
				yhead_r_w = 2;
			}
			Debug.Log (drctn);
			int k = 0;
			int l = 0;
			for (int i = Mathf.RoundToInt (xcur); i < Mathf.RoundToInt (xcur + rvs); i++) {
				l = 0;
				for (int j = Mathf.RoundToInt (ycur); j < Mathf.RoundToInt (ycur + rvs); j++) {
					//Debug.Log ("k=" + k + " l=" + l + " i=" + i + " j=" + j);
					current_vision [k, l] = CL.Map [i, j];
					if (i == Mathf.RoundToInt (xhead) && j == Mathf.RoundToInt (yhead)) {
						current_vision [k, l] = 99;//голова
					}
					for (int m = 1; m < (crush.Length - 1); m++) {
						if (i == Mathf.RoundToInt (crush.Objects[m].transform.position.x) && j == Mathf.RoundToInt (crush.Objects[m].transform.position.y)) {
							current_vision [k, l] = 98;//тушка
						}
					}
					if (i == Mathf.RoundToInt (crush.Objects[crush.Length-1].transform.position.x) && j == Mathf.RoundToInt (crush.Objects[crush.Length-1].transform.position.y)) {
						current_vision [k, l] = 97;//хвост
					}
					if (current_vision [k, l] == 1) {
						//Debug.Log (1);
					}
					l++;
				}
				k++;
			}



			string str = "";		
			//Debug.Log ("dir => " + dir);
			for (int y = 4; y >= 0; y--) {
				for (int x = 0; x < 5; x++) {
					str += current_vision [x, y];						
				}
				//Debug.Log (str);
				str = "";
			}


			int pivot = -1;
			for (int a = 0; a < GD.QMemCards; a++) {
				//Debug.Log ("**********");

				for (int z = 4; z >= 0; z--) {
					 str = "";
					for (int x = 0; x < 5; x++) {
						str += GD.MemCards [a, dir, x, z];						
					}
				//	Debug.Log (str);
					str = "";
				}
				//Debug.Log ("**********");
				if (pivot == -1) {
					pivot = a;
					for (int x = 0; x < rvs; x++) {
						for (int y = 0; y < rvs; y++) {
//							if (GD.MemCards [a, dir, x, y] == 99 || (current_vision [x, y] == 99)) {
//								//Debug.Log ("x "+x+" y "+ y);
//							}
							if (GD.MemCards [a, dir, x, y] > 0 && (current_vision [x, y] != GD.MemCards [a, dir, x, y])) { //GD.MemCards [a, dir, x, y] > 0 && 
								
								pivot = -1;
							}
						}
					}
				}
//				if (pivot>=0) {
//					//Debug.Log ("pivot "+pivot );
//				}
			}


			//если перед носом не бордюр, и мы не в курсе, куда двигаться, то считаем что нужно есть
//			if (current_vision [xhead_r_w, yhead_r_w] >1 && current_vision [xhead_r_w, yhead_r_w] < 90 && (pivot == -1 || (pivot > -1 && GD.MemCardsMove [pivot] == -1))) {//
//				Debug.Log(current_vision [xhead_r_w, yhead_r_w]);
//				return -1;
//			}


			if (GD.MemCardsMove [pivot]==0&&dir==0&&CL.Map[Mathf.RoundToInt (xhead)-1,Mathf.RoundToInt (yhead)]!=0){//если едем вверх и поворачиваем налево и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==2&&dir==0&&CL.Map[Mathf.RoundToInt (xhead)+1,Mathf.RoundToInt (yhead)]!=0){//если едем вверх и поворачиваем направо и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==0&&dir==1&&CL.Map[Mathf.RoundToInt (xhead)+1,Mathf.RoundToInt (yhead)]!=0){//если едем вниз и поворачиваем налево и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==2&&dir==1&&CL.Map[Mathf.RoundToInt (xhead)-1,Mathf.RoundToInt (yhead)]!=0){//если едем вниз и поворачиваем направо и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==0&&dir==2&&CL.Map[Mathf.RoundToInt (xhead),Mathf.RoundToInt (yhead)-1]!=0){//если едем влево и поворачиваем налево и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==2&&dir==2&&CL.Map[Mathf.RoundToInt (xhead),Mathf.RoundToInt (yhead)+1]!=0){//если едем влево и поворачиваем направо и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==0&&dir==3&&CL.Map[Mathf.RoundToInt (xhead),Mathf.RoundToInt (yhead)+1]!=0){//если едем вправо и поворачиваем налево и там не пусто
				WooF ();
				return -2;
			}
			if (GD.MemCardsMove [pivot]==2&&dir==3&&CL.Map[Mathf.RoundToInt (xhead),Mathf.RoundToInt (yhead)-1]!=0){//если едем вправо и поворачиваем направо и там не пусто
				WooF ();
				return -2;
			}


			Debug.Log (current_vision [xhead_r_w, yhead_r_w]+" "+pivot+" "+GD.MemCardsMove [pivot]);
			if (current_vision [xhead_r_w, yhead_r_w] != 90&&current_vision [xhead_r_w, yhead_r_w] != 91&&current_vision [xhead_r_w, yhead_r_w] != 0 && (pivot == -1 || (pivot > -1 && GD.MemCardsMove [pivot] == 1))) {//если перед носом змеи нечто отличное от травы, то останавливаем игру
				WooF ();
				return -2;
			}
			if (pivot >= 0) {
				 str = "";		
				//Debug.Log ("dir => " + dir);
				for (int y = 4; y >= 0; y--) {
					for (int x = 0; x < 5; x++) {
						str += current_vision [x, y];						
					}
//					Debug.Log (str);
					str = "";
				}
//				Debug.Log ("**********");
//				Debug.Log ("pivot= " + pivot + " return= " + GD.MemCardsMove [pivot]);
				return GD.MemCardsMove [pivot]; //предполагаем что имеется массив с номерами змей
//				//и соответствующими номерам карточек значениями поворота
			}
			return -1;
		}
		return -1;
	}

	void WooF ()
	{
		Crusher crush = Crusher.getInstance ();
		CurrLevel CL = CurrLevel.getInstance ();
		System.Threading.Thread.Sleep(1000);
		for (int i = 0; i < crush.Length; i++) {
			crush.Objects [i].transform.position= new Vector3(crush.Objects [i].transform.position.x,crush.Objects [i].transform.position.y, -100);
		}
		WinFail.SetActive(true);
		CL.play = false;
		int fin = CL.Map [Mathf.RoundToInt (crush.Objects [0].transform.position.x), Mathf.RoundToInt (crush.Objects [0].transform.position.y)];
		switch (fin) {

		case 91: 
			WF.text ="Win";
			break;
		case 90: 
			WF.text ="Renegade";
			break;
		default: 
			WF.text ="Fail";
			break;
		}
		//but.GetComponentInChildren<Text>().text ="Play x2";
		//				Debug.Log (xhead_r_w + " " + yhead_r_w + " " + current_vision [xhead_r_w, yhead_r_w] + " " + pivot + " " + GD.MemCardsMove [pivot]);
		//Debug.Log("return -1 ");
		string str;
		str = "";		
		//Debug.Log ("dir => " + dir);
		for (int y = 4; y >= 0; y--) {
			for (int x = 0; x < 5; x++) {
				//str += current_vision [x, y];						
			}
			//Debug.Log (str);
			str = "";
		}

	}

	void MoveSnake (int dest, int qtt) //dest куда движемся, qtt количество метров
	{
		if (dest == -2) {
			return;
		}
		Crusher crush = Crusher.getInstance ();
		cam.transform.position = new Vector3 (crush.Objects [0].transform.position.x, crush.Objects [0].transform.position.y, cam.transform.position.z);
		//система координат относительно положения змеи, т.к. концепция карт памяти с алгоритмами предполагает именно такую систему координат
		//dest - куда змея должна передвинуться
		//direction - абсолютное положение готовы змеи


		//0 toleft
		//1 toforward
		//2 toright

		//начинаем с головы
		if (dest == -1) {
			//Debug.Log ("Не знаю куда двигаться, буду двигаться прямо");
			dest = 1;
		}
		if (crush.TargetPosition [0].z == -1000) {//Если позиция нулевого элемента по зэт = -1000, значит мы дошли до целевой точки 
			//и надо по новой считать целевую точку
			crush.TargetPosition [0].z = 0; //Если мы считаем новую целевую точку, значит мы должны начинать движение
			//Debug.Log("Считаем целевую точку");
			crush.CurrPosition [0] = crush.Objects [0].transform.position;//сохраняем текущее положение для lerp
			crush.CurrAngle [0] = crush.Objects [0].transform.eulerAngles;
			//Debug.Log("Текущая позиция головы ="+crush.CurrPosition [0]);
			switch (dest) {
			case 2:
				switch (crush.Direction [0]) {
				case "toright":
					//Debug.Log("Если нам надо двигаться направо и голова повернута направо");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y - qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "todown";
					break;
				case "toleft":
					//Debug.Log("Если нам надо двигаться направо и голова повернута налево");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y + qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toup";
					break;
				case "toup":
					//Debug.Log("Если нам надо двигаться направо и голова повернута вверх");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x + qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toright";
					break;
				case "todown":
					//Debug.Log("Если нам надо двигаться направо и голова повернута вниз");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x - qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toleft";
					break;
				}
				crush.TargetAngle [0].z = crush.Objects [0].transform.eulerAngles.z - 90; 

				//crush.Objects [0].transform.rotation.eulerAngles.z=0;
				///crush.TargetPosition[0].
				//Mathf.RoundToInt (this.transform.rotation.eulerAngles.z) == 0) {
				//	this.transform.Rotate (0, 0, -90);
				break;

			
			case 0:
				switch (crush.Direction [0]) {
				case "toright":
					//Debug.Log("Если нам надо двигаться налево и голова повернута направо");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y + qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toup";
					break;
				case "toleft":
					//Debug.Log("Если нам надо двигаться налево и голова повернута налево");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y - qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "todown";
					break;
				case "toup":
					//Debug.Log("Если нам надо двигаться налево и голова повернута вверх");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x - qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toleft";
					break;
				case "todown":
					//Debug.Log("Если нам надо двигаться налево и голова повернута вниз");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x + qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toright";
					break;
				}
				crush.TargetAngle [0].z = crush.Objects [0].transform.eulerAngles.z + 90;
				break;

			case 1:
				switch (crush.Direction [0]) {
				case "toright":
					//Debug.Log("Если нам надо двигаться прямо и голова повернута направо");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x + qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toright";
					break;
				case "toleft":
					//Debug.Log("Если нам надо двигаться прямо и голова повернута налево");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x - qtt;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toleft";
					break;
				case "toup":
					//Debug.Log("Если нам надо двигаться прямо и голова повернута вверх");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y + qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "toup";
					break;
				case "todown":
					//Debug.Log("Если нам надо двигаться прямо и голова повернута вниз");
					crush.TargetPosition [0].x = crush.Objects [0].transform.position.x;
					crush.TargetPosition [0].y = crush.Objects [0].transform.position.y - qtt;
//					Debug.Log ("dest " + dest + " crush.Direction [0] " + crush.Direction [0]);
					crush.Direction [0] = "todown";
					break;
				}
				//Debug.Log (crush.TargetAngle [0].z +" "+ crush.Objects[0].transform.eulerAngles.z);
				crush.TargetAngle [0].z = crush.Objects [0].transform.eulerAngles.z;
				break;
			
			}

			//наметки на прыжки и кусание

//			if (dest == "tojumpright") {
//
//			}
//			if (dest == "tojumpleft") {
//
//			}
//			if (dest == "tojumpforward") {
//
//			}
//			if (dest == "toeat") {
//
//			}

			//за головой подтягиваем остальное тело
			for (int i = 1; i < (crush.Length); i++) {
				crush.CurrPosition [i] = crush.Objects [i].transform.position;
				crush.CurrAngle [i] = crush.Objects [i].transform.eulerAngles;
				//Debug.Log (crush.CurrAngle [i]);
				//Debug.Log("Текущая позиция элемента тела "+i+" ="+crush.CurrPosition [i]);
				if (crush.CurrPosition [i].x != crush.CurrPosition [i - 1].x || crush.CurrPosition [i].y != crush.CurrPosition [i - 1].y) {
					crush.TargetPosition [i] = crush.CurrPosition [i - 1];
					crush.TargetAngle [i] = crush.CurrAngle [i - 1];
					//Debug.Log("Целевая точка элемента тела "+i+" ="+crush.TargetPosition [i]);
				}

				if (i == 1 && (crush.TargetAngle [0].z != crush.CurrAngle [0].z)) {
//					Debug.Log ("!!!");
					crush.TargetPosition [i].x = crush.CurrPosition [i].x + (crush.TargetPosition [i - 1].x - crush.CurrPosition [i].x) / 2;
					crush.TargetPosition [i].y = crush.CurrPosition [i].y + (crush.TargetPosition [i - 1].y - crush.CurrPosition [i].y) / 2;
					crush.TargetAngle [i].z = crush.CurrAngle [i].z + (crush.TargetAngle [i - 1].z - crush.CurrAngle [i].z) / 2;
				}
			}
		}
		if (crush.TargetPosition [0].z != -1000) {//если зэт не равен -1000, значит считаем что движение не завершено
			//Debug.Log("crush.CurrPosition [0].x "+crush.Objects[0].transform.position.x+" crush.TargetPosition [0].x "+crush.TargetPosition [0].x);
			//Debug.Log("crush.CurrPosition [0].y "+crush.Objects[0].transform.position.y+" crush.TargetPosition [0].y "+crush.TargetPosition [0].y);

			//Debug.Log("crush.time "+crush.time+" crush.speed "+crush.speed);
			for (int i = 0; i < (crush.Length); i++) {//для каждого элемента змеи делаем движение 
				
				//Debug.Log("Движение элемента "+i+" позиция до: "+crush.CurrPosition [i]);
				crush.Objects [i].transform.position = Vector3.Lerp (new Vector3 (crush.CurrPosition [i].x, crush.CurrPosition [i].y, 0), new Vector3 (crush.TargetPosition [i].x, crush.TargetPosition [i].y, 0), crush.time * crush.speed);
				crush.Objects [i].transform.eulerAngles = new Vector3 (0, 0, Mathf.LerpAngle (crush.CurrAngle [i].z, crush.TargetAngle [i].z, crush.time * crush.speed));
				//Debug.Log("Движение элемента "+i+" позиция после: "+crush.CurrPosition [i]);
			}
			crush.time += Time.deltaTime;
		}
		if (crush.Objects [0].transform.position.x == crush.TargetPosition [0].x && crush.Objects [0].transform.position.y == crush.TargetPosition [0].y) {
			//Debug.Log ("**************************************************crush.TargetPosition [0].z " + crush.TargetPosition [0].z);
			CurrLevel CL = CurrLevel.getInstance ();
			//CL.play=false;
			crush.TargetPosition [0].z = -1000;
			crush.time = 0;

		}
	}
























}
