
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    [Header("HP設定")]
    public int maxHP = 100;
    private int currentHP;

    [Header("HPバー")]
    public Slider hpSlider;

    void Start()
    {
        currentHP = maxHP;

        if (hpSlider != null)
        {
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    // ダメージを受けたときに呼び出す
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        if (hpSlider != null)
        {
            hpSlider.value = currentHP;
        }

        // HPが0になったらスライムを削除
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
