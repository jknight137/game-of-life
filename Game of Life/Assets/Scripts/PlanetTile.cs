using UnityEngine;

public class PlanetTile
{
    public string biome;
    public Color color;
    public int x, y;

    public PlanetTile(string biome, Color color, int x, int y)
    {
        this.biome = biome;
        this.color = color;
        this.x = x;
        this.y = y;
    }
}
