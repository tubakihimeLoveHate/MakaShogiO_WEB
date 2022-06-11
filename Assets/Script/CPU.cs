using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPU  {

	//まだ駒がベンチにある状態には対応できていない
	int[,] isKoma;
	int rand;

	List<int> vlen = new List<int>();
	List<int> hlen = new List<int>();

	//BattleManagerでCPUのインスタンスを作成する
	//APIのように使いたいので全て返り値が存在する

	//別に配列でもいいかも、ランクごとの評価点＜ランク,評価点＞
	public static  readonly Dictionary<int,int> dictionary　= new Dictionary<int, int>(){
	{1,20},{2,40},{3,60},{4,80},{5,100}
	};

	public int SelectEnemy(Button[] button,string tag){
		bool dicied = true;
		while(dicied){
			rand = Random.Range(0,button.Length);
			if(button[rand].tag == tag){
				dicied = false;
			}
		}
		return rand;
		
	}


	/// <summary>
	/// 盤面から打つ駒を選出
	/// </summary>
	/// <param name="isKoma">存在チェック</param>
	/// <param name="peice">敵のランダムな駒</param>
	/// <returns></returns>
	public string SelectKoma(int[,] isKoma,PeiceStatus peice){

		//現在の配置状況をコピー
		this.isKoma = isKoma;
		
		int v = peice.v;
		int h = peice.h;
		bool permissionFlg = true;
		int t = -1;
		//動ける場所があるかどうかチェック(馬のような場合どうするか)
		if(peice.status.Marchingtype == BaseObject.MT.front){//動きかたが普通の場合
			for(int i = 0;i<8;i++){	//方角
						//p2 = p;
						if(peice.status.move[i]==0)	continue;
						//Debug.Log("一つの方向");
						for(int o = 1;o<peice.status.move[i]+1;o++){					//1方向の移動範囲
							try{//permissionflg共通変数にする？
								if(i==0) permissionFlg = VectorCheck(peice.v-o*t,peice.h);
								if(i==1) permissionFlg = VectorCheck(peice.v-o*t,peice.h+o*t);
								if(i==2) permissionFlg = VectorCheck(peice.v,peice.h+o*t);
								if(i==3) permissionFlg = VectorCheck(peice.v+o*t,peice.h+o*t);
								if(i==4) permissionFlg = VectorCheck(peice.v+o*t,peice.h);
								if(i==5) permissionFlg = VectorCheck(peice.v+o*t,peice.h-o*t);
								if(i==6) permissionFlg = VectorCheck(peice.v,peice.h-o*t);
								if(i==7) permissionFlg = VectorCheck(peice.v-o*t,peice.h-o*t);

								if(permissionFlg == false)break;

							}catch{
								//Debug.Log("index範囲外のため例外処理");
								continue;
							}
						}
					}
		}else{//ジャンプ系
			for(int i = 0;i <8;i++){//方がく
						//p2 = p;
						
						if(peice.status.move[i] == 0) continue;//Debug.Log("距離が０のため次の方角へ");
							try{		
								if(i == 0) if(VectorCheck(peice.v-peice.status.move[i]*t,peice.h)){};
								if(i == 1) if(VectorCheck(peice.v-peice.status.move[i]*t,peice.h+t)){};
								if(i == 2) if(VectorCheck(peice.v,peice.h+peice.status.move[i]*t)){};
								if(i == 3) if(VectorCheck(peice.v+peice.status.move[i]*t,peice.h+t)){};//動きが１つの方向のみになる
								if(i == 4) if(VectorCheck(peice.v+peice.status.move[i]*t,peice.h)){};
								if(i == 5) if(VectorCheck(peice.v+peice.status.move[i]*t,peice.h-t)){};
								if(i == 6) if(VectorCheck(peice.v,peice.h-peice.status.move[i]*t)){};
								if(i==7) if(VectorCheck(peice.v-peice.status.move[i]*t,peice.h-t)){};
							}catch{
								//Debug.Log("index範囲外のため例外処理");
								continue;
							}

					}

		}

		//よければKomasの番号返す
		
		return MoveKoma();
	}

	public bool VectorCheck(int v,int h){

		bool permisson = true;

		if(isKoma[v,h] == 0|| isKoma[v,h] == 1){
			//okFLG[v,h] = 1;
			vlen.Add(v);
			hlen.Add(h);
		}else{
			//okFLG[v,h] = 0;
			permisson = false;
		}
				
		
		return permisson;
	}

	//選択した駒がどこに動くか決定する
	public string MoveKoma(){
		string returnStr = "";
		int r = Random.Range(0,vlen.Count);
		int v = this.vlen[r];
		int h = this.hlen[r];

		returnStr = v.ToString() + "," + h.ToString() + "," + rand.ToString();
		return returnStr;
	}
	
	
}
