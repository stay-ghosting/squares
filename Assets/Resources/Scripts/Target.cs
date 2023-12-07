using UnityEngine;

public class Target : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject player;
    PlayerController playerController;

    void Awake()
    {
        this.spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        playerController = player.GetComponent<PlayerController>();
    }

    public void onHideTaregt(int player)
    {
        spriteRenderer.enabled = false;
    }

    public void UpdateTarget(Vector2 dir)
    {
        if (dir == Vector2.zero)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            Vector3 moveTo = playerController.PlayerGrid.vector2ToGridPosDict[dir];
            bool playerInDir = playerController.Transform.position == moveTo;

            if (playerInDir)
            {
                onHideTaregt(playerController.Player);
            }
            else
            {
                transform.position = moveTo;
                spriteRenderer.enabled = true;
            }
        }
    }
}