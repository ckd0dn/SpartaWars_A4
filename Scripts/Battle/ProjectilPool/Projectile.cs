using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("투사체의 속도")]
    public float speed = 10f;

    [Header("원거리 유닛의 데이터 SO배치")]
    public UnitData unitData;

    [Header("투사체의 유지시간")]
    [SerializeField] private float lifetime;

    [Header("적에 대한 레이어 설정")]
    public int enemyLayer;

    [Header("플레이어 불릿 구분 여부")]
    public bool player;
    public bool magic;

    private string poolTag;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    private void Start()
    {
        Invoke("ReturnToPool", lifetime); // 일정 시간이 지나면 풀로 반환
    }
    private void OnEnable()
    {
        Invoke("ReturnToPool", lifetime); // 재활용될 때마다 lifetime 타이머를 다시 시작
    }

    private void OnDisable()
    {
        CancelInvoke(); // 비활성화될 때 Invoke 취소
    }

    private void FixedUpdate()
    {
        if (player)
        {
            rb.velocity = -transform.right * speed; // 투사체를 오른쪽 방향으로 이동
        }
        if (!player)
        {
            rb.velocity = transform.right * speed; // 투사체를 오른쪽 방향으로 이동
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            Character enemy = collision.GetComponent<Character>();
            DefenceBase defenceBase = collision.GetComponent<DefenceBase>();
            if (enemy != null)
            {
                enemy.TakeDamage(unitData.attack);
                ReturnToPool();
            }

            if (defenceBase != null)
            {
                defenceBase.TakeDamage(unitData.attack);
                ReturnToPool();
            }
        }
    }

    public void SetPoolTag(string tag)
    {
        poolTag = tag;
    }

    private void ReturnToPool()
    {
        if (ObjectPool.Instance != null)
        {
            ObjectPool.Instance.ReturnToPool(poolTag, gameObject);
            CancelInvoke(); // ReturnToPool이 호출되었으므로, 예약된 Invoke 취소
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
