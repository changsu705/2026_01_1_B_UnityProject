using UnityEngine;
using System.Collections.Generic;

public class RankGameManager : MonoBehaviour
{
    public int gridWidth = 7;
    public int gridHeight = 7;
    public float cellSize = 1.3f;
    public GameObject cellPrefab;
    public Transform gridContainer;

    public GameObject rankPrefab;
    public Sprite[] rankSprites;
    public int MaxRankLevel = 7;

    public GridCell[,] grid;

   void InitializeGrid()
   {
        grid = new GridCell[gridWidth, gridHeight];
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(
                    x * cellSize - (gridWidth * cellSize / 2) + cellSize / 2,
                    y * cellSize - (gridWidth * cellSize / 2) + cellSize / 2,
                    1f
                );

                GameObject cellObj = Instantiate(cellPrefab, position, Quaternion.identity, gridContainer);
                GridCell cell = cellObj.GetComponent<GridCell>();

                grid[x, y] = cell;

            }
        }
        
   }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeGrid();

        for (int i = 0; i < 4; i++)
        {
            SpawnNewRank();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DraggableRank CreateRankInCell(GridCell cell, int level)
    {
        if(cell == null ||!cell.isEmpty()) return null;


        level = Mathf.Clamp(level, 1, MaxRankLevel);

        Vector3 rankPosition = new Vector3(cell.transform.position.x, cell.transform.position.y, 0f);

        GameObject rankObj = Instantiate(rankPrefab, rankPosition, Quaternion.identity, gridContainer);
        rankObj.name = "Rank_Level_" + level;

        DraggableRank rank = rankObj.AddComponent<DraggableRank>();

        rank.SetRankLevel(level);

        cell.SetRank(rank);

        return rank;

    }

    private GridCell FineEmptyCell()
    {
        List<GridCell> emptyCells = new List<GridCell>();
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y].isEmpty())
                {
                    emptyCells.Add(grid[x, y]);
                }
            }
        }

        if (emptyCells.Count == 0)
        {
            return null; 
        }
        
        return emptyCells[Random.Range(0, emptyCells.Count)];
    }

    public bool SpawnNewRank()
    {
        GridCell emptyCell = FineEmptyCell();
        if (emptyCell == null) return false;
        int rankLevel = Random.Range(0, 100) < 90 ? 1 : 2;
        CreateRankInCell(emptyCell, rankLevel);
        return true;
    }    
}
