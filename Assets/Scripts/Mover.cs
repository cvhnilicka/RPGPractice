using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.Rendering;

public class Mover : MonoBehaviour
{

    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
        }
        
        
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool hasHit = Physics.Raycast(ray, out hitInfo);
        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hitInfo.point;
        }
        
    }
}
