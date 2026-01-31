using UnityEngine;

public class Mask : MonoBehaviour
{
    public float invisibilityDuration = 10f;
    public Sprite normalSprite;
    public Sprite maskedSprite;
    public Color invisibleColor = new Color(1f, 1f, 1f, 0.5f);

    public bool IsInvisible { get; private set; }

    SpriteRenderer sr;
    Color normalColor;
    float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        normalColor = sr.color;
        sr.sprite = normalSprite;
        IsInvisible = false;
    }

    void Update()
    {
        if (!IsInvisible) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            DisableInvisibility();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mask"))
        {
            EnableInvisibility();
            Destroy(other.gameObject);
        }
    }

    void EnableInvisibility()
    {
        IsInvisible = true;
        timer = invisibilityDuration;
        sr.sprite = maskedSprite;
        sr.color = invisibleColor;
    }

    void DisableInvisibility()
    {
        IsInvisible = false;
        sr.sprite = normalSprite;
        sr.color = normalColor;
    }
}
