//#define CPU_CPU_MODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour {

	PeiceMST peicemst;//ステータスデータ
	
	string charTagCache;//駒のtagキャッシュ
	public Button[] Komas = new Button[40];//全体のprefab管理
	Vector3[,] Koma_Potision;//盤の位置情報表
	int[,] isKoma;//駒の存在表1：自分、0：存在しない、-1：相手

	//移動まえのいちを格納
	int vCache;
	int hCache;

	//<駒に関するデータ>
	Button komaOrigine;

	public Transform banTransform;

	//public GameObject Button_Detail;

	//<移動先ナビゲートUI関係>
	//移動可能先を示すボタンPrefab
	Button navigateOrigine;
	public RectTransform ImageTransform;
	//public Transform PanelsTransform;
	public RectTransform banContents1;
	public RectTransform banContents2;

	//prefabの添字用
	int preCache = 0;

	//敵のコマ変更はここから
	int[] enemyTable = {0,0,0,0,0,0,0,0,0,6,5,1,2,3,4,7,4,3,2,1}; 

//ベンチ関係
	public GameObject Bench1;
	public GameObject Bench2;

	Animator animator1;

	Animator animator2;

	//bool isTurn = false;
	bool openedMyBench = false;
	bool openedEnemyBench = false;

	//詳細画面表示
	public GameObject _detailView;
	DetailView detailView;

	//ターン制御
	//順番UI
	public Text sennte;
	public Text gote;
	//gamemanaからかんり
	int turnNumber = 0;
	bool textIn = false;

	[SerializeField]
	float deleteTime=2;

	//制限時間関係
	TimeUI timebar;

	public Image timer;

	//表示関係
	public Text Result_text;

	//効果発動メッセージ
	public Image selectEfectForm;
	bool selectedEfect = false;
	bool selected = true;
	public Button Yes_Button;
	//public Button No_Button;

	float detailTime;
	bool detailOn = false;
	
	int KomaCache;
	
	bool startFLG = true;
	bool finishFLG = false;
	SaveManager save;


	// Use this for initialization
	void Start () {
		peicemst = new PeiceMST();
		detailView = _detailView.GetComponent<DetailView>();

		//ポディション格納
		Vector3 basePosition;
		basePosition.x = -166;
		basePosition.y = 168;
		basePosition.z = 0;
		Koma_Potision = new Vector3[9,9];
		for(int i =0 ; i<9;i++){
			for(int ii =0 ;ii<9;ii++){
				Koma_Potision[i,ii] = basePosition;
				basePosition.x += 42;
			}
			basePosition.y -= 42;
			basePosition.x = -166;
		}

		//-1,0,1の範囲それぞれ敵,何もいない,自分駒 が存在している
		//Koma_Positionとは別物
		isKoma = new int[9,9];

		//バトル関係
		komaOrigine = Resources.Load<Button>("Plefab/koma");
		navigateOrigine = Resources.Load<Button>("Plefab/MoveButton");
		animator1 = Bench1.GetComponent<Animator>();
		animator2 = Bench2.GetComponent<Animator>();

		//表示制御
		_detailView.gameObject.SetActive(true);

		//制限時間
		timer.GetComponent<TimeUI>();
		timebar = timer.GetComponent<TimeUI>();
		charTagCache = "my";
		PrefabSet(Player.mydeck);
		charTagCache = "ene";
		PrefabSet(enemyTable);
	}
	
	// Update is called once per frame
	void Update () {
#if CPU_CPU_MODE

		if(startFLG){
			save = new SaveManager();
			startFLG = false;
		}
		CPUTurn2();
		CPUTurn();

#else
		if(textIn){
			if(turnNumber%2 == 1)sennte.gameObject.SetActive(true);
			else {
				gote.gameObject.SetActive(true);
				
			}
			deleteTime -= 1/deleteTime * Time.deltaTime;
		}

		//時間制御	
		if(deleteTime <= 0){


			sennte.gameObject.SetActive(false);
			gote.gameObject.SetActive(false);
			deleteTime = 2;
			textIn = false;
			if(turnNumber%2 != 1)CPUTurn();
			
		}

		if(detailOn){
			detailTime += Time.deltaTime;
		}

		if(timebar.endfaids)PlayerSwitching();

#endif
		/* 
		if (detailView.charStatusModeFlg1) {
			//詳細UIをtrueして表示
			detailView.secondery += Time.deltaTime;
			if (detailView.secondery >= 1.0) {
				detailView.charStatusModeFlg2 = true;
				detailView.charStatusModeFlg1 = false;
			}
			//Debug.Log(charStatusModeFlg2);
		}
		
		if (detailView.charStatusModeFlg2) {
			detailView.DetailIn ();
			detailView.secondery = 0;
			detailView.charStatusModeFlg2 = false;
		}
		*/
		
	}

	//peiceTableキャッシュしておくと良い
	/// <summary>
	/// 将棋版に駒を配置
	/// </summary>
	/// <param name="peiceTable">駒デッキテーブル（自、敵）</param>
	public void PrefabSet(int[] peiceTable){

		//int idx = 0;
		
		//int weight=8;//次のインデックスまでの距離
		int pS;
		if(charTagCache == "my")pS=1;
		else pS = -1;

		for(int idx =0;idx<peiceTable.Length;idx++){

			if(idx<9){
				if(pS == 1)createPiece(6,idx,peiceTable[idx]);
				else createPiece(2,idx,peiceTable[idx]);
				//idx++;
			}
			
			if(idx==9){
				if(pS == 1) createPiece(7,idx-8,peiceTable[idx]);
				else createPiece(1,idx-2,peiceTable[idx]);
				//idx++;
			}

			if(idx==10){
				if(pS == 1) createPiece(7,idx-3,peiceTable[idx]);
				else createPiece(1,idx-9,peiceTable[idx]);
				//idx++;
			}

			if(idx>10){
				if(pS == 1) createPiece(8,idx-11,peiceTable[idx]);
				else createPiece(0,idx-11,peiceTable[idx]);
				//idx++;
			}	
		}
		//idx=0;
		//Debug.Log("komaset");
	}

	
	/// <summary>
	///指定場所に駒インスタンス作成
	/// </summary>
	/// <param name="v">縦軸</param>
	/// <param name="h">横軸</param>
	/// <param name="komaId">全体管理している駒のidx</param>
	public void createPiece(int v,int h,int bint){
		int p = 0; //ボタン関数用

		//インスタンス生成
		Komas[preCache] = Instantiate(komaOrigine,banTransform);	
		Komas[preCache].transform.SetParent(banTransform,false);
		Komas[preCache].transform.localScale = Vector3.one;
		Komas[preCache].transform.localPosition = Koma_Potision[v,h];

		//ボタン関数付与
		PeiceStatus PS = Komas[preCache].GetComponent<PeiceStatus>();
		p = preCache;
		//Komas[preCache].onClick.AddListener(() => createPanel(p));

		//ステータス取得
		PS.Initial(peicemst.getBaseObject(bint));
		PS.tag = charTagCache;
		PS.h = h;
		PS.v = v;

		//Event取得
		EventTrigger trigger = Komas[preCache].GetComponent<EventTrigger> ();
		trigger.triggers = new List<EventTrigger.Entry>();
		//PointerDownSet
		EventTrigger.Entry entry = new EventTrigger.Entry ();
		entry.eventID = EventTriggerType.PointerDown;
		//entry.callback.AddListener ((eventData) => detailView.SwitchStatusView (PS));
		entry.callback.AddListener ((eventData) => KomaPointerDown(p));
		trigger.triggers.Add (entry);
		//PointerUpset
		EventTrigger.Entry entry2 = new EventTrigger.Entry ();
		entry2.eventID = EventTriggerType.PointerUp;
		entry2.callback.AddListener ((eventData) => KomaPointerUp());
		trigger.triggers.Add (entry2);
		

		//敵コマの場合180度回転させる
		if(charTagCache == "ene"){
			Komas[preCache].transform.Rotate(new Vector3(0,0,180));
			isKoma[v,h] = -1;
		}else isKoma[v,h] = 1;
		
		preCache++;
		//deckCnt++;
	}

	/// <summary>
	/// 駒選択時の移動範囲決定-駒のPointerUpで呼び出し
	/// </summary>
	/// <param name="bint"></param>
	public void createPanel(int bint){
			int t = 1;
			bool permissionFlg = true;
			//detailView.charStatusModeFlg1 = false;
			PeiceStatus br = Komas[bint].GetComponent<PeiceStatus>();
			//自分のターンに相手駒を触った時の動作
#if TestOut
			if(PlayerSwitch && Komas[bint].tag == "ene"){
				return;
				//相手ターンに自分駒を押した時の動作
			}else if(!PlayerSwitch && Komas[bint].tag == "my"){
				return;
			}
#endif
			DestroyPanels();

			if(br.isBench){//ベンチのこまをタッチした時の処理
				

				//bool onKoma = true;//この場合trueはいないという意味
		
				for(int v = 0;v<9;v++){
					for(int h = 0;h<9;h++){
							//存在チェック
							if(isKoma[v,h] !=0){
								//onKoma =false;//Debug.Log("駒と被っているため次へ")
							}else panelCreate(v,h,bint);
						}
					}
				
			}else{

				if(Komas[bint].tag == "ene")　t = -1;

				//p = Koma_Potision[br.v,br.h];
				//Debug.Log("[元の位置]"+br.v+","+br.h);
				//vCache = br.v;
				//hCache = br.h;
				//p2.z = 0;		//押した駒の位置
				if(br.status.Marchingtype == BaseObject.MT.front){

					for(int i = 0;i<8;i++){	//方角
						//p2 = p;
						if(br.status.move[i]==0)	continue;
						//Debug.Log("一つの方向");
						for(int o = 1;o<br.status.move[i]+1;o++){					//1方向の移動範囲
							try{//permissionflg共通変数にする？
								if(i==0) permissionFlg = VectorCheck(br.v-o*t,br.h,bint);
								if(i==1) permissionFlg = VectorCheck(br.v-o*t,br.h+o*t,bint);
								if(i==2) permissionFlg = VectorCheck(br.v,br.h+o*t,bint);
								if(i==3) permissionFlg = VectorCheck(br.v+o*t,br.h+o*t,bint);
								if(i==4) permissionFlg = VectorCheck(br.v+o*t,br.h,bint);
								if(i==5) permissionFlg = VectorCheck(br.v+o*t,br.h-o*t,bint);
								if(i==6) permissionFlg = VectorCheck(br.v,br.h-o*t,bint);
								if(i==7) permissionFlg = VectorCheck(br.v-o*t,br.h-o*t,bint);

								if(permissionFlg == false)break;

							}catch{
								//Debug.Log("index範囲外のため例外処理");
								continue;
							}
						}
					}
				}else{
					//Debug.Log("MType = stright.start");//正面でも動作するように修正完了
					for(int i = 0;i <8;i++){//方がく
						//p2 = p;
						
						if(br.status.move[i] == 0) continue;//Debug.Log("距離が０のため次の方角へ");
							try{		
								if(i == 0) if(VectorCheck(br.v-br.status.move[i]*t,br.h,bint)){};
								if(i == 1) if(VectorCheck(br.v-br.status.move[i]*t,br.h+t,bint)){};
								if(i == 2) if(VectorCheck(br.v,br.h+br.status.move[i]*t,bint)){};
								if(i == 3) if(VectorCheck(br.v+br.status.move[i]*t,br.h+t,bint)){};//動きが１つの方向のみになる
								if(i == 4) if(VectorCheck(br.v+br.status.move[i]*t,br.h,bint)){};
								if(i == 5) if(VectorCheck(br.v+br.status.move[i]*t,br.h-t,bint)){};
								if(i == 6) if(VectorCheck(br.v,br.h-br.status.move[i]*t,bint)){};
								if(i==7) if(VectorCheck(br.v-br.status.move[i]*t,br.h-t,bint)){};
							}catch{
								//Debug.Log("index範囲外のため例外処理");
								continue;
							}

					}
				}
			}
		}

	/// <summary>
	/// 範囲チェック＋重複チェック
	/// </summary>
	/// <param name="v">moved</param>
	/// <param name="h">moved</param>
	/// <param name="bint"></param>
	/// <returns></returns>
	public bool VectorCheck(int v,int h,int bint){

		//Debug.Log("[移動可能位置]"+v+"," +h);
		int t;
		//自分or相手が他の駒と重なった回数
		int coverCnt=0;
		bool permisson = true;
		PeiceStatus br = Komas[bint].GetComponent<PeiceStatus>();
		
		if(Komas[bint].tag == "my")t = 1;
		else t= -1;

		if(isKoma[v,h] != 0){
			if(isKoma[v,h] != t){
				coverCnt = 1;
				permisson = true;　　　//Debug.Log("敵と被っているが一度めなので許容");
			}else permisson = false;　//Debug.Log("自分のコマと被っているためfalse");
		}
				
		if(permisson){
			panelCreate(v,h,bint);
		
			if(coverCnt == 1){
				coverCnt = 0;
				//Debug.Log("相手のコマと一度重なっていて次は許容しないためfalse");
				return false;
			}
		}
		return permisson;
	}

	public void DestroyPanels(){
		var clones = GameObject.FindGameObjectsWithTag("panels");
		foreach(var clone in clones){
			Destroy(clone);
		}
		//navigateCnt = 0;
	}

	/// <summary>
	/// 実際にパネルを描画-駒のPointerUpに設定
	/// </summary>
	/// <param name="v"></param>
	/// <param name="h"></param>
	/// <param name="bint">全体の駒番号</param>
	public  void panelCreate(int v,int h,int bint){

		//移動先の変数
		int v2 = v;
		int h2 = h;
		int point = bint;

		Button navigatePanels;
		navigatePanels = Instantiate(navigateOrigine,banTransform);
		navigatePanels.transform.SetParent(banTransform,false);
		navigatePanels.transform.localScale = Vector3.one;
		navigatePanels.transform.localPosition = Koma_Potision[v,h];
		navigatePanels.onClick.AddListener(()=>moving(v2,h2,point));
		navigatePanels.tag = "panels";
	}

	/// <summary>
	/// パネル押下時の駒を動かした時の処理
	/// </summary>
	/// <param name="v,h">移動先の位置</param>
 	/// <param name="bint">駒の添字</param>
	public void moving(int movedV,int movedH,int bint){		

		
		PeiceStatus myKoma = Komas[bint].GetComponent<PeiceStatus>();
		//int cacheV = myKoma.v;
		//int cacheH = myKoma.h;
		DestroyPanels();
		//駒をベンチからおく処理
		if(myKoma.isBench){//駒がベンチにある場合
			//一時的に駒の種類を覚えておく
			int Filedinchash = myKoma.status.Id;
			charTagCache = Komas[bint].tag;

			//ベンチから指す駒情報をpreCntにキャッシュ
			DestroyKoma(bint);
			preCache = bint;
			createPiece(movedV,movedH,Filedinchash);
			PlayerSwitching();


		}else{//盤内のこまを動かす処理
			//自分か敵で内容変化する
			string charTag ="";
			Transform bench;
			GameObject[] clone;

			isKoma[myKoma.v,myKoma.h] = 0;
			Komas[bint].transform.localPosition = Koma_Potision[movedV,movedH];
		 	myKoma.v = movedV;
		 	myKoma.h = movedH;
			
			
			
			if(Komas[bint].tag == "ene"){//動いた駒が敵駒
				clone = GameObject.FindGameObjectsWithTag("my");
				charTag = "ene";
				bench = banContents2;
				isKoma[movedV,movedH] = -1;
			}else{
				clone = GameObject.FindGameObjectsWithTag("ene");
				charTag = "my";
				bench =banContents1;
				isKoma[movedV,movedH] = 1;
			}

			//移動した場所に駒が存在しているかのチェック
			if(isKoma[movedV,movedH] != 0){
				//存在している場合サーチ
				foreach(var clones in clone){
					if(clones.transform.localPosition == Komas[bint].transform.localPosition){
						PeiceStatus br = clones.GetComponent<PeiceStatus>();
						//Debug.Log("benchin");
						clones.transform.SetParent(bench);
						clones.tag = charTag;
						br.isBench = true;
						br.v = -1;
						br.h = -1;
						clones.transform.Rotate(new Vector3(0,0,180));
						
						//王をとった場合の処理
						if(br.status.Rank == 5){
#if CPU_CPU_MODE
							//終了時処理

							Komas = new Button[40];
							isKoma = new int[9,9];
							save.KIFU("end","",0,0,0,0);
							Debug.Log("終了");
							//次の処理
							charTagCache = "my";
							PrefabSet(Player.mydeck);
							charTagCache = "ene";
							PrefabSet(enemyTable);
							startFLG = true;

#else
							if(charTag == "ene"){
							Result_text.text = "プレイヤー２の勝利";
							Result_text.color = Color.blue;
							}else{
								Result_text.text = "プレイヤー１の勝利";
								Result_text.color = Color.red;
							}
							Result_text.gameObject.SetActive(true);
#endif
						}

						if(br.isEfect){//取った駒の処理
							//取得した駒が効果を発動している場合、Efectnumberを見に行くーつまり効果を発動した時自身のidをEfectnumberに入れる必要がある
							br.Initial(peicemst.getBaseObject(br.status.EfectNumber));
							//clones.GetComponentInChildren<Text>().text = br.name;
							clones.GetComponentInChildren<Text>().color = Color.black;
						}
						break;
					}
							
				}
			}

			if(!myKoma.isEfect){
				//奥３列に入った時の効果
				if(charTag == "ene"){
					if(myKoma.v >= 6){　
						//起動効果か任意か決める
						//Efect_Selection(bint);
#if !CPU_CPU_MODE
						EfectMessage(bint);
#endif
					//移動して終わりの時の処理
					}else　PlayerSwitching();	
				}else{
					if(myKoma.v <= 2){
						//Efect_Selection(bint);
#if !CPU_CPU_MODE
						EfectMessage(bint);
#endif
					}else PlayerSwitching();
				}
			}
				
		}

	}

	//普通の破壊か,ストックからの移動か
	public void DestroyKoma(int bint){
		Destroy(Komas[bint].gameObject);//普通の破壊
		
	}

	public void Efect_Selection(int bint){
		PeiceStatus br = Komas[bint].GetComponent<PeiceStatus>();

		switch(br.status.EfectType){
			case BaseObject.ET.Atack:
			break;
			case BaseObject.ET.Change:
				Skill_Change(bint);
				//Invoke("() => Skill_Change(bint))",5);
				//selectEfectForm.gameObject.SetActive(true);
				break;
			case BaseObject.ET.MoveChange:
				Skill_MoveChange(bint);
				//Invoke("()=>Skill_MoveChange(bint))",5);
				//selectEfectForm.gameObject.SetActive(true);
				break;
			case BaseObject.ET.Every:
			break;
			case BaseObject.ET.OneTime:
			break;
			case BaseObject.ET.Trap:
			break;
			default :
			break;
		}
		
	}


	
	public void Skill_Change(int bint){
		//Debug.Log("効果発動");
		//float waitSecond = 0;
		//selected = true;

		//StartCoroutine(EfectMessage());
		//if(selectedEfect){
			PeiceStatus br = Komas[bint].GetComponent<PeiceStatus>();
			int orizinalNum = br.status.Id;
			//int v = br.v;
			//int h = br.h;

			br.isEfect = true;
			//ステータスのみ変更している
			br.Initial(peicemst.getBaseObject(br.status.EfectNumber));
			//Komas[bint].GetComponentInChildren<Text>().text = br.name;
			
			br.status.EfectType = BaseObject.ET.None;
			//取られた時に元の駒に戻るための前のid
			br.status.EfectNumber = orizinalNum;
			
			//br.v = v;
			//br.h = h;
			//selectEfectForm.gameObject.SetActive(false);
		//}
		
	}

	//IEnumerator EfectWait(){
		//yield return new WaitUntil(() => (Efect_Yes() || Efect_No()));
	//	yield return new WaitUntil(selected == true);
		
	//	Selected = false;
	//}

	/// <summary>
	/// 効果発動確認画面
	/// </summary>
	/// <param name="bint"></param>
	public void EfectMessage(int bint){
		///範囲かつ効果が発動してなければこの関数に遷移
		
		
		//selectedEfect = true;
		
		selectEfectForm.gameObject.SetActive(true);
		//この効果発動選択中は他の操作はできないようにboolを設ける
		Yes_Button.onClick.AddListener(()=>Efect_Yes(bint));
		//No_Button.onClick.AddListener(Efect_No);
		//bratsh[brnt] = bint;//効果を発動するコマのキャッシュ
		//brnt++;
	}

	public void Efect_Yes(int bint){
		//今は効果一度だけ発動すればターン交代でいいが、これからは効果が連鎖する可能性あり
		selectEfectForm.gameObject.SetActive(false);
		Efect_Selection(bint);
		PlayerSwitching();
		//Yes_Button.onClick.RemoveAllListeners();
		//selected = false;
		//Debug.Log("Yes"+selected);
		selectedEfect = true;
		
	}
	public void Efect_No(){
		selectEfectForm.gameObject.SetActive(false);
		//bratsh[brnt] = null;
		//Yes_Button.onClick.RemoveAllListeners();
		//Efect_Selection(bint);
		PlayerSwitching();
		//selected = false;
		//Debug.Log("No"+selected);
		selectedEfect = false;
		
	}


	//IEnumerator 
	public void Skill_MoveChange(int bint){
			
		PeiceStatus br = Komas[bint].GetComponent<PeiceStatus>();
		br.isEfect = true;
		//int orizinalNum = br.Id;
		//if(br.status.EfectType == BaseObject.ET.Change){
			//change_Skills(bint);
					//}else
		//if(br.status.EfectType == BaseObject.ET.MoveChange){
			//movechange_Skills(bratsh[brnt]);
		br.Move_Pulus(peicemst.getBaseObject(br.status.EfectNumber).move);
		//br.status.EfectNumber = orizinalNum;
		//br.status.EfectType = BaseObject.ET.None;
		Komas[bint].GetComponentInChildren<Text>().color = Color.red;
		//selectEfectForm.gameObject.SetActive(false);
		
		
		
		//Debug.Log("効果を発動します");
	}


	public void OnPlayer(){
		this.openedMyBench = !this.openedMyBench;
		animator1.SetBool("parametor",this.openedMyBench);
	}

	public void OnEnemy(){
		this.openedEnemyBench = !this.openedEnemyBench;
		animator2.SetBool("parametor",this.openedEnemyBench);
	}

	public void TurnChange(){
		turnNumber += 1;
		textIn = true;
	}

	public void PlayerSwitching(){
		TurnChange();
		timebar.ResetTimer();
		//ターン交代処理
	}

	public void KomaPointerDown(int cache){
		//駒の管理番号と、位置をキャッシュ
		KomaCache = cache;
		detailOn = true;
	}

	public void KomaPointerUp(){
		detailOn = false;
		Debug.Log(detailTime);
		
		if(detailTime > 1.0f){
			Debug.Log("detail");
			PeiceStatus peice = Komas[KomaCache].GetComponent<PeiceStatus>();
			detailView.SwitchStatusView(peice);
		}else{
			Debug.Log("move");
			createPanel(KomaCache);
		}
		detailTime = 0;
	}

	void CPUTurn(){
		CPU cpu = new CPU();
		int randEnemy;
		string moveElements;	
		randEnemy =  cpu.SelectEnemy(Komas,"ene");
		PeiceStatus PS = Komas[randEnemy].GetComponent<PeiceStatus>();
		moveElements = cpu.SelectKoma(isKoma,PS);
		string[] ST = moveElements.Split(',');
		save.KIFU("ene",PS.status.PeiceName,PS.v,PS.h,int.Parse(ST[0]),int.Parse(ST[1]));
		moving(int.Parse(ST[0]),int.Parse(ST[1]),int.Parse(ST[2]));
	}

	void CPUTurn2(){
		CPU cpu = new CPU();
		int randEnemy;
		string moveElements;
		randEnemy =  cpu.SelectEnemy(Komas,"my");
		PeiceStatus PS = Komas[randEnemy].GetComponent<PeiceStatus>();
		moveElements = cpu.SelectKoma(isKoma,Komas[randEnemy].GetComponent<PeiceStatus>());
		string[] ST = moveElements.Split(',');
		save.KIFU("my",PS.status.PeiceName,PS.v,PS.h,int.Parse(ST[0]),int.Parse(ST[1]));
		moving(int.Parse(ST[0]),int.Parse(ST[1]),int.Parse(ST[2]));
	}



}

