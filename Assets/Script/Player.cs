using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO:なぜstaticにしている？static classにはしなくてよい？
public class Player {

//プレイヤー基本情報
	public static string playerName  = "将棋仙人";

	public static int level = 1;
	public static string playerClass = "新米";
	public static int rate = 0;
	public static float XP=0;

	const float XPrate = 1.2f;

	public static float nextLevel = 50;

	
	public static PeiceMST peiceData = new PeiceMST();

	public static int[] myList = new int[16];
	public static int[] mydeck = new int[20]; 

	//全てのカードキャッシュ。これは保存する必要ない
	public static int[] classicDeck = new int[20];
	public static string[] deckname = new string[10];//ゆくゆく増やして行くつもり

	public static bool newAccount = true;

	//TODO:自駒ここから手動で変えることができるが、テスト時のみ。クラシックモードも変化してしまうため
	static int[] firstTable = {0,0,0,0,0,0,0,0,0,6,5,1,2,3,4,7,4,3,2,1};
	

	public  static bool FirstInit(){

		if (newAccount)
		{
			//新規駒所持リストの作成
			if (newAccount)
			{
				myList[0] += 9;
				myList[1] += 2;
				myList[2] += 2;
				myList[3] += 2;
				myList[4] += 2;
				myList[5] += 1;
				myList[6] += 1;
				myList[7] += 1;
				myList[8] += 1;
			}
			mydeck = firstTable;
			classicDeck = firstTable;


			deckname[0] = "スタンダート";
			newAccount = false;
		}
		return true;
		
	}

	//MyListに追加する処理
	public static void AddMyList(int id){
		myList[id]++;
	}


	public static void Levelup(float getXP){
		XP += getXP;
		if(XP>=nextLevel){
			level++;
			float n = nextLevel*XPrate;
			nextLevel = n;
		} 
	}

}
