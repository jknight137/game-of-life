using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class PlanetRenderer : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase baseTile;
    public TMP_Text debugText;
    public GameObject playerMarkerPrefab;

    public PlanetMap planet;

    private GameObject playerMarkerObj;
    private int playerX = 0, playerY = 0;
    private int xOffset = 0, yOffset = 0;

    void Start()
    {
        planet = PlanetMap.LoadFromFile("planet_artifact.json");

        tilemap.ClearAllTiles();

        // Offsets to center the map
        xOffset = -planet.width / 2;
        yOffset = -planet.height / 2;

        for (int x = 0; x < planet.width; x++)
        {
            for (int y = 0; y < planet.height; y++)
            {
                var tile = planet.tiles[x, y];
                var pos = new Vector3Int(x + xOffset, y + yOffset, 0);
                tilemap.SetTile(pos, baseTile);
                tilemap.SetTileFlags(pos, TileFlags.None);
                tilemap.SetColor(pos, tile.color);
            }
        }

        // Start player in center
        playerX = planet.width / 2;
        playerY = planet.height / 2;

        playerMarkerObj = Instantiate(playerMarkerPrefab);
        UpdatePlayerMarker();
        UpdateDebugText();
    }

    void Update()
    {
        // Player movement: ARROW KEYS only
        int dx = 0, dy = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow)) dx = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow)) dx = -1;
        if (Input.GetKeyDown(KeyCode.LeftArrow)) dy = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow)) dy = 1;

        if (dx != 0 || dy != 0)
            MovePlayer(dx, dy);
    }

    void MovePlayer(int dx, int dy)
    {
        playerX = (playerX + dx + planet.width) % planet.width;
        playerY = (playerY + dy + planet.height) % planet.height;
        UpdatePlayerMarker();
        UpdateDebugText();
    }

    void UpdatePlayerMarker()
    {
        Vector3Int cell = new Vector3Int(playerX + xOffset, playerY + yOffset, 0);
        Vector3 cellWorldPos = tilemap.GetCellCenterWorld(cell);
        playerMarkerObj.transform.position = cellWorldPos + new Vector3(0, 0, -0.2f);
    }

    void UpdateDebugText()
    {
        if (debugText != null)
        {
            PlanetTile tile = planet.tiles[playerX, playerY];
            debugText.text = $"Planet:\n{planet.planetName}\n" +
                             $"Pos: ({playerX},{playerY})\n" +
                             $"Biome:\n{tile.biome}\n" +
                             $"Elev: {tile.elevation:F2}\n" +
                             $"Moist: {tile.moisture:F2}\n" +
                             $"Temp: {tile.temperature:F2}";
        }
    }
}
