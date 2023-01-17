using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APuppetState : IPuppetState
{
    protected IPuppet pup; //referencia al contexto
                              //protected quiere decir q solo la pueden ver y modificar las clases hijas q extienden esta clase

    public APuppetState(IPuppet pup)
    {
        this.pup = pup;
    }

    //Redefinimos estados como abstractos:
    public abstract void Enter();
    public abstract void Exit();
    public abstract void Update();
    public abstract void FixedUpdate();
}
