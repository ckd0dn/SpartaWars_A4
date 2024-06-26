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
    private float skillTimes = 180f; // 스킬 쿨타임 설정 (180초로 수정)
    private float getSkillTimes = 0f;
    private bool isCoolingDown = false; // 쿨타임 중인지 여부를 나타내는 변수

    // Start is called before the first frame update
    void Start()
    {
        hideSkillButtons.SetActive(false);
    }

    public void HideSkillSetting(int skillNum)
    {
        if (!isCoolingDown) // 쿨타임 중이 아닐 때만 스킬 사용 가능
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

        // 쿨타임 종료 후
        getSkillTimes = 0;
        isHideSkills = false;
        hideSkillButtons.SetActive(false);
        isCoolingDown = false; 
    }
}