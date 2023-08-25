using UnityEngine;
using LiderboardSystem;

[RequireComponent(typeof (SpriteRenderer))]
public class Balloon : MonoBehaviour
{
    public Liderboard liderboard;
    public BalloonEventChannelSO deathChannel;
    public ColorEventChannelSO scoreChannel;
    public IntEventChannelSO playerTapChannel;
    private bool mouseOverMe;

    public const int WORTH = 1;
    [HideInInspector]
    private Color myColor;
    public Color MyColor => myColor;

    [SerializeField]
    private Rigidbody2D myRig;
    [SerializeField]
    private float force = 1f;
    [SerializeField]
    private float maxSpeed = 10;
    [SerializeField]
    private float amplitude = 0.25f; //????????? ?????????? ?? ????????? ??????????
    [SerializeField]
    private float frequency = 0.1f; //??????? ?????????
    [SerializeField]
    private Vector2 direction = new Vector2(0, 1);
    private bool useSin;
    [SerializeField]
    private GameObject explosion;
    private SpriteRenderer spriteRenderer;

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
        playerTapChannel.OnEventRaised += DecreaseHealth;
        direction = direction*force;
        if (Random.Range(0, 2) == 0)
        {
            useSin = true;
        }
        else
        {
            useSin = false;
        }
    }

    private void OnDisable()
    {
        playerTapChannel.OnEventRaised -= DecreaseHealth;
    }

    private void FixedUpdate() 
    {
        if (myRig.velocity.magnitude < maxSpeed)
        {
            direction = new Vector2(GetOffset(transform.position.y, useSin), direction.y).normalized;
            myRig.AddForce(direction*force);
        }
        
    }

    public float GetOffset(float y, bool useSin)
    {
        if (useSin)
            return Mathf.Sin(frequency * y) * amplitude;
        else
            return Mathf.Cos(frequency * y) * amplitude;
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
        if(withScore)
        {
            if (scoreChannel != null)
            {
                scoreChannel.RaiseEvent(myColor);
            }
        }     
        else
        {
            if (deathChannel != null)
            {
                deathChannel.RaiseEvent(this);
            }
        }   
        // explosion.transform.parent = transform.parent;
        // explosion.SetActive(true);
        gameObject.SetActive(false);
    }
}
