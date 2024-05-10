using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Singleton;
using DG.Tweening;
using UnityEngine;

public class CoinsAnimationManager : Singleton<CoinsAnimationManager>{ // singleton pode instanciar em todo as class que ele foi chamado

    public List<ItemCollectableCoin> itens; // buscando moedas na class ItemCollectableCoin e colocando uma list

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    // Start is called before the first frame update
    void Start(){
        itens = new List<ItemCollectableCoin>();
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.T)){
            StartAnimations();
        }    
    }

    public void RegisterCoin(ItemCollectableCoin i){

        if (!itens.Contains(i)){
            itens.Add(i);
            i.transform.localScale = Vector3.zero;
        }
    }

    public void StartAnimations(){
        StartCoroutine(ScalePiecesByTime());
    }

    IEnumerator ScalePiecesByTime(){

        foreach (var p in itens){
            p.transform.localScale = Vector3.zero;
        }

        Sort();
        
        yield return null;

        for (int i = 0; i < itens.Count; i++){
            itens[i].transform.DOScale(1, scaleDuration).SetEase(ease); // colocando animação ao criar level
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }
    }

    private void Sort(){
        //quando distancia for maior ele vai colocar primeiro coins mais perto dele
        itens = itens.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToList();
    }
    
}