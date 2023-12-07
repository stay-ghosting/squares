using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;

    // update logic
    public void MoveInput(Vector2 dir)
    {
        if (dir == Vector2.zero) return;
        playerController.Target.UpdateTarget(dir);
        if (playerController.onMoveCooldown || playerController.state == PlayerState.dead || !playerController.canMove) return;

        Vector3 moveTo = playerController.PlayerGrid.vector2ToGridPosDict[dir];

        if (moveTo != playerController.Transform.position)
        {
            if (playerController.state != PlayerState.home)
            {
                playerController.DissolveSpawner.CreateDisolve(playerController.SpriteRenderer.sprite, playerController.DoFlipX, playerController.Transform.localScale, playerController.Transform.position);
            }
            else
            {
                ExitHome(false);
                playerController.gridCenter.isHome = false;

                FunctionTimer.current.removeTimer("ForceExitHome");
            }

            if (playerController.Transform.position.y != moveTo.y)
            {
                playerController.bullets = PlayerController.MAX_BULLETS;
                playerController.BulletsGui.playerUpdateBullets(playerController.bullets);
            }
            playerController.Transform.position = moveTo;

            playerController.onMoveCooldown = true;
            playerController.Target.onHideTaregt(playerController.Player);

            FunctionTimer.current.AddTimer(() =>
            {
                playerController.onMoveCooldown = false;
                MoveInput(dir);
            }, PlayerController.MAX_MOVE_COOLDOWN, $"MoveCooldown=False{playerController.Player}");
        }

    }


    public void ExitHome(bool toRandomPos)
    {
        if (toRandomPos)
        {
            playerController.Transform.position = playerController.PlayerGrid.RandomGridEdgePos();
        }

        playerController.gridCenter.isHome = false;
        playerController.SpriteRenderer.enabled = true;
        playerController.Col.enabled = true;
        playerController.state = PlayerState.idle;

        playerController.canMove = true;
    }


    public void EnterHome()
    {
        FunctionTimer.current.AddTimer(() => { ExitHome(true); }, GridCenter.TIME_TO_KICK, "ForceExitHome");
        FunctionTimer.current.AddTimer(() => { playerController.canMove = true; }, GridCenter.PULSE_TIME/2, "CanMove=True");

        playerController.onMoveCooldown = false;
        playerController.SpriteRenderer.enabled = false;
        playerController.Col.enabled = false;

        playerController.Transform.position = playerController.PlayerSpawnLocation.position;
        playerController.state = PlayerState.home;

        playerController.gridCenter.playerEnterHome();
    }

    // update logic
    //do testing
    public bool TryBlock()
    {
        if (playerController.state != PlayerState.idle) return false;

        playerController.state = PlayerState.blocking;
        playerController.PlayerAnimator.playBlockAnimation();

        FunctionTimer.current.AddTimer(() =>
        {
            playerController.state = PlayerState.idle;
            playerController.PlayerAnimator.PlayIdleAnimation();
        },
        PlayerController.MAX_ACTION_COOLDOWN, "state:Idle");

        return true;
    }

    // update logic
    //do testing

    public bool TryShoot(bool isHeavy)
    {
        // if not idle return false
        if (playerController.state != PlayerState.idle) return false;

        if (playerController.bullets <= 0) return false;
        if (isHeavy && playerController.bullets < PlayerController.MAX_BULLETS) return false;

        if (isHeavy) playerController.bullets = 0;
        else playerController.bullets--;

        playerController.state = PlayerState.onCooldown;
        playerController.PlayerAnimator.playShootAnimation();
        playerController.BulletSpawner.Shoot(isHeavy, playerController.color);
        playerController.BulletsGui.playerUpdateBullets(playerController.bullets);

        FunctionTimer.current.AddTimer(() => { playerController.state = PlayerState.idle; }, PlayerController.MAX_ACTION_COOLDOWN);
        return true;
    }

    // update logic
    // called by animation
    // update logic
    public void Hit()
    {
        if (playerController.state != PlayerState.home && playerController.state != PlayerState.dead)
        {
            if (playerController.state == PlayerState.blocking)
            {
                playerController.PlayerAnimator.playShieldHitAnimation();
                FunctionTimer.current.removeTimer("state:Idle");

                FunctionTimer.current.AddTimer(() =>
                {
                    playerController.state = PlayerState.idle;
                    playerController.PlayerAnimator.PlayIdleAnimation();
                },
                PlayerController.SHIELD_HIT_TIME, "state:Idle");
            }
            else
            {
                OnDeath();
            }
        }
    }
    // update logic
    public void OnDeath()
    {

        if (playerController.lives > 1)
        {
            FunctionTimer.current.removeTimer($"MoveCooldown=False{playerController.Player}");
            playerController.PlayerAnimator.playDeathAnimation();

            playerController.lives--;
            playerController.HealthBar.updateHealth(playerController.lives);

            playerController.bullets = PlayerController.MAX_BULLETS;
            playerController.BulletsGui.playerUpdateBullets(playerController.bullets);

            playerController.state = PlayerState.dead;
            playerController.canMove = false;

            // after death animation go home
            FunctionTimer.current.AddTimer(() => { EnterHome(); }, PlayerController.TIME_DEAD);
        }
        else
        {
            GameEvents.current.PlayerWin(playerController.otherPlayer);
        }
    }

    public void BulletBlocked()
    {
        playerController.state = PlayerState.recovering;
    }

}