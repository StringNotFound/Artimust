using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shield : SpaceObject {

    public ShieldNode shieldNode;

    new private void Start()
    {
        base.Start();
        // shields are energy-based (they don't cause missles to explode)
        this.isPhysical = false;
    }

    public override void HandleLaserHit(float damage)
    {
        if (this.isClient)
            Debug.LogError("Something terrible happened: a laser hit is being handled on a client!");
        shieldNode.ShieldHit(damage);
    }


}
