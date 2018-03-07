using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject Cinemachine;
    public GameObject Player;

    private Character _selectedCharacter;

	// Use this for initialization
	void Start ()
	{
	    var data = FindObjectOfType<PersistentDataController>();
	    _selectedCharacter = data.SelectedCharacter;
        InstantiatePlayer();
	}

    private void InstantiatePlayer()
    {
        var player = Instantiate(Player, transform.position, Quaternion.identity);
        var playerController = player.GetComponent<PlayerController>();
        playerController.SetCharacterDetails(_selectedCharacter);

        var cmScript = Cinemachine.GetComponent<CinemachineVirtualCamera>();
        cmScript.Follow = player.transform;
    }
}
