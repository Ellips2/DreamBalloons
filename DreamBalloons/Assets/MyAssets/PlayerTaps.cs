using UnityEngine;

public class PlayerTaps : MonoBehaviour
{
    public IntEventChannelSO tapChannel;

    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { if (value <= 0) damage = 1; else damage = value; }
    }    

    private void Update()
    {
        if (tapChannel != null && Input.GetMouseButton(0))
        {
            tapChannel.RaiseEvent(damage);
        }
    }
}
