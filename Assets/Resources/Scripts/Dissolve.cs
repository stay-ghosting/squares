using UnityEngine;

public class Dissolve: MonoBehaviour
{
    float Opacity = 1.0f;
    float timePassed = 0.0f;
    const float SECONDS_TO_DESOLVE = 0.3f;
    string sortingLayer = "PlayerDissolve";

    SpriteRenderer spriteRenderer;

    Material materialPrefab;
    Material dissolveMaterial;

    public void Awake()
    {
        materialPrefab = Resources.Load<Material>("GFX/Materials/M_dissolve");

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = sortingLayer;

        // set material
        spriteRenderer.material = materialPrefab;
        // set reference
        dissolveMaterial = spriteRenderer.material;

        dissolveMaterial.SetFloat("Opacity", Opacity);
        dissolveMaterial.SetFloat("Random", Random.Range(0, 10));
    }

    public void  dissolve(Sprite sprite, bool doFlipX, Vector3 scale, Vector3 pos,
        float noiseScale, Color color, float edgeThickness)
    {
        dissolveMaterial.SetColor("EdgeColor", color);
        dissolveMaterial.SetFloat("EdgeThickness", edgeThickness);
        dissolveMaterial.SetFloat("NoiseScale", noiseScale);

        gameObject.transform.position = pos;
        gameObject.transform.localScale = scale;

        spriteRenderer.sprite = sprite;
        spriteRenderer.flipX = doFlipX;
    }

    void Update()
    {
        float PercentOfDessolve = timePassed / SECONDS_TO_DESOLVE;
        Opacity = Mathf.Lerp(1, 0, PercentOfDessolve);
        timePassed += Time.deltaTime;

        dissolveMaterial.SetFloat("Opacity", Opacity);

        if (PercentOfDessolve >= 1)
        {
            Destroy(gameObject);
        }
    }
}
