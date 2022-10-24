using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorControll : MonoBehaviour
{
    public bool hideOnStart = true;
    void Start()
    {
        if (hideOnStart)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    
}
