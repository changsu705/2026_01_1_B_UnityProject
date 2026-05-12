using UnityEngine;

public class GridCell : MonoBehaviour
{
    public int x, y;
    public DraggableRank currentRank;
    public SpriteRenderer cellRenderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Initialize(int gridX, int gridY)
    {
        x = gridX;
        y = gridY;
        name = $"Cell_"+ x + "," + y;
    }

    public bool isEmpty()
    {
        return currentRank == null;
    }

    public bool ContainsPosition(Vector3Int position)
    {
        Bounds boinds = cellRenderers.bounds;
        return boinds.Contains(position);
    }
    public void SetRank(DraggableRank rank)
    {
        currentRank = rank;

        if (rank != null)
        {
            rank.currentCell = this;
        }

        rank.originalPosition = new Vector3(transform.position.x, transform.position.y, 0);
        rank.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
