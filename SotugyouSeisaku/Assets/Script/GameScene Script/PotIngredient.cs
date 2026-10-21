
using UnityEngine;
using UnityEngine.UI;

public class PotIngredient : MonoBehaviour
{
    [Header("食材を表示する3つのパネル")]
    public Image[] ingredientPanels = new Image[3];


    [Header("料理するボタン")]
    public GameObject cookButton;

    private int currentIndex = 0;

    private void Start()
    {
        //始まったときパネル空に
        ClearPanels();
        cookButton.SetActive(false);
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

        // 食材が1つでも入ったら料理ボタンを表示
        if (cookButton != null)
        {
            cookButton.SetActive(true);
        }

        //鍋に当たったら食材を消す
        Destroy(other.gameObject);
    }


    // 料理するボタンから呼び出す
    public void Cook()
    {
        // パネルをすべて空にする
        ClearPanels();

        // 料理ボタンを非表示にする
        if (cookButton != null)
        {
            cookButton.SetActive(false);
        }
    }

    // パネルを空にする処理
    private void ClearPanels()
    {
        foreach (Image panel in ingredientPanels)
        {
            if (panel != null)
            {
                panel.sprite = null;
                panel.enabled = false;
            }
        }

        // 次の食材を1番目のパネルから表示する
        currentIndex = 0;
    }
}
