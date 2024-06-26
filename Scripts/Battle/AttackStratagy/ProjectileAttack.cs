using System.Collections;
using UnityEngine;

public class ProjectileAttack : AttackOnEnemy
{
    public string projectileTag;
    public Transform firePoint;
    public SPUM_Prefabs spumPrefabs;

    private void Start()
    {
        firePoint = FindDeepChild(transform, "Spawn");
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

                if (enemy != null && enemy.currentHealth > 0)
                {

                    spumPrefabs.PlayAnimation(4);
                    attacked = true;
                    break; // �ִϸ��̼� Ʈ���� �� ���� ����
                }
                else if (defenceBase != null && defenceBase.currentCastleHp > 0)
                {

                    spumPrefabs.PlayAnimation(4);
                    attacked = true;
                    break; // �ִϸ��̼� Ʈ���� �� ���� ����
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

    public void LaunchProjectile()
    {
        GameObject projectile = ObjectPool.Instance.SpawnFromPool(projectileTag, firePoint.position, firePoint.rotation);
        if (projectile != null)
        {
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
            {
                projScript.unitData.attack = character.data.attack;
                projScript.SetPoolTag(projectileTag);
                projScript.player = true; // ���÷� �÷��̾� �Ҹ� ���θ� ����
            }
        }
    }


    // �ڽİ�ü�� ���� ã�Ƽ� �ش��ϴ� �̸��� �ڽİ�ü�� ã��
    private Transform FindDeepChild(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;
            Transform result = FindDeepChild(child, childName);
            if (result != null)
                return result;
        }
        return null;
    }
}