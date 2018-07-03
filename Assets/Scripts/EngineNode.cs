using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EngineNode : Node {

    private Rigidbody2D shipRB;

    // how much thrust is this engine currently emitting?
    [SyncVar]
    public float thrust;

    // how much thrust can this engine emit?
    [SyncVar]
    public float maxThrust = 1;

    [SyncVar]
    public float maxEnergyUsagePerSecond = 10f;

	// Use this for initialization
	void Start () {
        shipRB = ship.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// FixedUpdate is called at a fixed interval, I think
	void FixedUpdate () {
        ship.energyUsedThisTick += Time.fixedDeltaTime * maxEnergyUsagePerSecond * (thrust / maxThrust);
        shipRB.AddForceAtPosition(this.transform.up * thrust / maxThrust, this.transform.position);
	}

    public void SetThrust(float newThrust)
    {
        thrust = Mathf.Min(maxThrust, newThrust);
    }
}
