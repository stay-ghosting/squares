using UnityEngine;


//make bullets be able to be unlimited
public class BulletsGui : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites = new Sprite[4];

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void playerUpdateBullets(int bulletsLeft)
    {
        spriteRenderer.sprite = sprites[bulletsLeft];
    }
}
