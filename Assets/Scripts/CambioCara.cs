using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class CambioCara : MonoBehaviour
{
    [SerializeField] private ARFaceManager managerCara;
    public List<Material> mascaras = new List<Material>();
    public FuncionesFiltro fn;
    private int indexMascara = 0;

    // Start is called before the first frame update
    void Start()
    {
        managerCara = GetComponent<ARFaceManager>();
    }

    public void cambioTextura()
    {
        foreach (ARFace cara in managerCara.trackables)
        {
            cara.GetComponent<MeshRenderer>().material = mascaras[indexMascara];
        }

        indexMascara++;

        if (indexMascara == mascaras.Count)
        {
            indexMascara = 0;
        }
        fn.accion = true;
    }
}