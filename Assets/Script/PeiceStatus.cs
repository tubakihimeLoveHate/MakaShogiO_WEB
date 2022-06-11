using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 駒の対戦時、保持ステータス(駒にアタッチ)
/// </summary>
public class PeiceStatus : MonoBehaviour {

	//<駒詳細画面関係>


	//public int id;

	public BaseObject status;
	public int[] puluesMove = new int[8];

	public bool isEfect = false;
	public bool isBench = false;

	//現在地
	public int h;
	public int v;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		
	}

	//ステータスセット
	//BaseObjectはバトルマネージャーがPeiceMSTインスタンスからもらう
	public void Initial (BaseObject baseObj) {
		status = (BaseObject)GameObject.Instantiate(baseObj);
		Text text = gameObject.GetComponentInChildren<Text>();
		text.text = status.PeiceName;

		if(isEfect){
			text.color = Color.red;
		}
		//detailView = _detailView.GetComponent<DetailView>();
	}

	//public void setDetail (DetailView detail) {
	//	this.detailView = detail;
		
	//}

	public void Move_Pulus (int[] m) {

		for (int y = 0; y < 8; y++) {
			status.move[y] = status.move[y] + m[y];
			if (status.move[y] > 8)　 status.move[y] = 8;
		}
		status.EfectNumber = status.Id;
		status.EfectType = BaseObject.ET.None;
		isEfect = true;
	}
}