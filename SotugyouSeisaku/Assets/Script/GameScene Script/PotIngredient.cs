
using UnityEngine;
using UnityEngine.UI;

public class PotIngredient : MonoBehaviour
{
    [Header("食材を表示する3つのパネル")]
    public Image[] ingredientPanels = new Image[3];

    private int currentIndex = 0;

    private void Start()
    {
        // パネルを空にする
        foreach (Image panel in ingredientPanels)
        {
            if (panel != null)
            {
                panel.sprite = null;
                panel.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 3つすべて埋まっていたら終了
        if (currentIndex >= ingredientPanels.Length)
            return;

        // 当たった食材Prefabから画像を取得
        SpriteRenderer ingredientRenderer =
            other.GetComponent<SpriteRenderer>();

        if (ingredientRenderer == null ||
            ingredientRenderer.sprite == null)
            return;

        // 空いているパネルに画像を表示
        Image panel = ingredientPanels[currentIndex];

        if (panel == null)
            return;

        panel.sprite = ingredientRenderer.sprite;
        panel.preserveAspect = true;
        panel.enabled = true;

        currentIndex++;

        //鍋に当たったら食材を消す
        Destroy(other.gameObject);
    }
}
