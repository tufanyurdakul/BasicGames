using System;
using UnityEngine;

namespace GameOne
{
    public class PlayerData : MonoBehaviour
    {
        public int Score { get; private set; }
        public int Health { get; private set; }
        public float Diff { get; private set; }
        public static event Action<int> GetHealthAction;
        public static event Action GetScoreAction;
        private void Awake()
        {
            Diff = 0.5f;
            Score = (int)FirstValue.Score;
            Health = (int)FirstValue.Health;
        }
        private void Start()
        {
            Circle.HealthAction += HealthMinesAction;
            Circle.ScoreAction += ScorePlusAction;
        }

        private void HealthMinesAction(bool mines = false)
        {
            if (!mines)
                Health -= 1;
            else
                Health = 0;
            GetHealthAction?.Invoke(Health);
        }
        private void ScorePlusAction()
        {
            Score += 1;
            GetScoreAction?.Invoke();
        }
    }
}
public enum FirstValue
{
    Score = 0,
    Health = 3
}
