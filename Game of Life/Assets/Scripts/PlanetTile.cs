using UnityEngine;

public class PlanetTile
{
    public string biome;
    public Color color;
    public int x, y;
    public float elevation;
    public float moisture;
    public float temperature;

    public PlanetTile(string biome, Color color, int x, int y, float elevation, float moisture, float temperature)
    {
        this.biome = biome;
        this.color = color;
        this.x = x;
        this.y = y;
        this.elevation = elevation;
        this.moisture = moisture;
        this.temperature = temperature;
    }
}
