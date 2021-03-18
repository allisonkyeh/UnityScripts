using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormControl : MonoBehaviour {
    [SerializeField]
    [Range(0, 1)]
    private float progression = 0.0f;

    [SerializeField]
    Material[] colorMaterials;

    ParticleSystem rainSys;
    ParticleSystem rainSheetSys;
    ParticleSystem lightningSys;

    void Start() {
        rainSys = GameObject.Find("Rain").GetComponent<ParticleSystem>();
        rainSheetSys = GameObject.Find("Rain Sheet").GetComponent<ParticleSystem>();
        lightningSys = GameObject.Find("Lightning Sheet").GetComponent<ParticleSystem>();
    }

    void Update() {

        var playerX = transform.position.x;
        progression = (playerX - 24) / 88; // offset: 24, max: 112


        foreach (Material colorMat in colorMaterials) {
            colorMat.SetFloat("_ColorRange", progression);
        }

        var rainEmi = rainSys.emission;
        var rainSheetEmi = rainSheetSys.emission;
        var lightningEmi = lightningSys.emission;

        if (progression >= 0.41f) { // strongest state
            rainEmi.rateOverTime = 250f;
            rainSheetEmi.enabled = true;
            lightningEmi.enabled = true;
        } else if (progression >= 0.70f) { // ending state
            rainEmi.rateOverTime = 80f;
            lightningEmi.enabled = false;
        } else {
            rainEmi.rateOverTime = 150f; // starting state
            rainSheetEmi.enabled = false;
            lightningEmi.enabled = false;
        }
    }
}
