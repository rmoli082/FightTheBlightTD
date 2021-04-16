using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct FloatRange
{
    [SerializeField]
    float min;
    [SerializeField]
    float max;

    public float Min => min;
    public float Max => max;

    public FloatRange(float value)
    {
        min = max = value;
    }

    public FloatRange(float _min, float _max)
    {
        min = _min;
        max = _max;
    }

    public float RandomValueInRange { get => Random.Range(min, max); }
}
