using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;
    public float bulletLifeTime = 2f;
    public float bulletHitRadius = 0.15f; // hit size

    [Header("Shooting")]
    public float fireRate = 0.25f; // seconds between shots
    float nextFireTime;

    // last direction player moved (shoot direction)
    Vector2 lastMoveDir = Vector2.right;

    class BulletData
    {
        public GameObject obj;
        public Vector2 dir;
        public float alive;
    }

    readonly List<BulletData> bullets = new List<BulletData>();

    void Update()
    {
        // 1) Remember movement direction (WASD / Arrows)
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(mx, my);

        if (moveDir != Vector2.zero)
            lastMoveDir = moveDir.normalized;

        // 2) Shoot (hold Space) with fire rate limit
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot(lastMoveDir);
            nextFireTime = Time.time + fireRate;
        }

        // 3) Move bullets + hit check + lifetime
        UpdateBullets();
    }

    void Shoot(Vector2 dir)
    {
        if (bulletPrefab == null) return;

        // spawn slightly in front of player
        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);

        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        // rotate bullet to face direction (bullet sprite should face RIGHT by default)
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0, 0, angle);

        bullets.Add(new BulletData { obj = b, dir = dir, alive = 0f });
    }

    void UpdateBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            BulletData b = bullets[i];

            if (b.obj == null)
            {
                bullets.RemoveAt(i);
                continue;
            }

            // Move bullet
            b.obj.transform.position += (Vector3)(b.dir * bulletSpeed * Time.deltaTime);

            // Hit check (Enemy must have Collider2D + tag "Enemy")
            Collider2D hit = Physics2D.OverlapCircle((Vector2)b.obj.transform.position, bulletHitRadius);

            if (hit != null && hit.CompareTag("Enemy"))
            {
                Destroy(hit.gameObject);  // kill enemy
                Destroy(b.obj);           // destroy bullet
                bullets.RemoveAt(i);
                continue;
            }

            // Lifetime
            b.alive += Time.deltaTime;
            if (b.alive >= bulletLifeTime)
            {
                Destroy(b.obj);
                bullets.RemoveAt(i);
            }
        }
    }

    // Optional: shows the bullet hit radius in Scene view when player is selected
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
