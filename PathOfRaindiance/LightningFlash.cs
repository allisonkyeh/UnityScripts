using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningFlash : MonoBehaviour
{
    Light dirLight;
    ParticleSystem source;

    public float flashIntensity;
    float baseIntensity;
    int prevFrame;
    int currFrame;

    public float lerpDuration;
    float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        dirLight = GetComponent<Light>();
        source = GameObject.Find("Lightning Sheet").GetComponent<ParticleSystem>();
        baseIntensity = dirLight.intensity;
        prevFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currFrame = source.particleCount;
        if (currFrame - prevFrame > 0)
        {
            StartCoroutine(Flash());
        }
        prevFrame = currFrame;
    }

    IEnumerator Flash()
    {
        timeElapsed = 0;

        // delay to match the flash frame of the drawn lightning animation
        yield return new WaitForSeconds(0.1f);

        while (timeElapsed < lerpDuration)
        {
            dirLight.intensity = Mathf.Lerp(flashIntensity, baseIntensity, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // wait till next frame before continuing
        }
        dirLight.intensity = baseIntensity;
    }
}
