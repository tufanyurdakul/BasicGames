using System.Collections;
using UnityEngine;
namespace GameOne
{
    public class ObjectSpawner : MonoBehaviour
    {
        public Transform MaxUpLeft, MaxUpRight, MaxDownLeft, MaxDownRight;
        private GameObject _circle;
        private int _health;
        private float _diff;
        private void Awake()
        {
            _circle = Resources.Load("Game1/Circle") as GameObject;
            PlayerData.GetHealthAction += HealthMinesAction;
        }

        private void Start()
        {
            _diff = GetComponent<PlayerData>().Diff;
            StartCoroutine(CircleSpawn(2));
        }

        private IEnumerator CircleSpawn(float time)
        {
            _health = GetComponent<PlayerData>().Health;
            while (_health > 0)
            {
                yield return new WaitForSeconds(time);
                time = CalculateTime();
                GameObject circle = Instantiate(_circle, new Vector3(Random.Range(MaxUpLeft.position.x, MaxUpRight.position.x), Random.Range(MaxUpLeft.position.y, MaxDownLeft.position.y), 0), Quaternion.identity);
                circle.transform.localScale *= _diff;
                Circle circleScript = circle.GetComponent<Circle>();
                CircleType circleType = SetCircle(circle, time);
                if (circleScript?.WorkAction != null)
                    circleScript.WorkAction(circleType);
            }
            GameObject[] allCircle = GameObject.FindGameObjectsWithTag("Circle");
            foreach (GameObject circle in allCircle)
                Destroy(circle);
            SetScore();
        }

        private float CalculateTime()
        {
            float executeTime = (2 - ((Time.time / 45) * (0.1f * (1 / GetDiff(_diff)))));
            executeTime = executeTime < 0 ? 0 : executeTime;
            return Random.Range(executeTime + 0.1f, 0.25f + executeTime);
        }
        private void HealthMinesAction(int prm_health)
        {
            _health = prm_health;
        }

        private CircleType SetCircle(GameObject circle, float time)
        {
            int colorOfCircle = Random.Range(0, 10);
            TypeOfCircle circType = TypeOfCircle.Green;
            if (colorOfCircle == 0)
            {
                circType = TypeOfCircle.Red;
                circle.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
            }
            CircleType circleType = new CircleType()
            {
                time = time,
                circleType = circType
            };
            return circleType;
        }
        private void SetScore()
        {
            int score = PlayerPrefs.GetInt("Score");
            PlayerData pd = GetComponent<PlayerData>();
            if (score < pd.Score)
                PlayerPrefs.SetInt("Game1Score", pd.Score);
        }

        private float GetDiff(float diff)
        {
            switch (diff)
            {
                case 1f:
                    return 1;
                case 0.5f:
                    return 0.5f;
                case 1.5f:
                    return 1;
                case 0.25f:
                    return 0.5f;
                default:
                    return 1;
            }
        }
    }
}

