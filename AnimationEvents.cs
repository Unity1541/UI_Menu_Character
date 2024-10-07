using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [Header("Renzoku")]
    public ParticleSystem [] renzoku_particleSystems;

    [Header("Zentusi")]
    public ParticleSystem [] zentsu_particleSystems;

    [Header("Giyu")]
    public ParticleSystem [] giyu_particleSystems;

    [Header("muich")]
    public ParticleSystem [] muich_particleSystems;

    public void renzoku_particleSystemsStart(int i)
    {
        renzoku_particleSystems[i].Play();
    }
    public void renzoku_particleSystemsStop(int i)
    {
        renzoku_particleSystems[i].Stop();
    }

    public void zentsu_particleSystemsStart(int i)
    {
        zentsu_particleSystems[i].Play();
    }

    public void zentsu_particleSystemsStop(int i)
    {
        zentsu_particleSystems[i].Stop();
    }

    public void giyu_particleSystemStart(int i)
    {
        giyu_particleSystems[i].Play();
    }

    public void giyu_particleSystemStop(int i)
    {
        giyu_particleSystems[i].Stop();
    }

    public void muich_particleSystemStart(int i)
    {
        muich_particleSystems[i].Play();
    }

    public void muich_particleSystemStop(int i)
    {
        muich_particleSystems[i].Stop();
    }
}
