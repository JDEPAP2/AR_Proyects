using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class FuncionesFiltro : MonoBehaviour
{
    public bool accion = false;
    public ARFaceManager managerCara;
    public GameObject g;
    public Text txt;

    void Update()
    {
        GameObject locat = GameObject.Find("Localizador");
        if(locat != null)
        {
            g.transform.position = locat.transform.parent.position;
        }

        if (accion)
        {
            if (g != null)
            {
                for (int i = 0; i < g.transform.childCount - 1; i++)
                {
                    g.transform.GetChild(i).gameObject.SetActive(!g.transform.GetChild(i).gameObject.activeInHierarchy);
                }

                GameObject obj = Instantiate(g.transform.GetChild(g.transform.childCount - 1).gameObject).gameObject;
                obj.SetActive(true);
                Vector3 a = locat.transform.parent.position;
                obj.transform.position = new Vector3(a.x, a.y + 0.1f, a.z);
                obj.transform.SetParent(transform, true);
            }

            accion = false;

        }
    }


    public void ShowTree()
    {
        string mss = "qqq";
        
        for (int i = 0; i < transform.root.childCount; i++)
        {
            mss += transform.root.GetChild(i).name + "\n";
        }
        txt.text = mss;
    }
}
