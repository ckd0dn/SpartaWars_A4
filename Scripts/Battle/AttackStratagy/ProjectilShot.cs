using UnityEngine;

public class ProjectilShot : MonoBehaviour
{
    public string projectileTag;
    public Transform firePoint;
    public UnitData data;

    public void LaunchProjectile(Transform target)
    {
        GameObject projectile = ObjectPool.Instance.SpawnFromPool(projectileTag, firePoint.position, firePoint.rotation);
        if (projectile != null)
        {
            Projectile projScript = projectile.GetComponent<Projectile>();
            if (projScript != null)
            {
                projScript.unitData.attack = data.attack;
                projScript.SetPoolTag(projectileTag);
                projectile.transform.right = (target.position - firePoint.position).normalized;
            }
        }
    }
}
