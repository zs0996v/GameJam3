using UnityEngine;

public class GoldStack : MonoBehaviour
{
    public int stackValue = 500000;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMoney money = other.GetComponent<PlayerMoney>();

        if (money != null)
        {
            money.AddMoney(stackValue);
            Destroy(gameObject);
        }
    }
}