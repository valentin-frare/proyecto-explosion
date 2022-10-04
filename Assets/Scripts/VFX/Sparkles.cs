using System.Collections;
using UnityEngine;

public class Sparkles : MonoBehaviour 
{
    [SerializeField] private GameObject sparkPrefab;

    private PoolingManager poolingManager;

    private void Awake() 
    {
        poolingManager = new PoolingManager(sparkPrefab, 20);
        poolingManager.Init();
    }

    public void SpawnParticle(ContactPoint[] contactPoints)
    {
        foreach (ContactPoint contactPoint in contactPoints)
        {
            GameObject obj = poolingManager.GetPooledObject(contactPoint.point);

            StartCoroutine(DeleteAfter(obj, 1f));
        }
    }

    private IEnumerator DeleteAfter(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}