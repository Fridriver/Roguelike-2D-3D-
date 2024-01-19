using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 initialMousePosition;
    private Vector3 initialObjectCenter; // Ajout d'une variable pour stocker le centre initial de l'objet
    private Quaternion initialRotation;
    [SerializeField] private float rotationSpeed = 1f;

    private float maxRotation = -125f;
    private float minRotation = 125f;

    void OnMouseDown()
    {
        isDragging = true;
        initialMousePosition = Input.mousePosition;
        initialObjectCenter = transform.position; // Enregistre le centre initial de l'objet
        initialRotation = transform.rotation;
    }

    void OnMouseDrag()
    {
        RotateCube();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void RotateCube()
    {
        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;

            //Debug.Log("Mouse position : " + currentMousePosition);
            float angleRad = Mathf.Atan2(currentMousePosition.y, currentMousePosition.x);
            float angleDeg = angleRad * Mathf.Rad2Deg;
            angleDeg = (angleDeg + 360) % 360;
            Debug.Log("Angle en degrés : " + angleDeg);

            // Calculer la différence de position de la souris par rapport à la position initiale
            Vector3 mouseOffset = currentMousePosition - initialMousePosition;

            // Calculer l'angle en fonction de la différence de position
            float angle = Mathf.Atan2(mouseOffset.y, mouseOffset.x) * Mathf.Rad2Deg;

            // Appliquer la rotation seulement si la souris a bougé
            if (mouseOffset.sqrMagnitude > 0.1f) // Ajustez la sensibilité selon vos besoins
            {
                Quaternion rotation = Quaternion.AngleAxis(angle * rotationSpeed, Vector3.forward);
                transform.rotation = initialRotation * rotation;
            }
        }
    }

}
