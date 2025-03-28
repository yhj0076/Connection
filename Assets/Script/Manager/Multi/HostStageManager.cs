using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostStageManager : StageManager
{
    private PhotonView _view;
    private void Awake()
    {
        LeftRatio = 0.5f;
        RightRatio = 0.5f;

        state = State.InGame;
        tmpTime = LeftTime;
        firstSizePlayer = PlayerRatio.GetComponent<RectTransform>().sizeDelta;
        firstSizeEnemy = EnemyRatio.GetComponent<RectTransform>().sizeDelta;
        _view = gameObject.GetComponent<PhotonView>();
        Time.timeScale = 1;

        ABT = 0;
        ABP = 0;
        LL = 0;
        UCP = 0;
        DPR = 0;
        CountRound = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.InGame)
        {
            Timer();
            _view.RPC("synchronizeTime", RpcTarget.Others, LeftTime);
        }
        _view.RPC("UpdateData",RpcTarget.All);
    }

    [PunRPC]
    void SynchronizeTime(float leftTime)
    {
        LeftTime = leftTime;
    }
}
