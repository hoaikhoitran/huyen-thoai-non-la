using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Chỉ số Sinh mệnh")]
    public int maxHealth = 3;
<<<<<<< Updated upstream
    public Text health;
    public Animator animator;
    public Rigidbody2D rb;
    public float jumpHeight = 5f;
    private float movement;
    public float movementSpeed = 5f;
    private bool faceingRight = true;
    public bool isGround = true;
=======
    private int currentHealth;
    public Text healthText;
    public HealthBar healthBar;

    [Header("Di chuyển (Mượt như Silksong)")]
    public float movementSpeed = 8f;
    public float acceleration = 50f;
    public float deceleration = 60f;
    public float jumpForce = 12f;
    private float movement;
    private bool facingRight = true;
    public bool isGround = true;

    [Header("Hệ thống Hồi chiêu (Cooldown)")]
    public float kiemKhiCD = 2f;
    private float lastKiemKhi;
    public float ultiCD = 10f;
    private float lastUlti;
    public float healCD = 5f;
    private float lastHeal;

    [Header("Kỹ năng & Hiệu ứng")]
    public GameObject kiemKhiPrefab;
    public Transform firePoint;
    public Animator animator;
    public Rigidbody2D rb;
    private AudioSource playerAudio;
    public AudioClip attackSound;
    public AudioClip takeDamageSound;
    public AudioClip jumpSound; 
    public CameraShake cameraShake;
>>>>>>> Stashed changes

    [Header("Chiến đấu")]
    public Transform attackPoint;
    public float attackRadius = 1.2f;
    public LayerMask attackLayer;

    void Start()
    {
        
    }

    void Update()
    {
<<<<<<< Updated upstream
        if(maxHealth <= 0)
=======
        if (currentHealth <= 0) { Die(); return; }
        movement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGround) { Jump(); }

        if (Input.GetMouseButtonDown(0)) { animator.SetTrigger("Attack"); }

        if (Input.GetKeyDown(KeyCode.Q) && Time.time > lastKiemKhi + kiemKhiCD)
>>>>>>> Stashed changes
        {
            SkillKiemKhi();
            lastKiemKhi = Time.time;
        }
        health.text = maxHealth.ToString();

        if (Input.GetKeyDown(KeyCode.E) && Time.time > lastHeal + healCD)
        {
            HealAction();
            lastHeal = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.R) && Time.time > lastUlti + ultiCD)
        {
            UltiSpin();
            lastUlti = Time.time;
        }

        HandleAnimations();
        HandleFlip();
    }

    private void FixedUpdate()
    {
        float targetSpeed = movement * movementSpeed;
        float speedDif = targetSpeed - rb.linearVelocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movementForce = speedDif * accelRate;
        rb.AddForce(movementForce * Vector2.right, ForceMode2D.Force);
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, attackLayer);
        if (attackSound) playerAudio.PlayOneShot(attackSound);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PatrolEnemy>()?.TakeDamege(1);
            StartCoroutine(HitStop(0.07f));
            if (cameraShake) StartCoroutine(cameraShake.Shake(0.1f, 0.2f));
        }
    }

    void SkillKiemKhi()
    {
        animator.SetTrigger("Attack");
        if (kiemKhiPrefab && firePoint)
        {
            GameObject projectile = Instantiate(kiemKhiPrefab, firePoint.position, Quaternion.identity);
            float dir = facingRight ? 1 : -1;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(dir * 15f, 0);
            Destroy(projectile, 2f);
        }
    }

    void UltiSpin()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRadius * 2.5f, attackLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PatrolEnemy>()?.TakeDamege(3);
        }
    }

    void HealAction()
    {
        currentHealth = Mathf.Clamp(currentHealth + 1, 0, maxHealth);
        UpdateHealthUI();
    }

    public void TakeDamege(int damage) // Hàm này cực quan trọng để Quái chém trúng
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (cameraShake) StartCoroutine(cameraShake.Shake(0.2f, 0.4f));
        if (takeDamageSound) playerAudio.PlayOneShot(takeDamageSound);
    }

    IEnumerator HitStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }

    void HandleFlip()
    {
        if (movement > 0 && !facingRight) { Flip(); }
        else if (movement < 0 && facingRight) { Flip(); }
    }

    void Flip() { facingRight = !facingRight; transform.Rotate(0, 180, 0); }

    void Jump()
    {
<<<<<<< Updated upstream
        rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
=======
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGround = false;
        animator.SetBool("Jump", true);
        if (jumpSound) playerAudio.PlayOneShot(jumpSound);
    }

    void HandleAnimations() { animator.SetFloat("Run", Mathf.Abs(movement)); }

    private void UpdateHealthUI()
    {
        if (healthText) healthText.text = currentHealth.ToString();
        if (healthBar) healthBar.UpdateBar(currentHealth, maxHealth);
>>>>>>> Stashed changes
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { isGround = true; animator.SetBool("Jump", false); }
    }

<<<<<<< Updated upstream
    public void Attack() 
    {
        Collider2D collInfo = Physics2D.OverlapCircle(attackPoint.position, attackRadius, attackLayer);
        if(collInfo)       
        {
            if(collInfo.GetComponent<PatrolEnemy>() != null)
            {
                collInfo.GetComponent<PatrolEnemy>().TakeDamege(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    public void TakeDamege(int damage)
    {
        if(maxHealth <= 0)
        {
            return;
        }
        maxHealth -= damage;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "VictoryPoint")
        {
            //FindObjectOfType<>().LoadLevel();
            SceneManager.LoadScene("Win");

        }
    }

    void Die()
    {
        Debug.Log("Player Died");
        FindObjectOfType<GameManager>().isGameActive = false;
        Destroy(this.gameObject);
        SceneManager.LoadScene("Lose");
    }

}
=======
    void Die() { SceneManager.LoadScene("Lose"); Destroy(gameObject); }
}
>>>>>>> Stashed changes
