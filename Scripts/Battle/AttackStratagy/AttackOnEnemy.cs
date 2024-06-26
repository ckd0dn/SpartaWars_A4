using System.Collections;
using UnityEngine;

public class AttackOnEnemy : MonoBehaviour
{
    protected Character character;
    [SerializeField] protected LayerMask enemyLayerMask; // 적 레이어 마스크 설정
    protected bool isAttacking = false; // 공격 중인지 확인하는 플래그
    protected Vector2 initialDirection; // 초기 레이 방향 저장

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
