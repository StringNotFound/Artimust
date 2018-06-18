using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Test2GUI : GUI {

    public Button backButton;

    public void Start()
    {
        backButton.onClick.AddListener(GoBack);
    }

    public void GoBack()
    {
        pim.ChangeCurrentGUI("TestGUI1");
    }

}
