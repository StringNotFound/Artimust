using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipManager : NetworkBehaviour {

    // declare the events that the ship can trigger
    public delegate void TestEventDelegate(int var1, float var2);

    [SyncEvent]
    public event TestEventDelegate EventTest;

    // declare the variables that the ship has
    [SyncVar]
    public int testVal1;

    [SyncVar]
    public int testVal2;

    [SyncVar]
    public bool testBool;

    // variables specific to local clients
    public PlayerInterfaceManager pim;

    // here are the setter methods for the ship's variables
    // (they should only be called on the server)
    public void SetTestVal1(int newVal)
    {
        testVal1 = newVal;
    }

    public void SetTestVal2(int newVal)
    {
        testVal2 = newVal;
    }

    public void SetTestBool(bool newVal)
    {
        testBool = newVal;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
