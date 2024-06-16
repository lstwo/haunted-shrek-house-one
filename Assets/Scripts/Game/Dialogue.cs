using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    public string inspectorName;

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public float typingSpeed;
    public AudioClip characterTypeSound;

    public override string ToString()
    {
        return inspectorName;
    }
}
