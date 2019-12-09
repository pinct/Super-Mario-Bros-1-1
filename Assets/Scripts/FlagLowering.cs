using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagLowering : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > -1.64)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - 0.05f);
        }
    }
}
