using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Mage")]
public class Mage : Character
{
    [Space]
    [Header("Mage Details")]
    public GameObject Frostbolt;
    
    public override void Attack(bool isDirectionRight, Vector2 position)
    {
        position.y = position.y + 0.2f;
        var frostbolt = Instantiate(Frostbolt, position, Quaternion.identity);
        var fbController = frostbolt.GetComponent<FrostboltController>();
        fbController.SetDirection(isDirectionRight);
    }
}
