using UnityEngine;
using System.Collections;

public class ChangeObjTransform : MonoBehaviour
{
    public Transform origTransform;
    public Transform newTranform;
    public Vector3 newScale;
    public bool reverse;
    public bool startScalingAtStart;
    
    private Vector2 tempPos;
    private Vector2 origScale;
    private float stepPerSecond;
    private float t;

    private void Start() 
    {
        if(origTransform != null)
            origScale = origTransform.transform.localScale;
        
        //if(startScalingAtStart)

    }

    public void MoveAtPoint()
    {
        if(reverse)
        {
            tempPos = origTransform.position;
            origTransform.transform.position = newTranform.position;
            newTranform.transform.position = tempPos;
        }
        else{
            origTransform.transform.position = newTranform.position;
        }
    }

    public void RotateToPoint()
    {
        origTransform.transform.rotation = newTranform.rotation;
    }

    public void ScaleToNewScale()
    {
        if(reverse)
        {
            origTransform.transform.localScale = origScale;
        }
        else
            origTransform.transform.localScale = newScale;
    }    

    public IEnumerator ScaleToNewScaleLerp(float timeScaling)
    {
        t = 0;
        origScale = origTransform.localScale;
        stepPerSecond = 1 / timeScaling;
        while (t < 1)
        {
            t += Time.deltaTime * stepPerSecond;
            origTransform.localScale = new Vector2(Mathf.Lerp(origScale.x, newScale.x, t), Mathf.Lerp(origScale.y, newScale.y, t));
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator ScaleToNewScaleLerp(Transform scalingObj, Vector2 newScaleObj, float timeScaling)
    {
        t = 0;
        origScale = scalingObj.localScale;
        stepPerSecond = 1 / timeScaling;
        while (t < 1)
        {
            t += Time.deltaTime * stepPerSecond;
            scalingObj.localScale = new Vector2(Mathf.Lerp(origScale.x, newScaleObj.x, t), Mathf.Lerp(origScale.y, newScaleObj.y, t));
            yield return new WaitForEndOfFrame();
        }
    }
}
