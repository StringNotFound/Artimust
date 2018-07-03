using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShieldNode : Node {

    public Shield shield;

    [SyncVar]
    public bool on = false;

    [SyncVar]
    public float charge;
    public const float maxCharge = 100f;
    public const float chargePerSecond = 10f;

    public const float shieldBreakCost = 10f;

    private void Start()
    {
        if (shield == null)
        {
            Debug.LogError("Shield Node needs reference to its corresponding shield object!");
        }
        charge = maxCharge;
        shield.shieldNode = this;
    }

    private void Update()
    {
        if (!on && charge < maxCharge)
        {
            float toCharge = Mathf.Min(maxCharge - charge, chargePerSecond * Time.deltaTime);
            ship.energyUsedThisTick += toCharge;
        }
    }

    public void ShieldHit(float damage)
    {
        charge -= damage;
        if (charge <= 0)
        {
            charge -= shieldBreakCost;
            TurnOff();
        }
    }

    public void TurnOn()
    {
        if (charge <= 0)
        {
            // we only turn on if the charge if positive
            return;
        }
        on = true;
        shield.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        on = false;
        shield.gameObject.SetActive(false);
    }

}
