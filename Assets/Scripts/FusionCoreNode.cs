using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FusionCoreNode : Node {

    [SyncVar]
    public float energyProducedPerSecond = 100;

    // how much energy we have in the reserve. Negative energy represents
    // overheating and an increased chance of a core breach
    [SyncVar]
    public float reserveEnergy = 0;
    
    public const float maxReserveEnergy = 1000f;

    float lastEnergyUpdateTime = 0;

    // probability of explosions
    int lastExplosionCheck = 0;
    float mid_sigmoid = 500f;
    float y_offset = 0;

    float computeProbabilityExplosion(float x)
    {
        return (float) (0.5 * (x - mid_sigmoid) / Mathf.Sqrt(1 + Mathf.Pow(x - mid_sigmoid, 2)) + y_offset);
    }

    private void Start()
    {
        y_offset = -computeProbabilityExplosion(0f);
    }

    public void ConsumeEnergy(float energy)
    {
        float changeTime = Time.time - lastEnergyUpdateTime;
        lastEnergyUpdateTime = Time.time;

        float energyProducedSinceUpdate = energyProducedPerSecond * changeTime;
        float diff = energyProducedSinceUpdate - energy;

        reserveEnergy = Mathf.Min(diff + reserveEnergy, maxReserveEnergy);
    }

    private void Update()
    {
        int curSec = (int) Time.time;
        if (curSec != lastExplosionCheck)
        {
            lastExplosionCheck = curSec;
            if (reserveEnergy < 0)
            {
                float explodeProb = computeProbabilityExplosion(-reserveEnergy);
                float rand = Random.Range(0, 1);
                if (rand < explodeProb)
                {
                    ship.CoreBreachStart();
                }
            }
        }
    }
}
