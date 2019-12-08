using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10;
    public bool facingR = true;
    public float jumpForce = 50;
    public float moveX;
    public bool isGrounded;
    public bool stillJumping;
    public float jumpTime;
    public float jumpCounter;
    public float horMovement = 0;
    public float accel = 1;
    // Start is called before the first frame update
    void Start()
    {
        jumpCounter = jumpTime;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        PlayerCast();
        moveX = Input.GetAxis("Horizontal");
        if (moveX > 0.0f && facingR == false)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("Slide", true);
            facingR = true;
        }
        else if (moveX < 0.0f && facingR == true)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("Slide", true);
            facingR = false;
        }
        else
        {
            GetComponent<Animator>().SetBool("Slide", false);
        }
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("Running", true);
            horMovement += accel * moveX;
        }
        else
        {
            horMovement /= 2;
        }
        if (horMovement == 0)
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            GetComponent<Animator>().SetBool("Jump", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            isGrounded = false;
            stillJumping = false;
        }
        if (Input.GetButton("Jump") && !stillJumping && jumpCounter > 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            jumpCounter -= Time.deltaTime;
        }
        if ((Input.GetButtonUp("Jump")))
        {
            jumpCounter = jumpTime;
            stillJumping = true;
        }
        if (Mathf.Abs(horMovement) < 0.01)
        {
            horMovement = 0;
        }
        else if (horMovement > 20)
        {
            horMovement = 20;
        }
        else if (horMovement < -20)
        {
            horMovement = -20;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horMovement * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" && (transform.position.y - 1.0f) < other.collider.gameObject.transform.position.y)
        {
            GetComponent<PlayerHealth>().dead = true;
        }
    }
    void PlayerCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + .5f, transform.position.y), Vector2.down);
        RaycastHit2D hitleft = Physics2D.Raycast(new Vector2(transform.position.x - .5f, transform.position.y), Vector2.down);
        if ((hit.collider != null && hit.distance < 0.7f && hit.collider.tag == "Enemy") || (hitleft.collider != null && hitleft.distance < 0.7f && hitleft.collider.tag == "Enemy"))
        {
            if (hit.collider != null && hit.distance < 0.7f && hit.collider.tag == "Enemy")
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
                hit.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
                hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                hit.collider.gameObject.GetComponent<Animator>().SetBool("dead", true);
            }
            if (hitleft.collider != null && hitleft.distance < 0.7f && hitleft.collider.tag == "Enemy")
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * 200);
                hitleft.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
                hitleft.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                hitleft.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                hitleft.collider.gameObject.GetComponent<Animator>().SetBool("dead", true);
            }
        }
        else if ((hit.collider != null && hit.distance < 0.7f) || (hitleft.collider != null && hitleft.distance < 0.7f))
        {
            isGrounded = true;
            GetComponent<Animator>().SetBool("Jump", false);
        }
        else
        {
            isGrounded = false;
        }
        RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up);
        if (hitup.collider != null && hitup.distance < 0.7f && hitup.collider.tag == "Box")
        {
            hitup.collider.gameObject.GetComponent<Animator>().SetBool("isHit", true);
            stillJumping = true;
        }
    }
}
