using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class ElementManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    // this event is called when the _user_ changes the value of this Element, not
    // a script
    public delegate void UserChangedValue();
    public event UserChangedValue OnUserChangedValue;

    // did the user click on this object and hasn't released their mouse?
    protected bool activelySelected = false;
    // on which frame did the user most recently release the mouse for this object?
    protected int lastFrameDeselected = -1;

    // sadly, we can only activate events from within the scope that they were defined,
    // thus making us unable to call this event directly from the subclasses of this method.
    // This function fixes that problem
    protected void ActivateOnUserChangedValueEvent()
    {
        OnUserChangedValue();
    }

    // called when the object is clicked on
    public void OnPointerDown(PointerEventData eventData)
    {
        activelySelected = true;
    }

    // called when the mouse is released after clicking on this object
    // sadly, it's called before the object's new value is registered
    public void OnPointerUp(PointerEventData eventData)
    {
        activelySelected = false;
        lastFrameDeselected = Time.frameCount;
    }

    public bool IsActivelySelected()
    {
        return activelySelected;
    }
}
