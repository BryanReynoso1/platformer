using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{

    public float jumpPower;

    public float moveSpeed;

    public float jumpDetectOffset;

    public LayerMask jumpableObject;

    Rigidbody2D rb;

    SpriteRenderer sr;
    Vector3 startPos;
    BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
            sr.flipX = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-moveSpeed, rb.velocity.y, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            sr.flipX = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Respawn")
        {
            rb.velocity = Vector3.zero;
            transform.position = startPos;
        }
        if (c.gameObject.tag == "Platform")
        {
            transform.SetParent(c.transform);
        }
    }



    private void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.tag == "Platform")
        {
            transform.SetParent(null);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, jumpDetectOffset, jumpableObject);
        Color raycolor = Color.red;
            Debug.DrawRay(new Vector3(0, bc.bounds.extents.y + jumpDetectOffset), Vector2.right * bc.bounds.size.x, raycolor);
            return raycastHit.collider != null;
        }

    }


