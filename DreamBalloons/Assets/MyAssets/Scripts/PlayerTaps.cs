using UnityEngine;

public class PlayerTaps : MonoBehaviour
{
    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { if (value <= 0) damage = 1; else damage = value; }
    }    

    private void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if (hit && hit.collider.TryGetComponent(out Balloon balloon))
                {
                    balloon.DecreaseHealth(damage);
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit && hit.collider.TryGetComponent(out Balloon balloon))
            {
                balloon.DecreaseHealth(damage);
            }
        }
    }
}
