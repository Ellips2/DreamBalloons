using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class Balloon : MonoBehaviour
{
    public const int WORTH = 1;
    [SerializeField]
    private BalloonEventChannelSO balloonChannel;
    [SerializeField]
    private ColorEventChannelSO scoreChannel;
    [SerializeField]
    private TrailRenderer trailRenderer;    
    private SpriteRenderer spriteRenderer;
    private Color myColor;
    public Color MyColor => myColor;
    private int health = 1;
    public int Health { get { return health; } set { health = value; }}

    public void Init(Color colorValue)
    {
        myColor = colorValue;
        spriteRenderer.color = myColor;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        health = 1;
    }

    private void OnDisable()
    {
        trailRenderer.Clear();
    }

    public void DecreaseHealth(int damage)
    {
        health -= Mathf.Abs(damage);
        
        if (health <= 0)
        {
            Death(true);
        }
    }

    public void Death(bool withScore)
    {
        if (withScore)
        {
            scoreChannel?.RaiseEvent(myColor);
        }
        balloonChannel?.RaiseEvent(this);

        gameObject.SetActive(false);
    }
}
