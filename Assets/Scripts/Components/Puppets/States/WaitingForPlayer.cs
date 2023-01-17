using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingForPlayer : APuppetState
{
    public WaitingForPlayer(IPuppet pup) : base(pup)
    {
    }

    public override void Enter()
    {
        //pup.PlayerAtSightNull();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        if (pup.PlayerAtSight() != null) //si no devuelve null quiere decir q ha encontrado al jugador y lo persigue
        {
            pup.SetState(new FollowingPlayer(pup));
        }
    }

    public override void FixedUpdate()
    {
    }
}
