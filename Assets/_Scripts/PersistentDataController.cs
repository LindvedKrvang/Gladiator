using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataController : MonoBehaviour {

    public Character SelectedCharacter { get; set; }
    public int CharacterIndex { get; set; }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
