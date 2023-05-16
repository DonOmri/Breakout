using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBoardHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        print("Second Hit");
    }
}
