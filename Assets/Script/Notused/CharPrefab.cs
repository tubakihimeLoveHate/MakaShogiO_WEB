using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPrefab {
	public int id;
	public int rank;
	public int rear;
	public int efectnumber;
	public string name;
	public string rub1;
	public string rub2;
	//public int moveN,moveNE,moveE,moveSE,moveS,moveSW,moveW,moveNW;
	public int[] move = new int[8];
	public string efect;
	public ET Type;
	public MT MType;
	//public bool efect_onoff = false;
	//public bool bench_onoff = false;//onならtrue offならfalse

	//efectType
	public enum ET{
		Trap,	//取られた時
		Move,	//動く時
		OneTime,	//zoneに入って１回だけ発動
		change,		//zone二入った時コマ自体が変化・基本的に効果はなくなる
		movechange,	//移動距離だけが変化する
		Atack,		//相手コマを取る時
		Every,		//毎自ターンの始め
		None		//効果なし
	}

	public enum MT{
		front,
		straght
	}

	public CharPrefab(int i,int r,int l,int en ,string n,string r1,string r2,int N,int NE,int E,int SE,int S,int SW,int W,int NW,string e,ET t,MT MT){
		id = i;
		rank = r;
		rear = l;
		efectnumber = en;
		name = n;
		rub1 = r1;
		rub2 = r2;
		move[0]  = N;
		move[1] = NE;
		move[2] = E;
		move[3] = SE;
		move[4] = S;
		move[5] = SW;
		move[6] = W;
		move[7] = NW;
		efect = e;
		Type = t;
		MType = MT;

	}


}
