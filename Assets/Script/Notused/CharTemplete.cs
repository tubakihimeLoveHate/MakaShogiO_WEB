using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharTemplete :MonoBehaviour{

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
	public CharPrefab.ET Type;
	public CharPrefab.MT MType;
	public bool efect_onoff = false;
	public bool bench_onoff = false;//onならtrue offならfalse

	//場面に存在するかどうか
	public int h;
	public int v;

	

	public CharTemplete(int i,int r,int l,int en ,string n,string r1,string r2,int N,int NE,int E,int SE,int S,int SW,int W,int NW,string e,CharPrefab.ET t,CharPrefab.MT MT){
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


	//駒にステータスを付与
	public void InitStatus(CharPrefab t){
		this.id = t.id;
		this.rank = t.rank;
		this.rear = t.rear;
		this.efectnumber = t.efectnumber;
		this.name = t.name;
		this.rub1 = t.rub1;
		this.rub2 = t.rub2;
		this.move = t.move;
		this.efect = t.efect;
		this.Type = t.Type;
		this.MType = t.MType;
		efect_onoff = false;
		h = -1;
		v = -1;
	}

	public void Move_skills(int[] m){

		for(int y = 0;y<8;y++){
			this.move[y] = this.move[y] + m[y];
			if(move[y]>8)　move[y] = 8;
		}
		efectnumber=id;
		Type = CharPrefab.ET.None;
		efect_onoff = true;
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		
	}

}
