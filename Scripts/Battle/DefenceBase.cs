using UnityEngine;

public class DefenceBase : MonoBehaviour
{
    public int castleHp;
    public int currentCastleHp;
    public bool player;
    private PaintWhite paintWhite;
    public GameObject defeatPannel;
    public GameObject victoryPannel;
    public EnemySpawn bossSpawn;

    private bool bossSpawned; // ���� ���Ͱ� ��ȯ�Ǿ����� ���θ� ����


    public GameObject NextStageBtn;


    private void Awake()
    {
        if (!player)
        {
            currentCastleHp = castleHp * DataManager.Instance.selectStage;
        }
        paintWhite = GetComponent<PaintWhite>();
        bossSpawned = false;
    }

    private void GameOver()
    {
        Destroy(gameObject);
        ShowGameOverPannel();
    }

    public void TakeDamage(int damage)
    {
        currentCastleHp -= damage;
        // �ǰݽ� �Ͼ�� �̹����� ����
        SoundManager.Instance.PlaySFX(0);
        paintWhite.FlashWhite();
        if (!bossSpawned && currentCastleHp <= castleHp / 2 && !player)
        {
            SpawnBoss();
            bossSpawned = true; // ���� ���Ͱ� �� ���� ��ȯ�ǵ��� ����
        }

        if (currentCastleHp <= 0)
        {
            GameOver();
        }
    }

    private void ShowGameOverPannel()
    {
        int layer = gameObject.layer;

        int playerLayerMask = LayerMask.NameToLayer("Player");
        int enemeyLayerMask = LayerMask.NameToLayer("Enemy");

        if(layer == playerLayerMask)
        {
            defeatPannel.SetActive(true);
        }
        else if(layer == enemeyLayerMask)
        {
            if ( DataManager.Instance.selectStage == 4)
            {
                NextStageBtn.SetActive(false);
            }

            victoryPannel.SetActive(true);

            if ( DataManager.Instance.selectStage >= DataManager.Instance.userData.BestUnlockStage )
            {
                DataManager.Instance.userData.BestUnlockStage++;
            }
        }
    }


    public void CastleUpgradeApply()
    {
        if (player)
        {
            castleHp = DataManager.Instance.castleHP;
        }

        currentCastleHp = castleHp;
    }

    private void SpawnBoss()
    {
        if (bossSpawn != null)
        {
            bossSpawn.SpawnBoss(DataManager.Instance.selectStage);
        }
        else
        {
            Debug.LogError("EnemySpawn ��ũ��Ʈ�� ���ų�, �Ʊ��� ��");
        }
    }
}
