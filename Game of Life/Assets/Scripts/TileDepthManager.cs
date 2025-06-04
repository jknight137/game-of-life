using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileLayer
{
    public int depth; // 0 = surface, 1+ = underground layers
    public string layerType; // e.g., "cave", "mine", "base"
    public List<CreatureData> creatures = new();
    public List<string> resources = new();
    public bool discovered = false; // Fog of war per-layer
}

public class TileDepthManager
{
    private Dictionary<Vector2Int, List<TileLayer>> tileDepthMap = new();

    public void AddLayer(Vector2Int tileCoord, TileLayer layer)
    {
        if (!tileDepthMap.ContainsKey(tileCoord))
            tileDepthMap[tileCoord] = new List<TileLayer>();
        tileDepthMap[tileCoord].Add(layer);
    }

    public List<TileLayer> GetLayers(Vector2Int tileCoord)
    {
        if (!tileDepthMap.ContainsKey(tileCoord))
            return new List<TileLayer>();
        return tileDepthMap[tileCoord];
    }

    public TileLayer GetTopLayer(Vector2Int tileCoord)
    {
        if (!tileDepthMap.ContainsKey(tileCoord))
            return null;
        return tileDepthMap[tileCoord].Find(l => l.depth == 0);
    }

    public TileLayer GetLayerAtDepth(Vector2Int tileCoord, int depth)
    {
        if (!tileDepthMap.ContainsKey(tileCoord))
            return null;
        return tileDepthMap[tileCoord].Find(l => l.depth == depth);
    }

    public void DiscoverLayer(Vector2Int tileCoord, int depth)
    {
        var layer = GetLayerAtDepth(tileCoord, depth);
        if (layer != null)
            layer.discovered = true;
    }
}
