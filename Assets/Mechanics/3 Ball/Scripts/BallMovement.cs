using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    private Vector2 _prevVelocity = Vector2.down;

    void Start()
    {
        rigidbody2D.velocity = _prevVelocity.normalized *100 * Time.deltaTime;
        _prevVelocity = rigidbody2D.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        Vector2 contactNormal = contact.normal;
        FixCollisionVelocity(contactNormal);
        
        rigidbody2D.velocity = Vector2.Reflect(_prevVelocity, contactNormal);
        _prevVelocity = rigidbody2D.velocity;
    }

    private void FixCollisionVelocity(Vector2 contactNormal)
    {
        float angleDeg = Vector2.SignedAngle(contactNormal, -_prevVelocity);
        float absDeg = Mathf.Abs(angleDeg);

        float clampedDeg = Mathf.Clamp(absDeg, 15, 70);

        if (Mathf.Approximately(absDeg, clampedDeg)) return;

        float signedRad = clampedDeg * Mathf.Sign(angleDeg) * Mathf.Deg2Rad;
        float normalRad = Mathf.Atan2(contactNormal.y, contactNormal.x);
        float newRad = normalRad + signedRad;

        Vector2 newDir = new Vector2(Mathf.Cos(newRad), Mathf.Sin(newRad));
        float magnitude = _prevVelocity.magnitude;
        _prevVelocity = newDir * -magnitude;
    }
}
