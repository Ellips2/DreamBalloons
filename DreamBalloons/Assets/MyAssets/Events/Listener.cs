using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Listener : MonoBehaviour
{
    public VoidEventChannelSO deathChannel;

    private void Awake() 
    {
        deathChannel.OnEventRaised += UpdateScores;
    }

    private void OnDisable() 
    {
        deathChannel.OnEventRaised -= UpdateScores;
    }

    private void UpdateScores(){

    }
}
