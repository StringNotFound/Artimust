using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInterfaceManager : NetworkBehaviour {

    public ShipManager ship;

    public GUI[] GUIs;
    public GUI currentGUI;

	// Use this for initialization
	void Start () {

        ship = GameObject.FindGameObjectWithTag("Ship").GetComponent<ShipManager>();
        if (ship == null)
            Debug.LogError("Ship object could not be found!");

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (canvas == null)
            Debug.LogError("Canvas object could not be found!");

        // get all components, including the inactive ones
        GUIs = canvas.GetComponentsInChildren<GUI>(true);
        if (GUIs.Length == 0)
            Debug.LogError("Could not find any GUIs!");

        currentGUI = GUIs[0];

		if (NetworkClient.active)
        {
            // register the event handlers
            ship.EventTest += HandleTestEvent;
            ship.pim = this;
        }

        Debug.Log("Setting GUI's ship value");

        currentGUI.ship = ship;
        currentGUI.pim = this;
        currentGUI.Activate();

        Debug.Log("Done setting GUI's ship value");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // changes the currently active GUI
    public void ChangeCurrentGUI(string name)
    {
        for (int i = 0; i < GUIs.Length; i++)
        {
            if (GUIs[i].name == name)
            {
                currentGUI.Deactivate();
                currentGUI = GUIs[i];
                currentGUI.ship = ship;
                currentGUI.pim = this;
                currentGUI.Activate();
                return;
            }
        }
        Debug.LogError("Invalid name \"" + name + "\" specified for ChangeCurrentGUI");
    }

    // the event handlers (they should mostly just call the GUI's event handlers)
    public void HandleTestEvent(int var1, float var2)
    {
        currentGUI.HandleTestEvent(var1, var2);
    }

    [Command]
    public void CmdChangeTestVal1(int newVal)
    {
        Debug.Log("Setting val1 to " + newVal);
        ship.SetTestVal1(newVal);
    }

    [Command]
    public void CmdChangeTestVal2(int newVal)
    {
        ship.SetTestVal2(newVal);
    }

    [Command]
    public void CmdChangeTestBool(bool newVal)
    {
        Debug.Log("CmdChangeTestBool called with parameter " + newVal + "(this should be inside the server)");
        ship.SetTestBool(newVal);
    }
}
