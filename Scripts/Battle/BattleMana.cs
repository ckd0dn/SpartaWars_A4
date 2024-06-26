using UnityEngine;
using TMPro;

public class BattleMana : Singleton<BattleMana>
{
    [SerializeField] private float currentMana;
    private float recoveryRate;
    [SerializeField] private TextMeshProUGUI manaText;

    public TextMeshProUGUI manaLevelText;
    public TextMeshProUGUI manaUpgradeText;
    [SerializeField] private int manaLevel;

    private void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        recoveryRate = 1;
        UpdateMana();

        manaLevel = 1;
        manaLevelText.text = $"Lv.{manaLevel}";
    }

    private void Update()
    {
        if (currentMana < DataManager.Instance.maximumMana)
        {
            currentMana += Time.deltaTime * recoveryRate;
            currentMana = Mathf.Min(currentMana, DataManager.Instance.maximumMana); // 마나가 최대치를 넘지 않도록 제한
            UpdateMana();
        }
        manaUpgradeText.text = $"업그레이드\n {DataManager.Instance.maximumMana / 2}";
    }

    public bool ManaCheck(int manaCost)
    {
        if (currentMana - manaCost > 0) return true;
        else return false;
    }

    public void SpendMana(int manaCost)
    {
        currentMana -= manaCost;
        UpdateMana();
    }

    public void UpdateMana()
    {
        manaText.text = $" {currentMana.ToString("N0")} / {DataManager.Instance.maximumMana}";
    }

    public void ManaBoost()
    {
        if (currentMana > DataManager.Instance.maximumMana / 2)
        {
            recoveryRate++;
            currentMana -= (DataManager.Instance.maximumMana / 2);

            manaLevel++;
            manaLevelText.text = $"Lv.{manaLevel}";
        }
        else
        {
            UIManager.Instance.Show("업그레이드 마나 부족");
        }
    }

    public void ManaUpgradeApply()
    {
        recoveryRate = DataManager.Instance.manaRegen;
    }
}
