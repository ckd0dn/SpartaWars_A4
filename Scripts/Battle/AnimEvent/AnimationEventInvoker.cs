using UnityEngine;

public class AnimationEventInvoker : MonoBehaviour
{
    private MultipleAttack multipleAttack;
    private SigleAttack sigleAttack;
    private ProjectileAttack projectileAttack;
    private Character character;

    private void Start()
    {
        multipleAttack = GetComponentInParent<MultipleAttack>();
        sigleAttack = GetComponentInParent<SigleAttack>();
        projectileAttack = GetComponentInParent<ProjectileAttack>();
        character = GetComponentInParent<Character>();
    }

    public void InvokeDealAnimEvent()
    {
        if (multipleAttack != null)
        {
            multipleAttack.DealDamage();
        }
    }

    public void InvokeSingleDealAnimEvent()
    {
        if (sigleAttack != null)
        {
            sigleAttack.DealDamage();
        }
    }
    public void InvokeFireProjectile()
    {
        if (projectileAttack != null)
        {
            projectileAttack.LaunchProjectile();
        }
    }
    public void DieMotion()
    {
        character.DropGold();
        character.DropMana();
        Destroy(transform.root.gameObject);
    }
}
