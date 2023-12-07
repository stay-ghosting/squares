using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHomeInfo : MonoBehaviour
{
    public static GridHomeInfo current;
    
    [GradientUsage(true)] public readonly Gradient colorGradient = new Gradient();
    [SerializeField, ColorUsage(false, true)] public Color maxIntensity;
    [SerializeField, ColorUsage(false, true)] public Color minIntensity;

    public readonly float pulseTime = 2.0f;
    public float halfPulseTime
    {
        get{ return pulseTime/2;}
    }
    public readonly float timeToKick = 4.0f;

    void Awake()
    {
        current = this;
        setUpGradient();
    }

    void setUpGradient()
    {
        GradientColorKey[] Ckey = new GradientColorKey[2];
        Ckey[0].color = minIntensity;
        Ckey[0].time = 0.0f;
        Ckey[1].color = maxIntensity;
        Ckey[1].time = 1.0f;

        GradientAlphaKey[] Akey = new GradientAlphaKey[2];
        Akey[0].alpha = 1.0f;
        Akey[0].time = 0.0f;
        Akey[1].alpha = 1.0f;
        Akey[1].time = 1.0f;

        colorGradient.SetKeys(Ckey, Akey);
    }
}
