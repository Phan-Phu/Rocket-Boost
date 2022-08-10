using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaltor : TransformObject
{
    private Vector3 startScale;
    [SerializeField] Vector3 scaleVector;
    [SerializeField] float period = 2f;

    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float factor = base.SinWavePlot(period);
        transform.localScale = startScale + (scaleVector * factor);
    }
}
