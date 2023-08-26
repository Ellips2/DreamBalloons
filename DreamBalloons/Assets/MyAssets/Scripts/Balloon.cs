using UnityEngine;
using LiderboardSystem;

[RequireComponent(typeof (SpriteRenderer))]
public class Balloon : MonoBehaviour
{
    [SerializeField]
    private Liderboard liderboard;
    [SerializeField]
    private BalloonEventChannelSO balloonChannel;
    [SerializeField]
    private TransformEventChannelSO explosionChannel;
    [SerializeField]
    private ColorEventChannelSO scoreChannel;
    [SerializeField]
    private IntEventChannelSO playerTapChannel;

    private bool mouseOverMe;
    private SpriteRenderer spriteRenderer;
    private Color myColor;
    public Color MyColor => myColor;
    public const int WORTH = 1;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private TrailRenderer trailRenderer;    
    [SerializeField]
    private int health = 1;
    public int Health
    {
        get { return health; }
        set { if (health <= 0) Death(true); else health = value; }
    }

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
        trailRenderer.enabled = true;
        playerTapChannel.OnEventRaised += DecreaseHealth;
    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
        playerTapChannel.OnEventRaised -= DecreaseHealth;
    }

    private void OnMouseEnter()
    {
        mouseOverMe = true;
    }

    private void OnMouseExit()
    {
        mouseOverMe = false;
    }    

    private void DecreaseHealth(int damage)
    {
        if (mouseOverMe)
            Health -= Mathf.Abs(damage);
    }

    public void Death(bool withScore)
    {
        if (withScore)
        {
            scoreChannel?.RaiseEvent(myColor);            
            explosionChannel?.RaiseEvent(transform);
        }    

        balloonChannel?.RaiseEvent(this);
        gameObject.SetActive(false);
    }
}