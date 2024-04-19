using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpCoin : PowerUpBase{

    [Header("Coin collector")]
    public float sizeAmount = 7;
    
     protected override void StartPowerUp(){
        base.StartPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(sizeAmount);
        PlayerController.Instance.SetPowerUpText("Coins Collector");
    }

    protected override void EndPowerUp(){
        base.EndPowerUp();
        PlayerController.Instance.ChangeCoinCollectorSize(1);
        PlayerController.Instance.SetPowerUpText("");
    }
}