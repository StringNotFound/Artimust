using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TerminalManager : ElementManager {

    public Scrollbar scrollbar;

    private const string prompt = "shell> ";
    private string history = prompt;
    private string buffer;

    // used for the up arrow
    private List<string> previousCommands;
    private int currentCommand = 0;

    private float lastBackspaceEvent = 0f;
    public float backspaceDelay = 0.005f;

    public Text shellText;

    bool freeze_input = false;

	// Use this for initialization
	void Awake () {
        shellText.text = history;
        previousCommands = new List<string>();
	}

    private void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            // don't read in any value if we've frozen the input
            if (freeze_input)
            {
                break;
            }

            // we need to special case this, since backspace can be held down
            if (Input.GetKey(KeyCode.Backspace))
            {
                if (buffer.Length > 0 && Time.time - lastBackspaceEvent > backspaceDelay)
                {
                    buffer = buffer.Remove(buffer.Length - 1);
                    lastBackspaceEvent = Time.time;
                    shellText.text = history + buffer;
                }
            }
            if (Input.GetKeyDown(vKey))
            {
                scrollbar.value = 0;
                switch(vKey)
                {
                    case KeyCode.Return:
                    case KeyCode.KeypadEnter:
                        if (buffer != "")
                        {
                            previousCommands.Add(buffer);
                            currentCommand = previousCommands.Count;
                        }
                        SignalExecuteCommand();
                        return;
                    case KeyCode.UpArrow:
                        // if we've gone back as far as we can go
                        if (currentCommand == 0)
                            return;

                        // otherwise we lookup the corresponding command
                        currentCommand--;
                        buffer = previousCommands[currentCommand];
                        shellText.text = history + buffer;
                        return;
                    case KeyCode.DownArrow:
                        if (currentCommand + 1 >= previousCommands.Count)
                        {
                            currentCommand = previousCommands.Count;
                            buffer = "";
                            shellText.text = history + buffer;
                            return;
                        }

                        currentCommand++;
                        buffer = previousCommands[currentCommand];
                        shellText.text = history + buffer;
                        return;
                    default:
                        string input_str = vKey.ToString();
                        //char input_char = '\0';
                        if (input_str.Length == 1)
                        {
                            //input_char = input_str[0];
                            // it's a letter
                            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                            {
                                // make the letter lowercase
                                //input_char = (char) ((int)input_char + 32);
                                input_str = input_str.ToLower();
                            }
                        } else
                        {
                            // otherwise it's a special case
                            switch(vKey)
                            {
                                case KeyCode.Space:
                                    input_str = " ";
                                    break;
                                case KeyCode.Keypad0:
                                    input_str = "0";
                                    break;
                                case KeyCode.Keypad1:
                                    input_str = "1";
                                    break;
                                case KeyCode.Keypad2:
                                    input_str = "2";
                                    break;
                                case KeyCode.Keypad3:
                                    input_str = "3";
                                    break;
                                case KeyCode.Keypad4:
                                    input_str = "4";
                                    break;
                                case KeyCode.Keypad5:
                                    input_str = "5";
                                    break;
                                case KeyCode.Keypad6:
                                    input_str = "6";
                                    break;
                                case KeyCode.Keypad7:
                                    input_str = "7";
                                    break;
                                case KeyCode.Keypad8:
                                    input_str = "8";
                                    break;
                                case KeyCode.Keypad9:
                                    input_str = "9";
                                    break;
                                case KeyCode.Alpha0:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "0";
                                    else input_str = ")";
                                    break;
                                case KeyCode.Alpha1:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "1";
                                    else input_str = "!";
                                    break;
                                case KeyCode.Alpha2:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "2";
                                    else input_str = "@";
                                    break;
                                case KeyCode.Alpha3:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "3";
                                    else input_str = "#";
                                    break;
                                case KeyCode.Alpha4:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "4";
                                    else input_str = "$";
                                    break;
                                case KeyCode.Alpha5:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "5";
                                    else input_str = "%";
                                    break;
                                case KeyCode.Alpha6:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "6";
                                    else input_str = "^";
                                    break;
                                case KeyCode.Alpha7:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "7";
                                    else input_str = "&";
                                    break;
                                case KeyCode.Alpha8:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "8";
                                    else input_str = "*";
                                    break;
                                case KeyCode.Alpha9:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "9";
                                    else input_str = "(";
                                    break;
                                case KeyCode.Exclaim:
                                    input_str = "!";
                                    break;
                                case KeyCode.DoubleQuote:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "\"";
                                    else input_str = "|";
                                    break;
                                case KeyCode.Hash:
                                    input_str = "#";
                                    break;
                                case KeyCode.Dollar:
                                    input_str = "?";
                                    break;
                                case KeyCode.Ampersand:
                                    input_str = "&";
                                    break;
                                case KeyCode.Quote:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "'";
                                    else input_str = "\"";
                                    break;
                                case KeyCode.LeftParen:
                                    input_str = "(";
                                    break;
                                case KeyCode.RightParen:
                                    input_str = ")";
                                    break;
                                case KeyCode.Asterisk:
                                    input_str = "*";
                                    break;
                                case KeyCode.Plus:
                                    input_str = "+";
                                    break;
                                case KeyCode.Comma:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = ",";
                                    else input_str = "<";
                                    break;
                                case KeyCode.Minus:
                                    input_str = "-";
                                    break;
                                case KeyCode.Period:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = ".";
                                    else input_str = ">";
                                    break;
                                case KeyCode.Slash:
                                    if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                                        input_str = "/";
                                    else input_str = "?";
                                    break;
                                case KeyCode.Colon:
                                    input_str = ":";
                                    break;
                                case KeyCode.Semicolon:
                                    if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                                        input_str = ";";
                                    else input_str = ":";
                                    break;
                                case KeyCode.Less:
                                    input_str = "<";
                                    break;
                                case KeyCode.Greater:
                                    input_str = ">";
                                    break;
                                case KeyCode.Question:
                                    input_str = "?";
                                    break;
                                case KeyCode.At:
                                    input_str = "@";
                                    break;
                                case KeyCode.LeftBracket:
                                    input_str = "[";
                                    break;
                                case KeyCode.RightBracket:
                                    input_str = "]";
                                    break;
                                case KeyCode.Backslash:
                                    input_str = "\\";
                                    break;
                                case KeyCode.Caret:
                                    input_str = "^";
                                    break;
                                case KeyCode.Underscore:
                                    input_str = "_";
                                    break;
                                case KeyCode.BackQuote:
                                    input_str = "`";
                                    break;
                                default:
                                    input_str = "";
                                    break;
                            }
                        }
                        buffer = buffer + input_str;
                        break;
                }
                shellText.text = history + buffer;
            }
        }

    }

    public string GetBuffer()
    {
        return buffer;
    }

    // this should only be called from ExecuteCurrentCommand
    public void Clear()
    {
        buffer = "";
        history = "";
    }

    public void DisplayCommandResult(string res)
    {
        if (res == "")
            history = history + buffer + '\n' + prompt;
        else
            history = history + buffer + '\n' + res + '\n' + prompt;
        buffer = "";
        shellText.text = history + buffer;
        freeze_input = false;
    }

    /*
     * This function triggers the event which causes the current command to be handled
     * in ExecuteCurrentCommand in the TerminalGUI. We handle it there rather than here
     * because it has referenced to the PIM and ship, which certain commands need.
     * 
     * Note that this function does not clear the buffer. The handler must call GetBuffer to get
     * the buffer, but the buffer isn't cleared until the result of the command is displayed to
     * the terminal
     */
    private void SignalExecuteCommand()
    {
        freeze_input = true;
        this.ActivateOnUserChangedValueEvent();
    }

}
