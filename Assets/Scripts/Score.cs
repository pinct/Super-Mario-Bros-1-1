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
        time = time - Time.deltaTime;
        if (time <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().dead = true;
        }
        scoreUI.gameObject.GetComponent<Text>().text = $"Mario\n{score:000000}";
        timeUI.gameObject.GetComponent<Text>().text = $"TIME\n{(int)time}";
        coinsUI.gameObject.GetComponent<Text>().text = $"COINS\n{coins}";
        livesUI.gameObject.GetComponent<Text>().text = $"LIVES\n{lives}";
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
        score = score + ((int)time) * 10;
    }
}
