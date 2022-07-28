using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeiceMST {

	public BaseObject[] peiceMST;
	string getPath = "Peices/";

	int maxMSTSize = 17;

	public PeiceMST(){
		peiceMST = new BaseObject[maxMSTSize];
		//全データ読み込み
		try{
			int cnt =0;
			while(true){
				if(cnt==maxMSTSize-1){
					break;
				}else{
					peiceMST[cnt] = Resources.Load<BaseObject>(getPath + "peice" +cnt.ToString());
				}
				cnt++;
				
			}	
		}catch(UnityException e){
			Debug.Log("[Debug]:データ読み込みエラー");
			Debug.Log("[Debug]:" + e);
		}	
	}
	public BaseObject getBaseObject(int num){
		//チェックは呼び出し元で行う
		if(num>maxMSTSize){
			Debug.Log("[Debug]:不正なデータベースアクセスです");
			return peiceMST[0];
		}
		return peiceMST[num];
	}

	public string getPeiceName(int num){
		return peiceMST[num].PeiceName;
	}

	public int getRank(int num){
		return peiceMST[num].Rank;
	} 



}
