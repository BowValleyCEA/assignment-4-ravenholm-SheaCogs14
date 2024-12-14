using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBox : MonoBehaviour
{


    [SerializeField] public string playerTag = "Player";
    [SerializeField] private int _damageToPlayer = 100;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            var playerController = other.GetComponent<FPSController>();
            if (playerController != null)
            {
                playerController.takeDamage(_damageToPlayer);
            }
        }

    }
}
