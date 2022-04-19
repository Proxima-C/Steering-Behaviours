using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        livesText.text = "Lives: " + player.CurrentLivesCount;
        player.OnLivesCountChange += PlayerController_OnLivesCountChange;
    }

    private void PlayerController_OnLivesCountChange(object sender, EventArgs e)
    {
        livesText.text = "Lives: " + player.CurrentLivesCount;
    }
}
