using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCenter : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    SpriteRenderer spriteRenderer;
    Material materialPrefab;

    const float MAX_INTENSITY = 3.0f;
    const float MIN_INTENSITY = 1.05f;
    float intensity = MIN_INTENSITY;

    public const float TIME_TO_KICK = PULSES_TO_KICK * PULSE_TIME;
    public const int PULSES_TO_KICK = 2;
    public const float PULSE_TIME = 1.2f;

    float timer = 0.0f;
    bool isTimerDone = true;
    public bool isHome = true;

    Material material;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        materialPrefab = Resources.Load<Material>("GFX/Materials/M_BasicGlow");
        material = spriteRenderer.material;
    }

    void Update()
    {
        UpdateTimer();
        UpdateGFX();
    }

    void UpdateGFX()
    {
        float SecondsLeftOfPulse = timer % PULSE_TIME;

        float intensityNormalized = Mathf.Lerp(-1, 1, SecondsLeftOfPulse / PULSE_TIME);
        intensityNormalized = 1 - Mathf.Abs(intensityNormalized);

        intensity = Mathf.Lerp(MIN_INTENSITY, MAX_INTENSITY, intensityNormalized);

        material.SetFloat("Intensity", intensity);
    }

    void UpdateTimer()
    {
        if (!isTimerDone)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (isHome){
                    playerExitHome();
                }

                isTimerDone = true;
                timer = 0.0f;
            }
        }
    }

    public void playerEnterHome()
    {
        timer = TIME_TO_KICK;
        isTimerDone = false;
        isHome = true;
    }

    public void playerExitHome()
    {
        isHome = false;
        playerController.PlayerDo.ExitHome(true);
    }
}
