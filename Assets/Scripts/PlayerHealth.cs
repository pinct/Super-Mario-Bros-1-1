using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool dead;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead == true)
        {
           Death();
        }
    }
    void Death()
    {
        GetComponent<Score>().lives = GetComponent<Score>().lives - 1;
        GameObject.FindGameObjectWithTag("Mystery").GetComponent<DataManagement>().lives = GetComponent<Score>().lives;
        GameObject.FindGameObjectWithTag("Mystery").GetComponent<DataManagement>().score = GetComponent<Score>().score;
        SceneManager.LoadScene("1-1");
    }
}
