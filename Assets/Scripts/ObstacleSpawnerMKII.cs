using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerMKII : MonoBehaviour
{
    [SerializeField] GameObject[] Obstacles;
    [SerializeField] GameObject[] Foliage;

    public List<GameObject> objects;
    public List<GameObject> foliageObjects;

    [SerializeField] int maxObstacles;
    [SerializeField] int maxFoliageObstacles;

    public float despawningRadius;
    GameObject instance;
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            instance = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], new Vector2(transform.position.x + Random.Range(-30, 30), transform.position.y - Random.Range(10, 30)), Quaternion.identity);
            instance.transform.SetParent(GameObject.Find("MAP").transform);
            instance.GetComponent<despawner>().Source = this.gameObject;
            objects.Add(instance);
        }
    }
    void Update()
    {
        //check if obstacles !>20
        if (objects.Count < maxObstacles)
        {
            instance = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], new Vector2(transform.position.x + Random.Range(-30, 30), transform.position.y - Random.Range(10, 30)), Quaternion.identity);
            instance.transform.SetParent(GameObject.Find("MAP").transform);
            instance.GetComponent<despawner>().Source = this.gameObject;
            objects.Add(instance);
        }

        if (foliageObjects.Count < maxFoliageObstacles)
        {
            instance = Instantiate(Foliage[Random.Range(0, Foliage.Length)], new Vector2(transform.position.x + Random.Range(-30, 30), transform.position.y - Random.Range(10, 30)), Quaternion.identity);
            instance.transform.SetParent(GameObject.Find("MAP").transform);
            instance.GetComponent<despawner>().Source = this.gameObject;
            foliageObjects.Add(instance);
        }

    }

}
