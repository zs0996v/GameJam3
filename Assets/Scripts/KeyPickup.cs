using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public int keyID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        inventory.AddKey(keyID);
        Destroy(gameObject);
    }
}
