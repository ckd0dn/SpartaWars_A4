using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject[] enemyCharacterArray1;
    [SerializeField] GameObject[] enemyCharacterArray2;
    [SerializeField] GameObject[] enemyCharacterArray3;
    [SerializeField] GameObject[] enemyCharacterArray4;
    [SerializeField] GameObject[] bossEnemies; // �� ���������� �����ϴ� ���� ���� �迭

    [SerializeField] Transform spawnPoint; // ������ ������ ��ġ
    [SerializeField] Transform centerPoint; // Center ������Ʈ�� Transform

    [Header("�ʱ⽺�� ����")]
    [SerializeField] float initialSpawnInterval; // �ʱ� ���� ����

    [Header("ù ���� ����ð� (�� ����)")]
    [SerializeField] float firstIntervalChangeTime; // ù ���� ����ð�
    [SerializeField] float firstChangeSpawnTime;

    [Header("2��° ���� ���� �ð� (�� ����)")]
    [SerializeField] float secondIntervalChangeTime; // 2��° ���� ���� �ð�
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
                Debug.LogError("����ġ ���� ��Ż��");
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

                // y�� ���� �߰�
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
                    Debug.LogError("UnitMove ������Ʈ X.");
                }
            }
        }
        else
        {
            Debug.Log("���� ���� X.");
        }
    }

    public void SpawnBoss(int stage)
    {
        if (stage >= 1 && stage <= bossEnemies.Length && spawnPoint != null && centerPoint != null)
        {
            GameObject selectedBoss = bossEnemies[stage - 1]; // �������� ��ȣ�� �´� ���� ����

            // y�� ���� �߰�
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
                Debug.LogError("UnitMove ������Ʈ X.");
            }
        }
        else
        {
            Debug.LogError("���� ���� ���� ���� ���� ��Ż or ����Ʈ ���� �Ǽ�");
        }
    }
}
