using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RageEffect : MonoBehaviour
{
    [SerializeField] public SkinnedMeshRenderer Mesh;

    private Material[] Materials;
    public float disolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    [SerializeField] private ParticleSystem  dieParticles;

    private bool _activated;
    // Start is called before the first frame update
    void Start()
    {
        Materials = Mesh.materials;
    }

    public void Init(ref bool activated)
    {
        _activated = activated;
    }

    public IEnumerator ActivateEffect ()
    {
        dieParticles.Play();
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_GlowIntensity") < 1)
            {
                counter += disolveRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    Materials[i].SetFloat("_GlowIntensity", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

    public IEnumerator DisableEffect()
    {
        float counter = 1;
        while (Materials[0].GetFloat("_GlowIntensity") >= 0f)
        {
            yield return new WaitForSeconds(refreshRate);
            counter -= disolveRate;
            Materials[0].SetFloat("_GlowIntensity", counter);
        }

        //Materials[0].SetFloat("_GlowIntensity", 0);
    }
}