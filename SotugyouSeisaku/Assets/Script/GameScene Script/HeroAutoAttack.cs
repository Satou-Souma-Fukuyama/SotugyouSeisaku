
using System.Collections;
using UnityEngine;

public class HeroAutoAttack : MonoBehaviour
{
    [Header("攻撃設定")]
    public int attackDamage ;
    public float attackInterval ;

    [Header("攻撃対象")]
    public EnemyHP targetSlime;

    private bool battleStarted = false;

    void Start()
    {
        // バトルシーンが始まったら自動で戦闘開始
       // StartBattle();
    }

    public void StartBattle()
    {
        if (battleStarted)
        {
            return;
        }

        battleStarted = true;
        StartCoroutine(AutoAttackCoroutine());
    }

    IEnumerator AutoAttackCoroutine()
    {
        Debug.Log("バトル開始！");

        while (battleStarted)
        {
            yield return new WaitForSeconds(attackInterval);

            // スライムが存在する場合に攻撃
            if (targetSlime != null)
            {
                targetSlime.TakeDamage(attackDamage);
                Debug.Log("勇者の攻撃！ " + attackDamage + " ダメージ");
            }
            else
            {
                Debug.Log("攻撃対象のスライムがいません。");
                battleStarted = false;
            }
        }
    }
}
