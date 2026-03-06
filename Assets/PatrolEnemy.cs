using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public bool faceingLeft = true;
    public float moveSpeed = 2f;
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;
    public bool inRange = false;
    public Transform player;
    public float attackRange = 10f;
    public float retrieveDistance = 2.5f;
    public float chaseSpeed = 4f;
    public Animator animator;

    public Transform attackPoint;
    public float attackRadius = 1f;
    public LayerMask attackLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            inRange = true;
        }
        else 
        {  
            inRange = false;
        }

        if (inRange == true)
        {

            if(player.position.x > transform.position.x && faceingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                faceingLeft = false;
            }
            else if(player.position.x < transform.position.x && faceingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                faceingLeft = true;
            }

            if (Vector2.Distance(transform.position, player.position) > retrieveDistance)
            {
                animator.SetBool("Attack1", false);

                transform.position = Vector2.MoveTowards(transform.position,                player.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Attack1", true);
            }
        }
        else
        {
            transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

            RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

            if (hit == false && faceingLeft)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                faceingLeft = false;
            }
            else if (hit == false && faceingLeft == false)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                faceingLeft = true;
            }
        }

        
    }

    public void Attack()
    { 
        //Physics2D.OverlapCircle
    }

    private void OnDrawGizmosSelected()
    {
        if(checkPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
