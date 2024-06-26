using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttack : MonoBehaviour
{
    public GameObject skillPrefab; // 스킬 프리팹
    public Transform spawnPoint; // 스킬이 발사될 위치

    public float cooldownTime = 180f; // 쿨타임
    private float cooldownTimer = 0f;
    private bool isSkillReady = true; // 스킬 사용 가능 여부

    public void Update()
    {
        // 쿨타임 타이머 갱신
        if (!isSkillReady)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime)
            {
                isSkillReady = true;
                cooldownTimer = 0f;
            }
        }
    }

    public void LaunchSkill()
    {
        if (isSkillReady)
        {
            GameObject skillInstance = Instantiate(skillPrefab, spawnPoint.position, skillPrefab.transform.rotation);

            isSkillReady = false;

            Rigidbody rb = skillInstance.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = new Vector3(0, -0.1f, 0);
            }
        }
        else
        {
            Debug.Log("스킬 쿨타임 중입니다.");
        }
    }
}
