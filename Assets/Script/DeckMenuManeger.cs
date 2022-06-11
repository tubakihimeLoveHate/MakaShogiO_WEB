using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//このクラスをGetCompormentすれば同じ値を共有可能
public class DeckMenuManeger : MonoBehaviour {

	PeiceMST peicemst;
	//Deckmanager deck;
	//これも何かにアタッチすればよくね？
	//Player player ;
	public static bool deckMenuIn =false;
	public Image topPage_Image;

	/// <summary>
	/// デッキ編集関係→他のクラスへ移行
	/// </summary>
	public Button[] Koma_model=new Button[20];//現在のデッキを反映するほう
	//public Image Deck_Select;
	public Image Deck_Change;
	public Image[] Decks = new Image[10];
	//public Transform changePanelPearent;
	public Text errorMessage;

	//<駒詳細画面関係>

	public GameObject detail;
	DetailView detailView;

	public GameObject Button_Detail;


	//GameObject plePanel;
	//public GameObject[] pagePanels;
	//public Text pages;
	//int maxPage;
	//int minPage = 1;
	
	//int nowPage = 0;
	//マイリストのコマ一覧
	public Button[] changeKoma; 

	//全カード一覧
	public Button[] allViewKoma;

	Button changeOneKoma;
	Image selectFrame;
	Image frame;
	Image hightLightFrame;
	//Image prefabHightLight;
	public Transform parentTransform;
	public GameObject pageParent;
	public GameObject pageParent2;
	public Transform contents1;
	public Transform contents2;
	bool intoSelect = false;//編成選択時アニメーション
	bool selectOn = false;//trueで駒選択中
	float frameTime = 0.2f;
	int changeSelectCatsh;//選択したコマの種類をキャッシュ

	bool editMode = true;

	bool createButton = false;

	public Button editButton;
	public Button allViewButton;

	//<駒詳細画面関係>
	//0:訓読み,1:音読み,2Lank,3EfectText,4rear,5name,6予備
	//public Text[] detailUI = new Text[7];
　　 //public Button whiteChange,redChenge;
	

	// Use this for initialization
	void Start () {
		//plePanel = Resources.Load<GameObject>("Plefab/MyListPanel");
		changeOneKoma = Resources.Load<Button>("Plefab/komachange");
		selectFrame = Resources.Load<Image>("Plefab/SelectFrame_Image");
		//frame = Resources.Load<Image>("DeckSelect_Image");
		hightLightFrame = Resources.Load<Image>("Plefab/HightLightKoma");
		//player = GetComponent<Player>();
		detailView = detail.GetComponent<DetailView>();
	}
	
	// Update is called once per frame
	void Update () {
		if(deckMenuIn){
			if(intoSelect){
				frame.fillAmount += 1.0f / frameTime * Time.deltaTime;
				if(frame.fillAmount == 1)intoSelect = false;
			}
		}
/* 
		if(detailView.charStatusModeFlg1){
				//詳細UIをtrueして表示
				detailView.secondery += Time.deltaTime;
				if(detailView.secondery >= 1.0){
					detailView.charStatusModeFlg2 = true;
					detailView.charStatusModeFlg1 = false;
				}	
			}
			//これはこっちで制御
			if(detailView.charStatusModeFlg2){
				Button_Detail.transform.localPosition = transform.localPosition;
				detailView.secondery = 0;
				detailView.charStatusModeFlg2 = false;
				}*/
		
	}



	public void ModeChange(string s){
		if(s == "Edit"){
			pageParent.SetActive(true);
			pageParent2.SetActive(false);
			editButton.enabled = false;
			allViewButton.enabled = true;
			//ボタン無効化処理
		}else{
			if(createButton){
			}else{
				AllView();
				createButton=true;
			}
			pageParent2.SetActive(true);
			pageParent.SetActive(false);
			editButton.enabled = true;
			allViewButton.enabled = false;
		}
	}

	//最初に来た時
	public void OnTabDeckChange(/*int i*/){
		Deck_Change.transform.localPosition = transform.localPosition;
		//page数計算
		//int pagenumber = Mathf.CeilToInt(Player.myList.Count/8);
		int remainder = Player.myList.Length;
		
		changeKoma = new Button[remainder];
		
		//バトル用デッキの表示 1ok
		//あとは詳細画面が見られるように
		for(int d = 0;d < Player.mydeck.Length;d++){
			int d2 = d;
			//Koma_model[d].GetComponent<PeiceStatus>().InitStatus(Player.mydeck[d]);
			Koma_model[d].GetComponent<PeiceStatus>().Initial(Player.peiceData.getBaseObject(d));
			PeiceStatus CT = Koma_model[d].GetComponent<PeiceStatus>();
			Koma_model[d].GetComponentInChildren<Text>().text = CT.status.PeiceName;
			Koma_model[d].onClick.AddListener(()=>OnChangeMyKoma(d2));
			
			SettingEventButton(Koma_model[d]);
		}
		EditView();
	}

