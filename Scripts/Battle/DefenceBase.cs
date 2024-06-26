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

    private bool bossSpawned; // 보스 몬스터가 소환되었는지 여부를 추적


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
        // 피격시 하얗게 이미지가 변함
        SoundManager.Instance.PlaySFX(0);
        paintWhite.FlashWhite();
        if (!bossSpawned && currentCastleHp <= castleHp / 2 && !player)
        {
            SpawnBoss();
            bossSpawned = true; // 보스 몬스터가 한 번만 소환되도록 설정
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
            Debug.LogError("EnemySpawn 스크립트가 없거나, 아군측 성");
        }
    }
}
