using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static common.CommonType;

public class FieldCell
{
    public Vector3 cellPosition { get; set; }
    public CellStatus cellStatus { get; set; } = CellStatus.EMPTY;
    public int komaId { get; set; }

}
    
