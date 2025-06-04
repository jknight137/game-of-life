using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class PlanetMap
{
    public string planetName;
    public int width;
    public int height;
    public PlanetTile[,] tiles;

    public static PlanetMap LoadFromFile(string filename)
    {
        string path = Path.Combine(Application.streamingAssetsPath, filename);
        string json = File.ReadAllText(path);
        JObject obj = JObject.Parse(json);

        string planetName = (string)obj["planet_name"];
        int width = (int)obj["width"];
        int height = (int)obj["height"];
        JArray tileRows = (JArray)obj["tiles"];

        PlanetTile[,] tiles = new PlanetTile[width, height];
        for (int y = 0; y < height; y++)
        {
            JArray row = (JArray)tileRows[y];
            for (int x = 0; x < width; x++)
            {
                var t = row[x];
                string biome = (string)t["biome"];
                string colorHex = (string)t["color"];
                float elevation = t["elevation"] != null ? (float)t["elevation"] : 0f;
                float moisture = t["moisture"] != null ? (float)t["moisture"] : 0f;
                float temperature = t["temperature"] != null ? (float)t["temperature"] : 0f;
                Color color;
                ColorUtility.TryParseHtmlString(colorHex, out color);
                tiles[x, y] = new PlanetTile(biome, color, x, y, elevation, moisture, temperature);
            }
        }
        PlanetMap map = new PlanetMap
        {
            planetName = planetName,
            width = width,
            height = height,
            tiles = tiles
        };
        return map;
    }
}
