using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private int rows = 5;

    [SerializeField]
    private int cols = 8;

    [SerializeField]
    private float tileSize = 1;

    [SerializeField]
    GameObject Tile;

   
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Instantiate(Tile, transform);

                float posX = col * tileSize;
                float posY = row * - tileSize;  //Unity uses a cartesian positioning system, going down to up

                Tile.transform.position = new Vector2(posX, posY);
            }
        }


        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
