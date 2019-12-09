using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public bool dead;
    public bool big = false;
    public bool ishit = false;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ishit)
        {
            StartCoroutine(hit());
            ishit = false;
        }
        if(dead && !big)
        {
           Death();
        }
        else if (dead)
        {
            StartCoroutine(hit());
            ishit = false;
            dead = false;
        }
    }
    void Death()
    {
        GetComponent<Score>().lives = GetComponent<Score>().lives - 1;
        GameObject.FindGameObjectWithTag("Mystery").GetComponent<DataManagement>().lives = GetComponent<Score>().lives;
        SceneManager.LoadScene("1-1");
    }
    IEnumerator hit()
    {
        GetComponent<Animator>().SetBool("Big", false);
        GetComponent<Animator>().SetBool("Hit", true);
        GetComponents<AudioSource>()[2].PlayOneShot(GetComponents<AudioSource>()[2].clip);
        GetComponent<BoxCollider2D>().size = new Vector2(0.75f, 1.0f);
        yield return new WaitForSeconds(2.0f);
        big = false;
    }
}
