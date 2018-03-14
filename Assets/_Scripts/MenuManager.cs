using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   

    private PersistentDataController _persistentData;

	// Use this for initialization
	void Start ()
	{
	    _persistentData = FindObjectOfType<PersistentDataController>();
	}

    public void SelectCharacter(int characterIndex)
    {
        _persistentData.CharacterIndex = characterIndex;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
