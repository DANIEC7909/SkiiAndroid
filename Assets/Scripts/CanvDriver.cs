using UnityEngine;

public class CanvDriver : MonoBehaviour
{
    [SerializeField] Animator playanim;
    [SerializeField] Animator playanimt;
    public void StartAnim()
    {
        playanim.SetTrigger("start");
        playanimt.SetTrigger("start");
    }
}
