using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    [SerializeField] private float velocity = 0.5f;
    [SerializeField] private AudioSource jumpSound;


    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    private bool isGround = false;
    private float defY = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        defY = boxCollider.size.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") isGround = false;
    }

    private void Jump()
    {
        if (!isGround) return;
        rb.velocity = Vector2.up * velocity;
        jumpSound.Play();
    }
    private void DownEvent(bool move)
    {
        anim.SetBool("isDown", move);
        if (move)
        {
            boxCollider.size = new Vector2(boxCollider.size.x, defY - 0.20f);
            rb.velocity = Vector2.down * (velocity / 2);
        } else
        {
            boxCollider.size = new Vector2(boxCollider.size.x, defY);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            DownEvent(true);
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            DownEvent(false);
        }
    }
}
