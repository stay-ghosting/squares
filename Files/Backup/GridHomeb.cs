public class GridHomeb : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Material material;

    [SerializeField] IPlayerController playerController;
    [SerializeField] float timer = 0.0f;
    [SerializeField] bool timerDone = true;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    void Update()
    {
        UpdateTimer();
        UpdateGFX();
    }

    void UpdateGFX()
    {
        float t;
        t = timer % GridHomeInfo.current.pulseTime;
        if (t > GridHomeInfo.current.pulseTime / 2)
        {
            t = GridHomeInfo.current.pulseTime - t;
        }

        Color color = GridHomeInfo.current.colorGradient.Evaluate(t);
        material.SetColor("_Color", color);
    }

    void UpdateTimer()
    {
        if (timer > 0)
        {
            timerDone = false;
            timer -= Time.deltaTime;
        }
        else
        {
            if (timerDone == false)
            {
                playerController.PlayerDo.KickOut(playerController, this);
                timerDone = true;
            }
        }
    }



    public void playerExitHome()
    {
        timerDone = true;
        timer = 0.0f;
    }
}