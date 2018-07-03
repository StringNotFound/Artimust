using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosn : MonoBehaviour {

    public bool x = true;
    public bool y = true;
    public bool z = true;

    public Transform objectToFollow;

	// Use this for initialization
	void Start () {
        if (objectToFollow == null)
            Debug.LogError("Should not be given a null transform!");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newposn = transform.position;
        if (x)
            newposn.x = objectToFollow.position.x;
        if (y)
            newposn.y = objectToFollow.position.y;
        if (z)
            newposn.z = objectToFollow.position.z;

        transform.position = newposn;
	}
}
