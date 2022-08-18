using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static common.CommonCalcuration;
using static common.CommonType;
using System.Linq;
using System;
using UniRx;
using System.Threading.Tasks;

public class BattleManager : MonoBehaviour {

	PeiceMST peicemst;//ステータスデータ

	//移動まえのいちを格納
	int vCache;
	int hCache;


	//敵のコマ変更はここから
	private int[] enemyTable = {0,0,0,0,0,0,0,0,0,6,5,1,2,3,4,7,4,3,2,1}; 

//ベンチ関係
	//public GameObject Bench1;
	//public GameObject Bench2;

	//Animator animator1;

	//Animator animator2;

	//bool isTurn = false;
	bool openedMyBench = false;
	bool openedEnemyBench = false;


	//ターン制御
	//順番UI
	//public Text sennte;
	//public Text gote;
	//gamemanaからかんり
	int turnNumber = 0;
	bool textIn = false;

	[SerializeField]
	float deleteTime=2;

	//制限時間関係
	TimeUI timebar;

	//public Image timer;

	//表示関係
	//public Text Result_text;
	
	bool selected = true;
	public Button Yes_Button;
	//public Button No_Button;

	float detailTime;
	bool detailOn = false;
	
	int KomaCache;
	
	bool startFLG = true;
	bool finishFLG = false;
	SaveManager save;

	//---------------------------------used
	

	private Button komaPrefab;
	private Button movablePrefab;
	public Button[] allKomas = new Button[40];//全体のprefab管理
	int globalKomaIndex = 0;
	public string turn = "player1";

	public static BattleManager instance;
	[SerializeField]
	private RectTransform stockPlayer1;
	[SerializeField]
	private RectTransform stockPlayer2;

	private string selectedEffect = "";
	[SerializeField]
	private Image confirmEffectDialog;
	private bool isLock;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		peicemst = new PeiceMST();

		//バトル関係
		komaPrefab = Resources.Load<Button>("Plefab/used/koma");
		movablePrefab = Resources.Load<Button>("Plefab/used/MovableButton");
		//animator1 = Bench1.GetComponent<Animator>();
		//animator2 = Bench2.GetComponent<Animator>();

