using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMove : MonoBehaviour
{
    public int speed;
    public int xMove;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + 20);
        StartCoroutine(ActivateCollider());
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMove, 0));
        if (speed != 0)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMove * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        if (hit.collider != null && hit.distance < 0.7f)
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
                hit.collider.gameObject.GetComponent<Animator>().SetBool("Big", true);
                hit.collider.gameObject.GetComponent<PlayerHealth>().big = true;
                hit.collider.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 2.0f);
                StartCoroutine(Death());
            }
            xMove = xMove * -1;
        }
    }
    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GetComponents<AudioSource>()[1].PlayOneShot(GetComponents<AudioSource>()[1].clip);
            other.gameObject.GetComponent<Animator>().SetBool("Big", true);
            other.gameObject.GetComponent<PlayerHealth>().big = true;
            other.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.0f, 2.0f);
            StartCoroutine(Death());
        }
    }
    IEnumerator Death()
    {
        speed = 0;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().score += 1000;
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
}
