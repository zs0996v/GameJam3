using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 12f;
    public float bulletLifeTime = 2f;
    public float bulletHitRadius = 0.15f;
    public float fireRate = 0.25f;
    float nextFireTime;

    public AudioClip playerBulletSound;

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
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        Vector2 moveDir = new Vector2(mx, my);

        if (moveDir != Vector2.zero)
            lastMoveDir = moveDir.normalized;

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            Shoot(lastMoveDir);
            nextFireTime = Time.time + fireRate;
        }

        UpdateBullets();
    }

    void Shoot(Vector2 dir)
    {
        if (bulletPrefab == null) return;

        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);
        GameObject b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        b.transform.rotation = Quaternion.Euler(0, 0, angle);

        bullets.Add(new BulletData { obj = b, dir = dir, alive = 0f });

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayPlayerBullet(playerBulletSound);
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

            b.obj.transform.position += (Vector3)(b.dir * bulletSpeed * Time.deltaTime);

            Collider2D hit = Physics2D.OverlapCircle((Vector2)b.obj.transform.position, bulletHitRadius);

            if (hit != null && hit.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                    enemyHealth.TakeDamage(1);

                Destroy(b.obj);
                bullets.RemoveAt(i);
                continue;
            }

            b.alive += Time.deltaTime;
            if (b.alive >= bulletLifeTime)
            {
                Destroy(b.obj);
                bullets.RemoveAt(i);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}