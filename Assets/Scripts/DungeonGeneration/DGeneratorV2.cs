using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DGeneratorV2 : MonoBehaviour
{
    public static DGeneratorV2 instance;

    public int[] possibilidades; //dostiles

    public Tile tileAnterior;
    public Tile[] tiles;
    public int tamanhoDaMatriz;

    private List<Tile> tilesInstanciados = new List<Tile>();

    float linePosTileAtual;
    float columnPosTileAtual;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        StartCoroutine(PrepararCenario());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PrepararCenario()
    {
        for (int i = 0; i < tamanhoDaMatriz; i++) //coluna
        {
            columnPosTileAtual = tiles[0].transform.localScale.z * (i + 1);
            for (int j = 0; j < tamanhoDaMatriz; j++) //linha
            {
                linePosTileAtual = tiles[0].transform.localScale.x * (j + 1);

                yield return null;
                SelecionarTilePossivel(linePosTileAtual, columnPosTileAtual, (j+1)*(i+1));
            }
        }
    }

    void SelecionarTilePossivel(float posX, float posY, int posNaMatriz)
    {
        var tileId = 0;
        do
        {
            tileId = Random.Range(0, tiles.Length);
        }

        while (!tiles[tileId].Possivel());

        var go = Instantiate(tiles[tileId], new Vector3(posX, 0, posY), Quaternion.identity, transform);

        tilesInstanciados.Add(go);

        if (posNaMatriz % (tamanhoDaMatriz) == 0 && posNaMatriz > tamanhoDaMatriz * 2)
        {
            Debug.Log("SS");
            tileAnterior = tilesInstanciados[posNaMatriz - tamanhoDaMatriz];
        }
        else
            tileAnterior = tiles[tileId];
    }
}
