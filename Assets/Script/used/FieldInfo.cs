using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static common.CommonType;
using static common.CommonCalcuration;
using System;
using UnityEngine.UI;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;

    [SerializeField]
    private Vector3 basePosition;
    [SerializeField]
    private Vector3 offsetPosition;
    [SerializeField]
    public Transform fieldTransform;

    public FieldCell[,] fieldCell { get; set; }
    
    [SerializeField]
    private Button navigationPanel;

    public readonly Tuple<int, int>[] player1InitialKomaPosition = new Tuple<int, int>[]{
        Tuple.Create(6,0),
        Tuple.Create(6,1),
        Tuple.Create(6,2),
        Tuple.Create(6,3),
        Tuple.Create(6,4),
        Tuple.Create(6,5),
        Tuple.Create(6,6),
        Tuple.Create(6,7),
        Tuple.Create(6,8),
        Tuple.Create(7,1),
        Tuple.Create(7,7),
        Tuple.Create(8,0),
        Tuple.Create(8,1),
        Tuple.Create(8,2),
        Tuple.Create(8,3),
        Tuple.Create(8,4),
        Tuple.Create(8,5),
        Tuple.Create(8,6),
        Tuple.Create(8,7),
        Tuple.Create(8,8)
    };

    public readonly Tuple<int, int>[] player2InitialKomaPosition = new Tuple<int, int>[]{
        Tuple.Create(2,0),
        Tuple.Create(2,1),
        Tuple.Create(2,2),
        Tuple.Create(2,3),
        Tuple.Create(2,4),
        Tuple.Create(2,5),
        Tuple.Create(2,6),
        Tuple.Create(2,7),
        Tuple.Create(2,8),
        Tuple.Create(1,7),
        Tuple.Create(1,1),
        Tuple.Create(0,0),
        Tuple.Create(0,1),
        Tuple.Create(0,2),
        Tuple.Create(0,3),
        Tuple.Create(0,4),
        Tuple.Create(0,5),
        Tuple.Create(0,6),
        Tuple.Create(0,7),
        Tuple.Create(0,8)
    };



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //ポディション格納
        fieldCell = new FieldCell[9, 9];
        CalculateFieldPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
	/// 実際にパネルを描画-駒のPointerUpに設定
	/// </summary>
	/// <param name="v"></param>
	/// <param name="h"></param>
	/// <param name="bint">全体の駒番号</param>
	public void CreatePanel(int v, int h, int runtimeId)
    {

        //移動先の変数
        int _v = v;
        int _h = h;
        int _runtimeId = runtimeId;

        Button navigatePanels;
        navigatePanels = Instantiate(navigationPanel, fieldTransform);
        navigatePanels.transform.SetParent(fieldTransform, false);
        navigatePanels.transform.localScale = Vector3.one;
        navigatePanels.transform.localPosition = fieldCell[v, h].cellPosition;
        navigatePanels.onClick.AddListener(() => BattleManager.instance.MoveByField(_v, _h, _runtimeId));
        navigatePanels.tag = "panels";
    }

    public static void DestroyPanels()
    {
        var clones = GameObject.FindGameObjectsWithTag("panels");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    public void CalculateFieldPosition()
    {
        for (int v = 0; v < 9; v++)
        {
            for (int h = 0; h < 9; h++)
            {
                fieldCell[v, h] = new FieldCell();
                fieldCell[v, h].cellPosition = new Vector3(basePosition.x + offsetPosition.x * h, basePosition.y - offsetPosition.y * v, 0);
            }
        }
    }


}
