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
                hit.collider.gameObject.GetComponent<PlayerHealth>().dead = true;
            }
            xMove = xMove * -1;
        }
        //RaycastHit2D hitupright = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + .5), new Vector2(xMove, 0));
        //if (hitupright.collider != null && hitupright.distance < 0.7f)
        //{
        //    if (hitupright.collider.tag == "Player")
        //    {
        //        hitupright.collider.gameObject.GetComponent<PlayerHealth>().dead = true;
        //    }
        //}
        //RaycastHit2D hitdownright = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5), new Vector2(xMove, 0));
        //if (hitdownright.collider != null && hitdownright.distance < 0.7f)
        //{
        //    if (hitdownright.collider.tag == "Player")
        //    {
        //        hitdownright.collider.gameObject.GetComponent<PlayerHealth>().dead = true;
        //    }
        //}
        //RaycastHit2D hitdownright = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .5), new Vector2(xMove, 0));
        //if (hitdownright.collider != null && hitdownright.distance < 0.7f)
        //{
        //    if (hitdownright.collider.tag == "Player")
        //    {
        //        hitdownright.collider.gameObject.GetComponent<PlayerHealth>().dead = true;
        //    }
        //}
    }
}
