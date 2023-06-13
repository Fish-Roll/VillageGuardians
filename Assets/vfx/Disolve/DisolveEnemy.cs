using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveEnemy : MonoBehaviour
{
    [SerializeField] public SkinnedMeshRenderer Mesh;

    private Material[] Materials;
    public float disolveRate = 0.0125f;
    public float refreshRate = 0.025f;
    [SerializeField] private ParticleSystem  dieParticles;

    // Start is called before the first frame update
    void Start()
    {
        Materials = Mesh.materials;
    }

    public IEnumerator DisolveCo ()
    {
        dieParticles.Play();
        if (Materials.Length > 0)
        {
            float counter = 0;
            while (Materials[0].GetFloat("_DisolveAmount") < 1)
            {
                counter += disolveRate;
                for (int i = 0; i < Materials.Length; i++)
                {
                    Materials[i].SetFloat("_DisolveAmount", counter);
                }
                yield return new WaitForSeconds(refreshRate);
            }
        }
        Destroy(gameObject);
    }
}
