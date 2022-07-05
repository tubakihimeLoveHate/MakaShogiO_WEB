using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static common.CommonType;

/// <summary>
/// 駒の対戦時、保持ステータス(駒にアタッチ)
/// </summary>
public class PeiceStatus : MonoBehaviour {

	//<駒詳細画面関係>


	public int runtimeId;

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

	/// <summary>
	/// 駒選択時の移動範囲決定-駒のPointerUpで呼び出し
	/// </summary>
	public void createPanel()
	{
		int calcNum = (gameObject.tag == "player1") ? 1 : -1;
		bool permissionFlg = true;

		FieldInfo.instance.DestroyPanels();

		if (isBench)
		{//ベンチのこまをタッチした時の処理

			for (int v = 0; v < 9; v++)
			{
				for (int h = 0; h < 9; h++)
				{
					if (FieldInfo.instance.cellStatus[v, h] == CellStatus.EMPTY)
					{
						//panelCreate(v, h, status.Id);
					}
				}
			}

		}
		else
		{

			if (status.Marchingtype == BaseObject.MT.front)
			{

				for (int i = 0; i < 8; i++)
				{ //方角
					if (status.move[i] == 0)
					{
						continue;
					}
					for (int o = 1; o < status.move[i] + 1; o++)
					{                   //1方向の移動範囲
						try
						{
							if (i == 0) permissionFlg = VectorCheck(v - o * calcNum, h);
							if (i == 1) permissionFlg = VectorCheck(v - o * calcNum, h + o * calcNum);
							if (i == 2) permissionFlg = VectorCheck(v, h + o * calcNum);
							if (i == 3) permissionFlg = VectorCheck(v + o * calcNum, h + o * calcNum);
							if (i == 4) permissionFlg = VectorCheck(v + o * calcNum, h);
							if (i == 5) permissionFlg = VectorCheck(v + o * calcNum, h - o * calcNum);
							if (i == 6) permissionFlg = VectorCheck(v, h - o * calcNum);
							if (i == 7) permissionFlg = VectorCheck(v - o * calcNum, h - o * calcNum);

							if (permissionFlg == false) break;

						}
						catch
						{
							//Debug.Log("index範囲外のため例外処理");
							continue;
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < 8; i++)
				{//方がく

					if (status.move[i] == 0) continue;//Debug.Log("距離が０のため次の方角へ");
					try
					{
						if (i == 0) VectorCheck(v - status.move[i] * calcNum, h);
						if (i == 1) VectorCheck(v - status.move[i] * calcNum, h + calcNum);
						if (i == 2) VectorCheck(v, h + status.move[i] * calcNum);
						if (i == 3) VectorCheck(v + status.move[i] * calcNum, h + calcNum);
						if (i == 4) VectorCheck(v + status.move[i] * calcNum, h);
						if (i == 5) VectorCheck(v + status.move[i] * calcNum, h - calcNum);
						if (i == 6) VectorCheck(v, h - status.move[i] * calcNum);
						if (i == 7) VectorCheck(v - status.move[i] * calcNum, h - calcNum);
					}
					catch
					{
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
	/// <returns></returns>
	public bool VectorCheck(int v, int h)
	{

		CellStatus check;
		//繰り返しでない？
		int coverCnt = 0;
		bool permisson = true;

		if (FieldInfo.instance.cellStatus[v, h] != CellStatus.EMPTY)
		{
			if (BattleManager.instance.allKomas[runtimeId].gameObject.tag != gameObject.tag && coverCnt == 0)
			{
				coverCnt = 1;
				permisson = true;   //Debug.Log("敵と被っているが一度めなので許容");
			}
			else permisson = false; //Debug.Log("自分のコマと被っているためfalse");
		}

		if (permisson)
		{
			//panelCreate(v, h);
		}
		return permisson;
	}

}