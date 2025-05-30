using UnityEngine;

public class CreatureRenderer : MonoBehaviour
{
    public GameObject creaturePrefab; // Assign in Inspector (empty GameObject with SpriteRenderer)

    public void RenderCreature(CreatureData data, Vector3 position, Vector2Int mapSize, float xOffset, float yOffset)
    {
        GameObject creatureObj = Instantiate(creaturePrefab, position, Quaternion.identity, this.transform);

        // Visual: Set color and scale by size
        var sr = creatureObj.GetComponent<SpriteRenderer>();
        Color c;
        if (ColorUtility.TryParseHtmlString(data.color, out c))
            sr.color = c;
        else
            sr.color = Color.magenta; // fallback

        float scale = Mathf.Clamp(data.body_size, 1, 4); // 1x1, 2x2, 4x4
        creatureObj.transform.localScale = new Vector3(scale, scale, 1);

        var behavior = creatureObj.AddComponent<CreatureBehavior>();
        behavior.Init(data, mapSize, xOffset, yOffset);
        // Optional: Add pattern overlay, limb drawing, etc.
        creatureObj.name = data.name;
    }
}
