using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace GameTwo
{
    public class ScoreAndDie : MonoBehaviour
    {
        public TextMeshProUGUI tmpScore;
        private bool _isDead;
        private int _score;
        void Awake()
        {
            Collision.SetScore += ScoreSet;
            Collision.Dead += Dead;
            StartCoroutine(SetScoreByTime());
        }

        private void ScoreSet()
        {
            if (!_isDead)
            {
                _score += 1;
                tmpScore.SetText($"Score : {_score}");
            }
        }
        private void Dead()
        {
            _isDead = true;
            tmpScore.SetText($"Dead");
        }
        private IEnumerator SetScoreByTime()
        {
            while (!_isDead)
            {
                yield return new WaitForSeconds(5);
                ScoreSet();
            }
        }
    }
}