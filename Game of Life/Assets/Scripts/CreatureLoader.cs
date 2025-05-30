using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class CreatureLoader : MonoBehaviour
{
    public List<CreatureData> creatures;

    void Awake()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "creatures.json");
        string json = File.ReadAllText(path);
        creatures = JsonConvert.DeserializeObject<List<CreatureData>>(json);
        Debug.Log($"Loaded {creatures.Count} creatures!");
    }
}
