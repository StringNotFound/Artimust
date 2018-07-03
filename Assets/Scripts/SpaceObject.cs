using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class SpaceObject : NetworkBehaviour {

    [SyncVar]
    public float health;

    [SyncVar]
    public float maxHealth = 100f;

    public bool isPhysical = true;

    protected void Start()
    {
        health = maxHealth;
    }

    public bool IsDestroyed()
    {
        return health <= 0;
    }

    public virtual void HandleLaserHit(float damage)
    {
        health -= damage;
    }
    public virtual void HandleDDLaserHit(float damage)
    {
        health -= damage;
    }
    public virtual void HandleTorpedoHit(float damage)
    {
        health -= damage;
    }
}
