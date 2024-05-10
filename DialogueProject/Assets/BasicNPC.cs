using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class BasicNPC : MonoBehaviour
{
    private bool InDialogue = false;

    public void SetInDialogue(bool value)
    {
        InDialogue = value;
    }

    public bool IsInDialogue()
    {
        return InDialogue;
    }
    
}
