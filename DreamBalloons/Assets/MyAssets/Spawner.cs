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
    public Transform balloonHead;
    public Transform explosionHead;
    public List<Color> spriteRendererColors = new List<Color>();
    private Pool<Balloon> balloonPool;
    private Pool<Explosion> explosionPool;

    [SerializeField]
    private BalloonEventChannelSO deathChannel;
    [SerializeField]
    private ExplosionEventChannelSO explosionChannel;
    [SerializeField]
    private TransformEventChannelSO explosionTransformChannel;

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
        balloonPool = new Pool<Balloon>(instBalloon, balloonHead);
        explosionPool = new Pool<Explosion>(instExplosion, explosionHead);

        deathChannel.OnEventRaised += RefreshNonActiveBalloonPool;
        explosionChannel.OnEventRaised += RefreshNonActiveExplosionPool;
        explosionTransformChannel.OnEventRaised += CreateExplosion;
    }

    private void OnDisable()
    {
        deathChannel.OnEventRaised -= RefreshNonActiveBalloonPool;
        explosionChannel.OnEventRaised -= RefreshNonActiveExplosionPool;
        explosionTransformChannel.OnEventRaised -= CreateExplosion;
    }

    private void Start() 
    {
        if (instBalloon && spawnZone && balloonHead && instExplosion && explosionHead)
            StartCoroutine(Spawn());
        else
            Debug.LogError("Null Reference exception");
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            CreateBalloon();

            yield return new WaitForSeconds(period);
        }        
    }

    private void RefreshNonActiveBalloonPool(Balloon balloon)
    {
        balloonPool.DisbaleItem(balloon);
    }

    private void RefreshNonActiveExplosionPool(Explosion explosion)
    {
        explosionPool.DisbaleItem(explosion);
    }

    private Vector2 GetRandPosInZone(Bounds bounds)
    {        
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), 
                           Random.Range(bounds.min.y, bounds.max.y));
    }

    private void CreateBalloon()
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
    }

    private void CreateExplosion(Transform deathTransform)
    {
        Explosion newExplosion = explosionPool.GetItem();        
        newExplosion.transform.position = deathTransform.position;
    }
}
