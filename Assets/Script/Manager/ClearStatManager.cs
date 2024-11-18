using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  총 배틀 시간 : AllBattleTime
 *  총 부순 포션 : AllBreakedPotion
 *  가장 긴 링크 : LongestLink
 *  사용한 특수포션 수 : UsedChancePotion
 *  라운드 당 평균 데미지 : DamagePerRound
 */

public class ClearStatManager : MonoBehaviour
{
    float ABT;
    int ABP;
    int LL;
    int UCP;
    float DPR;

    public GameObject ABT_text;
    public GameObject ABP_text;
    public GameObject LL_text;
    public GameObject UCP_text;
    public GameObject DPR_text;
    // Start is called before the first frame update
    void Start()
    {
        ABT = SecurityPlayerPrefs.GetFloat("ABT", 0);
        ABP = SecurityPlayerPrefs.GetInt("ABP", 0);
        LL = SecurityPlayerPrefs.GetInt("LL", 0);
        UCP = SecurityPlayerPrefs.GetInt("UCP", 0);
        DPR = SecurityPlayerPrefs.GetFloat("DPR", 0);

        SecurityPlayerPrefs.SetFloat("ABT", 0);
        SecurityPlayerPrefs.SetInt("ABP", 0);
        SecurityPlayerPrefs.SetInt("LL", 0);
        SecurityPlayerPrefs.SetInt("UCP", 0);
        SecurityPlayerPrefs.SetFloat("DPR", 0);

        ABT_text.GetComponent<TextMeshProUGUI>().text = "총 플레이 시간 : " + (int)ABT;
        ABP_text.GetComponent<TextMeshProUGUI>().text = "총 파괴된 포션 : " + ABP;
        LL_text.GetComponent<TextMeshProUGUI>().text = "가장 긴 링크 : " + LL;
        UCP_text.GetComponent<TextMeshProUGUI>().text = "사용한 특수포션 : " + UCP;
        DPR_text.GetComponent<TextMeshProUGUI>().text = "평균 데미지 : " + ((int)DPR*100)/100;
    }
}