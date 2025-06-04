using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatureBehavior : MonoBehaviour
{
    public CreatureData data;

    private Vector2Int mapSize;
    private int xOffset;
    private int yOffset;
    private Tilemap tilemap;

    public float moveInterval = 1.5f;
    private float moveTimer;

    public void Init(CreatureData _data, Vector2Int _mapSize, int _xOffset, int _yOffset, Tilemap _tilemap)
    {
        data = _data;
        mapSize = _mapSize;
        xOffset = _xOffset;
        yOffset = _yOffset;
        tilemap = _tilemap;
        moveTimer = Random.Range(0f, moveInterval);

        // Position the creature correctly at start
        UpdateWorldPosition();
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0f)
        {
            MoveRandom();
            moveTimer = moveInterval + Random.Range(-0.3f, 0.3f);
        }
    }

    public void SimulationTick()
    {
        MoveRandom();
    }

    public void MoveRandom()
    {
        int dx = Random.Range(-1, 2);
        int dy = Random.Range(-1, 2);

        data.x = (data.x + dx + mapSize.x) % mapSize.x;
        data.y = (data.y + dy + mapSize.y) % mapSize.y;

        UpdateWorldPosition();
    }

    void UpdateWorldPosition()
    {
        Vector3Int cell = new Vector3Int(data.x + xOffset, data.y + yOffset, 0);
        Vector3 cellWorld = tilemap.GetCellCenterWorld(cell);
        transform.position = cellWorld + new Vector3(0, 0, -0.1f);
    }
}
