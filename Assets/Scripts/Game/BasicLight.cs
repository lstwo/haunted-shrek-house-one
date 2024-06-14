using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLight : MonoBehaviour
{
    public GameObject spotLight, pointLight;
    public bool enableSpotLight = true, enablePointLight = true;

    void Awake()
    {
        if(spotLight != null) spotLight.SetActive(enableSpotLight);
        if(pointLight != null) pointLight.SetActive(enablePointLight);
    }
}
