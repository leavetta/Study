using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPoint : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Text text;
    public void OnPointerEnter(PointerEventData eventData) 
    {
        text.enabled = true;
    }
    public void OnPointerExit(PointerEventData eventData) 
    {
        text.enabled = false;
    }
}
