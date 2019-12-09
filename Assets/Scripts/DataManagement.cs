using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    public int lives = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        DontDestroyOnLoad(transform.gameObject);
        if (GameObject.FindGameObjectsWithTag("Mystery").Length != 1 && lives == 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
