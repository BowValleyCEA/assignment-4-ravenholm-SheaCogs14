using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private Transform targetOrigin;
    [SerializeField] private Camera cam;
    [SerializeField] private float targetDistance = 1000f;
    [SerializeField] private LayerMask hitLayer;
    private AimTarget _currentTarget;
    private Rigidbody grabbedRB;

    [Header("Gravity Gun Settings")]
    [SerializeField] private float maxGrabDistance = 10f;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private float throwForce = 20.0f;
    [SerializeField] private Transform objectHolder;
    void Update()
    {

        HandleTargeting();
        HandleGrabInput();
        HandleObjectMovement();


    }

    private void UpdateTarget(AimTarget target)
    {
        if (target == _currentTarget) return;
        {
            _currentTarget?.StopTarget();
            _currentTarget = target;
            _currentTarget.Target();
        }
    }


    private void HandleTargeting()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, targetDistance, hitLayer))
        {
            if (hit.collider.TryGetComponent<AimTarget>(out AimTarget target))
            {
                UpdateTarget(target);
                return;
            }
        }
    }
    private void HandleObjectMovement()
    {
        if (grabbedRB)
        {
            grabbedRB.MovePosition(Vector3.Lerp(grabbedRB.position, objectHolder.position, Time.deltaTime * lerpSpeed));

            if (Input.GetMouseButtonDown(0)) 
            {
                ThrowObject();
            }
        }
    }

    private void ClearCurrentTarget()
    {
        _currentTarget?.StopTarget();
        _currentTarget = null;
    }

    private void HandleGrabInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedRB)
            {
                ReleaseObject();
            }
            else
            {
                TryGrabObject();
            }
        }
    }


    private void TryGrabObject()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, maxGrabDistance, hitLayer))
        {
            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                grabbedRB = rb;
                grabbedRB.isKinematic = true;
            }
        }
    }

    private void ReleaseObject()
    {
        if (grabbedRB)
        {
            grabbedRB.isKinematic = false;
            grabbedRB = null;
        }
    }

    private void ThrowObject()
    {
        grabbedRB.isKinematic = false;
        grabbedRB.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        grabbedRB = null;
    }
}
