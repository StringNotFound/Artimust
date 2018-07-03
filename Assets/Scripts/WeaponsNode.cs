using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WeaponsNode : Node {

    [SyncVar]
    public float damage = 5f;

    [SyncVar]
    public float secChargeTime = 0.5f;

    [SyncVar]
    public float charge = 0;

    [SyncVar]
    public float fullCharge = 100f;

    private void Update()
    {
        if (charge < fullCharge)
        {
            float frame_charge = Time.deltaTime / secChargeTime * fullCharge;
            frame_charge = Mathf.Min(fullCharge - charge, frame_charge);
            ship.energyUsedThisTick += frame_charge;
            charge += frame_charge;
        }
    }

    // returns true if something was hit, false otherwise
    public bool FireAt(Vector2 posn)
    {
        if (charge < fullCharge)
        {
            return false;
        }

        charge = 0;

        Vector2 origin = new Vector2(transform.position.x, transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, posn - origin);
        if (hit.collider == null)
            return false;

        SpaceObject so = hit.collider.GetComponent<SpaceObject>();
        if (so != null)
        {
            so.HandleLaserHit(damage);
        }
        return true;
    }
}
