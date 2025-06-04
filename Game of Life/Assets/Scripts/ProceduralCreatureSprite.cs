using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ProceduralCreatureSprite : MonoBehaviour
{
    public int resolution = 8;
    public Color primaryColor = Color.white;
    public string pattern = "symmetric"; // "random", "symmetric", etc.

    public void Generate()
    {
        Texture2D tex = new Texture2D(resolution, resolution);
        tex.filterMode = FilterMode.Point;

        // Clear background
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
                tex.SetPixel(x, y, Color.clear);
        }

        // Fill pixels
        int half = resolution / 2;
        for (int x = 0; x < half; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                if (Random.value > 0.5f)
                {
                    tex.SetPixel(x, y, primaryColor);
                    if (pattern == "symmetric")
                        tex.SetPixel(resolution - 1 - x, y, primaryColor);
                    else if (pattern == "random")
                        tex.SetPixel(resolution - 1 - x, y, Random.value > 0.5f ? primaryColor : Color.clear);
                }
            }
        }

        tex.Apply();

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, resolution, resolution), new Vector2(0.5f, 0.5f), 16);
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
