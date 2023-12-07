using UnityEngine;

public class Bullet : MonoBehaviour
{
    // set by bullet function
    Vector3 dir = Vector3.zero;
    LayerMask playerLayer;

    int player;
    static readonly float bulletSpeed = 10.0f;
    static readonly float maxTrailScale = 2.5f;

    // set in inpector 
    [SerializeField] GameObject G_bulletTrail;
    [SerializeField] GameObject G_bulletTrailParent;
    [SerializeField] SpriteRenderer bulletRendrer;
    
    [SerializeField] LayerMask[] playerLayers;

    // set in this script
    Vector3 startPos;
    
    public void bullet(int player)
    {
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

        Destroy(gameObject, 5.0f);

        G_bulletTrailParent.transform.localScale = new Vector3(dir.x, 1, 1);
        G_bulletTrail.transform.localScale = new Vector3(0, 1, 1);
        bulletRendrer.flipX = (dir.x > 0)? false : true;

    }

    public void Update()
    {
        updateTrail();
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

    public void updateTrail()
    {
        float amountOfMaxTrailLength =  Mathf.Abs(transform.position.x - startPos.x);
        Vector3 newScale = new Vector3( Mathf.Clamp(amountOfMaxTrailLength, 0, maxTrailScale) , 1, 1);
        G_bulletTrail.transform.localScale = newScale;
    }
}
