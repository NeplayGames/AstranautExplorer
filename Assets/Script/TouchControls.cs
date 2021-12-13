using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler { 
    [SerializeField] int data;
    public static event Action<int> OnPress;

   

    public void OnPointerDown(PointerEventData eventData)
    {
        print("pressed");
        if (OnPress != null)
            OnPress(data);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPress != null)
            OnPress(0);
    }
}