		//制限時間
		//timer.GetComponent<TimeUI>();
		//timebar = timer.GetComponent<TimeUI>();
		Player.FirstInit();
		InitialSetting(Player.mydeck, "player1");
		InitialSetting(enemyTable, "player2");
	}
	
	// Update is called once per frame
	void Update () {
		//if(textIn){
		//	if(turnNumber%2 == 1)sennte.gameObject.SetActive(true);
		//	else {
		//		gote.gameObject.SetActive(true);
				
		//	}
		//	deleteTime -= 1/deleteTime * Time.deltaTime;
		//}

		//時間制御	
		//if(deleteTime <= 0){


		//	sennte.gameObject.SetActive(false);
		//	gote.gameObject.SetActive(false);
		//	deleteTime = 2;
		//	textIn = false;
		//	if(turnNumber%2 != 1)CPUTurn();
			
		//}

		//if(detailOn){
		//	detailTime += Time.deltaTime;
		//}

		//if(timebar.endfaids)PlayerSwitching();
		
	}


	/// <summary>
	/// 将棋版に駒を配置
	/// </summary>
	/// <param name="peiceTable">駒デッキテーブル（自、敵）</param>
	public void InitialSetting(int[] peiceTable, string playerTag){

		Tuple<int,int>[] settingPosition = playerTag == "player1" ? FieldInfo.instance.player1InitialKomaPosition : FieldInfo.instance.player2InitialKomaPosition;
		

		for(int idx = 0; idx < peiceTable.Length; idx++)
		{
			createPiece(settingPosition[idx].Item1, settingPosition[idx].Item2, peiceTable[idx], playerTag);
			globalKomaIndex++;
		}
	}

	
	/// <summary>
	///指定場所に駒インスタンス作成
	/// </summary>
	/// <param name="v">縦軸</param>
	/// <param name="h">横軸</param>
	/// <param name="komaId">全体管理している駒のidx</param>
	public void createPiece(int v,int h,int peiceNo, string playerTag){

		//インスタンス生成
		allKomas[globalKomaIndex] = Instantiate(komaPrefab, FieldInfo.instance.fieldTransform);	
		allKomas[globalKomaIndex].transform.SetParent(FieldInfo.instance.fieldTransform, false);
		allKomas[globalKomaIndex].transform.localScale = Vector3.one;
		allKomas[globalKomaIndex].transform.localPosition = FieldInfo.instance.fieldCell[v,h].cellPosition;
		FieldInfo.instance.fieldCell[v, h].komaId = globalKomaIndex;

		//ボタン関数付与
		PeiceStatus peiceStatus = allKomas[globalKomaIndex].GetComponent<PeiceStatus>();
		peiceStatus.runtimeId = globalKomaIndex;
		allKomas[globalKomaIndex].onClick.AddListener(() => peiceStatus.CreateNavigation());

		//ステータス取得
		peiceStatus.Initial(peicemst.getBaseObject(peiceNo));
		peiceStatus.tag = playerTag;
		peiceStatus.h = h;
		peiceStatus.v = v;

		//Event取得
		EventTrigger trigger = allKomas[globalKomaIndex].GetComponent<EventTrigger> ();
		trigger.triggers = new List<EventTrigger.Entry>();
		//PointerDown
		EventTrigger.Entry pressDown = new EventTrigger.Entry ();
		pressDown.eventID = EventTriggerType.PointerDown;
		pressDown.callback.AddListener ((data) => DetailView.instance.PointerDown(peiceStatus));
		trigger.triggers.Add (pressDown);
		//PointerUpset
		EventTrigger.Entry pressUp = new EventTrigger.Entry ();
		pressUp.eventID = EventTriggerType.PointerUp;
		pressUp.callback.AddListener ((data) => DetailView.instance.PointerUp());
		trigger.triggers.Add (pressUp);


		//敵コマの場合180度回転させる
		if (playerTag == "player2")
		{
			allKomas[globalKomaIndex].transform.Rotate(new Vector3(0, 0, 180));
			FieldInfo.instance.fieldCell[v, h].cellStatus = CellStatus.PLAYER_2;
		}
		else
		{
			FieldInfo.instance.fieldCell[v, h].cellStatus = CellStatus.PLAYER_1;
		}
		
	}

	public void ReplaceBenchWithField(int targetV, int targetH, int runtimeId)
	{
		PeiceStatus myKoma = allKomas[runtimeId].GetComponent<PeiceStatus>();
		int komaId = myKoma.status.Id;
		globalKomaIndex = runtimeId;

		DestroyKoma(runtimeId);
		createPiece(targetV, targetH, komaId, turn);
		instance.PlayerSwitching();
	}

	//普通の破壊か,ストックからの移動か
	public void DestroyKoma(int runtimeId)
	{
		Destroy(allKomas[runtimeId].gameObject);//普通の破壊
	}

	/// <summary>
	/// パネル押下時の駒を動かした時の処理
	/// </summary>
	/// <param name="targetV,targetH">移動先の座標</param>
	/// <param name="runtimeId">駒の添字</param>
	public async Task MoveByField(int targetV,int targetH,int runtimeId){		

		
		PeiceStatus myKoma = allKomas[runtimeId].GetComponent<PeiceStatus>();
		FieldCell nowCell = FieldInfo.instance.fieldCell[myKoma.v, myKoma.h];
		FieldCell targetCell = FieldInfo.instance.fieldCell[targetV, targetH];


		nowCell.cellStatus = CellStatus.EMPTY;	
		allKomas[runtimeId].transform.localPosition = targetCell.cellPosition;
		Button targetButton = allKomas[targetCell.komaId];
		PeiceStatus targetStatus = targetButton.GetComponent<PeiceStatus>();


		//移動した場所に駒が存在しているかのチェック
		if (nowCell.cellStatus != targetCell.cellStatus && targetCell.cellStatus != CellStatus.EMPTY){
			
			MoveToBenchProcess(ref targetButton, ref targetStatus);
			ResultCheckProcess(ref targetStatus);

			if (targetStatus.isEfect){//取った駒の処理
									  //TODO:取得した駒が成っている場合、Efectnumberを見に行く→効果を発動した時自身のidをEfectnumberに入れる必要がある
				targetStatus.Initial(peicemst.getBaseObject(targetStatus.status.EfectNumber));
				targetButton.GetComponentInChildren<Text>().text = targetStatus.name;
				targetButton.GetComponentInChildren<Text>().color = Color.black;
			}
		}
		myKoma.v = targetV;
		myKoma.h = targetH;
		nowCell.cellStatus = CellStatus.EMPTY;
		targetCell.cellStatus = (turn == "player1") ? CellStatus.PLAYER_1 : CellStatus.PLAYER_2;
		nowCell.komaId = -1;
		targetCell.komaId = runtimeId;
		FieldInfo.DestroyPanels();

		if (!myKoma.isEfect){
			//奥３列に入った時の効果
			if(turn == "player2" && myKoma.v >= 6)
			{
				//Efect_Selection(bint);
				await StartCoroutine(EffectWait(runtimeId));
			}
			if(turn == "player1" && myKoma.v <= 2){
					//Efect_Selection(bint);
				//EfectMessage(runtimeId);
				await StartCoroutine(EffectWait(runtimeId));
			}
		}
		PlayerSwitching();
	}

	void MoveToBenchProcess(ref Button targetButton, ref PeiceStatus targetStatus)
	{
		RectTransform stockPosition = (turn == "player1") ? stockPlayer1 : stockPlayer2;
		
		if (targetStatus == null)
		{
			return;
		}
		if (targetStatus.isEfect)
		{
			SkillChange(targetStatus.runtimeId);
		}
		targetButton.transform.SetParent(stockPosition);
		targetButton.transform.Rotate(new Vector3(0, 0, 180));
		targetButton.tag = turn;
		targetStatus.isBench = true;
		targetStatus.v = -1;
		targetStatus.h = -1;
	}

	/// <summary>
	/// 敵のランク５を取った時の処理
	/// </summary>
	/// <param name="targetStatus"></param>
	void ResultCheckProcess(ref PeiceStatus targetStatus)
	{
		if (targetStatus.status.Rank == 5)
		{
			//TODO：result処理
		}
	}

	public void Efect_Selection(int bint){
		PeiceStatus br = allKomas[bint].GetComponent<PeiceStatus>();

		switch(br.status.EfectType){
			case BaseObject.ET.Atack:
			break;
			case BaseObject.ET.Change:
				SkillChange(bint);
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

	/// <summary>
	/// 成りと取られた時に元に戻る関数を一緒にしている
	/// </summary>
	/// <param name="runtimeId"></param>
	public void SkillChange(int runtimeId)
	{
		PeiceStatus peiceStatus = allKomas[runtimeId].GetComponent<PeiceStatus>();
		int orizinalId = peiceStatus.status.Id;

		peiceStatus.isEfect = !peiceStatus.isEfect;
		peiceStatus.Initial(peicemst.getBaseObject(peiceStatus.status.EfectNumber));
		//TODO:スキルがなくなるとは限らない
		peiceStatus.status.EfectType = BaseObject.ET.None;
		//取られた時に元の駒に戻るための前のid
		peiceStatus.status.EfectNumber = orizinalId;

	}

	IEnumerator EffectWait(int runtimeId){
		confirmEffectDialog.gameObject.SetActive(true);
		yield return new WaitUntil(() => { return selectedEffect != ""; });
		if(selectedEffect == "yes")
		{
			//効果内容
			Debug.Log("yesボタンが選択されました。");
		}
		selectedEffect = "";
		confirmEffectDialog.gameObject.SetActive(false);
		SkillChange(runtimeId);
	}

	/// <summary>
	/// 効果発動確認画面
	/// </summary>
	/// <param name="bint"></param>
	public void EfectMessage(int bint){
		//selectedEfect = true;
		
		
		//この効果発動選択中は他の操作はできないようにboolを設ける
		//Yes_Button.onClick.AddListener(()=>Efect_Yes(bint));
		//No_Button.onClick.AddListener(Efect_No);
		//bratsh[brnt] = bint;//効果を発動するコマのキャッシュ
		//brnt++;
	}

	public void OnYesButton(){
		selectedEffect = "yes";
	}
	public void OnNoButton(){
		selectedEffect = "no";
	}


	//IEnumerator 
	public void Skill_MoveChange(int bint){
			
		PeiceStatus br = allKomas[bint].GetComponent<PeiceStatus>();
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
		allKomas[bint].GetComponentInChildren<Text>().color = Color.red;
		//selectEfectForm.gameObject.SetActive(false);
		
		
		
		//Debug.Log("効果を発動します");
	}


	//public void OnPlayer(){
	//	this.openedMyBench = !this.openedMyBench;
	//	animator1.SetBool("parametor",this.openedMyBench);
	//}

	//public void OnEnemy(){
	//	this.openedEnemyBench = !this.openedEnemyBench;
	//	animator2.SetBool("parametor",this.openedEnemyBench);
	//}

	public void TurnChange(){
		turnNumber += 1;
		//textIn = true;
		if(turn == "player1")
		{
			turn = "player2";
		}
		else
		{
			turn = "player1";
		}
	}

	public void PlayerSwitching(){
		TurnChange();
		//timebar.ResetTimer();
		Debug.Log($"ターン交代処理:{turn}");
	}

	public void KomaPointerDown(int cache){
		//駒の管理番号と、位置をキャッシュ
		KomaCache = cache;
		detailOn = true;
	}


}

