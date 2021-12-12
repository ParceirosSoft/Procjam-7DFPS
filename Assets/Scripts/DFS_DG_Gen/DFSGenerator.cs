using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFSGenerator :MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] doorsStatus = new bool[4];
    }
    public Vector2 size;
    public GameObject PlayerPrefab;
    List<Cell> board;

    public int startPos = 0;
    public GameObject Room;
    public Vector2 offset;

    private bool firstRoomGenerated = false;

    void Start()
    {
        MazeGenerator( );
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateDungeon()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Cell currentCell = board[Mathf.FloorToInt(x + y * size.x)];
                if (currentCell.visited)
                {
                    //if (firstRoomGenerated == false)
                    //{
                    //    Instantiate(PlayerPrefab , new Vector3(( x * offset.x ) / 2 , 1f , ( -y * offset.y ) / 2) , Quaternion.identity);
                    //    firstRoomGenerated = true;
                    //}
                    var newRoom = Instantiate(Room , new Vector3(x * offset.x , 0 , -y * offset.y) , Quaternion.identity , transform).GetComponent<RoomsBehaviour>( );
                    newRoom.UpdateRoom(currentCell.doorsStatus);
                    newRoom.name = " " + x + " - " + y;


                }

            }
        }

    }
    private void MazeGenerator()
    {
        board = new List<Cell>( );

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                board.Add(new Cell( ));
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>( );
        int k = 0; // para forcar a parada do loop

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            // Define qual vai ser a ultima sala;
            if (currentCell == board.Count - 1)
                break;


            List<int> neighbours = CheckNeighbours(currentCell);

            if (neighbours.Count == 0)
            {
                // fim do algoritmo
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop( );
                }
            }
            else
            {
                path.Push(currentCell);
                int newCell = neighbours[Random.Range(0 , neighbours.Count)];

                if (newCell > currentCell)
                {
                    //down or right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].doorsStatus[2] = true;
                        currentCell = newCell;
                        board[currentCell].doorsStatus[3] = true;
                    }
                    else
                    {
                        board[currentCell].doorsStatus[1] = true;
                        currentCell = newCell;
                        board[currentCell].doorsStatus[0] = true;
                    }
                }
                else
                {
                    //up or left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].doorsStatus[3] = true;
                        currentCell = newCell;
                        board[currentCell].doorsStatus[2] = true;
                    }
                    else
                    {
                        board[currentCell].doorsStatus[0] = true;
                        currentCell = newCell;
                        board[currentCell].doorsStatus[1] = true;
                    }

                }

            }
        }
        GenerateDungeon( );


    }
    private List<int> CheckNeighbours(int cell)
    {

        List<int> _neighbours = new List<int>( );
        if (cell - size.x >= 0 && board[Mathf.FloorToInt(cell - size.x)].visited == false)
        {
            _neighbours.Add(Mathf.FloorToInt(cell - size.x));
        }

        if (cell + size.x < board.Count && board[Mathf.FloorToInt(cell + size.x)].visited == false)
        {
            _neighbours.Add(Mathf.FloorToInt(cell + size.x));
        }

        if (( cell + 1 ) % size.x != 0 && board[Mathf.FloorToInt(cell + 1)].visited == false)
        {
            _neighbours.Add(Mathf.FloorToInt(cell + 1));
        }

        if (cell % size.x != 0 && board[Mathf.FloorToInt(cell - 1)].visited == false)
        {
            _neighbours.Add(Mathf.FloorToInt(cell - 1));
        }

        return _neighbours;
    }
}
