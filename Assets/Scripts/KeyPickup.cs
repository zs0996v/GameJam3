using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public int keyID;
    public AudioClip keySound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        var inventory = other.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        inventory.AddKey(keyID);

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlayKey(keySound);

        Destroy(gameObject);
    }
}