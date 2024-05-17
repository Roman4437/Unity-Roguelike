using System;
using UnityEngine;

public class FireballSkill : BasicProjectileCast
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActivateSkill();
        }
    }
}