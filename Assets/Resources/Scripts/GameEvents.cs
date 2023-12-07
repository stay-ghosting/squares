using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public static int winner;

    void Awake()
    {
        current = this;
    }

    // public event Action<int> onPlayerHit;
    // public void PlayerHit(int _player)
    // {
    //     if (onPlayerHit != null)
    //     {
    //         onPlayerHit(_player);
    //     }
    // }

    // public event Action<int> onPlayerBlockBullet;
    // public void PlayerBlockBullet(int _player)
    // {
    //     if (onPlayerBlockBullet != null)
    //     {
    //         onPlayerBlockBullet(_player);
    //     }
    // }

    // public event Action<int> onPlayerKilled;
    // public void PlayerKilled(int _player)
    // {
    //     if (onPlayerKilled != null)
    //     {
    //         onPlayerKilled(_player);
    //     }
    // }

    // public event Action<int, bool> onPlayerShoot;
    // public void PlayerShoot(int _player, bool HeavyBullet = true)
    // {
    //     if (onPlayerShoot != null)
    //     {
    //         onPlayerShoot(_player, HeavyBullet);
    //     }
    // }

    // public event Action<int, int> onUpdateHealthBar;
    // public void UpdateHealthBar(int _player, int _lives)
    // {
    //     onUpdateHealthBar(_player, _lives);
    // }

    // public event Action<int> onPlayerStopBlocking;
    // public void PlayerStopBlocking(int player)
    // {
    //     if (onPlayerStopBlocking != null)
    //     {
    //         onPlayerStopBlocking(player);
    //     }
    // }

    // public event Action<int> onPlayerBlock;
    // public void PlayerBlock(int player)
    // {
    //     if (onPlayerBlock != null)
    //     {
    //         onPlayerBlock(player);
    //     }
    // }

    public void PlayerWin(int _player)
    {
        winner = _player;
        SceneManager.LoadScene("PlayerWins");
    }

    // public Action<int> onPlayerEnterHome;
    // public void PlayerEnterHome(int player)
    // {
    //     if (onPlayerEnterHome != null)
    //     {
    //         onPlayerEnterHome(player);
    //     }
    // }

    // public Action<int> onPlayerExitHome;
    // public void PlayerExitHome(int player)
    // {
    //     if (onPlayerExitHome != null)
    //     {
    //         onPlayerExitHome(player);
    //     }
    // }
    // public Action<int> onKickFromHome;
    // public void KickFromHome(int player)
    // {
    //     if (onKickFromHome != null)
    //     {
    //         onKickFromHome(player);
    //     }
    // }
    // public Action<int, int> onPlayerUpdateBullets;
    // public void PlayerUpdateBullets(int player, int bulletsLeft)
    // {
    //     if (onPlayerUpdateBullets != null)
    //     {
    //         onPlayerUpdateBullets(player, bulletsLeft);
    //     }
    // }
    

    // public event Action<int> onHideTarget;
    // public void HideTarget(int player)
    // {
    //     if (onHideTarget != null)
    //     {
    //         onHideTarget(player);
    //     }
    // }
}
