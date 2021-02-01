using UnityEngine.Experimental.Rendering.LWRP;
using System.Collections;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    private UnityEngine.Experimental.Rendering.Universal.Light2D lights;
    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private bool consistentFlicker;

    private void Start()
    {
        lights = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        StartCoroutine(Flicker());
    }
    //Turns on and off lights on a fixed or random interval
    IEnumerator Flicker() 
    {
        while (true) 
        {
            if(consistentFlicker)
                yield return new WaitForSeconds(maxWaitTime);
            else
                yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
            lights.enabled = !lights.enabled;
        }
    }
}
