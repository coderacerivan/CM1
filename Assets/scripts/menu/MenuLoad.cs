using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml.Serialization;
using System;


public static class GamePreferences
{
	public static int[,,] memcard1 = new int[21, 6, 6];
	//первое - номер карты памяти (1-20), второе и третье - ху
	public static int[,] levels1 = new int[201, 11];
	//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
	public static bool isactive1;
	//активен профиль или нет
	public static int money1;
	public static int score1;
	public static int[] advcards1 = new int[11];//расширения карт памяти
	public static int[] advsnake1 = new int[11];//расширения змеи

	public static int[,,] memcard2 = new int[21, 6, 6];
	//первое - номер карты памяти (1-20), второе и третье - ху
	public static int[,] levels2 = new int[201, 11];
	//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
	public static bool isactive2;
	//активен профиль или нет
	public static int money2;
	public static int score2;
	public static int[] advcards2 = new int[11];//расширения карт памяти
	public static int[] advsnake2 = new int[11];//расширения змеи

	public static int[,,] memcard3 = new int[21, 6, 6];
	//первое - номер карты памяти (1-20), второе и третье - ху
	public static int[,] levels3 = new int[201, 11];
	//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
	public static bool isactive3;
	//активен профиль или нет
	public static int money3;
	public static int score3;
	public static int[] advcards3 = new int[11];//расширения карт памяти
	public static int[] advsnake3 = new int[11];//расширения змеи

	//- в каждом змеепрофиле: активен, инфа о змее, т.е инфа о ее карточках (массив), +
	//- инфа о пройденных уровнях и прогрессе набранном в каждом из уровней (массив), +
	//- суммарная инфа о змее: сколько на счету денег (инт), тотал скор (инт), +
	//- инфа о купленных улучшениях мемкарт (стандарт (допустим: поворот, укус) - расширенный (прыжок, подкоп) - профессионал (ускорение)) +
	//- инфа о купленных улучшениях самой змеи (как перманентных, так и временных бустерах) +
	//(замысел в том что не достаточно купить только например бустер или только улучшение мемкарты, но соответственно надо купить и вторую составляющую
	//	и кроме того грамотно это задействовать в своей стратегии (наборе заготовленных мемкарт))



	[Serializable]
	public class GamePreferencesD
	{
		public GamePreferencesD() { }
		public int[,,] memcard1 = new int[21, 6, 6];
		//первое - номер карты памяти (1-20), второе и третье - ху
		public int[,] levels1 = new int[201, 11];
		//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
		public bool isactive1;
		//активен профиль или нет
		public int money1;
		public int score1;
		public int[] advcards1 = new int[11];//расширения карт памяти
		public int[] advsnake1 = new int[11];//расширения змеи

		public int[,,] memcard2 = new int[21, 6, 6];
		//первое - номер карты памяти (1-20), второе и третье - ху
		public int[,] levels2 = new int[201, 11];
		//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
		public bool isactive2;
		//активен профиль или нет
		public int money2;
		public int score2;
		public int[] advcards2 = new int[11];//расширения карт памяти
		public int[] advsnake2 = new int[11];//расширения змеи

		public int[,,] memcard3 = new int[21, 6, 6];
		//первое - номер карты памяти (1-20), второе и третье - ху
		public int[,] levels3 = new int[201, 11];
		//предположим что в игре может быть до 200 уровней)), в каждой ячейке до 10 значений статистики по каждому уровню
		public bool isactive3;
		//активен профиль или нет
		public int money3;
		public int score3;
		public int[] advcards3 = new int[11];//расширения карт памяти
		public int[] advsnake3 = new int[11];//расширения змеи

		//- в каждом змеепрофиле: активен, инфа о змее, т.е инфа о ее карточках (массив), +
		//- инфа о пройденных уровнях и прогрессе набранном в каждом из уровней (массив), +
		//- суммарная инфа о змее: сколько на счету денег (инт), тотал скор (инт), +
		//- инфа о купленных улучшениях мемкарт (стандарт (допустим: поворот, укус) - расширенный (прыжок, подкоп) - профессионал (ускорение)) +
		//- инфа о купленных улучшениях самой змеи (как перманентных, так и временных бустерах) +
		//(замысел в том что не достаточно купить только например бустер или только улучшение мемкарты, но соответственно надо купить и вторую составляющую
		//	и кроме того грамотно это задействовать в своей стратегии (наборе заготовленных мемкарт))
	}




