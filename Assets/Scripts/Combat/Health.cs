using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        float health = 100f;

        public void TakeDamage(float damageDone)
        {
            health = Mathf.Max(health - damageDone, 0);
            print(health);
        }

    }
}

