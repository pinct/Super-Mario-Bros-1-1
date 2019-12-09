using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public float time = 120;
    public int score = 0;
    public int lives = 3;
    public int coins = 0;
    public GameObject timeUI;
    public GameObject scoreUI;
    public GameObject coinsUI;
    public GameObject livesUI;
    private bool ending = false;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Mystery").GetComponent<DataManagement>().lives != 0)
        {
            lives = GameObject.FindGameObjectWithTag("Mystery").GetComponent<DataManagement>().lives;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Animator>().GetBool("Flag"))
        {
            time = time - Time.deltaTime;
        }
        if (time < 0 && !GetComponent<Animator>().GetBool("Flag"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().dead = true;
        }
        scoreUI.gameObject.GetComponent<Text>().text = $"Mario\n{score:000000}";
        timeUI.gameObject.GetComponent<Text>().text = $"TIME\n{(int)time}";
        coinsUI.gameObject.GetComponent<Text>().text = $"COINS\n{coins}";
        livesUI.gameObject.GetComponent<Text>().text = $"LIVES\n{lives}";
        Debug.Log(GetComponent<BoxCollider2D>().size.y);
        if (ending && transform.position.y < (-1.45 + (GetComponent<BoxCollider2D>().size.y - 0.9f)))
        {
            StartCoroutine(End());
        }
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Flag")
        {
            Count();
        }
        else if (other.gameObject.tag == "Coin")
        {
            score += 10;
            Destroy(other.gameObject);
        }
    }
    void Count()
    {
        ending = true;
        score = score + ((int)time) * 10;
        time = 0;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()[0].enabled = false;
        GameObject.FindGameObjectWithTag("KuppaFlag").GetComponent<FlagLowering>().enabled = true;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()[1].PlayOneShot(GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()[1].clip);
        GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()[2].PlayOneShot(GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>()[2].clip);
        GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Animator>().SetBool("Flag", true);
        transform.position = new Vector2(188.24f, transform.position.y);
    }
    IEnumerator End()
    {
        ending = false;
        yield return new WaitForSeconds(0.4f);
        transform.position = new Vector2(188.79f, transform.position.y);
        GetComponent<SpriteRenderer>().flipX = !(GetComponent<SpriteRenderer>().flipX);
        yield return new WaitForSeconds(0.4f);
        GetComponent<Animator>().SetBool("FlagBottom", true);
        GetComponent<Rigidbody2D>().velocity = new Vector2(2, 2);
        yield return new WaitForSeconds(0.4f);
        GetComponent<Animator>().SetBool("FlagBottom", false);
        GetComponent<SpriteRenderer>().flipX = !(GetComponent<SpriteRenderer>().flipX);
    }
}
