using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GUI : MonoBehaviour {

    public ShipManager ship;
    public PlayerInterfaceManager pim;

    public string panelname;

    // bring the GUI onto the screen
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    // put the GUI off of the screen
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    // the event handlers
    public void HandleTestEvent(int var1, float var2) { }

}
