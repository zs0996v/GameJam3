using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float followRange = 6f;
    public float moveSpeed = 2f;

    public GameObject bulletPrefab;
    public float shootRange = 4f;
    public float fireRate = 1f;

    float nextShootTime;
    Transform player;

    public Vector2 enemyHalfSize = new Vector2(0.5f, 0.5f);

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position);
        float distance = direction.magnitude;

        if (distance <= followRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }

        if (distance <= shootRange && Time.time >= nextShootTime)
        {
            Vector2 firePos = GetFirePosition(direction);
            Shoot(firePos, direction.normalized);
            nextShootTime = Time.time + fireRate;
        }
    }

    Vector2 GetFirePosition(Vector2 dir)
    {
        float absX = Mathf.Abs(dir.x);
        float absY = Mathf.Abs(dir.y);
        Vector2 offset = Vector2.zero;

        if (absX > absY)
        {
            offset.x = (dir.x > 0) ? enemyHalfSize.x : -enemyHalfSize.x;
        }
        else
        {
            offset.y = (dir.y > 0) ? enemyHalfSize.y : -enemyHalfSize.y;
        }

        return (Vector2)transform.position + offset;
    }

    void Shoot(Vector2 firePos, Vector2 dir)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * 6f;
        }
    }
}