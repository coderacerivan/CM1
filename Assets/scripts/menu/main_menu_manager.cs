using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class main_menu_manager : MonoBehaviour
{
	public Canvas canva;
	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt ("MemcardNumber", 0);
		GameData GD = GameData.getInstance ();
		GD.LoadGame ();
		CurrLevel CL = CurrLevel.getInstance ();
		CL.play = false;
		GameSettings GS = GameSettings.getInstance ();
		GS.Scale = Screen.width / 1920;
		canva.scaleFactor = GS.Scale;

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void AppExit ()
	{
		Application.Quit ();
	}

	public void AppPref ()
	{
		SceneManager.LoadScene (5);
	}

	public void AppStart ()
	{
		SceneManager.LoadScene (1);
	}
}

//Класс для хранения игровых данных
public class GameData
{
	private static GameData instance;
	//Объявляем как синглтон
	private GameData ()
	{
	}

	public static GameData getInstance ()
	{
		if (instance == null)
			instance = new GameData ();
		return instance;
	}

	public int QActiveProfiles;
	//Количество активных профилей сохранения
	public int Money;
	//Деньге
	public int LevelQuantity;
	//Количество уровней
	public int[,] LevelScore = new int[201, 5];
	//0 - мой скор, 1- макс. скор, 2 - бронза, 3 - серебро, 4 - золото
	public int[,] LevelLimitations = new int[201, 2];
	//0 - время, если есть, то количество секунд, 1 - ограничение по начальной длине змеи, если есть, количество блоков
	public int[] LevelSetting = new int[201];
	//число - номер набора тайлов (сеттинг - поле, лес, горы, лава и пр)
	public int[,,] LevelMaps = new int[201, 101, 101];
	//Карты уровней, считаем что уровней 200, размером 100х100
	public int[,,,] MemCards = new int[20, 4, 10, 10];
	//максимальное количество карточек, 4 варианта повернутости карточки, максимальные размеры
	public int[] MemCardsMove = new int[20];
	public int QMemCards;
	public int[] Bonuses = new int[10];
	//каждый номер - остаток определенного бонуса в инвентаре, который можно использовать
	public int CrusherLength;
//длина змеи
	public int[] CrusherStructure = new int[101];
//состав змеи при том что максимальная длина змеи - 100 кусков
	public void SaveGame ()
	{
		GameData GD = GameData.getInstance ();
		GameDataSer GDS = new GameDataSer ();
		GDS.Money = GD.Money;
		GDS.QActiveProfiles = GD.QActiveProfiles;
		GDS.LevelQuantity = GD.LevelQuantity;
		GDS.LevelScore = GD.LevelScore;
		GDS.LevelLimitations = GD.LevelLimitations;
		GDS.LevelSetting = GD.LevelSetting;
		GDS.LevelMaps = GD.LevelMaps;
		GDS.MemCards = GD.MemCards;
		GDS.MemCardsMove = GD.MemCardsMove;
		GDS.QMemCards = GD.QMemCards;
		GDS.Bonuses = GD.Bonuses;
		GDS.CrusherLength = GD.CrusherLength;
		GDS.CrusherStructure = GD.CrusherStructure;
		BinaryFormatter BF = new BinaryFormatter ();
		using (Stream fs = new FileStream ("GameData", FileMode.Create,
			                   FileAccess.Write, FileShare.None)) {
			BF.Serialize (fs, GDS);
		}
	}

