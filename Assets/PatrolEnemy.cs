using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public bool faceingLeft = true;
    public float moveSpeed = 2f;
    public Transform checkPoint;
    public float distance = 1f;
    public LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);

        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position, Vector2.down, distance, layerMask);

        if (hit == false && faceingLeft)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            faceingLeft = false;
        }
        else if(hit == false && faceingLeft == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            faceingLeft = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(checkPoint == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(checkPoint.position, Vector2.down * distance);
    }

}
