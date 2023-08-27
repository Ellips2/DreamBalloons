using UnityEngine;

public class BorderSetter : MonoBehaviour
{
    [SerializeField]
    private Transform borderRigth;
    [SerializeField]
    private Transform borderLeft;
    [SerializeField]
    private Transform borderTop;
    [SerializeField]
    private Transform borderBottom;

    private void Start() 
    {
        borderRigth.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height/2, 0)) + new Vector3(borderRigth.localScale.x/2, 0, 0);
        borderLeft.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2, 0)) + new Vector3(-borderLeft.localScale.x/2, 0, 0);
        borderTop.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0)) + new Vector3(0, borderTop.localScale.y, 0);
        borderBottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, 0, 0)) + new Vector3(0, -borderBottom.localScale.y, 0);
    }
}
