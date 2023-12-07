using System;
[Serializable]
public enum PlayerState
{
    idle,
    onCooldown,
    blocking,
    home,
    recovering,
    dead
}