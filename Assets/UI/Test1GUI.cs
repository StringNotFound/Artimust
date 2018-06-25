using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Test1GUI : GUI {

    public SliderManager val1Slider;
    public SliderManager val2Slider;
    public ToggleManager boolToggle;
    public Button forwardButton;

    public void Start()
    {
        val1Slider.OnUserChangedValue += Val1SliderChanged;
        val2Slider.OnUserChangedValue += Val2SliderChanged;
        boolToggle.OnUserChangedValue += Bool1UserChanged;
        forwardButton.onClick.AddListener(GoForward);
    }

    public void Update()
    {
        if (!boolToggle.IsActivelySelected())
            boolToggle.SetValue(ship.testBool);
        if (!val1Slider.IsActivelySelected())
            val1Slider.SetValue(ship.testVal1);
        if (!val2Slider.IsActivelySelected())
            val2Slider.SetValue(ship.testVal2);
    }

    public void GoForward()
    {
        pim.ChangeCurrentGUI("GUI 2");
    }

    // these are called when the values of the GUI objects change
    public void Val1SliderChanged()
    {
        // only update when we need to (this method is also called when a script
        // updates the value of the slider (i.e., OnValChanged)
        //if ((int) val1Slider.value != ship.testVal1)
            pim.CmdChangeTestVal1((int)val1Slider.GetValue());
    }

    public void Val2SliderChanged()
    {
        //if ((int) val2Slider.value != ship.testVal2)
            pim.CmdChangeTestVal2((int)val2Slider.GetValue());
    }

    public void Bool1UserChanged()
    {
        pim.CmdChangeTestBool(boolToggle.GetValue());
    }
}
