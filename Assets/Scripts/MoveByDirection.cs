using UnityEngine;

public class MoveByDirection : MonoBehaviour
{
    [SerializeField] GameObject directionByJoy;
    [SerializeField] float multiplayer = 1;
    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, directionByJoy.transform.rotation, Time.deltaTime * multiplayer);
    }
}
