using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  �� ��Ʋ �ð� : AllBattleTime
 *  �� �μ� ���� : AllBreakedPotion
 *  ���� �� ��ũ : LongestLink
 *  ����� Ư������ �� : UsedChancePotion
 *  ���� �� ��� ������ : DamagePerRound
 */

public class MultiClearManager : MonoBehaviour
{
    bool WinCheck;
    
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

    private GameObject Result;
    
    public Sprite WinSprite;
    public Sprite LoseSprite;

    private GameObject Host;
    private GameObject Guest;

    void Start()
    {
        WinCheck = SecurityPlayerPrefs.GetString("isWin", "null") == "true";
        Host = GameObject.Find("Host");
        Guest = GameObject.Find("Guest");
        Result = GameObject.Find("Result");
        /*ABT = SecurityPlayerPrefs.GetFloat("ABT", 0);
        ABP = SecurityPlayerPrefs.GetInt("ABP", 0);
        LL = SecurityPlayerPrefs.GetInt("LL", 0);
        UCP = SecurityPlayerPrefs.GetInt("UCP", 0);
        DPR = SecurityPlayerPrefs.GetFloat("DPR", 0);

        SecurityPlayerPrefs.SetFloat("ABT", 0);
        SecurityPlayerPrefs.SetInt("ABP", 0);
        SecurityPlayerPrefs.SetInt("LL", 0);
        SecurityPlayerPrefs.SetInt("UCP", 0);
        SecurityPlayerPrefs.SetFloat("DPR", 0);

        ABT_text.GetComponent<TextMeshProUGUI>().text = "총 전투 시간 : " + (int)ABT;
        ABP_text.GetComponent<TextMeshProUGUI>().text = "총 파괴 포션 수 : " + ABP;
        LL_text.GetComponent<TextMeshProUGUI>().text = "최장 링크 길이 : " + LL;
        UCP_text.GetComponent<TextMeshProUGUI>().text = "사용한 특수 포션 : " + UCP;
        DPR_text.GetComponent<TextMeshProUGUI>().text = "평균 데미지 : " + ((int)DPR*100)/100;*/

        if (WinCheck)
        {
            Result.GetComponent<TextMeshProUGUI>().text = "WIN";
            Host.GetComponent<SpriteRenderer>().sprite = WinSprite;
            Guest.GetComponent<Animator>().enabled = false;
            Guest.GetComponent<SpriteRenderer>().sprite = LoseSprite;
        }
        else
        {
            Result.GetComponent<TextMeshProUGUI>().text = "LOSE";
            Host.GetComponent<Animator>().enabled = false;
            Host.GetComponent<SpriteRenderer>().sprite = LoseSprite;
            Guest.GetComponent<SpriteRenderer>().sprite = WinSprite;
        }
    }
}