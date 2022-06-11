using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

//プレイヤー基本情報
	public static string playerName  = "将棋仙人";

	public static int level = 1;
	public static string playerClass = "新米";
	public static int rate = 0;
	public static float XP=0;

	const float XPrate = 1.2f;

	public static float nextLevel = 50;

	

	//参照用
	//public static Deckmanager deck = new Deckmanager();
	public static PeiceMST peiceData = new PeiceMST();
	//TODO:デッキはidで管理すれば良い 

	//手持ち駒リスト
	//public　static List<CharPrefab> myList = new List<CharPrefab>();

	//0なら持ってない1以上ならその枚数所持している
	//idがキー
	public static int[] myList = new int[16];
	//バトル用デッキ1
	//public static List<CharPrefab> mydeck = new List<CharPrefab>();
	public static int[] mydeck = new int[20]; 

	//全てのカードキャッシュ。これは保存する必要ない
	//public static List<CharPrefab> allList = new List<CharPrefab>();
	//public static List<CharPrefab> classicDeck = new List<CharPrefab>();
	public static int[] classicDeck = new int[20];
	public static string[] deckname = new string[10];//ゆくゆく増やして行くつもり

	public static bool newAccount = true;

	//TODO:自駒ここから手動で変えることができるが、テスト時のみ。クラシックモードも変化してしまうため
	static int[] firstTable = {0,0,0,0,0,0,0,0,0,6,5,1,2,3,4,7,4,3,2,1};
	

	public  static bool firstInit(){

		//自分の所持リスト
		/* 
		for(int i=0;i<9;i++){
			AddMyList(0);
		}
		AddMyList(6);
		AddMyList(5);
		AddMyList(1);
		AddMyList(2);
		AddMyList(3);
		AddMyList(4);
		AddMyList(7);
		AddMyList(4);
		AddMyList(3);
		AddMyList(2);
		AddMyList(1);
		AddMyList(8);//玉は初期デッキに含めない
		for(int y = 9;y<30;y++){
			AddMyList(y);
		}*/
		//新規駒所持リストの作成
		if(newAccount){
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
		/* 
		foreach(int i in firstTable){
			mydeck.Add(deck.PrivateLoad(i));
			classicDeck.Add(deck.PrivateLoad(i));
		}*/
		mydeck = firstTable;
		classicDeck = firstTable;
		//玉は最初デッキに含まないが一覧にはある


		deckname[0] = "スタンダート";
		newAccount = false;
		
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
