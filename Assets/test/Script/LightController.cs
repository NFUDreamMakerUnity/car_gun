using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    bool lightSwitch = false;
    [SerializeField] Renderer light_1;
    [SerializeField] Renderer light_2;

    [SerializeField] Material lightOff;
    [SerializeField] Material lightOn;

    [SerializeField] Light L_one;
    [SerializeField] Light L_two;

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LightSwitch();
        }

        Light();        
    }

    void LightSwitch()
    {
        lightSwitch = !lightSwitch;
    }

    void Light()
    {
        if (lightSwitch)
        {
            light_1.material = lightOn;
            light_2.material = lightOn;
            L_one.enabled = true;
            L_two.enabled = true;
        }
        else if (!lightSwitch)
        {
            light_1.material = lightOff;
            light_2.material = lightOff;
            L_one.enabled = false;
            L_two.enabled = false;
        }

    }

}
