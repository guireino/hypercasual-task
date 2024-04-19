using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : ItemsCollectableBase{

    [Header("PowerUp")]
    public float duration;

    protected override void OnCollect(){
        base.OnCollect();
        StartPowerUp();
    }

    protected virtual void StartPowerUp(){
        Invoke(nameof(EndPowerUp), duration);
        Debug.Log("StartPowerUp ");
    }

    protected virtual void EndPowerUp(){
        Debug.Log("EndPowerUp ");
    }
    
}