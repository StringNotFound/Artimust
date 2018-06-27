using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class TerminalGUI : GUI {

    public TerminalManager terminal;

    public void Awake()
    {
        terminal.OnUserChangedValue += ExecuteCurrentCommand;
    }

    public void ExecuteCurrentCommand()
    {
        string command = terminal.GetBuffer();
        var args = command.Split(" "[0]);

        string res = "invalid command '" + args[0] + "'";
        switch(args[0])
        {
            case "":
                res = "";
                break;
        }
        terminal.DisplayCommandResult(res);
    }

}
