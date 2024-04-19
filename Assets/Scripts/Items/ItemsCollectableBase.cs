using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollectableBase : MonoBehaviour{

    public GameObject graphicItem;
    public string compareTag = "Player";
    public int timeToHide = 3;

    private void OnTriggerEnter(Collider col) {

        if(col.transform.CompareTag(compareTag)){
            Collect();
        }
    }

    protected virtual void Collect(){

        graphicItem?.SetActive(false); // ? checando se object nao esta null para nao fica travando game
        Invoke("HideObject", timeToHide); // invoke chama um método por um tempo, para esperar efeitos das partículas antes do objeto ser destrói-o
        OnCollect();
    }

    public void HideObject(){
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect(){
        
    }
}
