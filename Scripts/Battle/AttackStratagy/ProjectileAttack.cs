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
                    break; // 애니메이션 트리거 후 루프 종료
                }
                else if (defenceBase != null && defenceBase.currentCastleHp > 0)
                {

                    spumPrefabs.PlayAnimation(4);
                    attacked = true;
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
                projScript.player = true; // 예시로 플레이어 불릿 여부를 설정
            }
        }
    }


    // 자식객체를 전부 찾아서 해당하는 이름의 자식객체를 찾음
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