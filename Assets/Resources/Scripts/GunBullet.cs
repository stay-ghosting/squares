using UnityEngine;

public abstract class GunBullet : MonoBehaviour
{
    Vector3 startPos;
    internal Vector3 StartPos { get { return startPos; } }

    Vector3 dir = Vector3.zero;

    bool flipX {
        get {
            if (dir.x > 0){
                return false;
            }
            else{
                return true;
            }
        }
    }

    LayerMask playerLayer;
    internal LayerMask PlayerLayer { get { return playerLayer; } }

    float bulletSpeed;
    internal float BulletSpeed { get { return bulletSpeed; } }

    internal SpriteRenderer spriteRenderer;

    const float SECONDS_TO_LIVE = 3.0f;

    internal GameObject G_Bullet;

    public void gunBullet(Vector3 dir, Vector3 startPos, LayerMask playerLayer,
        float bulletSpeed, GameObject prefab)
    {
        G_Bullet = Instantiate(prefab, startPos, Quaternion.identity);

        this.dir = dir;
        this.startPos = startPos;
        this.playerLayer = playerLayer;
        this.bulletSpeed = bulletSpeed;

        spriteRenderer = G_Bullet.GetComponent<SpriteRenderer>();
        if (flipX)
        {
            G_Bullet.transform.localScale = new Vector3(-1, 1, 1);
        }

        Destroy(G_Bullet, SECONDS_TO_LIVE);
        Destroy(this, SECONDS_TO_LIVE);
    }

    public void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector3 newPosition = G_Bullet.transform.position + dir * BulletSpeed * Time.deltaTime;
        RaycastHit2D hit = Physics2D.Linecast(G_Bullet.transform.position, newPosition, playerLayer);

        if (hit){
            PlayerHit(hit.transform.GetComponent<PlayerController>());
            Destroy(G_Bullet);
            Destroy(this);
        }
        else {
            G_Bullet.transform.position = newPosition;
        }
    }

    public void PlayerHit(PlayerController otherPlayerController)
    {
        otherPlayerController.PlayerDo.Hit();
        Destroy(G_Bullet);
    }
}

class LightBullet : GunBullet
{
    const float BULLET_SPEED = 10.0f;
    readonly float MAX_TRAIL_SIZE = 2.5f;

    GameObject G_LightBulletPrefab;

    GameObject G_bulletTrail;
    GameObject G_bulletTrailParent;

    public void lightBullet(Vector3 dir, Vector3 startPos, LayerMask playerLayer){
        G_LightBulletPrefab = Resources.Load("Prefabs/LightBullet") as GameObject;
        base.gunBullet(dir, startPos, playerLayer, BULLET_SPEED, G_LightBulletPrefab);

        this.G_bulletTrail = base.G_Bullet.transform.GetChild(1).gameObject;
        this.G_bulletTrail.transform.localScale = new Vector3(0, 1, 1);
    }

    new void Update()
    {
        base.Update();

        updateTrail();
    } 

    public void updateTrail(){
        float amountOfMaxTrailLength = Mathf.Abs(base.G_Bullet.transform.position.x - StartPos.x);
        Vector3 newScale = new Vector3(Mathf.Clamp(amountOfMaxTrailLength, 0, MAX_TRAIL_SIZE), 1, 1);
        G_bulletTrail.transform.localScale = newScale;
    }
}

public class HeavyBullet : GunBullet
{
    public enum bulletColor{red, blue}
    const float BULLET_SPEED = 15.0f;
    const float INTENSITY = 2.0f;
    
    Sprite redColor;
    Sprite blueColor;
    GameObject G_HeavyBulletPrefab;

    public void heavyBullet(Vector3 dir, Vector3 startPos, LayerMask playerLayer, bulletColor color){
        G_HeavyBulletPrefab = Resources.Load("prefabs/HeavyBullet") as GameObject;
        blueColor = Resources.Load<Sprite>("GFX/Sprite/S_BlueHeavyBullet");
        redColor = Resources.Load<Sprite>("GFX/Sprite/S_RedHeavyBullet");

        base.gunBullet(dir, startPos, playerLayer, BULLET_SPEED, G_HeavyBulletPrefab);

        spriteRenderer.material.SetFloat("Intensity", INTENSITY);
        
        switch (color){
            case(bulletColor.blue):
                base.spriteRenderer.sprite = blueColor;
                return;
            case(bulletColor.red):
                base.spriteRenderer.sprite = redColor;
                return;
        }
    }
}