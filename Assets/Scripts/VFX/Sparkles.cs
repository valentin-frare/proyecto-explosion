using System.Collections;
using UnityEngine;

public class Sparkles : MonoBehaviour 
{
    [SerializeField] private GameObject sparkPrefab;

    private PoolingManager poolingManager;

    private void Awake() 
    {
        var container = GameObject.FindGameObjectWithTag("LvlContainer").transform;
        poolingManager = new PoolingManager(sparkPrefab, 20, container);
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