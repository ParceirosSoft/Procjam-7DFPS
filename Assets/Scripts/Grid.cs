using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    public int Height => height;
    public int Width => width;
    public float CellSize => cellSize;

    private int height;
    private int width;
    private float cellSize;

    private int[,] gridArray;

    public Grid(int width , int height , float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;

        gridArray = new int[height , width];
    }

    public int GetValue(int xIndex , int yIndex) { return gridArray[xIndex , yIndex]; }
    public void SetValue(int xIndex , int yIndex , int value)
    {
        gridArray[xIndex , yIndex] = value;
    }

    public Vector3 GetCellWorldPositionXYPlane(Vector2 XYPoint)
    {
        return new Vector3(XYPoint.x * cellSize , XYPoint.y * cellSize , 0f);
    }

    public Vector3 GetCellWorldPositionXYPlane(int xIndex , int yIndex)
    {
        return new Vector3(xIndex * cellSize , yIndex * cellSize , 0f);
    }


    public Vector3 GetCellWorldPositionXZPlane(Vector2 XZPoint)
    {
        return new Vector3(XZPoint.x * cellSize , 0f , XZPoint.y * cellSize);
    }
    public Vector3 GetCellWorldPositionXZPlane(int xIndex , int zIndex)
    {
        return new Vector3(xIndex * cellSize , 0f , zIndex * cellSize);
    }

    public int GetMooreNeiboursCount(int x, int y)
    {
        int count = 0;
        int neibourX = 0;
        int neibourY = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                neibourX = x + i;
                neibourY = y + j;

                // If we are looking to the start point, then dont do nothing
                if (i == 0 || j == 0) continue;
                   

                else if (neibourX < 0 || neibourY < 0 || neibourX >= gridArray.GetLength(0) || neibourY >= gridArray.GetLength(1))
                {
                    count++;
                }

                else if (gridArray[neibourX , neibourY] != 0)
                    count++;

            }
        }

        return count;
    }



}
