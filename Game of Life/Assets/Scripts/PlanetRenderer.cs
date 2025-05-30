using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class PlanetRenderer : MonoBehaviour
{
    public GameObject tilePrefab; // assign in inspector: a 1x1 SpriteRenderer prefab
    public GameObject playerPrefab; // assign in inspector: a 1x1 SpriteRenderer prefab (e.g. white or marker)
    public TMP_Text debugText; // optional: assign in inspector

    public PlanetMap planet;
    private GameObject[,] tileObjects;
    private GameObject playerObject;

    private int playerX = 0;
    private int playerY = 0;

    void Start()
{
    planet = PlanetMap.LoadFromFile("planet_artifact.json");
    tileObjects = new GameObject[planet.width, planet.height];

    // Centering offset
    float xOffset = -planet.width / 2f + 0.5f;
    float yOffset = -planet.height / 2f + 0.5f;

    // Render tiles
    for (int x = 0; x < planet.width; x++)
    {
        for (int y = 0; y < planet.height; y++)
        {
            Vector3 pos = new Vector3(x + xOffset, y + yOffset, 0);
            GameObject tileObj = Instantiate(tilePrefab, pos, Quaternion.identity, this.transform);
            tileObj.GetComponent<SpriteRenderer>().color = planet.tiles[x, y].color;
            tileObjects[x, y] = tileObj;
        }
    }

    playerObject = Instantiate(playerPrefab, new Vector3(playerX + xOffset, playerY + yOffset, -0.1f), Quaternion.identity, this.transform);
    UpdateDebugText();
}


    void Update()
    {
        int dx = 0, dy = 0;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) dy = 1;
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) dy = -1;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) dx = -1;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) dx = 1;

        if (dx != 0 || dy != 0)
        {
            MovePlayer(dx, dy);
        }

        // Optional: hover info (e.g., with mouse)
    }

    void MovePlayer(int dx, int dy)
    {
        int newX = (playerX + dx + planet.width) % planet.width;
        int newY = (playerY + dy + planet.height) % planet.height;
        playerX = newX;
        playerY = newY;
        playerObject.transform.position = new Vector3(playerX, playerY, -0.1f);
        UpdateDebugText();
    }

    void UpdateDebugText()
    {
        if (debugText != null)
        {
            PlanetTile tile = planet.tiles[playerX, playerY];
            debugText.text = $"Planet: {planet.planetName}\nPos: ({playerX},{playerY})\nBiome: {tile.biome}";
        }
    }
}
