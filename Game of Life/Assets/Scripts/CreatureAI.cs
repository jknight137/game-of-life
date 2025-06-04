using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    private CreatureBehavior behavior;
    private CreatureData data;
    private float age = 0f;
    private int hunger = 0;
    private int hungerThreshold;

    private void Awake()
    {
        behavior = GetComponent<CreatureBehavior>();
        data = behavior.data;
        hungerThreshold = data.stats.hunger;
    }

    public void TickAI()
    {
        Age();
        GetHungry();

        if (data.is_predator)
        {
            Hunt();
        }
        else
        {
            FlockOrGraze();
        }

        if (CanReproduce()) Reproduce();
        if (ShouldDie()) Die();
    }

    void Age()
    {
        age += 1f;
    }

    void GetHungry()
    {
        hunger++;
    }

    void FlockOrGraze()
    {
        // TODO: Seek nearby same-biome and same-species creatures to flock
        // TODO: Move toward green biomes for food (simulate grazing)
        behavior.MoveRandom();
    }

    void Hunt()
    {
        // TODO: Seek nearest herbivore within a radius and move toward them
        // Placeholder: Random move for now
        behavior.MoveRandom();
    }

    bool CanReproduce()
    {
        return age > data.stats.max_age * 0.2f && hunger < hungerThreshold * 0.5f;
    }

    void Reproduce()
    {
        // TODO: Instantiate a new creature of same species nearby
        Debug.Log($"{data.name} reproduces");
    }

    bool ShouldDie()
    {
        return age > data.stats.max_age || hunger > hungerThreshold * 2f;
    }

    void Die()
    {
        Destroy(this.gameObject);
    }
}
