using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GameTwo
{
    public class Collision : MonoBehaviour
    {
        public static event Action SetScore,Dead;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("DeadObject"))
                Dead?.Invoke();
            else if(collision.CompareTag("ScoreObject"))
            {
                SetScore?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}
