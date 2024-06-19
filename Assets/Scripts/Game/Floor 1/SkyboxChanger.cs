using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public void ChangeSkybox(Material skyboxToChangeTo)
    {
        RenderSettings.skybox = skyboxToChangeTo;
    }
}
