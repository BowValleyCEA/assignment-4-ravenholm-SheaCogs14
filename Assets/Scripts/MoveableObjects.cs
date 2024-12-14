using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObjects : MonoBehaviour

{

    [SerializeField] private Animator bridgeAnimator;
    private string _triggerName = "Bridge";
    [SerializeField] private LayerMask hitableLayer;
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & hitableLayer) != 0)
        {
            bridgeAnimator.SetTrigger(_triggerName);
        }
    }
}