	public void LoadGame ()
	{
		GameData GD = GameData.getInstance ();
		GameDataSer GDS = new GameDataSer ();
		BinaryFormatter BF = new BinaryFormatter ();
		using (Stream fs = new FileStream ("GameData", FileMode.OpenOrCreate)) {
			GDS = (GameDataSer)BF.Deserialize (fs);
		}
		GD.Money = GDS.Money;
		GD.QActiveProfiles = GDS.QActiveProfiles;
		GD.LevelQuantity = GDS.LevelQuantity;
		GD.LevelScore = GDS.LevelScore;
		GD.LevelLimitations = GDS.LevelLimitations;
		GD.LevelSetting = GDS.LevelSetting;
		GD.LevelMaps = GDS.LevelMaps;
		GD.MemCards = GDS.MemCards;
//		for (int i=0;i<20;i++){
//			if ((GDS.MemCardsMove [i] != 0) || (GDS.MemCardsMove [i] != 1) || (GDS.MemCardsMove [i] != 2)) {
//				GD.MemCardsMove [i] = 1;
//			
//			} else {
//				GD.MemCardsMove[i] = GDS.MemCardsMove[i];
//			}
//			//GD.MemCardsMove [i] = 1;
//			//			for(int j=0;j<101;j++){
//			//				for(int k=0;k<101;k++){
//			//					if (GD.LevelMaps[i,j,k]!=0){
//			Debug.Log (GD.MemCardsMove[i]);//,j,k]);
//			//					}
//			//				}
//			//			}
//		}
		GD.MemCardsMove = GDS.MemCardsMove;
		GD.QMemCards = GDS.QMemCards;
		GD.Bonuses = GDS.Bonuses;
		GD.CrusherLength = GDS.CrusherLength;
		GD.CrusherStructure = GDS.CrusherStructure;
	}


	[Serializable]
	public class GameDataSer
	{
		public int QActiveProfiles;
		//Количество активных профилей сохранения
		public int Money;
		//Деньге
		public int LevelQuantity;
		//Количество уровней
		public int[,] LevelScore = new int[201, 5];
		//0 - мой скор, 1- макс. скор, 2 - бронза, 3 - серебро, 4 - золото
		public int[,] LevelLimitations = new int[201, 2];
		//0 - время, если есть, то количество секунд, 1 - ограничение по начальной длине змеи, если есть, количество блоков
		public int[] LevelSetting = new int[201];
		//число - номер набора тайлов (сеттинг - поле, лес, горы, лава и пр)
		public int[,,] LevelMaps = new int[201, 101, 101];
		//Карты уровней, считаем что уровней 200, размером 100х100
		public int[,,,] MemCards = new int[20, 4, 10, 10];
		public int[] MemCardsMove = new int[20];
		public int QMemCards;
		//максимальное количество карточек, 4 варианта повернутости карточки, максимальные размеры
		public int[] Bonuses = new int[10];
		//каждый номер - остаток определенного бонуса в инвентаре, который можно использовать
		public int CrusherLength;
//длина змеи
		public int[] CrusherStructure = new int[101];
//состав змеи при том что максимальная длина змеи - 100 кусков
	}
}





public class Crusher
{
	private static Crusher instance;
	//Объявляем как синглтон
	private Crusher ()
	{
	}

	public static Crusher getInstance ()
	{
		if (instance == null)
			instance = new Crusher ();
		return instance;
	}

	public int Length;
	public int[] Structure = new int[101];

	public Vector3[] CurrPosition = new Vector3[101];
	public Vector3[] CurrAngle = new Vector3[101];
	public Vector3[] TargetAngle = new Vector3[101];
	public Vector3[] TargetPosition = new Vector3[101];
	public GameObject[] Objects = new GameObject[101];
	public string Destiny;
	public string[] Direction = new string[101];
	public float time;
	public float speed;
}


public class CurrLevel
{
	private static CurrLevel instance;
	//Объявляем как синглтон
	private CurrLevel ()
	{
	}

	public static CurrLevel getInstance ()
	{
		if (instance == null)
			instance = new CurrLevel ();
		return instance;
	}

	public int[] Score = new int[5];
	//0 - мой скор, 1- макс. скор, 2 - бронза, 3 - серебро, 4 - золото
	public int[] Limitations = new int[2];
	//0 - время, если есть, то количество секунд, 1 - ограничение по начальной длине змеи, если есть, количество блоков
	public int[] Setting;
	//число - номер набора тайлов (сеттинг - поле, лес, горы, лава и пр)
	public int[,] Map = new int[ 101, 101];
	public bool play;
}
public class GameSettings
{
	private static GameSettings instance;
	//Объявляем как синглтон
	private GameSettings ()
	{
	}

	public static GameSettings getInstance ()
	{
		if (instance == null)
			instance = new GameSettings ();
		return instance;
	}

	public float Scale;
}