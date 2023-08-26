using UnityEngine;
using UnityEngine.Events;

public class EnableOrDisableActions : MonoBehaviour
{
    public UnityEvent actionEnable;
    public UnityEvent actionDisable; 
    
    private void OnEnable() 
    {
        actionEnable.Invoke();
    }

    private void OnDisable() 
    {
        actionDisable.Invoke();
    }
}
