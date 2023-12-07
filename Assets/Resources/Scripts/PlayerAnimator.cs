using UnityEngine;
using System;



public class PlayerAnimator : MonoBehaviour
{
    [Serializable]
    abstract class myAnimation
    {
        [SerializeField]
        protected float frameTime;
        internal float FrameTime { get { return frameTime; } }

        internal int Length { get { return animation.Length; } }

        protected string name;

        protected Sprite[] animation;
        internal Sprite this[int index] { get { return animation[index]; } }
    }

    class PlayerAnimation : myAnimation
    {
        internal readonly int startFrame;

        internal PlayerAnimation(string name, Sprite[] animation, float frameTime, int startFrame = 0)
        // if animation loops
        {
            this.startFrame = startFrame;
            base.name = name;
            base.animation = animation;
            base.frameTime = frameTime;

            if (startFrame > animation.Length - 1)
            {
                this.startFrame = 0;

                Debug.LogError($"Start frame out of range of index:{name}");
            }
        }
    }

    class PlayerAnimationAction : myAnimation
    {
        internal readonly float MAX_ACTION_COOLDOWN;

        internal PlayerAnimationAction(string name, Sprite[] animation, float frameTime, float maxActionCooldown)
        {
            // checks animation aint too long
            base.name = name;
            base.animation = animation;
            base.frameTime = frameTime;

            this.MAX_ACTION_COOLDOWN = maxActionCooldown;

            if (frameTime * animation.Length > maxActionCooldown)
            {
                Debug.LogError($"animation is longer than max cooldown:{name}");
            }
        }

        internal PlayerAnimationAction(string name, float maxActionCooldown, Sprite[] animation)
        // sets the frames to fit time perfectly
        {
            base.name = name;
            base.animation = animation;
            base.frameTime = maxActionCooldown / animation.Length;
            this.MAX_ACTION_COOLDOWN = maxActionCooldown;

            if (frameTime * animation.Length > maxActionCooldown)
            {
                Debug.LogError($"animation is longer than max cooldown:{name}");
            }
        }
    }

    enum AnimationState
    {
        idle,
        block,
        TriggerShoot,
        TriggerShieldHit,
        TriggerDeath,
    }
    AnimationState animationState = AnimationState.idle;

    SpriteRenderer spriteRenderer;

    myAnimation currentAnimation;

    [SerializeField] Sprite[] blockFrames;
    PlayerAnimation blockAnimation;
    [SerializeField] Sprite[] idleFrames;
    PlayerAnimation idleAnimation;

    [SerializeField] Sprite[] shieldHitFrames;
    PlayerAnimationAction shieldHitAnimation;
    [SerializeField] Sprite[] shootFrames;
    PlayerAnimationAction shootAnimation;
    [SerializeField] Sprite[] deathFrames;
    PlayerAnimationAction deathAnimation;

    readonly float MAX_ACTION_COOLDOWN = PlayerController.MAX_ACTION_COOLDOWN;
    readonly float TIME_DEAD = PlayerController.TIME_DEAD;
    readonly float SHIELD_HIT_TIME = PlayerController.SHIELD_HIT_TIME;

    int i = 1;
    float TimeInFrame = 0.0f;
    float fulltimer = 0.0f;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        blockAnimation = new PlayerAnimation("block", blockFrames, 0.3f);
        idleAnimation = new PlayerAnimation("idle", idleFrames, 0.3f, 1);

        shieldHitAnimation = new PlayerAnimationAction("ShieldHit", SHIELD_HIT_TIME, shieldHitFrames);
        shootAnimation = new PlayerAnimationAction("shoot", shootFrames, 0.02f, MAX_ACTION_COOLDOWN);
        deathAnimation = new PlayerAnimationAction("death", TIME_DEAD, deathFrames);
    }

    public void PlayIdleAnimation()
    {
        animationState = AnimationState.idle;
    }

    public void playBlockAnimation()
    {
        animationState = AnimationState.block;
    }

    public void playShieldHitAnimation()
    {
        //plays once
        animationState = AnimationState.TriggerShieldHit;
    }

    public void playShootAnimation()
    {
        // plays once
        animationState = AnimationState.TriggerShoot;
    }
    public void playDeathAnimation()
    {
        // plays once
        animationState = AnimationState.TriggerDeath;
    }


// check timing, google debug
    void PlayOnce(PlayerAnimationAction animation, AnimationState nextState)
    {
        if (nextState == animationState) Debug.LogError("line 81, current AnimatorState cannot be the same as nextState", this);

        // reset variables
        if (currentAnimation != animation)
        {
            i = 0;
            TimeInFrame = 0.0f;
            fulltimer = 0.0f;

            currentAnimation = animation;
            spriteRenderer.sprite = currentAnimation[0];
        }

        // update timer variables
        TimeInFrame += Time.deltaTime;
        fulltimer += Time.deltaTime;


        // check if animation should finish
        if (fulltimer > animation.MAX_ACTION_COOLDOWN)
        {
            // changing animation
            animationState = nextState;
        }
        // if animation hasnt finisheds
        else
        {
            // if time frame has been active >= how long it should be
            if (TimeInFrame >= currentAnimation.FrameTime)
            {
                TimeInFrame -= currentAnimation.FrameTime;
                //TimeInFrame = 0;

                if (i >= currentAnimation.Length)
                {
                    spriteRenderer.sprite = currentAnimation[animation.Length - 1];
                }
                else
                {
                    spriteRenderer.sprite = currentAnimation[i];
                    i++;
                }
            }
        }
    }

    void Animate(PlayerAnimation animation)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            i = animation.startFrame;
            TimeInFrame = 0.0f;
            spriteRenderer.sprite = currentAnimation[0];
        }

        TimeInFrame += Time.deltaTime;

        if (TimeInFrame >= currentAnimation.FrameTime)
        {
            TimeInFrame -= currentAnimation.FrameTime;

            i++;
            if (i >= currentAnimation.Length)
            {
                i = 0;
            }
            spriteRenderer.sprite = currentAnimation[i];
        }
    }

    void Update()
    {
        switch (animationState)
        {
            case AnimationState.idle:
                Animate(idleAnimation);
                break;
            case AnimationState.block:
                Animate(blockAnimation);
                break;
            case AnimationState.TriggerShoot:
                PlayOnce(shootAnimation, AnimationState.idle);
                break;
            case AnimationState.TriggerShieldHit:
                PlayOnce(shieldHitAnimation, AnimationState.idle);
                break;
            case AnimationState.TriggerDeath:
                PlayOnce(deathAnimation, AnimationState.idle);
                break;

        }
    }
}