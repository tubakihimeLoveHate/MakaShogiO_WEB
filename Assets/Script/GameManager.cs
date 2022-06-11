//#define TestOut //有効にすると自分ターンに相手コマを触れなくなる
//#define TestCode //テストしたいコードはこの中にかく
//#define CPU_CPU_MODE
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//画面遷移系の制御だけ行う
public class GameManager : MonoBehaviour {
	//全体的にUIに依存しすぎ、ゆくゆくは変えていく必要あり←デバイスごとに大きさを変えることで回避もしくはpivotを設定し直す
	//Deckmanager deck;
	//PeiceMST peicemst;
	//public GameObject data;

	//Player player = new Player();
	//テスト用プレイヤーのリスト、デッキの初期化
	//public bool playerFirstInit= false;


	//<駒に関するデータ>
	/* 
	Button komaOrigine; 

	//初期化用
	int preCache = 0;//prefabの添字用
	int deckCnt = 0;
	
	public Button[] Komas = new Button[40];//全体のprefab管理

	//<移動先ナビゲートUI関係>
	//移動可能先を示すボタンPrefab
	Button navigateOrigine;
	*/
	public RectTransform ImageTransform;
	//public Transform PanelsTransform;
	//public RectTransform banContents1;
	//public RectTransform banContents2;
	bool battleIn = false;

	public Transform banTransform;
	public Image selectEfectForm;
	/* 
	public Button Yes_Button;
	public Button No_Button;

	public Text Result_text;
	int[] bratsh = new int[10];//ボタンefect発動待ちキャッシュ
	string charTagCache;

*/
	public Button Button_Detail;

	//<駒詳細画面関係>

	//public GameObject detail;
	//DetailView detailView;
	
	//敵駒リスト,別にクラスも受けた方が良い
	//public List<CharPrefab> EnemyList = new List<CharPrefab>();
	//public List<PeiceStatus> EnemyList = new List<PeiceStatus>();


	BattleManager battle;
	

	//trueの時手前（プレイや−１の番）falseはプレイヤー２（奥側）
	bool PlayerSwitch = true;

	/// <summary>
	/// Topメニュー関係
	/// </summary>
	public Image Top_Image;

	//デッキセレクト画面へ
	public Image Deck_Select;
	//バトル画面へ
	public Image battleForm;

	//ショップ画面
	public Image shopForm;
	
	//ロード関係
	public GameObject loadObject;
	private LoadAnimation loadanimation;

	//チェック用
	int tapCount = 0;

	//サイドメニュー
	public GameObject sidemenu;
	private Animator sideAnimator;
	public Button returnShodow;

	//設定詳細ダイアログ
	public Animator daiaLogAnimator;
	public Transform daiaLog;

	
	
	void Start () {

		//画面初期化
		Button_Detail.gameObject.SetActive(false);
		Deck_Select.gameObject.SetActive(false);
		shopForm.gameObject.SetActive(false);
		//settingForm.gameObject.SetActive(false);
		
		//ロード
		loadanimation = loadObject.GetComponent<LoadAnimation>();

		battle = battleForm.GetComponent<BattleManager>();
		//バトル関係
		/* 
		komaOrigine = Resources.Load<Button>("Plefab/koma");
		navigateOrigine = Resources.Load<Button>("Plefab/MoveButton");
*/
		//deck = new Deckmanager();
		//deck.AllAdd();	//全カードデータ読み込み
		//peicemst = new PeiceMST();

		//効果発動許可画面の設定
		selectEfectForm.transform.localPosition = banTransform.transform.localPosition;
		selectEfectForm.gameObject.SetActive(false);

		//detailView = detail.GetComponent<DetailView>();

		if(Player.newAccount)Player.firstInit();

		//サイドメニュー
		sideAnimator = sidemenu.GetComponent<Animator>();

		
		/* もうidだけで良いかと
		foreach(int i in enemyTable){
			//EnemyList.Add(deck.PrivateLoad(i));
			EnemyList.Add(peicemst.getBaseObject(i));
		}*/
		//battleForm.gameObject.SetActive(battleIn);

	}
	
	// Update is called once per frame
	void Update () {
		//if(battleIn){
			

	}

#if TestCode
#else
#endif


	//対局画面へ遷移
	public void OnTabBattle(int i){
		/* 
		List<CharPrefab> list;
		if(i==0)list = Player.mydeck;
		else list = Player.classicDeck;
		loadanimation.LoadStart();
		Top_Image.gameObject.SetActive(false);
		battleIn = true;
		battleForm.gameObject.SetActive(battleIn);
			//menueBar.gameObject.SetActive(false);
		//この関数で対局画面をセットする
		charTagCache = "my";
		PrefabSet(list);
		deckCnt = 0;
		charTagCache = "ene";
		PrefabSet(list);
		//戻る時はデストロイ忘れずに
		//loadanimation.loadSection = false;
		loadanimation.LoadEnd();
		*/
#if CPU_CPU_MODE
		battleForm.gameObject.SetActive(true);
		Top_Image.gameObject.SetActive(false);
#else
		if(InvalidTwoTap())StartCoroutine(LoadCoroutine(i));
#endif
	}

	//TODO:ここはLoad画面についての制御だけ行う
	private IEnumerator LoadCoroutine(int i){
		int[] list;
		//bool wait = false;
		if(i==0)list = Player.mydeck;
		else list = Player.classicDeck;
		loadanimation.LoadStart();
		//battleがアクティブになった時にStartで、駒セット
		battleIn = true;
		battleForm.gameObject.SetActive(battleIn);
			//menueBar.gameObject.SetActive(false);
		//直す必要ありとりあえず
		yield return new WaitForSeconds(2.0f);
		Top_Image.gameObject.SetActive(false);
		//戻る時はデストロイ忘れずに
		//loadanimation.loadSection = false;
		yield return new WaitForSeconds(2.0f);
		loadanimation.LoadEnd();
		battle.TurnChange();
	}

	//sidemenuに変更
	public void OnTabSetting(){
		if(InvalidTwoTap()){
			//settingForm.gameObject.SetActive(true);
			//settingForm.transform.localPosition = Top_Image.transform.localPosition;
		}
	}

	//ガチャ画面へ遷移
	public void OnTapShop(){
		if(InvalidTwoTap()){
			shopForm.gameObject.SetActive(true);
			shopForm.transform.localPosition = Top_Image.transform.localPosition;
		}
	}

	
	public void OnTabDeckSelect(){
		//loadanimation.start = true;
		if(InvalidTwoTap()){
			Deck_Select.gameObject.SetActive(true);
			Deck_Select.transform.localPosition = Top_Image.transform.localPosition;
			Top_Image.gameObject.SetActive(false);
			DeckMenuManeger.deckMenuIn = true;
		}
	}

	public bool InvalidTwoTap(){
		if(tapCount == 0){
			tapCount += 1;
			return true;
		}
		return false;
	}

	public void OnSideMenuEnter(){
		StartCoroutine(SideIO("enter"));	
	}


    public void OnSideMenuReturn() {
		StartCoroutine(SideIO("return"));
	}

    public IEnumerator SideIO(string param){
		sideAnimator.SetTrigger(param);
		if(param == "return"){
			returnShodow.gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(0.6f);
		if(param == "enter"){
			returnShodow.gameObject.SetActive(true);
			
		}
	}
	public void OnOpenDetailDiaLog(/*int scopeNum*/){
		daiaLog.localPosition = Vector2.zero;
		daiaLog.gameObject.SetActive(true);
		daiaLogAnimator.SetTrigger("opendialog");
	}

}
