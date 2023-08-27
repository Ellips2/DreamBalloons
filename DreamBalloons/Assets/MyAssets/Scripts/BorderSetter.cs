using UnityEngine;

public class BorderSetter : MonoBehaviour
{
    [SerializeField]
    private Transform borderRight;
    [SerializeField]
    private Transform borderLeft;
    [SerializeField]
    private Transform borderTop;
    [SerializeField]
    private Transform borderBottom;

    private void Start() 
    {
        Vector3 posRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
        Vector3 posLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / 2, 0));
        Vector3 posTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        Vector3 posBottom = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));

        Vector3 offsetRight = new Vector3(borderRight.localScale.x / 2, 0, 0);
        Vector3 offsetLeft = new Vector3(-borderLeft.localScale.x / 2, 0, 0);
        Vector3 offsetTop = new Vector3(0, borderTop.localScale.y, 0);
        Vector3 offsetBottom = new Vector3(0, -borderBottom.localScale.y, 0);
        
        borderRight.transform.position = posRight + offsetRight;
        borderLeft.transform.position = posLeft + offsetLeft;
        borderTop.transform.position = posTop + offsetTop;
        borderBottom.transform.position = posBottom + offsetBottom;
    }
}
