using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScreenBallMovement : MonoBehaviour
{
    private Vector2 _prevVelocity;
    
    public new Rigidbody2D rigidbody2D;
    private float _xSpawnPoint;
    private float _ySpawnPoint;
    public AudioSource hitSound;
    public float[] initialVectorXRange = new float[2];
    public float[] initialVectorYRange = new float[2];
    public float[] anglesToClamp = new float[2];

    private void Start()
    {
        //get random x & y locations for ball to spawn, based on screen dimensions
        _xSpawnPoint = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
            Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        _ySpawnPoint = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y,
            Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        
        gameObject.transform.position = new Vector3(_xSpawnPoint, _ySpawnPoint,0);
        _prevVelocity = new Vector2(Random.Range(initialVectorXRange[0],initialVectorXRange[1]),
            Random.Range(initialVectorYRange[0], initialVectorYRange[1]));
        rigidbody2D.velocity = _prevVelocity.normalized *10;
        _prevVelocity = rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
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
}
