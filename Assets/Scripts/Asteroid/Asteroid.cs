using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float tailleMin = 5f;
    public float tailleMax = 100f;

    void Start()
    {
        ChangerTailleAleatoire();
    }

    void ChangerTailleAleatoire()
    {
        float nouvelleTaille = Random.Range(tailleMin, tailleMax);

        transform.localScale = new Vector3(nouvelleTaille, nouvelleTaille, nouvelleTaille);
    }
}
