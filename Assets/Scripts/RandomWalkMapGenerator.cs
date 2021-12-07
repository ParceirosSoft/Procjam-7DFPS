using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkMapGenerator :MonoBehaviour
{
    // A grid that represents a map.
    // INT code : 0 -> non walkable tile
    //            1 -> walkable tile
    private Grid gridMap;

    [SerializeField] private int mapWidth = 0;
    [SerializeField] private int mapHeight = 0;
    [SerializeField] private int maxRandomWalkIteractions = 0;
    [SerializeField] private GameObject groundPrefab = null;
    private void Start()
    {
        gridMap = new Grid(mapWidth , mapHeight , 1f);

        GenerateMap( );
    }

    private void GenerateMap()
    {
        Vector2Int _startPoint = Vector2Int.zero;
        Vector2Int _auxPoint = Vector2Int.zero;

        //// Define the first point
        _startPoint.x = Random.Range(1,mapWidth);
        _startPoint.y = Random.Range(1,mapHeight);
        // Set the start point tile 
       // gridMap.SetValue(_startPoint.x , _startPoint.y , 1);
        _auxPoint = _startPoint;

        Debug.Log(_auxPoint);
        for (int i = 0; i < maxRandomWalkIteractions; i++)
        {
            while (ValidadeWalk(_auxPoint) == false)
            {

                // Get the new random point
                _auxPoint = RandomWalk(_auxPoint);
                Debug.Log(ValidadeWalk(_auxPoint));
            }
            //_auxPoint = RandomWalk(_auxPoint);
            // Mark in the grid the new point as a walkable tile
            gridMap.SetValue(_auxPoint.x , _auxPoint.y , 1);
            InstantiateTile(_auxPoint);
        }
    }

    private Vector2Int RandomWalk(Vector2Int referenceCell)
    {
        int _randomX = Random.Range(-1 , 2);
        int _randomY = Random.Range(-1 , 2);
        Debug.Log("X: " + _randomX + " Y: " + _randomY);
        return new Vector2Int(referenceCell.x + _randomX , referenceCell.y + _randomY);
    }

    private void InstantiateTile(Vector2 XZPos)
    {

        GameObject _go = GameObject.Instantiate(groundPrefab,transform);
        _go.transform.position = gridMap.GetCellWorldPositionXZPlane(XZPos);
        _go.transform.localScale = Vector3.one * gridMap.CellSize ;
       // _go.transform.SetParent(this.transform);
    }

    private bool ValidadeWalk(Vector2Int input)
    {
        return input.x <= mapWidth && input.y <= mapHeight && gridMap.GetValue(input.x , input.y) != 1;
    }
}
