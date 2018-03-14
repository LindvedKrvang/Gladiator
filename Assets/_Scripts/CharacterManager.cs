using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public Character[] Characters;

    public Character GetCharacter(int index)
    {
        return Characters[index];
    }
}
