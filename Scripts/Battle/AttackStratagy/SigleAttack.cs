using System.Collections;
using UnityEngine;

public class SigleAttack : AttackOnEnemy
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
            Collider2D closestHit = null;
            float closestDistance = float.MaxValue;
            bool attacked = false;

            foreach (var hit in hits)
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestHit = hit;
                }
            }

            if (closestHit != null)
            {
                Character enemy = closestHit.GetComponent<Character>();
                DefenceBase defenceBase = closestHit.GetComponent<DefenceBase>();

                if ((enemy != null && enemy.currentHealth > 0) || (defenceBase != null && defenceBase.currentCastleHp > 0))
                {
                    // ���� �ִϸ��̼� ���
                    spumPrefabs.PlayAnimation(4);
                    yield return new WaitForSeconds(character.data.attackSpeed);
                    spumPrefabs.PlayAnimation(0);
                }
            }
            if (attacked)
            {
                yield return new WaitForSeconds(character.data.attackSpeed);
            }
            else
            {
                yield return null; // ���� ����� ������ ���� �����ӱ��� ���
            }
        }
        isAttacking = false;
    }

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, character.data.Range, enemyLayerMask);

        Collider2D closestHit = null;
        float closestDistance = float.MaxValue;

        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHit = hit;
            }
        }

        if (closestHit != null)
        {
            Character enemy = closestHit.GetComponent<Character>();
            DefenceBase defenceBase = closestHit.GetComponent<DefenceBase>();

            if (enemy != null && enemy.currentHealth > 0)
            {
                // ���� ����
                enemy.TakeDamage(character.data.attack);
            }
            else if (defenceBase != null && defenceBase.currentCastleHp > 0)
            {
                // ���� ����
                defenceBase.TakeDamage(character.data.attack);
            }
        }
    }
}
