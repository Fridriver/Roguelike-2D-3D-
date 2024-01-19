using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radar : MonoBehaviour
{
    [SerializeField] private GameObject containerPing = default;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private LayerMask radarLayerMask;
    [SerializeField] private Transform prefabRadarPing;
    [SerializeField] private Transform spaceshipPosition;
    [SerializeField] private Image radarImage;
    [SerializeField] private Camera cameraRadar;

    private float rayonRadar;
    private float cameraSize;

    private Quaternion initialRotation;
    private Dictionary<int, float> enemyLastPingTime = new Dictionary<int, float>();

    void Start()
    {
        initialRotation = radarImage.rectTransform.rotation;
        cameraSize = cameraRadar.orthographicSize;
        rayonRadar = cameraSize;
    }

    void Update()
    {
        Vector3 direction = Quaternion.Euler(0, rotationSpeed * Time.time, rotationSpeed * Time.time) * Vector3.forward;

        Ray ray = new Ray(spaceshipPosition.position, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayonRadar, radarLayerMask))
        {
            int enemyID = hit.collider.GetInstanceID();

            if (!enemyLastPingTime.ContainsKey(enemyID) || Time.time - enemyLastPingTime[enemyID] > 1f)
            {
                float taille = cameraSize / 10f;
                Vector3 scale = new Vector3(taille, taille, taille);

                RadarPing radarPing = Instantiate(prefabRadarPing, hit.point, initialRotation * Quaternion.identity).GetComponent<RadarPing>();
                
                radarPing.transform.localScale = scale;
                radarPing.SetColor(new Color(1, 0, 0));

                radarPing.transform.SetParent(containerPing.transform);

                enemyLastPingTime[enemyID] = Time.time;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction * rayonRadar, Color.green);

        radarImage.rectTransform.rotation = initialRotation * Quaternion.Euler(0, 0, -rotationSpeed * Time.time);
    }
}