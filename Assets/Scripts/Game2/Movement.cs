using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameTwo
{
    public class Movement : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        public Transform leftUpper, rightDown;
        private Camera _cam;

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            if (transform.position.x < leftUpper.position.x)
                transform.position = new Vector3(leftUpper.position.x, transform.position.y, 10);
            if (transform.position.y > leftUpper.position.y)
                transform.position = new Vector3(transform.position.x, leftUpper.position.y, 10);
            if (transform.position.x > rightDown.position.x)
                transform.position = new Vector3(rightDown.position.x, transform.position.y, 10);
            if (transform.position.y < rightDown.position.y)
                transform.position = new Vector3(transform.position.x, rightDown.position.y, 10);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        private void Start()
        {
            _cam = Camera.main;
        }
    }
}
