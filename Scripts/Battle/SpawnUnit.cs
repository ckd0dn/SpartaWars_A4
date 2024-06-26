using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnUnit : MonoBehaviour
{
    [SerializeField] GameObject unitCharacter;
    private Transform spawnPoint; // 유닛이 스폰될 위치
    private Transform centerPoint; // Center 오브젝트의 Transform
    private UnitData unitData; // 유닛 데이터
    [SerializeField] BattleMana battleMana; // BattleMana 클래스 참조
    [SerializeField] private Image unitImage;
    [SerializeField] private TextMeshProUGUI unitCost;

    private void Start()
    {
        // unitCharacter에서 UnitDataSO를 가져와서 unitData에 할당
        if (unitCharacter != null)
        {
            Character dataHolder = unitCharacter.GetComponent<Character>();
            if (dataHolder != null)
            {
                unitData = dataHolder.data;
            }
            else
            {
                Debug.LogError("UnitCharacter에 UnitDataHolder가 없습니다.");
            }
        }

        // 태그를 사용하여 스폰 포인트와 센터 포인트를 동적으로 찾아 할당
        GameObject spawnPointObj = GameObject.FindWithTag("SpawnPoint");
        if (spawnPointObj != null)
        {
            spawnPoint = spawnPointObj.transform;
        }
        else
        {
            Debug.LogError("SpawnPoint 태그를 가진 객체를 찾을 수 없습니다.");
        }

        GameObject centerPointObj = GameObject.FindWithTag("CenterPoint");
        if (centerPointObj != null)
        {
            centerPoint = centerPointObj.transform;
        }
        else
        {
            Debug.LogError("CenterPoint 태그를 가진 객체를 찾을 수 없습니다.");
        }
        unitImage.sprite = unitData.icon;
        unitCost.text = unitData.cost.ToString();
    }

    public void Spawn()
    {
        if (unitCharacter != null && spawnPoint != null && centerPoint != null && battleMana != null && unitData != null)
        {
            // 유닛 생성 비용 체크
            if (battleMana.ManaCheck(unitData.cost))
            {
                // Y축의 랜덤 값을 생성
                float randomY = Random.Range(spawnPoint.transform.position.y, spawnPoint.transform.position.y + 0.1f);
                Vector3 randomSpawnPosition = new Vector3(spawnPoint.transform.position.x, randomY, spawnPoint.transform.position.z);

                GameObject newUnit = Instantiate(unitCharacter, randomSpawnPosition, spawnPoint.transform.rotation);
                newUnit.transform.Rotate(0f, 180f, 0f); // y축 회전을 180도로 설정하여 좌우 반전
                UnitMove unitMover = newUnit.GetComponent<UnitMove>();

                if (unitMover != null)
                {
                    unitMover.Initialize(centerPoint.transform.position);
                }
                else
                {
                    Debug.LogError("UnitMove 컴포넌트가 없습니다.");
                }

                // 마나 소모
                battleMana.SpendMana(unitData.cost);
                battleMana.UpdateMana();
            }
            else
            {
                Debug.LogError("마나가 부족합니다.");
            }
        }
        else
        {
            Debug.LogError("유닛 생성 실패: 필수 요소가 누락되었습니다.");
        }
    }
}
