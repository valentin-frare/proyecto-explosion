using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    [SerializeField] private GameObject plants;
    [SerializeField] private int amount;
    [SerializeField] private SwipeControl swipeCtrl;

    private PoolingManager poolingManager;
    private Camera cam;
    private Transform route;
    private Transform anyRoute;
    private Transform player;
    
    private void Awake()
    {
        cam = Camera.main;
        
        poolingManager = new PoolingManager(plants, amount);
        poolingManager.Init();
    
        GameEvents.OnPlayerSpawn += OnPlayerSpawn;
    }

    private void OnPlayerSpawn(GameObject player)
    {
        route = GameObject.FindGameObjectWithTag("Route").transform;        
        anyRoute = GameObject.FindGameObjectWithTag("Road").transform;
        this.player = player.transform;
        InvokeRepeating("ActivateObject", 1.0f, 3.0f);
    }

    private void ActivateObject()
    {
        Vector3 position = new Vector3();

        float left = cam.ScreenToWorldPoint(new Vector3(0f + cam.pixelWidth*0.1f, 0f)).x;
        float right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - cam.pixelWidth*0.1f, 0f)).x;

        float rightRoute = anyRoute.position.x + (anyRoute.localScale.x/(Mathf.Sign(anyRoute.localScale.x) >= 0 ? -2 : 2) - anyRoute.localScale.x*0.1f);
        if (right < rightRoute)
        {
            right = rightRoute;
        }

        float leftRoute = anyRoute.position.x - (anyRoute.localScale.x/(Mathf.Sign(anyRoute.localScale.x) >= 0 ? -2 : 2) - anyRoute.localScale.x*0.1f);
        if(left > leftRoute)
        {
            left = leftRoute;
        }

        //Debug.Log(route.position.z - (route.localScale.z/(Mathf.Sign(route.localScale.z) >= 0 ? 2 : -2)) - player.transform.position.z);

        position = new Vector3(NotSoRandom(left, right), 0, player.transform.position.z - 70);
        StartCoroutine(DeleteAfter(20f, poolingManager.GetPooledObject(position)));
    }

    private float NotSoRandom(float left, float right)
    {
        float steer = swipeCtrl.Steering;

        if(steer == 0)
        {
            return Random.Range(left, right);
        }
        else if (steer > 0)
        {
            return Random.Range(left, right+((left-right)*0.25f));
        }
        else
        {
            return Random.Range(left-((left-right)*0.25f), right);
        }
    }

    public IEnumerator DeleteAfter(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(false);
        }
        obj.SetActive(false);
    }
}