	public class MenuLoad : MonoBehaviour
	{
		public GameObject profile1;
		public GameObject profile2;
		public GameObject profile3;
		// Use this for initialization
		void Start()
		{
			if (File.Exists("GameConfiguration") == false)
			{
				FileStream fs = File.Create("GameConfiguration");
				fs.Close();
				GamePreferences.money1 = 0;
				GamePreferences.money2 = 0;
				GamePreferences.money3 = 0;

				GamePreferences.score1 = 0;
				GamePreferences.score2 = 0;
				GamePreferences.score3 = 0;

				GamePreferences.isactive1 = false;
				GamePreferences.isactive2 = false;
				GamePreferences.isactive3 = false;

				for (int i = 0; i < 201; i++)
				{
					for (int j = 0; j < 11; j++)
					{
						GamePreferences.levels1[i, j] = 0;
						GamePreferences.levels2[i, j] = 0;
						GamePreferences.levels3[i, j] = 0;
					}
				}
				for (int i = 0; i < 21; i++)
				{
					for (int j = 0; j < 6; j++)
					{
						for (int k = 0; k < 6; k++)
						{
							GamePreferences.memcard1[i, j, k] = 0;
							GamePreferences.memcard2[i, j, k] = 0;
							GamePreferences.memcard3[i, j, k] = 0;
						}
					}
				}
				for (int i = 0; i < 11; i++)
				{
					GamePreferences.advcards1[i] = 0;
					GamePreferences.advsnake1[i] = 0;
					GamePreferences.advcards2[i] = 0;
					GamePreferences.advsnake2[i] = 0;
					GamePreferences.advcards3[i] = 0;
					GamePreferences.advsnake3[i] = 0;
				}


				GamePreferencesD GP = new GamePreferencesD();
				GP.advcards1 = GamePreferences.advcards1;
				GP.advcards2 = GamePreferences.advcards2;
				GP.advcards3 = GamePreferences.advcards3;
				GP.advsnake1 = GamePreferences.advsnake1;
				GP.advsnake2 = GamePreferences.advsnake2;
				GP.advsnake3 = GamePreferences.advsnake3;
				GP.isactive1 = GamePreferences.isactive1;
				GP.isactive2 = GamePreferences.isactive2;
				GP.isactive3 = GamePreferences.isactive3;
				GP.levels1 = GamePreferences.levels1;
				GP.levels2 = GamePreferences.levels2;
				GP.levels3 = GamePreferences.levels3;
				GP.memcard1 = GamePreferences.memcard1;
				GP.memcard2 = GamePreferences.memcard2;
				GP.memcard3 = GamePreferences.memcard3;
				GP.money1 = GamePreferences.money1;
				GP.money2 = GamePreferences.money2;
				GP.money3 = GamePreferences.money3;
				GP.score1 = GamePreferences.score1;
				GP.score2 = GamePreferences.score2;
				GP.score3 = GamePreferences.score3;


				//XmlSerializer formatter = new XmlSerializer(typeof(GamePreferencesD));


				using (FileStream fssave = new FileStream("GameConfiguration", FileMode.OpenOrCreate))
				{
					//formatter.Serialize(fssave, GP);
				}

				//string SaveString = JsonUtility.ToJson (GP);
				//Debug.Log (SaveString);
				//Debug.Log (GamePreferences.memcard1);
				//Debug.Log (GP.memcard1[1,2,3]);
				//StreamWriter fssave = new StreamWriter ("GameConfiguration");
				//fssave.Write (SaveString);
				//fssave.Close();
				//Bin
			}


			for (int i = 1; i < 4; i++)
			{
				int newprofile = PlayerPrefs.GetInt("profile" + i);
				if (newprofile == 1)
				{
					GameObject currprof = GameObject.Find("profile" + i);
					currprof.GetComponentInChildren<Text>().text = "Snake" + i;
				}
			}
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}
