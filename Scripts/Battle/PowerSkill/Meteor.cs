using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject explosionPrefab; // 폭발 이펙트 프리팹
    public float explosionDuration = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // CompareTag로 태그 비교하는 것이 더 효율적입니다.
        {
            Destroy(gameObject); // 메테오 파괴

            // 폭발 이펙트 생성
            GameObject explosion = Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);

            Destroy(explosion, explosionDuration);

            ActivateAllColliders();

            // 충돌체크를 통해 Layer가 Enemy를 가진 애들의 Hp를 0
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

    // 맵 전체 콜라이더 활성화 함수
    private void ActivateAllColliders()
    {
        Collider2D[] allColliders = FindObjectsOfType<Collider2D>();
        foreach (Collider2D collider in allColliders)
        {
            collider.enabled = true;
        }
    }
}