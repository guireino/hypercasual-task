using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class LevelManager : MonoBehaviour{

    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currentSetup;
    private int _index;
    public Transform container;
    //public List<GameObject> levels;
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    [Header("Animation")]
    public float scaleDuration = .2f;
    public float scaleTimeBetweenPieces = .1f;
    public Ease ease = Ease.OutBack;

    // Start is called before the first frame update
    void Start(){
        CreateLevelPieces();
    }

    // Update is called once per frame
    void Update(){

        if(Input.GetKeyDown(KeyCode.D)){
            //SpawnNextLevel();
            CreateLevelPieces();
        }
    }

    private void CreateLevelPieces(){
        
        CleanSpawnedPieces();

        if (_currentSetup != null){

            _index++;

            if(_index >= levelPieceBasedSetups.Count){
                ResetLevelIndex();
            }
        }

        _currentSetup = levelPieceBasedSetups[_index];
    
        for (int i = 0; i < _currentSetup.piecesStartNumber; i++){ //percorre todas pesas list piecesStartNumber
            CreateLevelPiece(_currentSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currentSetup.piecesNumber; i++){  //percorre todas pesas list piecesNumber
            CreateLevelPiece(_currentSetup.levelPieces);
        }

        for (int i = 0; i < _currentSetup.piecesEndNumber; i++){  //percorre todas pesas list piecesEndNumber
            CreateLevelPiece(_currentSetup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(_currentSetup.artType);

        StartCoroutine(ScalePiecesByTime());
    }

    private void ResetLevelIndex(){
        _index = 0; // se valor for maior que os levels ele volta para 0
    }

    IEnumerator ScalePiecesByTime(){

        foreach (var p in _spawnedPieces){
            p.transform.localScale = Vector3.zero;
        }

        yield return null;

        for (int i = 0; i < _spawnedPieces.Count; i++){
            _spawnedPieces[i].transform.DOScale(1, scaleDuration).SetEase(ease); // colocando animação ao criar level
            yield return new WaitForSeconds(scaleTimeBetweenPieces);
        }

        CoinsAnimationManager.Instance.StartAnimations();
    }


    private void CreateLevelPiece(List<LevelPieceBase> list){

        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container); // instanciando pedaço e container da level onde vai fica os pedaços.

        if(_spawnedPieces.Count > 0){
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];
            spawnedPiece.transform.position = lastPiece.endPiece.position; //colocar o ultimo posição e no inicial de cada pesa
            //Debug.Log("spawnedPiece.transform.position " + spawnedPiece.transform.position);
        }else{
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach (var p in spawnedPiece.GetComponentsInChildren<ArtPiece>()){ // ele vai achar pesas das variável spawnedPiece
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currentSetup.artType).gameObject);
            Debug.Log("ChangePiece = gameObject: " + gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces(){
        
         for (int i = _spawnedPieces.Count - 1; i >= 0; i--){ //deletando de trás para frente
            Destroy(_spawnedPieces[i].gameObject);
        }

        _spawnedPieces.Clear();
    }
}