	void EditView(){
		
		int remainder = Player.myList.Length;
		
		changeKoma = new Button[remainder];

		//１ページ作る処理
		

			for(int f =0;f<remainder;f++){
				//int hchash = h;//コマ種類の数
				int fchash = f;//ページ内での添字
				changeKoma[f] = Instantiate(changeOneKoma,contents1.transform);
				changeKoma[f].transform.SetParent(contents1.transform);
				changeKoma[f].transform.localScale = Vector3.one;
				changeKoma[f].GetComponent<PeiceStatus>().status = peicemst.getBaseObject(Player.myList[f]);
				changeKoma[f].GetComponentInChildren<Text>().text = peicemst.getPeiceName(Player.myList[f]);

				SettingEventButton(changeKoma[f]);
				
				changeKoma[f].onClick.AddListener(()=>OnFirstTapKoma(fchash));
		}
	}

	void AllView(){
		
		int remainder = Player.myList.Length;

		allViewKoma= new Button[remainder];

		//１ページ作る処理
		

			for(int f =0;f<remainder;f++){
				//int hchash = h;//コマ種類の数
				int fchash = f;//ページ内での添字
				allViewKoma[f] = Instantiate(changeOneKoma,contents2.transform);
				allViewKoma[f].transform.SetParent(contents2.transform);
				allViewKoma[f].transform.localScale = Vector3.one;
				allViewKoma[f].GetComponent<PeiceStatus>().status = peicemst.getBaseObject(f);
				allViewKoma[f].GetComponentInChildren<Text>().text = peicemst.getPeiceName(f);

				SettingEventButton(allViewKoma[f]);
				//触れるかどうかの処理
				//所持数処理
				//changeKoma[f].onClick.AddListener(()=>OnFirstTapKoma(fchash));
			}
	}

	void SettingEventButton(Button button){
	
		PeiceStatus CT = button.GetComponent<PeiceStatus>();
		//Debug.Log(CT.name);
		EventTrigger trigger = button.GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener((eventData)=> detailView.SwitchStatusView(CT));
		trigger.triggers.Add(entry);
	}

	

	//編成で駒を押した時の処理、選択アニメーション
	public void OnFirstTapKoma(int f){
		//detailView.charStatusModeFlg1 = false;
		detailView.OnDeltailOut_reset();
		PeiceStatus CP1 = changeKoma[f].GetComponent<PeiceStatus>();
		Image prefabHightLight;
	　　 changeSelectCatsh = f;
		errorMessage.text = "";
		//機能は実現できるが、前のフレームを消せばいいだけなので全て探す必要はない
		
		var clones = GameObject.FindGameObjectsWithTag("fram");
			foreach(var clone in clones){
				Destroy(clone);
			}


		//if(!frame)Destroy(frame);
		//Frame呼び出し
		frame = Instantiate(selectFrame,changeKoma[f].transform);
		//frame.color = new Color(0f,0f,0f,0f);
		frame.fillAmount = 0;
		frame.transform.SetParent(changeKoma[f].transform);
		frame.transform.localScale = new Vector3(1.9f,2.3f,0);
		
		frame.transform.localPosition = Vector3.zero;
		intoSelect = true;
		selectOn = true;//コマ選択状態

		//ハイライト呼び出し
		for (int n = 0;n<Koma_model.Length;n++)
		{
			PeiceStatus CP2 = Koma_model[n].GetComponent<PeiceStatus>();
			//Debug.Log(CP1.rank+":"+CP2.rank);
			if(CP2.status.Rank == peicemst.getRank(changeSelectCatsh)){
				prefabHightLight= Instantiate(hightLightFrame,parentTransform);
				prefabHightLight.transform.SetParent(parentTransform);
				//prefabHightLight.transform.localScale = new Vector3(1,1,1);
				prefabHightLight.transform.localPosition = Koma_model[n].transform.localPosition;
			}
		}


	}

	/// <summary>
	/// 変更先の駒を選択するとき　2
	/// </summary>
	/// <param name="dint">バトルデッキの添字</param>
	public void OnChangeMyKoma(int dint){
		//player.mydeckを変更する
		//detailView.charStatusModeFlg1 = false;
		detailView.OnDeltailOut_reset();
		int dcache  = dint; 
		if(selectOn){
			PeiceStatus PS = Koma_model[dint].GetComponent<PeiceStatus>();
			if(PS.status.Rank ==peicemst.getRank(changeSelectCatsh)){
			Player.mydeck[dint] = changeSelectCatsh;
			PS.status =  peicemst.getBaseObject(Player.mydeck[dint]);
			Koma_model[dint].GetComponentInChildren<Text>().text = peicemst.getPeiceName(Player.mydeck[dint]);
			Koma_model[dint].onClick.RemoveAllListeners();
			Koma_model[dint].onClick.AddListener(()=>OnFirstTapKoma(dcache));
			SettingEventButton(Koma_model[dint]);
			//Mylistの数を元に戻す（未実装）
			}else{
				errorMessage.text = "ランクが違うため選択できません";
			}
		}else return;
	}

	public void OnReteturnTop(){
		
		topPage_Image.gameObject.SetActive(true);
		transform.localPosition = new Vector2(1500,0);
		Deck_Change.transform.localPosition = new Vector2(1500,0);
		deckMenuIn = false;
		createButton = false;
	}

}
