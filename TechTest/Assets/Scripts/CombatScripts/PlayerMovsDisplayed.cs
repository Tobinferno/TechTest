using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovsDisplayed : MonoBehaviour
{
    
    [SerializeField] private TMP_Text textMovsBox;
    [SerializeField] private string movs;

    [SerializeField] private GameObject player;
    private PlayerMovements playerMovement;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovements>();
    }

    private void FixedUpdate()
    {
        movs = "Moves left: " + playerMovement.currentMovs;

        textMovsBox.text = movs;
    }
}
