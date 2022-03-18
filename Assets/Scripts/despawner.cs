using UnityEngine;

public class despawner : MonoBehaviour
{
    public GameObject Source;
    public float distance = 40;
    PlayerControlerMKII PlayerControlerMKII;
    float nearMissDistance;
    Collider2D[] col;
    [SerializeField] bool nearMissOnce = true;
    private void Start()
    {
        PlayerControlerMKII = Source.GetComponent<PlayerControlerMKII>();
        nearMissDistance = PlayerControlerMKII.NearMissDistance;
        col = Physics2D.OverlapCircleAll(transform.position, Source.GetComponent<ObstacleSpawnerMKII>().despawningRadius);
        if (gameObject.tag == "tree" || gameObject.tag == "rock" || gameObject.tag == "ramp")
        {
            if (col.Length > 1)
            {
                Destroy(gameObject);
                Source.GetComponent<ObstacleSpawnerMKII>().objects.Remove(this.gameObject);
            }
        }
    }
    void Update()
    {
        if (nearMissOnce)
        {
            if (Vector2.Distance(transform.position, Source.transform.position) < nearMissDistance)
            {
                switch (tag)
                {
                    case "rock":
                        PlayerControlerMKII.deliverPoints(2, 2);
                        break;
                    case "tree":
                        PlayerControlerMKII.deliverPoints(2, 1);
                        break;
                }
                nearMissOnce = false;
            }
        }

        if (Vector2.Distance(transform.position, Source.transform.position) > distance)
        {
            if (Source.GetComponent<ObstacleSpawnerMKII>().objects.Contains(this.gameObject))
            {
                Source.GetComponent<ObstacleSpawnerMKII>().objects.Remove(this.gameObject);
            }
            else
            {
                Source.GetComponent<ObstacleSpawnerMKII>().foliageObjects.Remove(this.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
