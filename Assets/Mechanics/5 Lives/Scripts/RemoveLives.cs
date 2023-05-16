using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLives : MonoBehaviour
{
    public GameObject firstLife;
    public GameObject secondLife;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (firstLife) Destroy(firstLife);
            else if(secondLife) Destroy(secondLife);
            else
            {
                Destroy(gameObject);
                print("GAME OVER");
            }
        }
    }
}
