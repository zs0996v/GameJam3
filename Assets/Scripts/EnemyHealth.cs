using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 5;

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}