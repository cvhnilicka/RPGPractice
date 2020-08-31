using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform target;

        [SerializeField] float smoothSpeed = 0.5f;

        Vector3 offset;

        private void Start()
        {
            offset = target.position - transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {


            Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position, smoothSpeed*Time.deltaTime);
            transform.position = smoothPosition;
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * smoothSpeed);
        }
    }
}

