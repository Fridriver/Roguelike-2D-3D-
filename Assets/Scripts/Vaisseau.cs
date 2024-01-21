using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vaisseau : MonoBehaviour
{
    [SerializeField] private float vitesseDeplacement = 5f;
    [SerializeField] private float vitesseRotation = 5f;


    [SerializeField] private GameObject mancheRotation;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //float deplacementHorizontal = Input.GetAxis("Horizontal");
        //float deplacementVertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(1)) // Clic droit enfoncé
        {
            float rotationX = -Input.GetAxis("Mouse Y") * vitesseRotation * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse X") * vitesseRotation * Time.deltaTime;

            // Appliquer la rotation au vaisseau
            transform.Rotate(-rotationX, rotationY, 0f);
            mancheRotation.transform.Rotate(-rotationX, 0f, -rotationY);

            // Déplacer le vaisseau vers le haut ou le bas en fonction du mouvement vertical de la souris
            float mouvementVerticalSouris = Input.GetAxis("Mouse Y");
            Vector3 deplacementVerticalSouris = new Vector3(0f, mouvementVerticalSouris, 0f) * vitesseDeplacement;
            transform.Translate(deplacementVerticalSouris * Time.deltaTime);
        }
        else
        {
            // Obtenez la rotation actuelle du mancheRotation
            Quaternion rotationActuelle = mancheRotation.transform.rotation;
            Vector3 rotationVaisseauActuelle = transform.rotation.eulerAngles;

            // Créez une nouvelle rotation avec chacune des composantes (x, y, z) à 0
            Quaternion rotationCible = Quaternion.Euler(0f, 0f, 0f);

            // Utilisez RotateTowards pour ajuster progressivement chaque composante de la rotation vers 0
            float rotationSpeed = 50f; // Ajustez cette valeur selon vos besoins

            mancheRotation.transform.rotation = Quaternion.RotateTowards(rotationActuelle, rotationCible * Quaternion.Euler(rotationVaisseauActuelle), rotationSpeed * Time.deltaTime);
        }

        //// Rotation du vaisseau avec les touches horizontales
        //transform.Rotate(0f, deplacementHorizontal * vitesseRotation * Time.deltaTime, 0f);

        //// Ajout de force au vaisseau dans la direction du déplacement vertical
        //Vector3 forceDeplacement = transform.forward * deplacementVertical * vitesseDeplacement;
        //rb.AddForce(forceDeplacement);
        mouvementVaisseau();
        LimitRotZ();
    }

    private void mouvementVaisseau()
    {
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");
        // Rotation du vaisseau avec les touches horizontales
        transform.Rotate(0f, deplacementHorizontal * vitesseRotation * Time.deltaTime, 0f);
        // Ajout de force au vaisseau dans la direction du déplacement vertical
        Vector3 forceDeplacement = transform.forward * deplacementVertical * vitesseDeplacement;
        rb.AddForce(forceDeplacement);
    }

    private void LimitRotZ()
    {
        Vector3 mancheEulerAngles = mancheRotation.transform.rotation.eulerAngles;

        mancheEulerAngles.z = (mancheEulerAngles.z > 180f) ? mancheEulerAngles.z - 360f : mancheEulerAngles.z;
        mancheEulerAngles.z = Mathf.Clamp(mancheEulerAngles.z, -22.5f, 22.5f);

        mancheRotation.transform.rotation = Quaternion.Euler(mancheEulerAngles);
    }
}
