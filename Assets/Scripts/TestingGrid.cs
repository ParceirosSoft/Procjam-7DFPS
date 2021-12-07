using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGrid :MonoBehaviour
{
    private Grid grid;
    void Start()
    {
        grid = new Grid(2 , 2 , 2f);

        if (grid == null)
            return;
        Gizmos.color = Color.green;
        for (int i = 0; i < grid.Height; i++)
        {
            for (int j = 0; j < grid.Width; j++)
            {
                //Gizmos.DrawSphere(grid.GetCellWorldPositionXYPlane(i,j),grid.CellSize);
               var _primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
               _primitive.transform.position = grid.GetCellWorldPositionXZPlane(i,j);
               _primitive.transform.localScale = Vector3.one* grid.CellSize;
            }
        }
    }

}
