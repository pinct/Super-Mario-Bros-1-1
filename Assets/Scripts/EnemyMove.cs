using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int speed;
    public int xMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMove, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMove * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        if (hit.collider != null && hit.distance < 0.7f)
        {
            if (hit.collider.tag == "Player")
            {
                if (!hit.collider.gameObject.GetComponent<PlayerHealth>().big)
                {
                    hit.collider.gameObject.GetComponent<PlayerHealth>().dead = true;
                }
                else
                {
                    hit.collider.gameObject.GetComponent<PlayerHealth>().ishit = true;
                }
            }
            xMove = xMove * -1;
        }
    }
}
