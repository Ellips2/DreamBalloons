using UnityEngine;

public class PlayerTaps : MonoBehaviour
{
    [SerializeField]
    private IntEventChannelSO tapChannel;
    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { if (value <= 0) damage = 1; else damage = value; }
    }    

    private void Update()
    {
        if (tapChannel != null && (Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0))
        {
            tapChannel.RaiseEvent(damage);
        }
    }
}
