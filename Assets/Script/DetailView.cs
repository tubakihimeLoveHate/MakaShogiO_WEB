using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetailView :MonoBehaviour{

	Vector3 inPosition = new Vector3(0,0,0);

	//public Button Button_Detail;
	public Text[] detailUI = new Text[7];

	public Button whiteChange,redChenge;
	//white = true red = false 
	public bool wORr;

	//private PeiceStatus peiceCache;

	PeiceMST peicemst;

	public Text[] moveUI = new Text[8];

	//public bool charStatusModeFlg1 = false;
	///public bool charStatusModeFlg2 = false;
	//public float secondery = 0;

	// Use this for initialization
	void Start () {
		//transform.parent.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		/* 
		if (charStatusModeFlg1) {
			//詳細UIをtrueして表示
			secondery += Time.deltaTime;
			if (secondery >= 1.0) {
				charStatusModeFlg2 = true;
				charStatusModeFlg1 = false;
			}
		}
		
		if (charStatusModeFlg2) {
			DetailIn ();
			secondery = 0;
			charStatusModeFlg2 = false;
		}*/
		/* 
		if (charStatusModeFlg1) {
			//詳細UIをtrueして表示
			secondery += Time.deltaTime;
			if (secondery >= 1.0) {
				charStatusModeFlg2 = true;
				charStatusModeFlg1 = false;
			}
			//Debug.Log(charStatusModeFlg2);
		}
		
		if (charStatusModeFlg2) {
			DetailIn ();
			secondery = 0;
			charStatusModeFlg2 = false;
		}
		*/
		
	}

	//ここが最初に実行されるように
	//デッキ編集時でも使えるように
	public void SwitchStatusView(PeiceStatus charT){
		//LogUtility.Logs("start SwichStatusView");
		//PeiceStatus charsCache = charT;
		//charStatusModeFlg1 = true;
		//secondery = 0;
		DetailIn();
		wORr = charT.isEfect;
		//int p;

		//p = charsCache.id;

		//ここで詳細UIの内容変えるTODO:↓いらないんじゃね？
		//PeiceStatusTextSet(peice);
		
		//Debug.Log("Longtappp");
		//whiteChange.onClick.AddListener(()=> ChangeText(charsCache));
		//redChenge.onClick.AddListener(()=> ChangeText(charsCache));
		/* 
		if(whiteChange.interactable)ChangeText();
		else ChangeEfect_white();
*/
		if(charT.isEfect){
			charT.status = peicemst.getBaseObject(charT.status.EfectNumber);
			detailUI[5].color = Color.red;
			//redbutton無効
			redChenge.interactable = false;
			//whitebutton有効
			whiteChange.interactable = true;
		}else{
			detailUI[5].color = Color.black;
			//whitebutton無効
			whiteChange.interactable = false;
			//redbutton有効
			redChenge.interactable = true;
		}

		detailUI[0].text = charT.status.Rub1;
		detailUI[1].text = charT.status.Rub2;
		detailUI[2].text = charT.status.Rank.ToString();
		detailUI[3].text = charT.status.Efect;
		detailUI[4].text = charT.status.Rear.ToString();
		detailUI[5].text = charT.status.PeiceName;
		for(int i=0;i<8;i++){
			if(charT.status.move[i] == 0)moveUI[i].text = "";
			else if(charT.status.move[i] == 8)moveUI[i].text = "∞";
			else moveUI[i].text = charT.status.move[i].ToString();
		}
		
	}


	//public void ChangeText(PeiceStatus peice){
		
		

		
	//}
 
	//public void WhiteTextView(){
		
	//	if(charsCache.isEfect){
	//		charsCache.status = peicemst.getBaseObject(charsCache.status.EfectNumber);
	//	}

		//PeiceStatusTextSet();
		
	//}

//ホワイト状態のテキストを呼ぶ機能と現在の状況を呼ぶテキストは同じ役割
	//public void PeiceStatusTextSet(PeiceStatus peiceChahe){
		//LogUtility.Logs("start PeiceStatusTextSet");
	//}
 /* 
	void RedTextView(){
		
		detailUI[0].text = Cahr.rub1;
		detailUI[1].text = cc.rub2;
		detailUI[2].text = cc.rank.ToString();
		detailUI[4].text = cc.rear.ToString();
		detailUI[5].text = cc.name;
		detailUI[3].text = cc.efect;
		for(int i=0;i<8;i++){
			if(cc.move[i] == 0)moveUI[i].text = "";
			else if(charsCache.move[i] == 8)moveUI[i].text = "∞";
		    else moveUI[i].text = cc.move[i].ToString();
		}
	}
*/
	public void OnDetailOut(){
		//charStatusModeFlg1 = false;
		//charStatusModeFlg2 = false;
		
		transform.parent.transform.localPosition = new Vector3(500,0,0);
		gameObject.SetActive(false);
		OnDeltailOut_reset();
		whiteChange.interactable = true;
		redChenge.interactable = false;
		
	}

	public void DetailIn(){
		transform.parent.transform.localPosition = inPosition;
		gameObject.SetActive(true);
		//secondery = 0;
		//charStatusModeFlg1 = false;
	}


	public void OnDeltailOut_reset(){
		whiteChange.onClick.RemoveAllListeners();
		redChenge.onClick.RemoveAllListeners();
	}

}
