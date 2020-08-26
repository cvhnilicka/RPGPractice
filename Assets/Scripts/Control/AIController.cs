using RPG.Combat;
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

        private void Start()
        {
            fighter = GetComponent<Fighter>();
            player = GameObject.FindWithTag("Player");
        }


        private void Update()
        {

            CombatHandler();

        }

        private void CombatHandler()
        {
            
            if (GetInRange(player) && fighter.CanAttack(player))
            {
                fighter.Attack(player);
            }
            else
            {
                // stop attacking 
                fighter.Cancel();
            }
        }

        private bool GetInRange(GameObject player)
        {
            
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

    }

}
