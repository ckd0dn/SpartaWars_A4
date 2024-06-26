using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private Character character;
    private Vector3 moveDirection;
    private float moveSpeed;
    [SerializeField] public bool canMove = true; // ������ �̵��� �� �ִ� �������� Ȯ���ϴ� �÷���
    public int enemyLayer;
    private float detectionRange;
    private LayerMask enemyLayerMask;
    public SPUM_Prefabs spumPrefabs;

    private void Awake()
    {
        character = GetComponent<Character>();
        moveSpeed = character.data.moveSpeed;
        enemyLayerMask = 1 << enemyLayer; // �� ���̾� ����ũ ����
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

        DetectEnemy(); // �� ����
    }

    private void MoveTowardsTarget()
    {
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        // �߶����� �ʱ� ���� y���� ������ ���¿��� �̵�
        newPosition.y = transform.position.y; // y�� ����
        transform.position = newPosition;
        spumPrefabs.PlayAnimation(1); // Run �ִϸ��̼� ����
    }

    private void DetectEnemy()
    {
        Debug.DrawLine(transform.position, transform.position + moveDirection * detectionRange, Color.black);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, character.data.Range, enemyLayerMask);

        bool enemyDetected = false;

        // ������ ��� �浹�� �˻��մϴ�.
        foreach (var hit in hits)
        {
            if (hits != null)
            {
                enemyDetected = true;
                break;
            }
        }

        // �� ���� ���ο� ���� �̵� ���� ���θ� �����մϴ�.
        canMove = !enemyDetected;

        if (!canMove)
        {
            spumPrefabs.PlayAnimation(0); // Idle �ִϸ��̼� ����
        }
    }
}