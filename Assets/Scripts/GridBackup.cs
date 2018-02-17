using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBackup : MonoBehaviour {

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

    static float hexWidth = 1.712f;
    static float hexHeight = 2.0f;
    static float gab = 0.1f;

    Vector3 startPos;

    public void OnClick()
    {
        placedLand = 0;
        placedWater = 0;
        Clear();
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

    void CreateGrid()
    {
        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int r = Random.Range(0, 2);
                if (r == 1 || placedWater == tilesWater)
                {
                    hex = Instantiate(hexLand);
                    placedLand++;
                }
                else
                {
                    if ((placedWater - 6) % 7 == 1)
                    {
                        hex = Instantiate(hexHabour);
                    }
                    else
                    {
                        hex = Instantiate(hexWater);
                    }
                    placedWater++;
                }
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = this.transform;
                hex.name = "Hexagon" + x + "|" + y;
            }
        }
    }
}
