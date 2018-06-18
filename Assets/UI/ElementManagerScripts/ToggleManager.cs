using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ToggleManager : ElementManager {

    public Toggle toggle;

	// Use this for initialization
	void Awake () {
        toggle = this.GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);
	}

    public bool GetValue()
    {
        return toggle.isOn;
    }

    public void SetValue(bool val)
    {
        toggle.isOn = val;
    }

    private void OnValueChanged(bool newVal)
    {
        Debug.Log("Toggle value changed");
        if (Time.frameCount == lastFrameDeselected)
        {
            // the user just released the mouse, changing the value of this object (rather
            // than a script just having changed the value of this object)
            Debug.Log("User change!");
            Debug.Log(toggle.isOn);
            lastFrameDeselected = -1;
            ActivateOnUserChangedValueEvent();
        }
    }

}
