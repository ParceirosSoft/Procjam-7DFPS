using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //permite conexao:
    //0 - esquerda direita cima baixo
    //1 - cima baixo ||
    //2 - esquerda direita =
    public int pos;

    private List<int> poss = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Possivel()
    {
        poss.Clear();
        for (int i = 0; i < DGeneratorV2.instance.possibilidades.Length; i++)
        {
            switch (pos)
            {
                case 0:
                    poss.Add(0);
                    poss.Add(1);
                    poss.Add(2);
                    break;
                case 1:
                    poss.Add(0);
                    //poss.Add(1);
                    break;
                case 2:
                    poss.Add(0);
                    //poss.Add(2);
                    break;
            }
        }

        if (poss.Contains(DGeneratorV2.instance.tileAnterior.pos))
            return true;

        return false;
    }
}
