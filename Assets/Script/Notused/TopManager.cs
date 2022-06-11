using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TopManager : MonoBehaviour {
/* 
	public Text playerName;
	public Text Level;
	public Text className;
	public Text rate;

	public Button[] Komas = new Button[40];//全体のprefab

	public Deckmanager deck;

	public GameObject Deck_Tab;
	public GameObject[] pagePanels;
	public GameObject plePanel;
	public Button[] changeKoma;
	public Button changeOneKoma;

	public Button[] Koma_model = new Button[20];

	public Image selectFrame; 

	public Transform changePanelPearent;

	//Player player;

	public Text pages;

	public int nowPage;

	/// <summary>
	/// Topメニュー関係
	/// </summary>
	public Image Top_Image;
	public Image menueBar;

	/// <summary>
	/// デッキ編集関係
	/// </summary>
	public Image Deck_Select;
	public Image Deck_Change;
	public Image[] Decks = new Image[10];
	public Text errorMessage;

	public Image frame;
	public bool intoSelect = false;//編成選択時アニメーション
	public bool selectOn = false;//trueで駒選択中
	public float frameTime = 0.2f;
	public int changeSelectCatsh;//選択したコマの種類をキャッシュ

		//駒詳細画面関係
	public Text Kun,Onn,Lank_Text,Efect_Text,rear_Text,name_Text;
　　 public Button whiteChange,redChenge;

//制限時間関係
	public bool charStatusModeFlg1 = false;
	public bool charStatusModeFlg2 = false;
	//Deltatime保存用
	public float secondery = 0;

	public Button Button_Detail;


	// Use this for initialization
	void Start () {
		player = GetComponent<Player>();
		playerName.text = player.playerName;
		Level.text = player.level.ToString();
		className.text = player.playerClass;
		rate.text = player.rate.ToString();
		//編成関係
		plePanel = Resources.Load<GameObject>("Plefab/MyListPanel");
		changeOneKoma = Resources.Load<Button>("Plefab/komachange");
		selectFrame = Resources.Load<Image>("Plefab/SelectFrame_Image");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Scenemove_Battle(){
		SceneManager.LoadScene("BattulScene");
	}

	//対局画面へ遷移
	public void OnTabBattle(){//ここいらないScenemove_Battleで代用
		gameObject.SetActive(false);
		//battleIn = true;

		//この関数で対局画面をセットする
		/* これはGameManagerのStartで行う
		charTagCache = "my";
		PrefabSet(1);
		deckCnt = 0;
		charTagCache = "ene";
		PrefabSet(-1);
		*/
		//戻る時はデストロイ忘れずに
	//} 

	//デッキ編集画面へ移動
	/* 
	public void OnTabDeckChange(/*int i*/ /* ){
		Deck_Tab.transform.localPosition = transform.localPosition;
		//page数計算
		int index = Mathf.CeilToInt(player.myList.Count/8);
		Debug.Log("[Debug]:index"+index);
		int remainder = player.myList.Count;
		int s = 0;//列の制御
		int h = 0;


		if(player.myList.Count%8 != 0)	index++;
		pagePanels = new GameObject[index];

		changeKoma = new Button[remainder];
		
		//バトル用デッキの表示 1
		for(int d = 0;d < player.mydeck.Count;d++){
			int d2 = d;
			Koma_model[d].GetComponent<CharTemplete>().InitStatus(player.mydeck[d]);
			Koma_model[d].GetComponentInChildren<Text>().text = player.mydeck[d].name;
			Koma_model[d].onClick.AddListener(()=>OnChangeMyKoma(d2));
		}

		//１ページ作る処理
		for(int i = 0;i<index;i++){
			Debug.Log("page数"+i);
			pagePanels[i] = Instantiate(plePanel,changePanelPearent);
			pagePanels[i].transform.SetParent(changePanelPearent);
			pagePanels[i].transform.localScale = Vector3.one;
			if(i == 0)pagePanels[i].transform.localPosition = new Vector3(0,0,0);
			else pagePanels[i].transform.localPosition = new Vector3(330,0,0);

			for(int f =0;f<remainder;f++){
				int hchash = h;//コマ種類の数
				int fchash = f;//ページ内での添字
				if(f>7)break;
				changeKoma[f] = Instantiate(changeOneKoma,pagePanels[i].transform);
				changeKoma[f].transform.SetParent(pagePanels[i].transform);
				changeKoma[f].transform.localScale = new Vector3(1.8f,1.8f,1.8f);
				changeKoma[f].GetComponent<CharTemplete>().InitStatus(player.myList[h]);
				changeKoma[f].GetComponentInChildren<Text>().text = player.myList[h].name;

				Button mirrar = changeKoma[f];

				//ステータス表示せってぃんぐ
				EventTrigger trigger = mirrar.GetComponent<EventTrigger>();
				EventTrigger.Entry entry = new EventTrigger.Entry();
				entry.eventID = EventTriggerType.PointerDown;
				entry.callback.AddListener((eventData)=> SwitchStatusView(hchash));
				trigger.triggers.Add(entry);


				if(f<4)changeKoma[f].transform.localPosition = new Vector3(-110+s*70,50,0);
				else changeKoma[f].transform.localPosition = new Vector3(-110+s*70,-50,0);
				//選択するとストックと入れ替わるようなボタン関数を設定する
				changeKoma[f].onClick.AddListener(()=>OnTopChangeKoma(fchash,hchash));
				s++;
				if(s>3)s=0;
				
				h++;
			}
		Debug.Log(remainder);
		remainder -= 8;//余分が出ないようにする処理　
		}
		pages.text = "1/" + index.ToString();
		nowPage = 0;


	}

	//編成で駒を押した時の処理、選択アニメーション
	public void OnTopChangeKoma(int f,int h){
	　　changeSelectCatsh = h;
		errorMessage.text = "";
		//機能は実現できるが、前のフレームを消せばいいだけなので全て探す必要はない
		var clones = GameObject.FindGameObjectsWithTag("fram");
			foreach(var clone in clones){
				Destroy(clone);
			}

			//Image frame=GetComponent<Image>();
		frame = Instantiate(selectFrame,pagePanels[f/8].transform);
		//frame.color = new Color(0f,0f,0f,0f);
		frame.fillAmount = 0;
		frame.transform.SetParent(pagePanels[nowPage].transform);
		frame.transform.localScale = new Vector3(1.8f,1.8f,1.8f);
		frame.transform.localPosition = changeKoma[f].transform.localPosition;
		intoSelect = true;
		selectOn = true;//コマ選択状態

	}

	/// <summary>
	/// 変更先の駒を選択するとき　2
	/// </summary>
	/// <param name="dint">バトルデッキの添字</param>
	public void OnChangeMyKoma(int dint){
		//player.mydeckを変更する

		if(selectOn){

			if(player.mydeck[dint].rank==player.myList[changeSelectCatsh].rank){
			player.mydeck[dint] = deck.PrivateLoad(player.myList[changeSelectCatsh].id);
			Koma_model[dint].GetComponentInChildren<Text>().text = player.mydeck[dint].name;
			//Mylistの数を元に戻す（未実装）
			}else{
				errorMessage.text = "ランクが違うため選択できません";
			}
		}else return;
	}

	//orがtrueならバトル時,falseならデッキ編成時
	public void SwitchStatusView(int bint){
		int p;
		CharTemplete br;
		br = Komas[bint].GetComponent<CharTemplete>();
		charStatusModeFlg1 = true;
		secondery = 0;
		p = bint;
		//ここで詳細UIの内容変える
		Kun.text = br.rub1;
		Onn.text = br.rub2;
		Lank_Text.text = br.rank.ToString();
		Efect_Text.text = br.efect;
		rear_Text.text = br.rear.ToString();
		name_Text.text = br.name;
		
		Debug.Log("Longtappp");
		whiteChange.onClick.AddListener(()=> ChangeEfect_white(p));
		redChenge.onClick.AddListener(()=> ChangeEfect_red(p));
		whiteChange.interactable = br.efect_onoff;
		redChenge.interactable = !br.efect_onoff;
		if(br.efect_onoff)ChangeEfect_red(p);
		else ChangeEfect_white(p);

	}

		//一つにできる？
	public void ChangeEfect_red(int bint){
		CharTemplete br = Komas[bint].GetComponent<CharTemplete>();
		
		
		if(br.Type != CharPrefab.ET.movechange){
			if(br.efect_onoff){
				Kun.text = br.rub1;
				Onn.text = br.rub2;
				Lank_Text.text = br.rank.ToString();
				Efect_Text.text = br.efect;
				rear_Text.text = br.rear.ToString();
				name_Text.text = br.name;
			}else{
				CharPrefab cc = deck.PrivateLoad(br.efectnumber);
				Kun.text = cc.rub1;
				Onn.text = cc.rub2;
				Lank_Text.text = cc.rank.ToString();
				rear_Text.text = cc.rear.ToString();
				name_Text.text = cc.name;
				Efect_Text.text = cc.efect;	
			}
		}
		name_Text.color = Color.red;
		//redbutton無効
		redChenge.interactable = false;
		//whitebutton有効
		whiteChange.interactable = true;
	}

	public void ChangeEfect_white(int bint){
		CharTemplete br = Komas[bint].GetComponent<CharTemplete>();
		if(br.Type != CharPrefab.ET.Move){
			if(br.efect_onoff){
				CharPrefab cc = deck.PrivateLoad(br.efectnumber);
				Kun.text = cc.rub1;
				Onn.text = cc.rub2;
				Lank_Text.text = cc.rank.ToString();
				rear_Text.text = cc.rear.ToString();
				name_Text.text = cc.name;
				Efect_Text.text = cc.efect;	
			}else{
				Kun.text = br.rub1;
				Onn.text = br.rub2;
				Lank_Text.text = br.rank.ToString();
				Efect_Text.text = br.efect;
				rear_Text.text = br.rear.ToString();
				name_Text.text = br.name;
		}

		}
		name_Text.color = Color.black;
		//whitebutton無効
		whiteChange.interactable = false;
		//redbutton有効
		redChenge.interactable = true;
	}

	//ステータス画面の切り替え
	public void OnDetailOut(){
		charStatusModeFlg1 = false;
		charStatusModeFlg2 = false;
		Button_Detail.transform.localPosition = new Vector3(500,0,0);
		OnDeltailOut_reset();
		whiteChange.interactable = true;
		redChenge.interactable = false;
	}

	public void OnDeltailOut_reset(){
		whiteChange.onClick.RemoveAllListeners();
		redChenge.onClick.RemoveAllListeners();
	}*/
}
