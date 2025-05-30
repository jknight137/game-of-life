using UnityEngine;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour
{
    public CreatureLoader loader;            // Drag in the CreatureLoader component in Inspector
    public CreatureRenderer renderer;        // Drag in the CreatureRenderer component in Inspector
    public PlanetRenderer planetRenderer;

    void Start()
    {
        var planet = planetRenderer.planet; 
        // Center offsets as used in PlanetRenderer
        float xOffset = -planet.width / 2f + 0.5f;
        float yOffset = -planet.height / 2f + 0.5f;
        Vector2Int mapSize = new Vector2Int(planet.width, planet.height);

        foreach (var creature in loader.creatures)
        {
            // Find all positions in the correct biome
            List<Vector2Int> validPositions = new List<Vector2Int>();
            for (int x = 0; x < planet.width; x++)
            {
                for (int y = 0; y < planet.height; y++)
                {
                    if (planet.tiles[x, y].biome == creature.biome)
                        validPositions.Add(new Vector2Int(x, y));
                }
            }
            if (validPositions.Count == 0) continue;

            Vector2Int chosen = validPositions[Random.Range(0, validPositions.Count)];
            Vector3 worldPos = new Vector3(chosen.x + xOffset, chosen.y + yOffset, -0.2f);

            renderer.RenderCreature(creature, worldPos, mapSize, xOffset, yOffset);
        }
    }
}
