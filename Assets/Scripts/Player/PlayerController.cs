using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class PlayerController : Singleton<PlayerController>{

    private Vector3 _pos, _startPosition;
    private float _currentSpeed, _baseSpeedToAnimation = 7;
    private bool _canRun;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("lerp")]
    public GameObject endScreen;
    public Transform target;
    public float lerpSpeed = 1f;
    public float speed = 1f;
    public string tagToCheckEnemy = "Enemy";
    public string tagToCheckEndLine = "EndLine";

    [Header("TextMeshPro")]
    public TMP_Text uiTextPowerUp;

    [Header("PowerUps")]
    public bool invincible = false;

    [Header("Coin Setup")]
    public GameObject coinCollector;


    private void Start() {
        _startPosition = transform.position;  //salvando posição original
        ResetSpeed();
        SetPowerUpText("");
    }

    // Update is called once per frame
    void Update(){
        
        if(!_canRun) return; // se ele nao poder corre ele nao passa da aqui

        _pos = target.position; // criando variável para pegar position

        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position,  _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision col){
        if(col.transform.tag == tagToCheckEnemy){
            MoveBack(col.transform);
            if(!invincible) EndGame(AnimatorManager.AnimationType.DEAD);
        }
    }

    public void OnTriggerEnter(Collider col){
        if(col.transform.tag == tagToCheckEndLine){
            if(!invincible) EndGame();
        }
    }

    private void MoveBack(Transform t){ // fazer personagem mover para atrás quando ele morre
        t.DOMoveZ(1f, .3f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.IDLE){
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun(){
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation); //fazendo calculo para animação seguir speed player
    }

    #region POWER UPS

    public void PowerUpSpeedUp(float f){
        _currentSpeed = f;
    }

    public void SetPowerUpText(string s){
        
        uiTextPowerUp.text = s;
    }

    public void ResetSpeed(){
        _currentSpeed = speed;
    }

    public void SetInvincible(bool b = true){
        invincible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease){
        /*
        var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/

        //fazendo animação com o movimento no y
        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);
    }

    public void ResetHeight(){

        transform.DOMoveY(_startPosition.y, .1f);
        /*
        var p = transform.position; // vai pegar posição voltar original
        p.y = _startPosition.y;
        transform.position = p; */
    }

    public void ChangeCoinCollectorSize(float amount){
        coinCollector.transform.localScale = Vector3.one * amount;
        Debug.Log("ChangeCoinCollectorSize coinCollector " + coinCollector);
    }

    #endregion

}