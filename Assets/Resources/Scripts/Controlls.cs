using UnityEngine;
using System;

public class Controlls : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    Vector2 dir = Vector2.zero;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    void Update()
    {
        // light shoot
        if (Input.GetButtonDown($"{playerController.Player}shoot"))
        {
            playerController.PlayerDo.TryShoot(false);
        }
        // heavy shoot
        if (Input.GetButtonDown($"{playerController.Player}shoot2"))
        {
            playerController.PlayerDo.TryShoot(true);
        }
        // block
        if (Input.GetButtonDown($"{playerController.Player}block"))
        {
            playerController.PlayerDo.TryBlock();
        }
        //  move update
        dir = getMoveInput;
        playerController.PlayerDo.MoveInput(dir);
    }

    Vector2 getMoveInput
    {
        get
        {
            return new Vector2(
                Mathf.Round(Input.GetAxisRaw($"{playerController.Player}right") * 1.25f), // make corners easyer to get to
                Mathf.Round(Input.GetAxisRaw($"{playerController.Player}up") * 1.25f)
            );
        }
    }

    public bool isMoving
    {
        get { return getMoveInput != Vector2.zero; }
    }
}