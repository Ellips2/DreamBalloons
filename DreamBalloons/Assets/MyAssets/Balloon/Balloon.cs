using UnityEngine;
using LiderboardSystem;
using System;

public class Balloon : MonoBehaviour
{
    public Liderboard liderboard;
    public VoidEventChannelSO deathChannel;
    public IntEventChannelSO playerTapChannel;
    private bool mouseOverMe;
    private const int WORTH = 1;

    public Rigidbody2D myRig;
    public float force = 1f;
    public float maxSpeed = 10;
    public float amplitude = 0.25f; //????????? ?????????? ?? ????????? ??????????
    public float frequency = 0.1f; //??????? ?????????
    private Vector2 direction = new Vector2(0, 1);

    private int health = 1;
    public int Health
    {
        get { return health; }
        set { if (health <= 0) Death(); else health = value; }
    }


    private void Awake()
    {
        playerTapChannel.OnEventRaised += DecreaseHealth;
        direction = direction*force;
    }

    private void OnDisable()
    {
        playerTapChannel.OnEventRaised -= DecreaseHealth;
    }

    private void FixedUpdate() 
    {
        if (myRig.velocity.magnitude < maxSpeed)
        {
            direction = new Vector2(GetSinOffset(transform.position.y), direction.y).normalized;
            myRig.AddForce(direction*force);
        }
        
    }

    public float GetSinOffset(float y)
    {
        return Mathf.Sin(frequency * y) * amplitude;
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

    private void Death()
    {
        if (deathChannel != null)
        {
            deathChannel.RaiseEvent();
        }
        liderboard.AddResult("Fedor", WORTH);
        Destroy(gameObject);
    }
}
