using UnityEngine;

public class SlimeAnimationController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator slimeAnimator;
    void Start()
    {
        slimeAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SlimeAttack();
    }

    public void SlimeAttack()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            slimeAnimator.SetTrigger("Attack");
            Debug.Log("A");
        }
    }
}
