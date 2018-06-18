using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderManager : ElementManager {

    public Slider slider;

	// Use this for initialization
	void Awake () {
        slider = this.GetComponent<Slider>();
        slider.onValueChanged.AddListener(OnValueChanged);
	}

    public float GetValue()
    {
        return slider.value;
    }

    public void SetValue(float val)
    {
        slider.value = val;
    }

    private void OnValueChanged(float newVal)
    {
        /*
         * unlike the toggle OnValueChanged, we don't care whether or not
         * the user just released the mouse. This is because the slider values
         * update whenever the user moves the mouse, not when they release the mouse
         * Therefore, in order to ensuer that we call the User event only when the user
         * actually changes the value, we want to trigger the event only when the user
         * has this object selected
         */
        if (this.IsActivelySelected())
        {
            ActivateOnUserChangedValueEvent();
        }
    }

}
