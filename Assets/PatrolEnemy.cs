using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public int maxHealth = 3;
    public int attackDamage = 1;
    public float attackCooldown = 1f;
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
    private float nextAttackTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(FindObjectOfType<GameManager>().isGameActive == false)
        {
            return;
        }

        if (maxHealth <= 0)
        {
            Die();
        }
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

                transform.position = Vector2.MoveTowards(transform.position,  
                    player.position, chaseSpeed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("Attack1", true);
                Attack();
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
        if (Time.time < nextAttackTime)
        {
            return;
        }

        nextAttackTime = Time.time + attackCooldown;

        Player targetPlayer = null;
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);

        if (collInfo)
        {
            targetPlayer = collInfo.gameObject.GetComponent<Player>();
        }

        if (targetPlayer == null && player != null && Vector2.Distance(transform.position, player.position) <= retrieveDistance + 0.2f)
        {
            targetPlayer = player.GetComponent<Player>();
        }

        if (targetPlayer != null)
        {
            targetPlayer.TakeDamege(attackDamage);
        }
    }

    public void TakeDamege(int damage)
    {
        if (maxHealth <= 0)
        {
            return;
        }
        maxHealth -= damage;
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

        if(attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    void Die()
    {
        Debug.Log(this.transform.name + " Die");
        Destroy(this.gameObject);
    }

}
