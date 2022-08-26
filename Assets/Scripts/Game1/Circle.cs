using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameOne
{
    public class Circle : MonoBehaviour
    {
        public Action<CircleType> WorkAction;
        public static event Action ScoreAction;
        public static event Action<bool> HealthAction;
        private TypeOfCircle _typeCircle;
        private void OnMouseDown()
        {
            switch (_typeCircle)
            {
                case TypeOfCircle.Green:
                    ScoreAction?.Invoke();
                    break;
                case TypeOfCircle.Red:
                    HealthAction?.Invoke(true);
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }

        private void Awake()
        {
            WorkAction += Starter;
        }

        private void Starter(CircleType typeOfCircle)
        {
            _typeCircle = typeOfCircle.circleType;
            StartCoroutine(DestroyObject(typeOfCircle.time));
        }

        private IEnumerator DestroyObject(float time)
        {
            yield return new WaitForSeconds(time);
            switch (_typeCircle)
            {
                case TypeOfCircle.Green:
                    HealthAction?.Invoke(false);
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
    public class CircleType
    {
        public float time;
        public TypeOfCircle circleType;
    }
    public enum TypeOfCircle
    {
        Green = 1,
        Red = 2
    }
}



