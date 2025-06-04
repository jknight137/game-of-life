using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatureRenderer : MonoBehaviour
{
    public GameObject creaturePrefab; // Assign in Inspector (empty GameObject with SpriteRenderer)

    public void RenderCreature(CreatureData data, Vector3Int cell, Vector2Int mapSize, int xOffset, int yOffset, Tilemap tilemap)
    {
        GameObject creatureObj = Instantiate(creaturePrefab, Vector3.zero, Quaternion.identity, this.transform);

        var sr = creatureObj.GetComponent<SpriteRenderer>();
        Color c;
        if (ColorUtility.TryParseHtmlString(data.color, out c))
            sr.color = c;
        else
            sr.color = Color.magenta;

        var generator = creatureObj.AddComponent<ProceduralCreatureSprite>();
        generator.primaryColor = sr.color;
        generator.pattern = "symmetric";
        generator.Generate();

        float scale = Mathf.Clamp(data.body_size, 1, 4);
        creatureObj.transform.localScale = new Vector3(scale, scale, 1);
        creatureObj.name = data.name;

        var behavior = creatureObj.AddComponent<CreatureBehavior>();
        behavior.Init(data, mapSize, xOffset, yOffset, tilemap);
    }
}
