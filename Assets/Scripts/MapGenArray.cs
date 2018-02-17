using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenArray : MonoBehaviour {

    public Transform hexLand;
    public Transform hexWater;
    public Transform hexHabour;
    public Transform hex;

    public int tilesLand = 70;
    public int tilesWater = 56;

    public int placedLand = 0;
    public int placedWater = 0;

    public int gridWidth = 14;
    public int gridHeight = 9;

    public int numberIslands = 12;

    static float hexWidth = 1.712f;
    static float hexHeight = 2.0f;
    static float gab = 0.1f;

    Vector3 startPos;

    int[] island;
    int[,] map;

    public void Genration()
    {
        Clear();
        SetMapSize();
        CreateIsland();
        CreateMap();
        InputIsland();
        AddGap();
        CalcStartPos();
        CreateGrid();
    }

    public void Clear()
    {
        hexWidth = 1.712f;
        hexHeight = 2.0f;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void AddGap()
    {
        hexWidth += hexWidth * gab;
        hexHeight += hexHeight * gab;

    }

    void CalcStartPos()
    {
        float offset = 0;
        if (gridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (gridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (gridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float x = 0;
        float z = 0;

        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        x = startPos.x + gridPos.x * hexWidth + offset;
        z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    void SetMapSize()
    {
        map = new int[gridWidth, gridHeight];
    }

    void CreateIsland()
    {
        while (placedLand != tilesLand)
        {
        placedLand = 0;
        island = new int[numberIslands];
        for (int i = 0; i < numberIslands; i++)
            {
                int b = Random.Range(1, 11);
                island[i] = b;
                placedLand += b;
            }
        }
    }

    void CreateMap()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                map[x, y] = 0;
            }
        }
    }

   void InputIsland()
    {
        for (int i = 0; i < 1; i++)
        {
            for (int ii = 0; ii < island[1]; ii++)
            {
                if(map[1,1] == 0)
                {
                    int rnd = Random.Range(1, 6);
                    for (int r = 0 ; r < rnd; rnd++)
                        {
                                for (int y = 0; y < gridHeight; y++)
                                {
                                    for (int x = 0; x < gridWidth; x++)
                                {
                            }
                        }
                    }
                }
            }
        }
    }

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (map[x,y] == 1)
                {
                    hex = Instantiate(hexLand);
                }
                else
                {
                    hex = Instantiate(hexWater);
                }
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }
}
