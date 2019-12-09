using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject objects;
    private bool recentlyhit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Animator>().GetBool("isHit") && !recentlyhit)
        {
            if (objects != null)
            {
                Instantiate(objects, transform);
            }
            recentlyhit = true;
        }
    }
}
