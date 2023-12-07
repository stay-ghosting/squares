using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo current;

    void Awake()
    {
        current = this;
    }

    // public readonly float maxMoveCooldown = 0.6f;
    
    // public readonly float maxActionCooldown = 0.5f;

    // public readonly int maxLives = 3;

    // public readonly int maxBullets = 3;
}
