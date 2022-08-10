using UnityEngine;

public class TransformObject : MonoBehaviour
{
    protected virtual float SinWavePlot(float period)
    {
        if (period <= Mathf.Epsilon) { return 0; }

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        float factor = (rawSinWave + 1) / 2;
        return factor;
    }
}
