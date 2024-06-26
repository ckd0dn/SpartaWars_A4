using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] GameObject unitCharacter;
    private Transform spawnPoint; // ������ ������ ��ġ
    private Transform centerPoint; // Center ������Ʈ�� Transform
    private UnitData unitData; // ���� ������
    [SerializeField] BattleMana battleMana; // BattleMana Ŭ���� ����
    [SerializeField] private Image unitImage;
    [SerializeField] private TextMeshProUGUI unitCost;

    private void Start()
    {
        // unitCharacter���� UnitDataSO�� �����ͼ� unitData�� �Ҵ�
        if (unitCharacter != null)
        {
            Character dataHolder = unitCharacter.GetComponent<Character>();
            if (dataHolder != null)
            {
                unitData = dataHolder.data;
            }
            else
            {
                Debug.LogError("UnitCharacter�� UnitDataHolder�� �����ϴ�.");
            }
        }

        // �±׸� ����Ͽ� ���� ����Ʈ�� ���� ����Ʈ�� �������� ã�� �Ҵ�
        GameObject spawnPointObj = GameObject.FindWithTag("SpawnPoint");
        if (spawnPointObj != null)
        {
            spawnPoint = spawnPointObj.transform;
        }
        else
        {
            Debug.LogError("SpawnPoint �±׸� ���� ��ü�� ã�� �� �����ϴ�.");
        }

        GameObject centerPointObj = GameObject.FindWithTag("CenterPoint");
        if (centerPointObj != null)
        {
            centerPoint = centerPointObj.transform;
        }
        else
        {
            Debug.LogError("CenterPoint �±׸� ���� ��ü�� ã�� �� �����ϴ�.");
        }
        unitImage.sprite = unitData.icon;
        unitCost.text = unitData.cost.ToString();
    }

    public void Spawn()
    {
        if (unitCharacter != null && spawnPoint != null && centerPoint != null && battleMana != null && unitData != null)
        {
            // ���� ���� ��� üũ
            if (battleMana.ManaCheck(unitData.cost))
            {
                // Y���� ���� ���� ����
                float randomY = Random.Range(spawnPoint.transform.position.y, spawnPoint.transform.position.y + 0.1f);
                Vector3 randomSpawnPosition = new Vector3(spawnPoint.transform.position.x, randomY, spawnPoint.transform.position.z);

                GameObject newUnit = Instantiate(unitCharacter, randomSpawnPosition, spawnPoint.transform.rotation);
                newUnit.transform.Rotate(0f, 180f, 0f); // y�� ȸ���� 180���� �����Ͽ� �¿� ����
                UnitMove unitMover = newUnit.GetComponent<UnitMove>();

                if (unitMover != null)
                {
                    unitMover.Initialize(centerPoint.transform.position);
                }
                else
                {
                    Debug.LogError("UnitMove ������Ʈ�� �����ϴ�.");
                }

                // ���� �Ҹ�
                battleMana.SpendMana(unitData.cost);
                battleMana.UpdateMana();
            }
            else
            {
                Debug.LogError("������ �����մϴ�.");
            }
        }
        else
        {
            Debug.LogError("���� ���� ����: �ʼ� ��Ұ� �����Ǿ����ϴ�.");
        }
    }
}
