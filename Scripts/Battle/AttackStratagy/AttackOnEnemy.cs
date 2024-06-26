using System.Collections;
using UnityEngine;

public class AttackOnEnemy : MonoBehaviour
{
    protected Character character;
    [SerializeField] protected LayerMask enemyLayerMask; // �� ���̾� ����ũ ����
    protected bool isAttacking = false; // ���� ������ Ȯ���ϴ� �÷���
    protected Vector2 initialDirection; // �ʱ� ���� ���� ����

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    protected virtual IEnumerator Attack()
    {
        return null;
    }
}
