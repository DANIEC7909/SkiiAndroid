using UnityEngine;

public class DirectionByJoy : MonoBehaviour
{
    public Joystick joystick;
    [SerializeField] float value;
    public float saValue;
    void Update()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }//find joystick if = null
        else
        {
            if (Mathf.Abs(joystick.Horizontal) > 0.97)
            {
                saValue = 0;
            }
            else if (Mathf.Abs(joystick.Horizontal) < 0.97)
            {
                saValue = -1;
            }
            value = Vector2.SignedAngle(-transform.up, new Vector2(joystick.Horizontal, saValue));
            //calculate joistick.x to rotation
            transform.Rotate(new Vector3(0, 0, value));
            Debug.Log(Vector2.SignedAngle(-transform.up, new Vector2(joystick.Horizontal, saValue)));
            Debug.Log(joystick.Horizontal);

        }
    }
}
