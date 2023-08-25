using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour
{
    public VoidEventChannelSO deathBalloonChannel;

    private void Update() 
    {
        if (deathBalloonChannel != null && Input.touches.Length > 0) 
        {
            deathBalloonChannel.RaiseEvent();
        }

        
    }
}
