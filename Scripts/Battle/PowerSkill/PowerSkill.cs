using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerAttack : MonoBehaviour
{
    public GameObject skillPrefab; // ��ų ������
    public Transform spawnPoint; // ��ų�� �߻�� ��ġ

    public float cooldownTime = 180f; // ��Ÿ��
    private float cooldownTimer = 0f;
    private bool isSkillReady = true; // ��ų ��� ���� ����

    public void Update()
    {
        // ��Ÿ�� Ÿ�̸� ����
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
            Debug.Log("��ų ��Ÿ�� ���Դϴ�.");
        }
    }
}
