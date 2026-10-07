using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("食材を出す場所")]
    public Transform spawnPoint;

    [Header("食材Prefab")]
    public GameObject potatoPrefab;
    public GameObject tomatoPrefab;
    //public GameObject meatPrefab;
   // public GameObject applePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ジャガイモ
    public void SpawnPotato()
    {
        SpawnIngredient(potatoPrefab);
    }

    // 人参
    public void Spawntomato()
    {
        SpawnIngredient(tomatoPrefab);
    }

    // 食材を生成
    private void SpawnIngredient(GameObject ingredient)
    {
        if (ingredient == null)
        {
            Debug.LogWarning("食材Prefabが設定されていません");
            return;
        }

        Instantiate(
            ingredient,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}
