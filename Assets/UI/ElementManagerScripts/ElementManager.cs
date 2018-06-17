using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ElementManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    private bool isSelected = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelected = false;
    }

    public bool IsSelected()
    {
        return isSelected;
    }
}
