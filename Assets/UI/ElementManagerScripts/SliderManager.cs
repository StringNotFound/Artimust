using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderManager : ElementManager {

    public Slider slider;

	// Use this for initialization
	void Awake () {
        slider = this.GetComponent<Slider>();
	}

    public float GetValue()
    {
        return slider.value;
    }

    public void SetValue(float val)
    {
        slider.value = val;
    }

    public void RegisterChangeHandler(UnityAction<float> changeHandler)
    {
        slider.onValueChanged.AddListener(changeHandler);
    }
}
