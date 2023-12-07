using UnityEngine;
using System.Collections.Generic;

public class BulletSpawner : MonoBehaviour
{
    // lern params
    // so can only shoot one player
    
    [SerializeField] Vector3 dir;
    [SerializeField] LayerMask playerLayer;
    
    public void Shoot(bool isHeavy, HeavyBullet.bulletColor color = HeavyBullet.bulletColor.red)
    {
        if (isHeavy){
            //GameObject bullet = new GameObject("HeavyBullet", typeof(HeavyBullet));
            var g = gameObject.AddComponent<HeavyBullet>();
            g.heavyBullet(dir, transform.position, playerLayer, color);
        }
        else{
            //GameObject bullet = new GameObject("LightBullet", typeof(LightBullet));
            var g = gameObject.AddComponent<LightBullet>();
            g.lightBullet(dir, transform.position, playerLayer);
        }
    }

}
