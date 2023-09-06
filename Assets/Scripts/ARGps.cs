using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.Android;
using Unity.XR.CoreUtils;

public class ARGps : MonoBehaviour
{
    public XROrigin sessionOrigin;
    public GameObject prefab;
    public Text txtGPS;

    int maxWait = 20;
    private bool gpsEnabled = false;
    public double latitude;
    public double longitude;
    public double altitude;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateGPS());
    }

    private void Awake()
    {
        if (!Application.isEditor)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
                Permission.RequestUserPermission(Permission.FineLocation);
            if (Permission.HasUserAuthorizedPermission(Permission.Camera))
                Permission.HasUserAuthorizedPermission(Permission.Camera);
        }
    }

    IEnumerator UpdateGPS()
    {
        if (Input.location.isEnabledByUser)
        {
            txtGPS.text = "Servicio de Localización: " + Input.location.isEnabledByUser;
        }
        else
        {
            txtGPS.text = "Servicio de Localización: " + Input.location.isEnabledByUser;
        }
        // Inicia el servicio de localización
        Input.location.Start();

        // Espera que esté activado
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            txtGPS.text = "Esperando: " + maxWait;
        }

        if (maxWait <= 0)
        {
            txtGPS.text = "Inicialización del GPS por fuera del tiempo límite";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            txtGPS.text = "Imposible determinar la posición del dispositivo.";
            yield break;
        }
        else
        {
            gpsEnabled = true;
            txtGPS.text = "Lectura GPS inicializda";

            //Obtiene los datos del gps
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
        }

        if (gpsEnabled)
        {
            //crea una posición en la escena basada en los datos del gps
            Vector3 position = sessionOrigin.transform.position;
            position.x = (float)longitude;
            position.y = 0;
            position.z = (float)latitude;

            //posicionar el marcador en la posición del gps
            prefab.transform.position = position;

            //calcula la orientación del marcador basado en la orientacion del dispositivo
            Vector3 forward = sessionOrigin.transform.forward;
            forward.y = 0;
            Quaternion rotation = Quaternion.LookRotation(forward);
            prefab.transform.rotation = rotation;

            //datos del gps en interfaz
            txtGPS.text = "Long: " + position.x.ToString() + "\n" + "Alt: " + position.y.ToString()
                + "\n" + "Lat: " + position.z.ToString() + "\n \n" + prefab.transform.position;

            Input.location.Stop();
        }


    }


}