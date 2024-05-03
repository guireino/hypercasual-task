using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour{

    private GameObject _currentLevel;
    private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currentSetup;
    private int _index;
    public Transform container;
    //public List<GameObject> levels;
    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    // Start is called before the first frame update
    void Start(){
        CreateLevelPieces();
    }

    // Update is called once per frame
    void Update(){
        
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
    }

    private void ResetLevelIndex(){
        _index = 0; // se valor for maior que os levels ele volta para 0
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
