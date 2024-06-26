using UnityEngine;

public class Character : MonoBehaviour
{
    public UnitData data;
    public int currentHealth;
    private GoldUI goldUI;
    private PaintWhite paintWhite;
    [SerializeField] private SPUM_Prefabs spumPrefabs;
    public bool player;

    private void Awake()
    {
        currentHealth = data.health;
        goldUI = FindObjectOfType<GoldUI>(); // GoldUI 스크립트를 가진 오브젝트를 찾아 할당
        paintWhite = GetComponent<PaintWhite>();
    }

    private void Die()
    {
        spumPrefabs.PlayAnimation(2);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // 피격시 하얗게 이미지가 변함
        SoundManager.Instance.PlaySFX(0);
        paintWhite.FlashWhite();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void DropMana()
    {
        BattleMana.Instance.SpendMana(-data.dropMana);
    }

    public void DropGold()
    {
        DataManager.Instance.userData.Gold += data.dropGold;
        goldUI.UpdateGoldUI();
    }
}
