using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // default 5 meters


        private void Update()
        {
            
            if (GetInRange())
            {
                print("Enemy : " + this.name + " should give chase");
            }

        }

        private bool GetInRange()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

    }

}
