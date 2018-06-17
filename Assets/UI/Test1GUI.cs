using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Test1GUI : GUI {

    public Slider val1Slider;
    public Slider val2Slider;
    public Toggle boolToggle;

    public void Start()
    {
        val1Slider.onValueChanged.AddListener(delegate { Val1SliderChanged(); });
        val2Slider.onValueChanged.AddListener(delegate { Val2SliderChanged(); });
        boolToggle.onValueChanged.AddListener(delegate { Bool1Changed(); });
    }

    // these are called when the ship's syncvar's change
    public override void OnBoolChanged()
    {
        Debug.Log("Changing GUI toggle to " + ship.testBool);
        boolToggle.isOn = ship.testBool;
    }

    public override void OnValChanged()
    {
        Debug.Log("New val1 slider value: " + ship.testVal1);
        val1Slider.value = ship.testVal1;
        val2Slider.value = ship.testVal2;
    }

    // these are called when the values of the GUI objects change
    public void Val1SliderChanged()
    {
        // only update when we need to (this method is also called when a script
        // updates the value of the slider (i.e., OnValChanged)
        //if ((int) val1Slider.value != ship.testVal1)
            pim.CmdChangeTestVal1((int) val1Slider.value);
    }

    public void Val2SliderChanged()
    {
        //if ((int) val2Slider.value != ship.testVal2)
            pim.CmdChangeTestVal2((int) val2Slider.value);
    }

    public void Bool1Changed()
    {
        //if (boolToggle.isOn != ship.testBool)
            pim.CmdChangeTestBool(boolToggle.isOn);
    }
}
