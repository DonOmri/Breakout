using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class InGameBallMovement : MonoBehaviour
{
    public GameObject paddle;
    public new Rigidbody2D rigidbody2D;
    public AudioSource hitSound;
    public InGameManager gameManager;
    public bool bounceBack = true;
    private const float TIME_TO_DELAY_COLLIDER = 0.1f;
    [SerializeField] private float[] initialVectorXRange = new float[2];
    [SerializeField] private float[] initialVectorYRange = new float[2];
    [SerializeField] private float[] anglesToClamp = new float[2];
    private const int BALL_SIZE_ADJUSTER = 15;
    private readonly Vector3 _shiftPosition = new Vector3(0, 0.15f, 0);
    private Vector2 _prevVelocity;
    private float _bricksCountMultiplier = 1;
    private float _ballSizeMultiplier;
    private float _ballSpeedMultiplier;

    private void Start()
    {
        Transform trans = gameObject.transform;
        _ballSizeMultiplier = gameManager.GetSettings("Ball Size");
        _ballSpeedMultiplier = gameManager.GetSettings("Ball Speed") / 2;
        
        Vector3 ballSize = trans.localScale;
        ballSize.x += _ballSizeMultiplier / BALL_SIZE_ADJUSTER;
        ballSize.y += _ballSizeMultiplier / BALL_SIZE_ADJUSTER;
        trans.localScale = ballSize;
        
        RestartBall();
    }

    
    private void Update()
    {
        if (rigidbody2D.simulated) return;
        FollowPaddle();
    }

    private void FollowPaddle()
    {
        gameObject.transform.position = paddle.GetComponent<Transform>().position + _shiftPosition;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RestoreCollider());
            rigidbody2D.simulated = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!bounceBack && other.gameObject.name == "Paddle") //when all bricks were broken
        {
            rigidbody2D.simulated = false;
            return;
        }
        
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        
        FixCollisionVelocity(contactNormal);
        
        hitSound.Play();
        rigidbody2D.velocity = Vector2.Reflect(_prevVelocity, contactNormal);
        _prevVelocity = rigidbody2D.velocity;
    }

    private void FixCollisionVelocity(Vector2 contactNormal)
    {
        float angleDeg = Vector2.SignedAngle(contactNormal, -_prevVelocity);
        float absDeg = Mathf.Abs(angleDeg);
        float clampedDeg = Mathf.Clamp(absDeg, anglesToClamp[0], anglesToClamp[1]);

        if (Mathf.Approximately(absDeg, clampedDeg)) return;

        float signedRad = clampedDeg * Mathf.Sign(angleDeg) * Mathf.Deg2Rad;
        float normalRad = Mathf.Atan2(contactNormal.y, contactNormal.x);
        float newRad = normalRad + signedRad;

        Vector2 newDir = new Vector2(Mathf.Cos(newRad), Mathf.Sin(newRad));
        float magnitude = _prevVelocity.magnitude;
        _prevVelocity = newDir * -magnitude;
    }

    public void RestartBall()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        rigidbody2D.simulated = false;
        _prevVelocity = new Vector2(Random.Range(initialVectorXRange[0],initialVectorXRange[1]),
            Random.Range(initialVectorYRange[0], initialVectorYRange[1]));
        rigidbody2D.velocity = (5 + _ballSpeedMultiplier) * _prevVelocity.normalized * _bricksCountMultiplier;
        _prevVelocity = rigidbody2D.velocity;
    }

    private IEnumerator RestoreCollider()
    {
        yield return new WaitForSeconds(TIME_TO_DELAY_COLLIDER);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    public void IncreaseBallSpeedMultiplier(float amount)
    {
        _bricksCountMultiplier *= amount;
        _prevVelocity *= _bricksCountMultiplier;
    }
}
