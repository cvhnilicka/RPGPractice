using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; // default 5 meters
        private Fighter fighter;
        private GameObject player;
        private Health health;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();
        }


        private void Update()
        {
            if (health.IsDead()) return;
            CombatHandler();

        }

        private void CombatHandler()
        {
            
            if (GetInRange() && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                // stop attacking 
                fighter.Cancel();
            }
        }

        private bool GetInRange()
        {
            
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

    }

}
