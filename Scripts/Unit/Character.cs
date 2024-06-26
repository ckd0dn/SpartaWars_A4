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
        goldUI = FindObjectOfType<GoldUI>(); // GoldUI ��ũ��Ʈ�� ���� ������Ʈ�� ã�� �Ҵ�
        paintWhite = GetComponent<PaintWhite>();
    }

    private void Die()
    {
        spumPrefabs.PlayAnimation(2);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        // �ǰݽ� �Ͼ�� �̹����� ����
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
