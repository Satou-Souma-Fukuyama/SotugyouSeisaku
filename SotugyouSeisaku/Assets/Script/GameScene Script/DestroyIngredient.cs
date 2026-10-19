using UnityEngine;

public class DestroyIngredient : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int destroyPoint = -6;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < destroyPoint)
        {
            Destroy(gameObject);
        }
    }
}
