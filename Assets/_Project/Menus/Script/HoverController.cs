using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverController : MonoBehaviour, IPointerEnterHandler
{
    public StudioEventEmitter hoverSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverSound.Play();
    }
}
