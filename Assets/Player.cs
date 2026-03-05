using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    private float movement;
    public float movementSpeed = 5f;
    private bool faceingRight = true;
    public bool isGround = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        if(movement < 0f && faceingRight)
        {
            transform.eulerAngles = new Vector3(0f, -180f, 0f);  
            faceingRight = false;
        }
        else if(movement > 0f  && faceingRight == false)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            faceingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
            animator.SetBool("Jump", true);
        }

        if(Mathf.Abs(movement) > .1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if(movement < .1f)
        {
            animator.SetFloat("Run", 0f);

        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }


    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(movement, 0f, 0f) * Time.fixedDeltaTime * movementSpeed;
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            animator.SetBool("Jump", false);
        }
    }

}
