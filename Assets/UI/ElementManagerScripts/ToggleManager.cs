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
	}

    public bool GetValue()
    {
        return toggle.isOn;
    }

    public void SetValue(bool val)
    {
        toggle.isOn = val;
    }

    public void RegisterChangeHandler(UnityAction<bool> changeHandler)
    {
        toggle.onValueChanged.AddListener(changeHandler);
    }

}
