using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject prefabTile;
    public int quantityInitialTiles = 2;
    private float sizeTileZ = 1.0f;
    public float distanceGenerate = -100.0f; // Distância entre a última posição gerada e a nova geração
    public int maxTiles = 4; // Número máximo de cubos antes de começar a remover

    public Transform playerTransform; // Referência ao transform do jogador
    private float lastPositionGenerate = 0.0f;

    private void Start()
    {
        sizeTileZ = prefabTile.transform.localScale.z;
        GenerateTile();
    }

    private void Update()
    {
        GenerateTileUpdate();
    }

    private void GenerateTile()
    {
        for (int i = 0; i < quantityInitialTiles; i++)
        {
            GenerateNextTile();
        }
    }

    private void GenerateTileUpdate()
    {
        // Verifique se é necessário gerar um novo cubo
        if (playerTransform.position.z - lastPositionGenerate > distanceGenerate)
        {
            GenerateNextTile();

            // Remova os cubos mais antigos se atingir o número máximo
            if (transform.childCount > maxTiles)
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }

    private void GenerateNextTile()
    {
        float newPos = lastPositionGenerate + sizeTileZ;
        float newYPos = Mathf.PerlinNoise(newPos * 0.1f, 0) * 2;

        GameObject novoCubo = Instantiate(prefabTile, new Vector3(0, newYPos, newPos), Quaternion.identity);
        novoCubo.transform.parent = transform;

        lastPositionGenerate = newPos;
    }
}
