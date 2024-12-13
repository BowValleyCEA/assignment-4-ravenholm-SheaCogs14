using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTarget : MonoBehaviour, ITargetable
{
    private Material _currentMaterial;

    [SerializeField] private Color targetColor = Color.red;

    private Color initialColor;
    void Start()
    {
        _currentMaterial = GetComponent<Renderer>().material;
        initialColor = _currentMaterial.color;
    }


    public void Target()
    {
        _currentMaterial.color = Color.red;
    }

    public void StopTarget()
    {
        _currentMaterial.color = initialColor;
    }
}
