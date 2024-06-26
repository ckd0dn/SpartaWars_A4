using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("����ü�� �ӵ�")]
    public float speed = 10f;

    [Header("���Ÿ� ������ ������ SO��ġ")]
    public UnitData unitData;

    [Header("����ü�� �����ð�")]
    [SerializeField] private float lifetime;

    [Header("���� ���� ���̾� ����")]
    public int enemyLayer;

    [Header("�÷��̾� �Ҹ� ���� ����")]
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
        Invoke("ReturnToPool", lifetime); // ���� �ð��� ������ Ǯ�� ��ȯ
    }
    private void OnEnable()
    {
        Invoke("ReturnToPool", lifetime); // ��Ȱ��� ������ lifetime Ÿ�̸Ӹ� �ٽ� ����
    }

    private void OnDisable()
    {
        CancelInvoke(); // ��Ȱ��ȭ�� �� Invoke ���
    }

    private void FixedUpdate()
    {
        if (player)
        {
            rb.velocity = -transform.right * speed; // ����ü�� ������ �������� �̵�
        }
        if (!player)
        {
            rb.velocity = transform.right * speed; // ����ü�� ������ �������� �̵�
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
            CancelInvoke(); // ReturnToPool�� ȣ��Ǿ����Ƿ�, ����� Invoke ���
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
