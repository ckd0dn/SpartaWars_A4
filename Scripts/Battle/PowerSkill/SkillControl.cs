using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillControl : MonoBehaviour
{
    public GameObject hideSkillButtons;
    public TextMeshProUGUI hideSkillTimeTexts;
    public Image hideSkillImages;
    private bool isHideSkills = false;
    private float skillTimes = 180f; // ��ų ��Ÿ�� ���� (180�ʷ� ����)
    private float getSkillTimes = 0f;
    private bool isCoolingDown = false; // ��Ÿ�� ������ ���θ� ��Ÿ���� ����

    // Start is called before the first frame update
    void Start()
    {
        hideSkillButtons.SetActive(false);
    }

    public void HideSkillSetting(int skillNum)
    {
        if (!isCoolingDown) // ��Ÿ�� ���� �ƴ� ���� ��ų ��� ����
        {
            hideSkillButtons.SetActive(true);
            getSkillTimes = skillTimes;
            isHideSkills = true;
            HideSkillCheck();
        }
    }

    private void HideSkillCheck()
    {
        if (isHideSkills)
        {
            StartCoroutine(SkillTimeCheck());
        }
    }

    IEnumerator SkillTimeCheck()
    {
        isCoolingDown = true; 
        yield return null;

        while (getSkillTimes > 0)
        {
            yield return new WaitForSeconds(1f);
            getSkillTimes -= 1f;

            hideSkillTimeTexts.text = getSkillTimes.ToString("000");

            float time = getSkillTimes / skillTimes;
            hideSkillImages.fillAmount = time;


        }

        // ��Ÿ�� ���� ��
        getSkillTimes = 0;
        isHideSkills = false;
        hideSkillButtons.SetActive(false);
        isCoolingDown = false; 
    }
}