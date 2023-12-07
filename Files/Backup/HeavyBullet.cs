using UnityEngine;

public class HHeavyBullet : MonoBehaviour
{
    // set by bullet function
    Vector3 dir = Vector3.zero;
    LayerMask playerLayer;
 
    int player;
    static readonly float bulletSpeed = 13.0f;

    // set in inpector
    SpriteRenderer bulletRendrer;
    Material material;

    [SerializeField] Sprite[] bulletSprites;
    [SerializeField] LayerMask[] playerLayers;

    [SerializeField, ColorUsage(false, true)] Color color;

    // set in this script
    Vector3 startPos;
    
    public void bullet(int player)
    {
        bulletRendrer = GetComponent<SpriteRenderer>();
        material = bulletRendrer.material;
        material.SetColor("_Color", color);

        this.player = player;
        switch (player)
        {
            case 1:
                dir.x = 1;
                break;
            case 2:
                dir.x = -1;
                break;
            default:
                dir.x = 0;
                break;
        }

        playerLayer = playerLayers[player-1];
        startPos = transform.position;
        
        bulletRendrer.flipX = (dir.x > 0)? false : true;
        bulletRendrer.sprite = bulletSprites[player-1];

        Destroy(gameObject, 5.0f);
    }

    public void Update()
    {
        move();
    }

    public void move()
    {
        Vector3 newPosition = transform.position + dir * bulletSpeed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Linecast(transform.position, newPosition, playerLayer);
        if (hit)
        {
            hitPlayer();
        }else
        {
            transform.position = newPosition;
        }
    }

    public void hitPlayer()
    {
        GameEvents.current.PlayerHit(PlayerController.otherPlayer(player));
        Destroy(gameObject);
    }
}
