using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static common.CommonType;
using static common.CommonCalcuration;
using System;

public class FieldInfo : MonoBehaviour
{
    public static FieldInfo instance;

    [SerializeField]
    private Vector3 basePosition;
    [SerializeField]
    private Vector3 offsetPosition;
    [SerializeField]
    public Transform fieldTransform;

    public Vector3[,] fieldPosition = new Vector3[9, 9];
    public CellStatus[,] cellStatus { get; set; } = new CellStatus[9, 9];

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
        //ƒ|ƒfƒBƒVƒ‡ƒ“Ši”[
        CalculateFieldPosition(basePosition, offsetPosition, ref fieldPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyPanels()
    {
        var clones = GameObject.FindGameObjectsWithTag("panels");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

}
