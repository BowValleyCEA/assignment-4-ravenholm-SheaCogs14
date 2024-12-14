using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{



    [SerializeField] private int _damageToPlayer = 1000;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerController = other.GetComponent<FPSController>();
            if (playerController != null)
            {
                playerController.takeDamage(_damageToPlayer);
            }
        }

    }
}
