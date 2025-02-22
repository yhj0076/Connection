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
        GetResult();
    }

    protected void PrintResult()
    {
        PrintABT();
        PrintABP();
        PrintLL();
        PrintUCP();
        PrintDPR();
    }

    protected void PrintABT()
    {
        ABT_text.GetComponent<TextMeshProUGUI>().text = "�� �÷��� �ð� : " + (int)ABT;
    }
    protected void PrintABP()
    {
        ABP_text.GetComponent<TextMeshProUGUI>().text = "�ı��� ���� : " + ABP;
    }

    protected void PrintLL()
    {
        LL_text.GetComponent<TextMeshProUGUI>().text = "���� �� ��ũ : " + LL;
    }

    protected void PrintUCP()
    {
        UCP_text.GetComponent<TextMeshProUGUI>().text = "����� Ư������ : " + UCP;
    }

    protected void PrintDPR()
    {
        DPR_text.GetComponent<TextMeshProUGUI>().text = "��� ������ : " + ((int)DPR * 100) / 100;
    }

    void GetResult()
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

        PrintResult();
    }
}