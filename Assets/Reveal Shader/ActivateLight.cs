using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ActivateLight : MonoBehaviour
{
    [SerializeField] Material revealMat;
    [SerializeField] AudioClip flashlightAudio;
    [SerializeField] GameObject[] otherLights;
    [SerializeField] GameObject flashlightTooltip;
    Light light;

    float nextFLTime = 0;

    bool lightActive = true;
    private void Awake()
    {
        light = GetComponent<Light>();
        ChangeLightActive();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.F) && Time.time > nextFLTime)
        {
            if (flashlightTooltip)
                Destroy(flashlightTooltip);
            nextFLTime = Time.time + 1.0f;
            // Play SFX
            GameObject newAudioSource = new GameObject();
            AudioSource src = newAudioSource.AddComponent<AudioSource>();
            src.clip = flashlightAudio;
            src.pitch = (1 + ((float)Random.Range(-6, 6) / 100.0f));
            src.Play();
            Destroy(newAudioSource, flashlightAudio.length);

            ChangeLightActive();
        }

        if (lightActive)
        {
            UpdateShader();
        }
        
    }

    void ChangeLightActive()
    {
        if (lightActive)
        {
            ResetShader();
        }

        lightActive = !lightActive;
        light.enabled = !light.enabled;
        foreach (var l in otherLights) { l.SetActive(!l.activeSelf); }
    }
    void UpdateShader()
    {
        revealMat.SetVector("_Light_Position", light.transform.position);
        revealMat.SetVector("_Light_Direction", -light.transform.forward);
        revealMat.SetFloat("_Light_Angle", light.spotAngle);
        revealMat.SetFloat("_Range", light.range);
    }

    void ResetShader()
    {
        revealMat.SetVector("_Light_Position", Vector3.zero);
        revealMat.SetVector("_Light_Direction", Vector3.zero);
        revealMat.SetFloat("_Light_Angle", 0);
        revealMat.SetFloat("_Range", 0);
    }
}
