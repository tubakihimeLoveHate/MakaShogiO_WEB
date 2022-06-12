using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    public static class CommonCalcuration
    {

        public static void CalculateFieldPosition(Vector3 basePosition, Vector3 offsetPosition, ref Vector3[,] fieldPosition)
        {
            for (int v = 0; v < 9; v++)
            {
                for (int h = 0; h < 9; h++)
                {
                    fieldPosition[v, h] = new Vector3(basePosition.x + offsetPosition.x * h, basePosition.y - offsetPosition.y * v, 0);
                }
            }
        }

    }
}
