using UnityEngine;

public class CreatureBehavior : MonoBehaviour
{
    public CreatureData data; // Assign when spawning
    public float moveInterval = 1.0f; // Seconds between moves
    private float moveTimer;
    private Vector2Int mapSize;
    private float xOffset;
    private float yOffset;

    public void Init(CreatureData _data, Vector2Int _mapSize, float _xOffset, float _yOffset)
    {
        data = _data;
        mapSize = _mapSize;
        xOffset = _xOffset;
        yOffset = _yOffset;
        moveTimer = moveInterval * Random.value; // Randomize starting interval
    }

    void Update()
    {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0f)
        {
            MoveRandom();
            moveTimer = moveInterval + Random.Range(-0.3f, 0.3f); // Randomize a little
        }
    }

    void MoveRandom()
    {
        // Simple random movement (later: check for biomes, collision, etc.)
        int dx = Random.Range(-1, 2); // -1, 0, or 1
        int dy = Random.Range(-1, 2);
        Vector3 pos = transform.position + new Vector3(dx, dy, 0);

        // Clamp to map boundaries (or use wraparound)
        pos.x = Mathf.Repeat(pos.x + mapSize.x / 2f, mapSize.x) - mapSize.x / 2f;
        pos.y = Mathf.Repeat(pos.y + mapSize.y / 2f, mapSize.y) - mapSize.y / 2f;

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
}
