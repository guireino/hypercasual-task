using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour{

    public Animator animator;
    public List<AnimatitorSetup> animatorSetups;

    public enum AnimationType{
        IDLE, RUN, DEAD
    }

    public void Play(AnimationType type, float currentSpeedFactor = 1f){

        animatorSetups.ForEach(i => {

            if (i.type == type){
                animator.SetTrigger(i.trigger);
            }
        });

        foreach(var animation in animatorSetups){

            if(animation.type == type){
                animator.SetTrigger(animation.trigger);
                animator.speed = animation.speed * currentSpeedFactor;
                break; // break e para porque ele ja achou que ele precisa
            }
        }
    }

    // Update is called once per frame
    void Update(){
        
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            Play(AnimationType.RUN);
        }else if (Input.GetKeyDown(KeyCode.Alpha2)){
            Play(AnimationType.DEAD);
        }else if (Input.GetKeyDown(KeyCode.Alpha3)){
            Play(AnimationType.IDLE);
        }    
    }
}

[System.Serializable]
public class AnimatitorSetup{

    public AnimatorManager.AnimationType type;
    public string trigger;
    public float speed = 1f; //colocando speed por que quando personagem fica muito rápido animação fica bug.

}