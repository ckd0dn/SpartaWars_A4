using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyCharacterArray1;
    [SerializeField] GameObject[] enemyCharacterArray2;
    [SerializeField] GameObject[] enemyCharacterArray3;
    [SerializeField] GameObject[] enemyCharacterArray4;
    [SerializeField] GameObject[] bossEnemies; // 각 스테이지에 대응하는 보스 몬스터 배열

    [SerializeField] Transform spawnPoint; // 유닛이 스폰될 위치
    [SerializeField] Transform centerPoint; // Center 오브젝트의 Transform

    [Header("초기스폰 간격")]
    [SerializeField] float initialSpawnInterval; // 초기 스폰 간격

    [Header("첫 간격 변경시간 (초 단위)")]
    [SerializeField] float firstIntervalChangeTime; // 첫 간격 변경시간
    [SerializeField] float firstChangeSpawnTime;

    [Header("2번째 간격 변경 시간 (초 단위)")]
    [SerializeField] float secondIntervalChangeTime; // 2번째 간격 변경 시간
    [SerializeField] float secondChangeSpawnTime;

    public void StartSpawnRoutine()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        float spawnInterval = initialSpawnInterval;
        float elapsedTime = 0f;
        GameObject[] currentEnemyArray = GetEnemyArray(DataManager.Instance.selectStage);
        int numberOfEnemiesToSpawn = 1;

        while (true)
        {

            yield return new WaitForSeconds(spawnInterval);

            Spawn(currentEnemyArray, numberOfEnemiesToSpawn);

            elapsedTime += spawnInterval;
            
            if (elapsedTime >= secondIntervalChangeTime)
            {
                spawnInterval = secondChangeSpawnTime;
                numberOfEnemiesToSpawn = 3;
            }
            else if (elapsedTime >= firstIntervalChangeTime)
            {
                spawnInterval = firstChangeSpawnTime;
                numberOfEnemiesToSpawn = 2;
            }
            currentEnemyArray = GetEnemyArray(DataManager.Instance.selectStage);
        }


    }
    private GameObject[] GetEnemyArray(int selectStage)
    {
        switch (selectStage)
        {
            case 1:
                return enemyCharacterArray1;
            case 2:
                return enemyCharacterArray2;
            case 3:
                return enemyCharacterArray3;
            case 4:
                return enemyCharacterArray4;
            default:
                Debug.LogError("스위치 범위 이탈함");
                return null;
        }
    }

    public void Spawn(GameObject[] enemyCharacter, int numberOfEnemiesToSpawn)
    {
        if (enemyCharacter.Length > 0 && spawnPoint != null && centerPoint != null)
        {
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                int randomIndex = Random.Range(0, enemyCharacter.Length);
                GameObject selectedEnemy = enemyCharacter[randomIndex];

                // y값 고정 추가
                float randomY = Random.Range(spawnPoint.position.y, spawnPoint.position.y + 0.1f);
                Vector3 randomSpawnPosition = new Vector3(spawnPoint.position.x, randomY, spawnPoint.position.z);

                GameObject newUnit = Instantiate(selectedEnemy, randomSpawnPosition, spawnPoint.rotation);
                UnitMove unitMover = newUnit.GetComponent<UnitMove>();

                if (unitMover != null)
                {
                    unitMover.Initialize(centerPoint.position);
                }
                else
                {
                    Debug.LogError("UnitMove 컴포넌트 X.");
                }
            }
        }
        else
        {
            Debug.Log("유닛 생성 X.");
        }
    }

    public void SpawnBoss(int stage)
    {
        if (stage >= 1 && stage <= bossEnemies.Length && spawnPoint != null && centerPoint != null)
        {
            GameObject selectedBoss = bossEnemies[stage - 1]; // 스테이지 번호에 맞는 보스 선택

            // y값 고정 추가
            float randomY = Random.Range(spawnPoint.position.y, spawnPoint.position.y + 0.1f);
            Vector3 randomSpawnPosition = new Vector3(spawnPoint.position.x, randomY, spawnPoint.position.z);

            GameObject newBoss = Instantiate(selectedBoss, randomSpawnPosition, spawnPoint.rotation);

            if (newBoss.transform.localScale.x <= 1.5f)
            {
                newBoss.transform.localScale = new Vector3(1.5f, 1.5f, 0f);
            }

            UnitMove unitMover = newBoss.GetComponent<UnitMove>();
            
            if (unitMover != null)
            {
                unitMover.Initialize(centerPoint.position);
            }
            else
            {
                Debug.LogError("UnitMove 컴포넌트 X.");
            }
        }
        else
        {
            Debug.LogError("보스 몬스터 스폰 실패 범위 이탈 or 포인트 설정 실수");
        }
    }
}
