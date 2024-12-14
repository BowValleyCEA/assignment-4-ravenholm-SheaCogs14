using UnityEngine;

public class EndScreen : MonoBehaviour
{



    [SerializeField] private int _damageToPlayer = 1000;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var screen = other.GetComponent<PauseMenu>();
            if (screen != null)
            {
                screen.LevelOver();
            }
        }
    }

}

    