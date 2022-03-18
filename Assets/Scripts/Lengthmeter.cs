using TMPro;
using UnityEngine;
public class Lengthmeter : MonoBehaviour
{
    GameObject playerObj;
    PlayerControlerMKII player;
    [SerializeField] TextMeshProUGUI ui;
    [SerializeField] TextMeshProUGUI ui_Points;
    Vector3 LastAccidentPos;
    void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerControlerMKII>();
    }
    private void Update()
    {
        if (player.canMove)
        {

        }
        else
        {
            LastAccidentPos = playerObj.transform.position;
        }
        ui.text = Mathf.FloorToInt(Mathf.Abs(Vector3.Distance(LastAccidentPos, playerObj.transform.position))) + "m";
        ui_Points.text = player.points.ToString();
    }
}
