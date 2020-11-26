using UnityEngine;

public class LowPassFilter
{
    // The lower this value, the less smooth the value is and faster Accel is updated. 30 seems fine for this
    private const float UPDATE_SPEED = 30.0f;
    private const float _updateInterval = 1f / UPDATE_SPEED;
    // The greater the value of LowPassKernelWidthInSeconds, the slower the filtered value will converge towards current input sample(and vice versa).
    private const float _lowPassKernelWidthInSeconds = 1.0f;
    private float _lowPassFilterFactor = _updateInterval / _lowPassKernelWidthInSeconds;
    private float _lowPassValue;

    public float FilterInput(float normalized)
    {
        _lowPassValue = Mathf.Lerp(_lowPassValue, normalized, _lowPassFilterFactor);
        return _lowPassValue;
    }
}
