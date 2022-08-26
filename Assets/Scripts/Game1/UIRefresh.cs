using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace GameOne
{
    public class UIRefresh : MonoBehaviour
    {
        public TextMeshProUGUI tmpScore;
        public List<GameObject> Healths;
        private PlayerData pd;
        private void Start()
        {
            pd = GetComponent<PlayerData>();
            tmpScore.SetText($"{(int)FirstValue.Score}");
            PlayerData.GetHealthAction += HealthRefresh;
            PlayerData.GetScoreAction += ScoreRefresh;
        }
        private void HealthRefresh(int prm_health)
        {
            if (prm_health == 0)
                foreach (var item in Healths)
                    item.SetActive(false);
            else
            {
                var GetLastImage = Healths.Where(p => p.activeSelf)?.LastOrDefault();
                GetLastImage.SetActive(false);
            }
        }
        private void ScoreRefresh()
        {
            tmpScore.SetText(pd.Score.ToString());
        }
        
    }
}
