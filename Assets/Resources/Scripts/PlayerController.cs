using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int player;
    public int Player { get { return player; } }
    public int otherPlayer{
        get 
        {
            switch(player)
            {
                case 1:
                    return 2;
                case 2:
                    return 1;
                default:
                    return 0;
            }
            
        }
    }

    [System.NonSerialized]
    public int lives;
    [System.NonSerialized]
    public int bullets;

    [System.NonSerialized]
    bool doFlipX = false;
    public bool DoFlipX { get { return doFlipX; } }

    public const float MAX_MOVE_COOLDOWN = 0.6f;
    public const float MAX_ACTION_COOLDOWN = 0.5f;
    public const float TIME_DEAD = 1.5f;
    public const float SHIELD_HIT_TIME = 0.5f;

    public const int MAX_LIVES = 3;

    public const int MAX_BULLETS = 3;

    // [System.NonSerialized]
    public bool onMoveCooldown = false;
    public bool canMove = true;

    // [System.NonSerialized]
    [SerializeField]
    public PlayerState state = PlayerState.home;

    // set in awake

    [System.NonSerialized]
    public Player PlayerDo;

    [System.NonSerialized]
    public BoxCollider2D Col;

    [System.NonSerialized]
    public PlayerAnimator PlayerAnimator;

    [System.NonSerialized]
    public DissolveSpawner DissolveSpawner;

    [System.NonSerialized]
    public SpriteRenderer SpriteRenderer;

    public Transform Transform { get { return transform; } }
    // set in inspector
    
    [SerializeField]
    public HeavyBullet.bulletColor color;

    [SerializeField]
    public GridCenter gridCenter;

    [SerializeField]
    public Transform PlayerSpawnLocation;

    [SerializeField]
    public HealthBar HealthBar;

    [SerializeField]
    public BulletsGui BulletsGui;

    [SerializeField]
    public BulletSpawner BulletSpawner;

    [SerializeField]
    public Target Target;

    [SerializeField]
    public PlayerGrid PlayerGrid;

    void Awake()
    {
        Col = gameObject.GetComponent<BoxCollider2D>();
        DissolveSpawner = gameObject.GetComponent<DissolveSpawner>();
        PlayerAnimator = gameObject.GetComponent<PlayerAnimator>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        PlayerDo = gameObject.GetComponent<Player>();

        lives = MAX_LIVES;
        bullets = MAX_BULLETS;

        PlayerDo.EnterHome(); 
    }
}