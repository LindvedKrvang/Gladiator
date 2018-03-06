using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Projectile")]
public class Projectile : Ability {

    private float _adjustedY;

    [Space]
    [Header("Projectile Specific")]
    public GameObject ProjectileObject;

    public override void InitializeAbility(float adjustedY)
    {
        _adjustedY = adjustedY;
    }

    public override void UseAbility(Vector2 position, bool facingRight)
    {
        var rot = SetRotation(facingRight);

        position.y += _adjustedY;
        
        var frostbolt = Instantiate(ProjectileObject, position, Quaternion.Euler(rot));

        var fbController = frostbolt.GetComponent<ProjectileController>();
        fbController.SetDirection(facingRight);
        
    }

    private Vector3 SetRotation(bool facingRight)
    {
        var rot = ProjectileObject.transform.rotation.eulerAngles;
        if (!facingRight)
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
        return rot;
    }
}
