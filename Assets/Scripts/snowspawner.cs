using UnityEngine;

public class snowspawner : MonoBehaviour
{
    [SerializeField] Transform firstSnow;
    [SerializeField] GameObject snow;
    [SerializeField] float distnace = 10;

    void Start()
    {

    }

    void Update()
    {
        //list here
        if (Vector2.Distance(transform.position, firstSnow.position) > distnace)
        {
            //destroy object in list 
            firstSnow = Instantiate(snow, transform.position, Quaternion.identity).transform;
            firstSnow.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            firstSnow.GetComponent<despawner>().Source = this.gameObject;
            Instantiate(snow, transform.position + new Vector3(0, -13.84f), Quaternion.identity).GetComponent<despawner>().Source = this.gameObject;
            Instantiate(snow, transform.position + new Vector3(-13.84f, -13.84f), Quaternion.identity).GetComponent<despawner>().Source = this.gameObject;
            Instantiate(snow, transform.position + new Vector3(13.84f, -13.84f), Quaternion.identity).GetComponent<despawner>().Source = this.gameObject;
            Instantiate(snow, transform.position + new Vector3(-13.84f, 0), Quaternion.identity).GetComponent<despawner>().Source = this.gameObject;
            Instantiate(snow, transform.position + new Vector3(13.84f, 0), Quaternion.identity).GetComponent<despawner>().Source = this.gameObject;
        }
    }
}
