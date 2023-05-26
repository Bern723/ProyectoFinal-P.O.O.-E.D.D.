using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int Recolectables = 0;
    public int Lives = 3;
    private float runspeed = 8f;
    private float horizontal;
    public float JumpingPower = 15f;
    private bool isFacingRight = true;
    public bool HasTrashBag = false;
    public Animator animator;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;





    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {

            rb.velocity = new Vector2(rb.velocity.x, JumpingPower);
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.4f);
        }



        Flip();

        if (Lives == 0)
        {
            gameObject.SetActive(false);
        }
    }


    public void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * runspeed, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            gameObject.transform.SetParent(null);
        }

        if (Input.GetButtonDown("Jump"))
        {
            gameObject.transform.SetParent(null);
        }

        if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TomarDaño();
            animator.SetBool("isHurt", true);
        }

        if (other.CompareTag("Recolectable"))
        {
            Recolectables = Recolectables + 1;

            if (Recolectables == 5)
            {
                Lives = Lives + 1;
                Recolectables = 0;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            animator.SetBool("isHurt", false);
        }
    }

    public void TomarDaño()
    {
        Lives = Lives - 1;
        if (Lives == 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("TrashBag"))
        {
            if (Input.GetKey("space"))
            {
                HasTrashBag = true;
                Debug.Log("Trash grabbed");
                
            }
        }
        else
        {
            if (other.CompareTag("DeletableObstacle"))
            {
                if (Input.GetKey("space"))
                {
                    if (HasTrashBag == true)
                    {
                        other.GetComponent<DeletableObstacle>().SelfDestruct();
                        HasTrashBag = false;
                    }


                }
            }
        }

        
    }
}
