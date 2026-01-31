using UnityEngine;
using TMPro;

public class PlayerMoney : MonoBehaviour
{
    public int money = 0;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = "£ 0";
    }

    public void AddMoney(int amount)
    {
        StartCoroutine(AnimateMoney(amount, 3f));
    }

    private System.Collections.IEnumerator AnimateMoney(int amount, float duration)
    {
        int startMoney = money;
        int targetMoney = money + amount;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            moneyText.text = "£ " + Mathf.RoundToInt(Mathf.Lerp(startMoney, targetMoney, t)).ToString("N0"); 
            yield return null;
        }

        money = targetMoney;
        moneyText.text = "£ " + money.ToString("N0");
    }
}