using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObjectByClick : MonoBehaviour
{
    private const int LEFT = 0;

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
