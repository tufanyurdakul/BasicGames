using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameTwo
{
    public class Spawner : MonoBehaviour
    {
        private Color32 _green;
        private GameObject _player;
        void Awake()
        {
            _green = new Color32(0, 255, 0, 255);
            _player = GameObject.FindGameObjectWithTag("Player");
            GameScript.SpawnAction += SpawnObject;
        }

        private void SpawnObject(SpawnInformation inf)
        {
            GameObject capsule = Instantiate(inf.spawnObject, inf.spawnPosition, Quaternion.identity);
            LoadData(inf.isScoreObject,capsule);
            Rotation(capsule);
            Velocity(capsule);
            Destroy(capsule, 5);
        }
        private void LoadData(bool data,GameObject capsule)
        {
            switch (data)
            {
                case true:
                    capsule.GetComponent<SpriteRenderer>().color = _green;
                    capsule.tag = "ScoreObject";
                    break;
                case false:
                    capsule.tag = "DeadObject";
                    break;
            }
        }
        private void Rotation(GameObject capsule)
        {
            float offset = 90f;
            Vector2 direction = (Vector2)_player.transform.position - (Vector2)capsule.transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            capsule.transform.rotation = Quaternion.Euler(Vector3.forward * (0 + (angle + offset)));
        }
        private void Velocity(GameObject capsule)
        {
            Rigidbody2D rb = capsule.GetComponent<Rigidbody2D>();
            float alpha = Mathf.Atan2(_player.transform.position.y - capsule.transform.position.y, _player.transform.position.x - capsule.transform.position.x);
            rb.velocity = new Vector2(Mathf.Cos(alpha), Mathf.Sin(alpha)) * Random.Range(1, 3 + Time.time / 1000);
        }
    }
}