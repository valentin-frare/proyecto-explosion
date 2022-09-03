using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlants : MonoBehaviour
{
    private PoolingManager poolingManager;
    [SerializeField]
    private GameObject plants;
    [SerializeField]
    private int amount;
    private Transform route;
    [SerializeField]
    private SwipeControl swipeCtrl;
    private Camera cam;
    
    void Awake(){
        cam = Camera.main;
        route = GameObject.FindGameObjectWithTag("Route").transform;
    }
    
    void Start()
    {
        poolingManager = new PoolingManager(plants, amount);
        poolingManager.Init();

        InvokeRepeating("ActivateObject", 1.0f, 3.0f);
    }

    private void ActivateObject(){
        Vector3 position = new Vector3();

        float left = cam.ScreenToWorldPoint(new Vector3(0f, 0f)).x;
        float right = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f)).x;

        float rightRoute = route.position.x + (route.localScale.x/(Mathf.Sign(route.localScale.x) >= 0 ? -2 : 2));
        if (Mathf.Abs(right) > Mathf.Abs(rightRoute))
        {
            right = rightRoute;
        }

        float leftRoute = route.position.x - (route.localScale.x/(Mathf.Sign(route.localScale.x) >= 0 ? -2 : 2));
        if(Mathf.Abs(left) > Mathf.Abs(leftRoute))
        {
            left = leftRoute;
        }

        position = new Vector3(Random.Range(left, right), 0, route.position.z - (route.localScale.z/(Mathf.Sign(route.localScale.z) >= 0 ? 2 : -2)));
        StartCoroutine(DeleteAfter(20f, poolingManager.GetPooledObject(position)));
    }

    private float NotSoRandom(float left, float right){
        float steer = swipeCtrl.Steering;

        if(left > right){
            float num = left;
            right = left;
            left = num;
        }

        if(steer == 0)
        {
            return Random.Range(left, right);
        }
        else if (steer > 0)
        {
            return Random.Range(left+((right-left)*0.25f), right);
        }
        else
        {
            return Random.Range(left, right-((right-left)*0.25f));
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
