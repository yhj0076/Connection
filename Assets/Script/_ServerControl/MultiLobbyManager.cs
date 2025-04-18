using System;
using TMPro;
using UnityEngine;

namespace Script._ServerControl
{
    public class MultiLobbyManager : MonoBehaviour
    {
        private GameObject networkManager;
        private GameObject gameManager;
        private GameObject announce;
        private GameObject countText;
        public bool enemyExist = false;
        
        private float LeftTime;
        private void Awake()
        {
            LeftTime = 3;
            gameManager = GameObject.Find("GameManager");
            networkManager = GameObject.Find("NetworkManager");
            countText = GameObject.Find("CountText");
            announce = GameObject.Find("Announce");
        }
        
        private void Update()
        {
            if (enemyExist)
            {
                announce.GetComponent<TextMeshProUGUI>().text = "찾았습니다!";
                if (LeftTime > 0)
                {
                    LeftTime -= Time.deltaTime;
                    countText.GetComponent<TextMeshProUGUI>().text = $"{(int)LeftTime}초 후 진입";
                }
                else
                {
                    gameManager.GetComponent<GameManager>().MultiGame();
                }
            }
            else
            {
                announce.GetComponent<TextMeshProUGUI>().text = "적 찾는 중...";
                countText.GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
}
