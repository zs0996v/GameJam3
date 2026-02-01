using UnityEngine;

public class GoldStack : MonoBehaviour
{
    public int stackValue = 500000;
    public AudioClip moneySound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMoney money = other.GetComponent<PlayerMoney>();

        if (money != null)
        {
            money.AddMoney(stackValue);

            if (AudioManager.Instance != null)
                AudioManager.Instance.PlayMoney(moneySound);

            Destroy(gameObject);
        }
    }
}