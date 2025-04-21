using System;
using UnityEngine;

namespace Script._ServerControl
{
    public class MultiHealthManager : HealthManager
    {
        float _playerHealth;
        float _enemyHealth;
        
        private void Start()
        {
            _playerHealth = 100;
            _enemyHealth = 100;
            Fade.SetActive(false);
            PlayerFullHP = _playerHealth;
            EnemyFullHP = _enemyHealth;
            PlayerFullGage = new Vector2(PlayerHP.GetComponent<RectTransform>().sizeDelta.x, PlayerHP.GetComponent<RectTransform>().sizeDelta.y);
            EnemyFullGage = new Vector2(EnemyHP.GetComponent<RectTransform>().sizeDelta.x, EnemyHP.GetComponent<RectTransform>().sizeDelta.y);

            StageManager = GameObject.Find("StageManager");
            Stage = GameObject.Find("Stage");
            Player = GameObject.Find("Host");
            Enemy = GameObject.Find("Guest");
        }

        public void Calculate(int damage)
        {
            if(damage > 0)
            {
                _enemyHealth += damage;
                Player.GetComponent<PlayerAnimation>().toAttack();
                if (_playerHealth > 0)
                {
                    Enemy.GetComponent<PlayerAnimation>().toDamage();
                    Vector2 DMG = new Vector2(_playerHealth / PlayerFullHP, 1);
                    EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
                }
                else
                {
                    Fade.SetActive(true);
                    Enemy.GetComponent<PlayerAnimation>().toDeath();
                    RefreshClearStat();
                    Vector2 DMG = new Vector2(0, 1);
                    EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
                }
            }
            else if(damage < 0)
            {
                _playerHealth += damage;
                Enemy.GetComponent<PlayerAnimation>().toAttack();
                if (_playerHealth > 0)
                {
                    Player.GetComponent<PlayerAnimation>().toDamage();
                    Vector2 DMG = new Vector2(_playerHealth / PlayerFullHP, 1);
                    PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
                }
                else
                {
                    Fade.SetActive(true);
                    Player.GetComponent<PlayerAnimation>().toDeath();
                    RefreshClearStat();
                    Vector2 DMG = new Vector2(0, 1);
                    PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
                }
            }
            
            RefreshClearStat();
        }
        
        void RefreshClearStat()
        {
            SecurityPlayerPrefs.SetFloat("ABT", StageManager.GetComponent<MultiStageManager>().ABT);
            SecurityPlayerPrefs.SetInt("ABP", StageManager.GetComponent<MultiStageManager>().GetABP());
            SecurityPlayerPrefs.SetInt("LL", StageManager.GetComponent<MultiStageManager>().GetLL());
            SecurityPlayerPrefs.SetInt("UCP", StageManager.GetComponent<MultiStageManager>().GetUCP());
            SecurityPlayerPrefs.SetFloat("DPR", StageManager.GetComponent<MultiStageManager>().GetDPR());
        }
    }
}