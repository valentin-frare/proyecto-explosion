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
    [SerializeField]
    private Transform truck;
    private Camera cam;
    
    void Awake(){
        cam = Camera.main;
    }
    
    void Start()
    {
        poolingManager = new PoolingManager(plants, amount);
        poolingManager.Init();

        InvokeRepeating("ActivateObject", 1.0f, 3.0f);
    }

    private void ActivateObject(){
        Vector3 posicion = new Vector3();

        float izquierda = cam.ScreenToWorldPoint(new Vector3(0f, 0f)).x;
        float derecha = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0f)).x;
        
        posicion = new Vector3(Random.Range(izquierda, derecha), 0, truck.position.z - 100);

        StartCoroutine(DeleteAfter(20f, poolingManager.GetPooledObject(posicion)));
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
