using System.Collections;
using UnityEngine;

public class MultipleAttack : AttackOnEnemy
{
    public SPUM_Prefabs spumPrefabs;

    private void Update()
    {
        if (!isAttacking && character.currentHealth > 0)
        {
            StartCoroutine(Attack());
        }
    }

    protected override IEnumerator Attack()
    {
        isAttacking = true;
        while (character.currentHealth > 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, character.data.Range, enemyLayerMask);
            bool attacked = false;

            foreach (var hit in hits)
            {
                Character enemy = hit.GetComponent<Character>();
                DefenceBase defenceBase = hit.GetComponent<DefenceBase>();

                if ((enemy != null && enemy.currentHealth > 0) || (defenceBase != null && defenceBase.currentCastleHp > 0))
                {
                    // 공격 애니메이션 시작
                    spumPrefabs.PlayAnimation(4);
                    attacked = true;
                    spumPrefabs.PlayAnimation(0);
                    break; // 애니메이션 트리거 후 루프 종료
                }
            }
            if (attacked)
            {
                yield return new WaitForSeconds(character.data.attackSpeed);
            }
            else
            {
                yield return null; // 공격 대상이 없으면 다음 프레임까지 대기
            }
        }
        isAttacking = false;

    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, character.data.Range, enemyLayerMask);

        foreach (var hit in hits)
        {
            Character enemy = hit.GetComponent<Character>();
            DefenceBase defenceBase = hit.GetComponent<DefenceBase>();

            if (enemy != null && enemy.currentHealth > 0)
            {
                // 적 유닛 공격
                enemy.TakeDamage(character.data.attack);
            }

            if (defenceBase != null && defenceBase.currentCastleHp > 0)
            {
                // 성 공격
                defenceBase.TakeDamage(character.data.attack);
            }
        }
    }
}
