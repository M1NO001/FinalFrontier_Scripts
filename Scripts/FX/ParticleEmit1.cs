using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmit1 : MonoBehaviour
{
    ParticleSystem particle;


    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        //particle.Emit(1);
        Invoke("SetActiveFalse", 1f);
    }

    private void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }

}
