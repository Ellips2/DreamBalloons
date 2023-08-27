using UnityEngine;
using LiderboardSystem;
using TMPro;

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

    private Transform canvas;
    private bool mouseOverMe;
    private SpriteRenderer spriteRenderer;
    private Color myColor;
    public Color MyColor => myColor;
    public const int WORTH = 1;
    [SerializeField]
    private TextMeshProUGUI myDesireText;

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private TrailRenderer trailRenderer;    
    [SerializeField]
    private int health = 1;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public void Init(Color colorValue, string newDesireText, Transform headDesireText)
    {
        myColor = colorValue;
        myDesireText.text = newDesireText;
        spriteRenderer.color = myColor;
        canvas = headDesireText;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() 
    {
        health = 1;
        trailRenderer.enabled = true;
        playerTapChannel.OnEventRaised += DecreaseHealth;
    }

    private void OnDisable()
    {
        trailRenderer.enabled = false;
        playerTapChannel.OnEventRaised -= DecreaseHealth;
        mouseOverMe = false;
    }

    private void OnMouseEnter()
    {
        mouseOverMe = true;
    }

    private void OnMouseExit()
    {
        mouseOverMe = false;
    }    

    private void OnMouseDown() 
    {
        //DecreaseHealth(1);
    }

    private void DecreaseHealth(int damage)
    {
        if (mouseOverMe)
            health -= Mathf.Abs(damage);
        if(health <= 0)
        {
            Death(true);
        }
    }

    public void Death(bool withScore)
    {
        if (withScore)
        {
            scoreChannel?.RaiseEvent(myColor);
            Instantiate(myDesireText, Input.mousePosition, Quaternion.identity, canvas);
        }
        balloonChannel?.RaiseEvent(this);

        gameObject.SetActive(false);
    }
}
