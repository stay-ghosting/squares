using UnityEngine;
using TMPro;
using System;

public class winnerTextScript : MonoBehaviour
{
    [SerializeField] GameObject G_icon;
    [SerializeField] GameObject G_error;
    [SerializeField] GameObject G_text;

    [SerializeField] Transform[] playerIconPos = new Transform[2];
    [SerializeField] Transform[] textPos = new Transform[2];

    Animator animator;
    TextMeshPro textComponent;
    
    int winner;

    void Awake()
    {
        winner = GameEvents.winner;

        animator = G_icon.GetComponent<Animator>();
        textComponent = G_text.GetComponent<TextMeshPro>();

        animator.SetInteger("PlayerWinner", winner);

        textComponent.text = $"Player {winner} Wins!";

        try
        {
            G_icon.transform.position = playerIconPos[winner - 1].position;
            G_text.transform.position = textPos[winner - 1].position;

        }
        catch (IndexOutOfRangeException)
        {
            G_error.SetActive(true);
            G_text.SetActive(false);
            G_icon.SetActive(false);
        }

    }
}
