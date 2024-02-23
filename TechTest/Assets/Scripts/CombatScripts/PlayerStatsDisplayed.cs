using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsDisplayed : MonoBehaviour
{
    [SerializeField] private TMP_Text textHealthBox;

    [SerializeField] private string health;
    //[SerializeField] private string movs;

    [SerializeField] private GameObject player;
    private PlayerMovements playerMovement;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovements>();
    }

    private void FixedUpdate()
    {
        health = "Health: " + playerMovement.currentHealthPlayer;
        //movs = "Remaining moves: " + playerMovement.currentMovs;

        textHealthBox.text = health;
        //textMovsBox.text = movs;
    }

}
