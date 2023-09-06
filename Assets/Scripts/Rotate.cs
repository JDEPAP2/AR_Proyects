using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.parent.position, transform.parent.up, 90f * Time.deltaTime);
    }
}
