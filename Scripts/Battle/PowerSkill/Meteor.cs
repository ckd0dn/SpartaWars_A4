using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosionPrefab; // ���� ����Ʈ ������
    public float explosionDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // CompareTag�� �±� ���ϴ� ���� �� ȿ�����Դϴ�.
        {
            Destroy(gameObject); // ���׿� �ı�

            // ���� ����Ʈ ����
            GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            Destroy(explosion, explosionDuration);

            ActivateAllColliders();

            // �浹üũ�� ���� Layer�� Enemy�� ���� �ֵ��� Hp�� 0
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10f); 

            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    UnitData enemy = collider.gameObject.GetComponent<UnitData>();
                    if (enemy != null)
                    {
                       //enemy.TakeDamage(enemy.health); 
                    }
                }
            }
        }
    }

    // �� ��ü �ݶ��̴� Ȱ��ȭ �Լ�
    private void ActivateAllColliders()
    {
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in allColliders)
        {
            collider.enabled = true;
        }
    }
}