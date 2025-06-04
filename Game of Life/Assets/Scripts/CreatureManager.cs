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
            creature.x = chosen.x;
            creature.y = chosen.y;

            Vector3Int cell = new Vector3Int(creature.x + (int)xOffset, creature.y + (int)yOffset, 0);
            renderer.RenderCreature(creature, cell, mapSize, (int)xOffset, (int)yOffset, planetRenderer.tilemap);
        }

    }
}
