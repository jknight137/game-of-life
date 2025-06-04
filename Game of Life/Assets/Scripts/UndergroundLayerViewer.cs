using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public class UndergroundLayerViewer : MonoBehaviour
{
    public GameObject panel;                  // UI container (enable/disable)
    public Tilemap undergroundTilemap;        // Assign a separate Tilemap
    public TileBase baseTile;                 // Assign a tile for underground
    public TMP_Text titleText;                // Display tile info
    public TMP_Text contentsText;             // List resources/creatures

    private TileDepthManager depthManager;
    private Vector2Int activeCoord;

    public void Init(TileDepthManager manager)
    {
        depthManager = manager;
        Hide();
    }

    public void EnterTile(Vector2Int tileCoord)
    {
        activeCoord = tileCoord;
        List<TileLayer> layers = depthManager.GetLayers(tileCoord);

        undergroundTilemap.ClearAllTiles();

        for (int i = 0; i < layers.Count; i++)
        {
            Vector3Int cell = new Vector3Int(0, -i, 0); // stack vertically
            undergroundTilemap.SetTile(cell, baseTile);
            undergroundTilemap.SetTileFlags(cell, TileFlags.None);
            undergroundTilemap.SetColor(cell, layers[i].discovered ? Color.gray : Color.black);
        }

        titleText.text = $"Tile ({tileCoord.x}, {tileCoord.y}) Layers: {layers.Count}";

        string display = "";
        foreach (var layer in layers)
        {
            display += $"Depth {layer.depth}: {layer.layerType}\n";
            display += $" - Creatures: {layer.creatures.Count}\n";
            display += $" - Resources: {string.Join(", ", layer.resources)}\n";
        }

        contentsText.text = display;
        Show();
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
    }
}
