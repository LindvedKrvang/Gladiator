using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(menuName = "Abilities/Projectile")]
public class Projectile : Ability {

    private float _adjustedY;
    private float _adjustedX;

    [Space]
    [Header("Projectile Specific")]
    public GameObject ProjectileObject;

    public override void InitializeAbility(float adjustedY)
    {
        _adjustedY = adjustedY;
        _adjustedX = 0.4f;
    }

    public override GameObject UseAbility(Vector2 position, bool facingRight)
    {
        var rot = SetRotation(facingRight);

        position.y += _adjustedY;

        //TODO RKL: Refactor
        if (facingRight)
            position.x += _adjustedX;
        else
            position.x -= _adjustedX;
        
        var projectile = Instantiate(ProjectileObject, position, Quaternion.Euler(rot));

        var fbController = projectile.GetComponent<ProjectileController>();
        fbController.SetDirection(facingRight);

        return projectile;
    }

    private Vector3 SetRotation(bool facingRight)
    {
        var rot = ProjectileObject.transform.rotation.eulerAngles;
        if (!facingRight)
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
        return rot;
    }
}
