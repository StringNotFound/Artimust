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
    [SyncVar(hook = "OnVal1Changed")]
    public int testVal1;

    [SyncVar(hook = "OnVal2Changed")]
    public int testVal2;

    [SyncVar(hook = "OnBoolChanged")]
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

    // called on every client when val1 changes
    public void OnVal1Changed(int v)
    {
        Debug.Log("Ship OnVal1Changed called");
        // the syncvar evidently doesn't update until after this system call, so
        // we force it to update now
        testVal1 = v;
        pim.OnValChanged();
    }

    public void OnVal2Changed(int v)
    {
        Debug.Log("Ship OnVal2Changed called");
        testVal2 = v;
        pim.OnValChanged();
    }

    // called on every client when the Bool variable changes
    public void OnBoolChanged(bool b)
    {
        Debug.Log("Ship OnBoolChanged called");
        this.testBool = b;
        pim.OnBoolChanged();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
