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

	//<Status以外はランタイム情報>
	public int runtimeId;

	public BaseObject status;

	public bool isEfect = false;
	public bool isBench = false;

	//現在地
	public int h;
	public int v;

	Text text;

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
		text = gameObject.GetComponentInChildren<Text>();
		text.text = status.PeiceName;
		ChangeColor();
	}

	public void ChangeColor()
	{
		text.color = isEfect ? Color.red : Color.black;
	}


	public void Move_Pulus (int[] m) {

		for (int y = 0; y < 8; y++) {
			status.move[y] = status.move[y] + m[y];
			if (status.move[y] > 8)　 status.move[y] = 8;
		}
		status.EfectNumber = status.Id;
		status.EfectType = BaseObject.ET.None;
		isEfect = true;
	}

	public void CreateNavigation()
	{
		if(gameObject.tag != BattleManager.instance.turn)
		{
			return;
		}
		if (isBench)
		{
			CreateNavigationByBench();
		}
		else
		{
			CreateNavigationByField();
		}
	}

	/// <summary>
	/// 駒選択時の移動範囲決定-駒のPointerUpで呼び出し
	/// </summary>
	void CreateNavigationByField()
	{
		int calcNum = (gameObject.tag == "player1") ? 1 : -1;
		FieldInfo.DestroyPanels();

		if (status.Marchingtype == BaseObject.MT.front)
		{

			for (int i = 0; i < 8; i++)
			{ //方角
				bool permissionOrverrap = true;
				int coverCount = 0;
				if (status.move[i] == 0)
				{
					continue;
				}
				for (int o = 1; o < status.move[i] + 1; o++)
				{                   //1方向の移動範囲
					int _v = v , _h = h;
					if (i == 0) { _v = v - o * calcNum; _h = h; }
					if (i == 1) { _v = v - o * calcNum; _h = h + o * calcNum; }
					if (i == 2) { _v = v; _h = h + o * calcNum; }
					if (i == 3) { _v = v + o * calcNum; _h = h + o * calcNum; }
					if (i == 4) { _v = v + o * calcNum; _h = h; }
					if (i == 5) { _v = v + o * calcNum; _h = h - o * calcNum; }
					if (i == 6) { _v = v; _h = h - o * calcNum; }
					if (i == 7) { _v = v - o * calcNum; _h = h - o * calcNum; }
					permissionOrverrap = VectorCheck(_v, _h, ref coverCount);

					if (permissionOrverrap == false || permissionOrverrap && coverCount > 1) break;
					FieldInfo.instance.CreatePanel(_v, _h, runtimeId, "move");
				}
			}
		}
		else
		{
			for (int i = 0; i < 8; i++)
			{//方がく
				int coverCount = 0;

				if (status.move[i] == 0) continue;//Debug.Log("距離が０のため次の方角へ");

				if (i == 0) VectorCheck(v - status.move[i] * calcNum, h, ref coverCount);
				if (i == 1) VectorCheck(v - status.move[i] * calcNum, h + calcNum, ref coverCount);
				if (i == 2) VectorCheck(v, h + status.move[i] * calcNum, ref coverCount);
				if (i == 3) VectorCheck(v + status.move[i] * calcNum, h + calcNum, ref coverCount);
				if (i == 4) VectorCheck(v + status.move[i] * calcNum, h, ref coverCount);
				if (i == 5) VectorCheck(v + status.move[i] * calcNum, h - calcNum, ref coverCount);
				if (i == 6) VectorCheck(v, h - status.move[i] * calcNum, ref coverCount);
				if (i == 7) VectorCheck(v - status.move[i] * calcNum, h - calcNum, ref coverCount);

			}
		}
		
	}

	/// <summary>
	/// 範囲チェック＋重複チェック
	/// </summary>
	/// <param name="v">moved</param>
	/// <param name="h">moved</param>
	/// <returns></returns>
	public bool VectorCheck(int v, int h, ref int coverCount)
	{

		bool permisson = true;
		if (v < 0 || h < 0 || v > 8 || h > 8) return false;

		FieldCell targetcell = FieldInfo.instance.fieldCell[v, h];

		if (targetcell.cellStatus != CellStatus.EMPTY)
		{
			if (BattleManager.instance.allKomas[targetcell.komaId].gameObject.tag == gameObject.tag)
			{
				return false;   //Debug.Log("自分のコマと被っているためfalse");
			}
			else
			{
				//Debug.Log("敵と被っているので許容");
				coverCount++;
			}

		}

		return permisson;
	}

	void CreateNavigationByBench()
	{
		FieldInfo.DestroyPanels();
		for (int v = 0; v < 9; v++)
		{
			for (int h = 0; h < 9; h++)
			{
				if (FieldInfo.instance.fieldCell[v, h].cellStatus == CellStatus.EMPTY)
				{
					FieldInfo.instance.CreatePanel(v, h, status.Id, "entry");
				}
			}
		}
	}

}