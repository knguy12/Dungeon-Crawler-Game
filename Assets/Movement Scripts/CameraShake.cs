using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private new Transform transform;
    // Desired duration of the shake effect
    private float shakeDuration;
    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude;
    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;
    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    { 
        if (transform == null)
            transform = GetComponent(typeof(Transform)) as Transform;
    }
    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }
    public void TriggerShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
