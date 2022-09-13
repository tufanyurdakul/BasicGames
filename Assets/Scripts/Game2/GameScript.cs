using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameTwo
{
    public class GameScript : MonoBehaviour
    {
        public static event Action<SpawnInformation> SpawnAction;
        private List<Transform> _spawnTransforms;
        private GameObject _capsule;
        
        private void Start()
        {
            _spawnTransforms = new List<Transform>();
            _capsule = Resources.Load("Game2/Capsule") as GameObject;
            for (int i = 1; i <= 33; i++)
                _spawnTransforms.Add(transform.Find("Spawner" + i));
            StartCoroutine(Spawner(0.5f));
        }

        private IEnumerator Spawner(float time)
        {
            while (true)
            {
                int spawnRandom = UnityEngine.Random.Range(0, _spawnTransforms.Count);
                int colorRandom = UnityEngine.Random.Range(0, 4);
                yield return new WaitForSeconds(time);
                SpawnInformation spawn = new SpawnInformation()
                {
                    spawnObject = _capsule,
                    isScoreObject = (colorRandom == 3 ? true : false),
                    spawnPosition = _spawnTransforms[spawnRandom].position
                };
                SpawnAction?.Invoke(spawn);
                float upper = 1;
                if (Time.time > 100)
                    upper = 0.75f;
                else if (Time.time > 250)
                    upper = 0.6f;
                else if (Time.time > 500)
                    upper = 0.5f;
                time = UnityEngine.Random.Range(0.05f, 0.05f + upper);
            }
        }
    }
    public class SpawnInformation
    {
        public GameObject spawnObject;
        public bool isScoreObject;
        public Vector3 spawnPosition;
    }
}
