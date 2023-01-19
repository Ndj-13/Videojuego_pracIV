using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitActivation : MonoBehaviour
{
    [SerializeField] BoxCollider weapon;

    private void Start()
    {
        if (!weapon) { gameObject.GetComponent<BoxCollider>(); }
        DesactivarCollider();
    }

    private void Update()
    {
        
    }

    public void ActivarCollider()
    {
        Debug.Log("ActivarCollider");
        weapon.enabled = true;
    }
    public void DesactivarCollider()
    {
        Debug.Log("DesactivarCollider");
        weapon.enabled = false;    
    }
}
