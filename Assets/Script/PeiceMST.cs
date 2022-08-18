using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeiceMST {

	string getPath = "Peices/";

	public PeiceMST(){
		
	}
	public BaseObject getBaseObject(int num){
		//チェックは呼び出し元で行う
		BaseObject result = null;
		try
		{
			result = Resources.Load<BaseObject>(getPath + "peice" + num.ToString());
		}
		catch(UnityException e)
		{
			Debug.Log("[Debug]:データ読み込みエラー");
			Debug.Log("[Debug]:" + e);
		}
		return result;
	}

	public string getPeiceName(int num){
		string name = null;
		try
		{
			name = Resources.Load<BaseObject>(getPath + "peice" + num.ToString()).PeiceName;
		}
		catch (UnityException e)
		{
			Debug.Log("[Debug]:データ読み込みエラー");
			Debug.Log("[Debug]:" + e);
		}
		return name;
	}

	public int getRank(int num){
		int rank = 0;
		try
		{
			rank = Resources.Load<BaseObject>(getPath + "peice" + num.ToString()).Rank;
		}
		catch (UnityException e)
		{
			Debug.Log("[Debug]:データ読み込みエラー");
			Debug.Log("[Debug]:" + e);
		}
		return rank;
	} 



}
