using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private Character character;
    private Vector3 moveDirection;
    private float moveSpeed;
    [SerializeField] public bool canMove = true; // 유닛이 이동할 수 있는 상태인지 확인하는 플래그
    public int enemyLayer;
    private float detectionRange;
    private LayerMask enemyLayerMask;
    public SPUM_Prefabs spumPrefabs;

    private void Awake()
    {
        character = GetComponent<Character>();
        moveSpeed = character.data.moveSpeed;
        enemyLayerMask = 1 << enemyLayer; // 적 레이어 마스크 설정
        detectionRange = character.data.Range;
    }

    public void Initialize(Vector3 target)
    {
        moveDirection = (target - transform.position).normalized;
    }

    private void Update()
    {
        if (canMove)
        {
            MoveTowardsTarget();
        }

        DetectEnemy(); // 적 감지
    }

    private void MoveTowardsTarget()
    {
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        // 추락하지 않기 위해 y값을 고정한 상태에서 이동
        newPosition.y = transform.position.y; // y값 고정
        transform.position = newPosition;
        spumPrefabs.PlayAnimation(1); // Run 애니메이션 실행
    }

    private void DetectEnemy()
    {
        Debug.DrawLine(transform.position, transform.position + moveDirection * detectionRange, Color.black);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, character.data.Range, enemyLayerMask);

        bool enemyDetected = false;

        // 감지된 모든 충돌을 검사합니다.
        foreach (var hit in hits)
        {
            if (hits != null)
            {
                enemyDetected = true;
                break;
            }
        }

        // 적 감지 여부에 따라 이동 가능 여부를 설정합니다.
        canMove = !enemyDetected;

        if (!canMove)
        {
            spumPrefabs.PlayAnimation(0); // Idle 애니메이션 실행
        }
    }
}