using UnityEngine;

[System.Serializable]
public class CreatureData
{
    public string name;
    public string species_id;
    public bool is_predator;
    public string gender;
    public int body_size;
    public string color;
    public string pattern;
    public int limb_count;
    public CreatureStats stats;
    public string biome;
}

[System.Serializable]
public class CreatureStats
{
    public int health;
    public float speed;
    public int strength;
    public int max_age;
    public int hunger;
}
