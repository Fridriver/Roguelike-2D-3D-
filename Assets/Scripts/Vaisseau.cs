using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaisseau : MonoBehaviour
{
    [SerializeField] private float vitesseDeplacement = 5f;
    [SerializeField] private float vitesseRotation = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float deplacementHorizontal = Input.GetAxis("Horizontal");
        float deplacementVertical = Input.GetAxis("Vertical");

        if (Input.GetMouseButton(1)) // Clic droit enfoncé
        {
            float rotationX = -Input.GetAxis("Mouse Y") * vitesseRotation * Time.deltaTime;
            float rotationY = Input.GetAxis("Mouse X") * vitesseRotation * Time.deltaTime;

            // Appliquer la rotation au vaisseau
            transform.Rotate(rotationX, rotationY, 0f);

            // Déplacer le vaisseau vers le haut ou le bas en fonction du mouvement vertical de la souris
            float mouvementVerticalSouris = Input.GetAxis("Mouse Y");
            Vector3 deplacementVerticalSouris = new Vector3(0f, mouvementVerticalSouris, 0f) * vitesseDeplacement;
            transform.Translate(deplacementVerticalSouris * Time.deltaTime);
        }
        else
        {
            
        }
        // Rotation du vaisseau avec les touches horizontales
        transform.Rotate(0f, deplacementHorizontal * vitesseRotation * Time.deltaTime, 0f);

        // Ajout de force au vaisseau dans la direction du déplacement vertical
        Vector3 forceDeplacement = transform.forward * deplacementVertical * vitesseDeplacement;
        rb.AddForce(forceDeplacement);
    }
}
