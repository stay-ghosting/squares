using UnityEngine;
using System.Collections.Generic;

public class HealthBar : MonoBehaviour
{
    int maxLives = PlayerController.MAX_LIVES;
    List<GameObject> G_Hearts = new List<GameObject>();

    [SerializeField]
    Material heartMaterial;

    [SerializeField]
    int dir;
    
    [SerializeField]
    Sprite heart;
    [SerializeField]
    Sprite noHeart;

    void Awake()
    {
        for (int i = 0; i < maxLives; i++)
        {
            G_Hearts.Add(Instantiate(new GameObject($"heart {maxLives}", typeof(SpriteRenderer)), 
                                      transform.position + Vector3.right * dir * i, Quaternion.identity, transform));
            SpriteRenderer spriteRenderer = G_Hearts[i].GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = heart;
            spriteRenderer.material = heartMaterial;
        }
    }
    

    public void updateHealth(int lives)
    {
        print(lives);
        for (int i = 0; i < G_Hearts.Count; i++)
        {
            if (i > lives-1)
            {
                G_Hearts[i].GetComponent<SpriteRenderer>().sprite = noHeart;
            }
        }
    }
}
