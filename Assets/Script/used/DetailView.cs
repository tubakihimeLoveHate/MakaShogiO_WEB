using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DetailView :MonoBehaviour{

	public static DetailView instance;
	private bool isPressDown = false;
	[SerializeField]
	private float pressTime = 1.0f;
	[SerializeField]
	private Button detailDialog;
	private PeiceStatus selectedPeiceStatus;
	private Coroutine pressCorutine;

	[SerializeField]
	private Button closeButton;
	[SerializeField]
	private Text nameText;
	[SerializeField]
	private Text[] amountMovement = new Text[8];
	[SerializeField]
	private Text rank;
	[SerializeField]
	private Text skillDescription;
	[SerializeField]
	private Text rub1;
	[SerializeField]
	private Text rub2;
	[SerializeField]
	private Button whiteChange,redChenge;
	bool isWhiteStatus = true;

	PeiceMST peicemst;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start () {
		peicemst = new PeiceMST();
	}
	

	public void PointerDown(PeiceStatus peiceStatus)
	{
		Debug.Log("Press Start");
		selectedPeiceStatus = peiceStatus;
		if(peiceStatus.isEfect)
		{
			isWhiteStatus = false;
		}
		if (pressCorutine != null)
		{
			StopCoroutine(pressCorutine);
		}
		pressCorutine = StartCoroutine(TimeForPointerDown());
		
	}

	IEnumerator TimeForPointerDown()
	{
		//プレス開始
		isPressDown = true;

		//待機時間
		yield return new WaitForSeconds(pressTime);

		//押されたままなら長押しの挙動
		if (isPressDown)
		{
			DisplayDetailView();
			ChangeText();
		}
		//プレス処理終了
		isPressDown = false;
	}

	public void PointerUp()
	{
		if (isPressDown)
		{
			isPressDown = false;

			//お好みの短押し時の挙動をここに書く(無い場合は書かなくても良い)

		}
	}


	public void ChangeText(){
		BaseObject status = (isWhiteStatus) ? Instantiate(selectedPeiceStatus.status)
			: Instantiate(peicemst.getBaseObject(selectedPeiceStatus.status.EfectNumber));
		nameText.text = status.PeiceName;
		foreach (var text in status.move.Select((value, index) => new { value, index }))
		{
			amountMovement[text.index].text = text.value.ToString();
		}
		rank.text = status.Rank.ToString();
		rub1.text = status.Rub1;
		rub2.text = status.Rub2;
		skillDescription.text = status.Efect;
		if (isWhiteStatus)
		{
			whiteChange.interactable = false;
			redChenge.interactable = true;
		}
		else
		{
			whiteChange.interactable = true;
			redChenge.interactable = false;
		}
		
	}

	public void OnPushWhite()
	{
		if (isWhiteStatus)
		{
			return;
		}
		isWhiteStatus = true;

		ChangeText();
	}

	public void OnPushRed()
	{
		if (!isWhiteStatus)
		{
			return;
		}
		isWhiteStatus = false;

		ChangeText();
	}

	void SwitchInteractable()
	{
		whiteChange.interactable = !whiteChange.interactable;
		redChenge.interactable = !redChenge.interactable;
	}
 

	public void DisplayDetailView(){
		if (selectedPeiceStatus != null)
		{
			detailDialog.gameObject.SetActive(true);
			isPressDown = false;
		}
	}

	public void HiddenDetailView()
	{
		detailDialog.gameObject.SetActive(false);
		selectedPeiceStatus = null;
	}

}
