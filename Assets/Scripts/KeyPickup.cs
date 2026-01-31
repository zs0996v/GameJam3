using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var pk = other.GetComponent<PlayerInventory>();
        if (pk == null) return;

        pk.hasKey = true;
        Destroy(gameObject);
    }
}