using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateRandomLevel : MonoBehaviour
{
    public GameObject[] brickTypes;
    public InGameScoreMechanism scoreManager;
    [SerializeField] private int rows;
    [SerializeField] private int cols;
    [SerializeField] private float leftmostBrickX;
    [SerializeField] private float lowestBrickY;
    [SerializeField] private float spaceBetweenBricks;
    private InGameManager _gameManager;

    void Start()
    {
        _gameManager = GetComponent<InGameManager>();
        for (int i = 0; i < brickTypes.Length; ++i)
        {
            brickTypes[i].GetComponent<BallBrickCollision>().scoreMechanism = scoreManager;
            brickTypes[i].GetComponent<BallBrickCollision>().gameManager = _gameManager;
        }
        CreateLevel();
    }

    public void CreateLevel()
    {
        for (int row = 0; row < rows; ++row)
        {
            for (int col = 0; col < cols; ++col)
            {
                int randomBrick = Random.Range(0, brickTypes.Length - 1);
                Vector3 location = new Vector3(leftmostBrickX+col*spaceBetweenBricks,
                    lowestBrickY+row*spaceBetweenBricks, 0);
                Instantiate(brickTypes[randomBrick], location,transform.rotation);
                
                _gameManager.ChangeBricksCount(true);
            }
        }
    }
}
