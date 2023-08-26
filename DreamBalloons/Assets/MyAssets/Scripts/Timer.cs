using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public bool runAtEnable;
    public bool runAtStart;
    public float time = 0;
    public UnityEvent endTimerAction;
    public bool destroySelf;
    public bool onlySetActive;
    [HideInInspector]
    public bool isEnd;

    private void OnValidate()
    {
        if (runAtEnable)
            runAtStart = false;
        if (runAtStart)
            runAtEnable = false;
    }

    private void OnEnable()
    {
        if (runAtEnable)
            StartCoroutine(StartTimer(time));
    }

    private void Awake()
    {
        isEnd = true;
    }
    private void Start()
    {
        if(runAtStart)
            StartCoroutine(StartTimer(time));
    }

    public IEnumerator StartTimer(float duration)
    {
        isEnd = false;
        time = duration;
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        time = duration;
        isEnd = true;
        if (endTimerAction != null)
            endTimerAction.Invoke();
        if (destroySelf)
            DestroySelf();
        if (onlySetActive)
            DisableSelf();
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void DisableSelf()
    {
        gameObject.SetActive(false);
    }
}
