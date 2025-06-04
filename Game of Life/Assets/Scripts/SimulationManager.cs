using UnityEngine;
using System.Collections.Generic;

public class SimulationManager : MonoBehaviour
{
    public float tickInterval = 1.0f; // Time in seconds between simulation updates
    private float tickTimer;

    public List<CreatureBehavior> creatures = new List<CreatureBehavior>();
    // public List<VillageBehavior> villages = new List<VillageBehavior>(); // If you implement villages
    // public List<StructureBehavior> structures = new List<StructureBehavior>(); // If you implement structures

    void Start()
    {
        tickTimer = tickInterval;
        // Optionally: Find all creatures in the scene and add them to the creatures list
        // creatures.AddRange(FindObjectsOfType<CreatureBehavior>());
    }

    void Update()
    {
        tickTimer -= Time.deltaTime;
        if (tickTimer <= 0f)
        {
            StepSimulation();
            tickTimer = tickInterval;
        }
    }

    void StepSimulation()
    {
        foreach (var creature in creatures)
        {
            creature.SimulationTick();
        }

        // foreach (var village in villages)
        // {
        //     village.SimulationTick();
        // }

        // foreach (var structure in structures)
        // {
        //     structure.SimulationTick();
        // }

        // Add more logic: spawn entities, handle global events, etc.
    }

    // Call this from CreatureBehavior.OnEnable if you want dynamic add/remove
    public void RegisterCreature(CreatureBehavior creature)
    {
        if (!creatures.Contains(creature))
            creatures.Add(creature);
    }
    public void UnregisterCreature(CreatureBehavior creature)
    {
        creatures.Remove(creature);
    }
}
