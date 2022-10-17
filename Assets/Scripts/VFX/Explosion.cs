using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Fireball")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private float fireballScaleTime;
    [SerializeField] private LeanTweenType fireballEase;
    [SerializeField] private float fireballFadeTime;
    [SerializeField] private float fireballScaleTarget;

    [Header("SmokeRing")]
    [SerializeField] private GameObject smokeRing;
    [SerializeField] private float smokeRingFadeTime;
    [SerializeField] private float smokeRingScaleTime;
    [SerializeField] private LeanTweenType smokeRingEase;
    [SerializeField] private float smokeScaleTarget;

    [Header("Particles")]
    [SerializeField] private ParticleSystem particles;

    private void Start() 
    {
        ExplosionF();
    }

    void ExplosionF()
    {
        CameraInit.instance?.Shake();
        Vibration.Vibrate(100);

        particles.Play();
        fireball.transform.localScale = new Vector3(0, 0, 0);
        smokeRing.transform.localScale = new Vector3(0, 0, 0);
        for (var i = 0; i < fireball.transform.childCount; i++)
        {
            LeanTween.alpha(fireball.transform.GetChild(i).gameObject, 1, 0);
        }
        LeanTween.alpha(smokeRing, 1, 0);

        BloomController.instance?.ExplosionBloom();
        LeanTween.scale(fireball, new Vector3(fireballScaleTarget, fireballScaleTarget, fireballScaleTarget), fireballScaleTime).setEase(fireballEase).setOnComplete(() => {
            for (var i = 0; i < fireball.transform.childCount; i++)
            {
                LeanTween.alpha(fireball.transform.GetChild(i).gameObject, 0, fireballFadeTime);
            }
        });

        LeanTween.scale(smokeRing, new Vector3(smokeScaleTarget, smokeScaleTarget, smokeScaleTarget), smokeRingScaleTime).setEase(smokeRingEase);
        LeanTween.alpha(smokeRing, 0, smokeRingFadeTime).setOnComplete(() => {
            particles.gameObject.transform.parent = null;
            //BloomController.instance?.ExplosionBloomOff();
            Destroy(gameObject);
        });
    }
}
