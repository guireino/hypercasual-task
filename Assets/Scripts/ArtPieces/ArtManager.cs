using System;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using UnityEngine;

public class ArtManager : Singleton<ArtManager>{
    
    public enum ArtType{
        TYPE_01, TYPE_02, BEACH, SNOW
    }

    public List<ArtSetup> artSetups;

    public ArtSetup GetSetupByType(ArtType artType){
        return artSetups.Find(i => i.artType == artType);
    }
}

[System.Serializable]
public class ArtSetup{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}