using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForBricks : MonoBehaviour
{
    public GameObject background;
    private SpriteRenderer _backgroundSpriteRenderer;
    public GameObject[] bricks;


    private void Start()
    {
        _backgroundSpriteRenderer = background.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bricks.Length; ++i)
        {
            if(bricks[i] != null) return;
        }
        _backgroundSpriteRenderer.color = Color.green;
    }
}
