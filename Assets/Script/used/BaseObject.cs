using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseObject : ScriptableObject {

	[SerializeField]
	int id,rank,rear,efectNumber;

	[SerializeField]
	string peiceName,rub1,rub2,efect;

	public int[] move = new int[8];

	[SerializeField]
	ET efectType;
	[SerializeField]
	MT marchingType;

	public enum ET{
		Trap,
		Move,
		OneTime,
		Change,
		MoveChange,
		Atack,
		Every,
		None
	} 

	public enum MT {
		front,
		stright
		
	}

	public int Id{get{ return id;}}
	public int Rank{get{return rank;}}
	public int Rear{get{return rear;}}
	public int EfectNumber{get{return efectNumber;}set{this.efectNumber = value;}}
	public string PeiceName{get{return peiceName;}}
	public string Rub1{get{return rub1;}}
	public string Rub2{get{return rub2;}}
	public string Efect{get{return efect;}}
	public int getMove(int idx){return move[idx];}
	public ET EfectType{get{return efectType;}set{this.efectType = value;}}
	public MT  Marchingtype{get{return marchingType;}}


}
