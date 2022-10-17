using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BloomController : MonoBehaviour
{
    public static BloomController instance;

    private PostProcessVolume volume;
    private Bloom bloom;

    private void Awake() 
    {
        instance = this;
        volume = GetComponent<PostProcessVolume>();
        bloom = volume.profile.GetSetting<Bloom>();
        bloom.intensity.value = 0;
    }

    public void ExplosionBloomOff()
    {
        StartCoroutine(BloomOff());
    }

    public void ExplosionBloomOn()
    {
        StartCoroutine(BloomOn());
    }

    public void ExplosionBloom()
    {
        StartCoroutine(Bloom());
    }

    private IEnumerator BloomOn()
    {
        while (bloom.intensity.value < 11)
        {
            bloom.intensity.value += 10f * Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator BloomOff()
    {
        while (bloom.intensity.value > 0)
        {
            bloom.intensity.value -= 20f * Time.fixedDeltaTime;
            yield return null;
        }
    }

    private IEnumerator Bloom()
    {
        while (bloom.intensity.value < 11)
        {
            bloom.intensity.value += 10f * Time.deltaTime;
            yield return null;
        }
        while (bloom.intensity.value > 0)
        {
            bloom.intensity.value -= 20f * Time.deltaTime;
            yield return null;
        }
    }
}