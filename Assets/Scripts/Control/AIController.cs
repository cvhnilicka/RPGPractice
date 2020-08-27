using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // default 5 meters
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float wayPointTolerance = 1;
        private Fighter fighter;
        private GameObject player;
        private Health health;
        private Vector3 guardLocation;
        private Mover mover;
        private ActionScheduler actionScheduler;
        int waypointIndex = 0;

        // remember last time guard saw player
        float timeSinceLastSawPlayer = Mathf.Infinity;


        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
            guardLocation = transform.position;
            mover = GetComponent<Mover>();
            actionScheduler = GetComponent<ActionScheduler>();
        }


        private void Update()
        {
            if (health.IsDead()) return;
            if (GetInRange() && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                // stop attacking 
                // the move action will automatically cancel fighting action
                PatrolBehavior();
            }
            timeSinceLastSawPlayer += Time.deltaTime;



        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardLocation;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            mover.StartMoveAction(nextPosition);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.transform.GetChild(waypointIndex).position;
        }

        private void CycleWaypoint()
        {
            waypointIndex += 1;
            print(waypointIndex);
            if (waypointIndex >= patrolPath.transform.childCount)
            {
                waypointIndex = 0;
            }
        }

        private bool AtWaypoint()
        {
            float distToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distToWaypoint < wayPointTolerance;
        }

        private void SuspicionBehavior()
        {
            actionScheduler.CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            fighter.Attack(player);
        }

        private bool GetInRange()
        {
            
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        // called by unity to draw gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

    }

}
