using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float period;
    public Balloon instBalloon;
    public Explosion instExplosion;
    public BoxCollider2D spawnZone;
    public Transform headOfObjs;
    public List<Color> spriteRendererColors = new List<Color>();
    //private List<Balloon> poolObjectsActive = new List<Balloon>();
    //private List<Balloon> poolObjectsNonActive = new List<Balloon>();
    private Pool<Balloon> balloonPool;
    private Pool<Explosion> explosionPool;

    [SerializeField]
    private BalloonEventChannelSO deathChannel;

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

    private void Awake()
    {
        balloonPool = new Pool<Balloon>(instBalloon, headOfObjs);
        explosionPool = new Pool<Explosion>(instExplosion, headOfObjs);
        deathChannel.OnEventRaised += UpdateNonActivePoolObj;
    }
    private void OnDisable()
    {
        deathChannel.OnEventRaised -= UpdateNonActivePoolObj;
    }

    private void Start() 
    {
        if (instBalloon && spawnZone && headOfObjs && instExplosion)
            StartCoroutine(Spawn());
        else
            Debug.LogError("Null Reference exception");
    }

    private IEnumerator Spawn()
    {
        while(true)
        { 
            Balloon newBallon = balloonPool.GetItem();
            
            newBallon.transform.position = GetRandPosInZone(spawnZone.bounds);

            if (spriteRendererColors.Count > 0)
            {
                Color newColor = spriteRendererColors[Random.Range(0, spriteRendererColors.Count)];
                newBallon.Init(newColor);
            }

            float x = Random.Range(minSize, maxSize);
            Vector2 newSize = new Vector2(x, x);
            newBallon.transform.localScale = newSize;

            // if (poolObjectsNonActive.Count == 0)
            // {
            //     poolObjectsActive.Add(CreateBalloon());
            // }
            // else
            // {
            //     poolObjectsActive.Add(poolObjectsNonActive[0]);
            //     poolObjectsNonActive[0].gameObject.SetActive(true);
            //     poolObjectsNonActive[0].gameObject.transform.position = GetRandPosInZone(spawnZone.bounds);
            //     poolObjectsNonActive.RemoveAt(0);
            // }

            yield return new WaitForSeconds(period);
        }        
    }

    // private Balloon CreateBalloon()
    // {
    //     Balloon createdBalloon;

    //     if (useRandScale)
    //     {
    //         float x = Random.Range(minSize, maxSize);
    //         Vector2 newSize = new Vector2(x, x);
    //         spawnObj.transform.localScale = newSize;
    //         createdBalloon = Instantiate(spawnObj, GetRandPosInZone(spawnZone.bounds), Quaternion.identity, headOfObjs);
    //     }
    //     else
    //         createdBalloon = Instantiate(spawnObj, GetRandPosInZone(spawnZone.bounds), Quaternion.identity, headOfObjs);

    //     if (spriteRendererColors.Count > 0)
    //     {
    //         Color newColor = spriteRendererColors[Random.Range(0, spriteRendererColors.Count)];
    //         createdBalloon.Init(newColor);
    //     }
    //     return createdBalloon;
    // }

    private void UpdateNonActivePoolObj(Balloon balloon)
    {
        balloonPool.DisbaleItem(balloon);
    }    

    private Vector2 GetRandPosInZone(Bounds bounds)
    {        
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), 
                           Random.Range(bounds.min.y, bounds.max.y));
    }
}
