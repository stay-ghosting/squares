using System.Collections.Generic;
using UnityEngine;

public class DissolveSpawner : MonoBehaviour
{
    [SerializeField, ColorUsage(false, true)] Color color;
    float noiseScale = 3.0f;
    float edgeThickness = 0.05f;

    public void CreateDisolve(Sprite sprite, bool doFlipX, Vector3 scale, Vector3 pos)
    {
        GameObject curDissolve = new GameObject("dissolve", typeof(Dissolve));
        Dissolve curDissolveScript = curDissolve.GetComponent<Dissolve>(); 
        curDissolveScript.dissolve(sprite, doFlipX, scale, pos, noiseScale, color, edgeThickness);
    }
}
