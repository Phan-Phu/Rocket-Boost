using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateRightConection : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool _connection = false;
    public bool Connection { get { return _connection; } }

    public void OnPointerDown(PointerEventData eventData)
    {
        _connection = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _connection = false;
    }

}
