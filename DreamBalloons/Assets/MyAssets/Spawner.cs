using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float period;
    public GameObject spawnObj;
    public BoxCollider2D spawnZone;
    public Transform headOfObjs;
    public List<Color> spriteRendererColors = new List<Color>();
    private GameObject createdBalloon;

    public bool useRandScale;    
    [Range(0.1f, 2.0f)]
    public float minSize = 0.0f;
    [Range(0.1f, 2.0f)]
    public float maxSize = 1.1f;

    private void OnValidate() 
    {
        if (maxSize <= minSize)
        {
            maxSize = minSize+0.1f;
        }
    }
    
    private void Start() 
    {
        if (spawnObj && spawnZone && headOfObjs)
            StartCoroutine(Spawn());
        else
            Debug.LogError("Null Reference exception");
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            if (useRandScale)
            {
                float x = Random.Range(minSize, maxSize);
                Vector2 newSize = new Vector2(x, x);
                spawnObj.transform.localScale = newSize;
                createdBalloon = Instantiate(spawnObj, GetRandPosInZone(spawnZone.bounds), new Quaternion(0,0,0,0), headOfObjs);
            }
            else
                createdBalloon = Instantiate(spawnObj, GetRandPosInZone(spawnZone.bounds), new Quaternion(0, 0, 0, 0), headOfObjs);

            if(spriteRendererColors.Count > 0)
                createdBalloon.GetComponent<SpriteRenderer>().color = spriteRendererColors[Random.Range(0, spriteRendererColors.Count)];

            yield return new WaitForSeconds(period);
        }        
    }

    private Vector2 GetRandPosInZone(Bounds bounds)
    {        
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), 
                           Random.Range(bounds.min.y, bounds.max.y));
    }
}
