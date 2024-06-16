using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public bool allowAny = false;
    public string colliderTag = "Player";

    [Space(20)]
    public UnityEvent onTriggerEnter;

    [Space(5)]
    public UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == colliderTag || allowAny)
        {
            onTriggerEnter.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == colliderTag || allowAny)
        {
            onTriggerExit.Invoke();
        }
    }
}