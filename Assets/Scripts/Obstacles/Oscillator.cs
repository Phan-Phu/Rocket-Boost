using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : TransformObject
{
    private Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        float factor = base.SinWavePlot(period);
        transform.position = startingPosition + (movementVector * factor);
    }
}
