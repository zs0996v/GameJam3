using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        var pk = collision.collider.GetComponent<PlayerInventory>();
        if (pk != null && pk.hasKey)
        {
            Destroy(gameObject);
        }
    }
}