using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGeneration :MonoBehaviour
{
    [SerializeField] private GameObject HorizontalRoomsPrefab;
    [SerializeField] private GameObject VerticalRoomsPrefab;
    [SerializeField] private GameObject ConexionRoomsPrefab;
    [SerializeField] private Vector2Int MapDimensions;

    // 1 - Horizontal
    // 2 - Vertical
    // 3 - Conexion
    private Grid map;



    private void Start()
    {
        map = new Grid(MapDimensions.x , MapDimensions.y , 2f);
        InitilizeMap();
        DrawMap();
    }

    public void InitilizeMap()
    {

        float _rand = 0f;
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                _rand = Random.Range(0f , 1.3f);
                if (_rand > 1f)
                {
                    map.SetValue(x , y , 3);
                    _rand = 0f;
                }
                else if (_rand >= 0.5f)
                {
                    map.SetValue(x , y , 1);
                    _rand = 0f;
                }
                else
                {
                    map.SetValue(x , y , 2);
                    _rand = 0f;

                }
            }
        }

    }

    public void DrawMap()
    {
        GameObject _clone = null;
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map.GetValue(x , y) == 1)
                {
                    _clone = GameObject.Instantiate(HorizontalRoomsPrefab , map.GetCellWorldPositionXZPlane(x , y) , HorizontalRoomsPrefab.transform.rotation , transform);
                }
                if (map.GetValue(x , y) == 2)
                {
                    _clone = GameObject.Instantiate(VerticalRoomsPrefab , map.GetCellWorldPositionXZPlane(x , y) , VerticalRoomsPrefab.transform.rotation , transform);
                }
                if (map.GetValue(x , y) == 3)
                {
                    _clone = GameObject.Instantiate(ConexionRoomsPrefab , map.GetCellWorldPositionXZPlane(x , y) , ConexionRoomsPrefab.transform.rotation , transform);
                }
            }
        }

    }



}
