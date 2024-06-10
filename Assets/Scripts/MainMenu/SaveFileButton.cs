using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveFileButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public AudioSource hoverSource;
    public AudioSource pressedSource;

    public void OnPointerEnter(PointerEventData ped)
    {
        hoverSource.Play();
    }

    public void OnPointerDown(PointerEventData ped)
    {
        pressedSource.Play();
    }
}
