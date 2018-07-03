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

    // Begin Actual variables

    // How much energy was consumed this frame?
    [SyncVar]
    public float energyUsedThisTick;

    Dictionary<string, Node> nodes = new Dictionary<string, Node>();
    FusionCoreNode fusionCore;

	// Use this for initialization
	void Start () {
        Node[] childrenNodes = this.GetComponentsInChildren<Node>();
        foreach (Node n in childrenNodes)
        {
            nodes.Add(n.name, n);
            n.ship = this;
        }

        fusionCore = (FusionCoreNode) nodes["Fusion Core"];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jump(Vector2 coords)
    {
        // this, of course, preserves velocity and rotation
        this.transform.SetPositionAndRotation(new Vector3(coords.x, coords.y), this.transform.rotation);
    }

    public void CoreBreachStart()
    {

    }

    public void SetEngineThrust(string engineName, float newThrust)
    {
        Node engine = null;
        if (nodes.TryGetValue(engineName, out engine))
        {
            EngineNode en = (EngineNode)engine;
            en.SetThrust(newThrust);
        }
    }

    public bool FireLaser(string laserName, Vector2 targetPosn)
    {
        Node laser = null;
        if (nodes.TryGetValue(laserName, out laser))
        {
            WeaponsNode ln = (WeaponsNode)laser;
            return ln.FireAt(targetPosn);
        }

        Debug.LogError("Invalid weapons node '" + laserName + "'");
        return false;
    }
}
