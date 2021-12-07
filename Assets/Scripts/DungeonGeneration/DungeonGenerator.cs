using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator :MonoBehaviour
{
    public GameObject PrefabWall;
    public GameObject PrefabGround;
    public Transform parentEnviroment;

    public int MaxInteractions = 20;

    // 0 para parede
    // 1 para chao
    // 8 para agente
    private Grid map;

    private float changeDirChance = 0.1f;
    private Directions currentAgentDir = Directions.UP;
    private void Start()
    {
        int _posX = 0;
        int _posY = 0;
        Vector2Int _newDirection = new Vector2Int( );
        map = new Grid(10 , 10 , 1f);
        // Posiciona o agente em uma posição aleatoria
        _posX = Random.Range(0 , map.Width);
        _posY = Random.Range(0 , map.Height);
        map.SetValue(_posX , _posY , 8);
        for (int i = 0; i < MaxInteractions; i++)
        {
            // calcula a chance do agente mudar de direção
            if (Random.Range(0f , 1f) > changeDirChance)
            {
                //Se nao mudar, faca o agente andar para a direcao que ele ja estava
                Move(ref _posX , ref _posY);
                // incrementa a chance de mudar de direcao para facilitar na proxima vez
                changeDirChance += 0.1f;
            }
            else //Se mudar, faca o agente andar para a nova direcao
            {
                currentAgentDir = (Directions)Random.Range(0 , 3);
                Move(ref _posX , ref _posY);
                changeDirChance = 0.1f;
            }
        }


        DrawMap( );

    }

    private void DrawMap()
    {
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                if (map.GetValue(x , y) == 0)
                {
                    var CloneGO = Instantiate(PrefabWall , map.GetCellWorldPositionXZPlane(x , y) , Quaternion.identity , parentEnviroment);
                }
                else if (map.GetValue(x , y) == 1)
                {
                    var CloneGO = Instantiate(PrefabGround , map.GetCellWorldPositionXZPlane(x , y) , Quaternion.identity , parentEnviroment);
                }
            }
        }
    }

    private void Move(ref int _posX , ref int _posY)
    {
        Vector2Int _newDirection = MoveTowardsDirection( );
        _newDirection.x += _posX;
        _newDirection.y += _posY;
        if (_newDirection.x < map.Width && _posY < _newDirection.y && _newDirection.x >= 0 && _newDirection.y >= 0)
        {
            map.SetValue(_posX , _posY , 1);
            map.SetValue(_newDirection.x , _newDirection.y , 8);
            _posX = _newDirection.x;
            _posY = _newDirection.y;
            _newDirection = Vector2Int.zero;
        }
    }

    private Vector2Int MoveTowardsDirection()
    {
        Vector2Int _direction = new Vector2Int( );
        switch (currentAgentDir)
        {
            case Directions.UP:
            _direction.x = 0;
            _direction.y = 1;
            break;

            case Directions.DOWN:
            _direction.x = 0;
            _direction.y = -1;
            break;

            case Directions.LEFT:
            _direction.x = -1;
            _direction.y = 0;
            break;

            case Directions.RIGHT:
            _direction.x = 1;
            _direction.y = 0;
            break;
        }

        return _direction;

    }


}

public enum Directions
{
    UP = 0,
    DOWN = 1,
    LEFT = 2,
    RIGHT = 3
}
