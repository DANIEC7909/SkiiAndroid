using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstaclespawner : MonoBehaviour
{
    [SerializeField] GameObject[] Obstacles;
    public List<GameObject> objects;
    [SerializeField] int maxObstacles = 40;
    public float despawningRadius = 5;
    Vector3 objPos;
    void Update()
    {
       
        if (objects.Count < maxObstacles)
        {

            float rand = Random.Range(0, 100);
            if (rand == 0 || rand == 55)
            {
                GameObject go = Instantiate(Obstacles[0], transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-15, -26)), Quaternion.identity);
                go.GetComponent<despawner>().Source = this.gameObject;
                objects.Add(go);
            }
            if (rand == 5 || rand == 42)
            {
                GameObject goo = Instantiate(Obstacles[1], transform.position + new Vector3(Random.Range(-10, 10), Random.Range(-15, -26)), Quaternion.identity);
                goo.GetComponent<despawner>().Source = this.gameObject;
                objects.Add(goo);
            }
        }
    }
    Vector3 GenerateNewPos()
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-8, -12));
        return pos;
    }
    
}
