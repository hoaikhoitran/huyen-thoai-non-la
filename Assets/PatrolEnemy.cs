using UnityEngine;
using System.Collections; // ?ã thêm ?? h?t l?i IEnumerator

public class PatrolEnemy : MonoBehaviour
{
    [Header("Ch? s? c? b?n")]
    public int maxHealth = 3;
<<<<<<< Updated upstream
    public bool faceingLeft = true;
=======
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
>>>>>>> Stashed changes
    public float moveSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Ph?m vi nh?n bi?t")]
    public Transform player;
    public float attackRange = 10f;
    public float stopDistance = 1.5f;
    public bool inRange = false;

    [Header("Ki?m tra v?c th?m (Patrol)")]
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;
    private bool faceingLeft = true;

    [Header("T?n công")]
    public Animator animator;
    public Transform attackPoint;
    public float attackRadius = 0.8f;
    public LayerMask attackLayer;

    [Header("V?t lý & ??y lùi")]
    public Rigidbody2D rb;
    public float knockbackForce = 10f;

    private GameManager gameManager; // L?u t?m ?? t?i ?u hi?u su?t

    void Start()
    {
        // Tìm GameManager m?t l?n duy nh?t lúc b?t ??u ?? tránh t?n tài nguyên
        gameManager = Object.FindFirstObjectByType<GameManager>();

        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Ki?m tra tr?ng thái Game
        if (gameManager != null && !gameManager.isGameActive)
            return;

        // 2. Ki?m tra máu
        if (maxHealth <= 0) { Die(); return; }

        // 3. Tính kho?ng cách t?i Player
        if (player != null)
        {
            float distToPlayer = Vector2.Distance(transform.position, player.position);
            inRange = distToPlayer <= attackRange;

            if (inRange)
            {
                HandleChase(distToPlayer);
            }
            else
            {
<<<<<<< Updated upstream
                animator.SetBool("Attack1", true);
=======
                HandlePatrol();
>>>>>>> Stashed changes
            }
        }
        else
        {
            HandlePatrol(); // N?u không th?y Player thì c? ?i tu?n
        }
    }

    // Logic ?u?i theo
    void HandleChase(float distToPlayer)
    {
        if (player.position.x > transform.position.x && faceingLeft) { Flip(); }
        else if (player.position.x < transform.position.x && !faceingLeft) { Flip(); }

        if (distToPlayer > stopDistance)
        {
            // ?ang ?u?i theo
            animator.SetBool("Attack1", false);
            animator.SetFloat("Run", 1f); // ??m b?o ?ã có bi?n 'Run' trong Animator

            // Ch? di chuy?n theo tr?c X
            Vector2 target = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
        }
        else
        {
            // ?ã ??n g?n -> T?n công
            animator.SetFloat("Run", 0f);
            if (Time.time >= nextAttackTime)
            {
                Attack();
            }
        }
    }

    // Logic Tu?n tra (Tránh r?i xu?ng v?c)
    void HandlePatrol()
    {
        animator.SetFloat("Run", 1f);
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);
        Debug.DrawRay(checkPoint.position, Vector2.down * distance, Color.red);

        if (hit.collider == null)
        {
            Flip();
        }
    }

    public void Attack()
<<<<<<< Updated upstream
    { 
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);

        if (collInfo)
        {
            if (collInfo.gameObject.GetComponent<Player>() != null) 
            {
                collInfo.gameObject.GetComponent<Player>().TakeDamege(1);
=======
    {
        nextAttackTime = Time.time + attackCooldown;
        animator.SetTrigger("Attack1");

        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if (hitPlayer != null)
        {
            Player p = hitPlayer.GetComponent<Player>();
            if (p != null)
            {
                p.TakeDamege(attackDamage); // Kh?p v?i hàm TakeDamege trong Player.cs
>>>>>>> Stashed changes
            }
        }
    }

    public void TakeDamege(int damage)
    {
        maxHealth -= damage;
        Debug.Log("Quái b? chém! Máu còn: " + maxHealth);

        // X? lý Knockback (??y lùi)
        if (rb != null && player != null)
        {
            Vector2 knockbackDirection = (transform.position - player.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(knockbackDirection.x, 0.2f) * knockbackForce, ForceMode2D.Impulse);
        }

        StartCoroutine(FlashRed());
    }

    void Flip()
    {
        faceingLeft = !faceingLeft;
        transform.Rotate(0, 180, 0);
    }

    void Die()
    {
        // Có th? Instantiate hi?u ?ng khói ? ?ây tr??c khi Destroy
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (checkPoint) Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
        if (attackPoint) Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator FlashRed()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = Color.white;
        }
    }
}