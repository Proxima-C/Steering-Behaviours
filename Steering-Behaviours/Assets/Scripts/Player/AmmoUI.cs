using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private PlayerController player;

    private void Start()
    {
        ammoText.text = "Bullets: " + player.BulletsCount;
        player.OnBulletsCountChange += PlayerController_OnBulletsCountChange;
    }

    private void PlayerController_OnBulletsCountChange(object sender, EventArgs e)
    {
        ammoText.text = "Bullets: " + player.BulletsCount;
    }
}
