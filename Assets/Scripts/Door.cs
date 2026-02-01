using UnityEngine;

public class Door : MonoBehaviour
{
    public int requiredKeyID;
    public AudioClip doorSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        var inventory = collision.collider.GetComponent<PlayerInventory>();
        if (inventory == null) return;

        if (inventory.HasKey(requiredKeyID))
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayDoor(doorSound);

            Destroy(gameObject);
        }
    }
}