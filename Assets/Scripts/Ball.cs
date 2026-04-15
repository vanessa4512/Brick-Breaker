using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }

    public float speed = 30f;

    public float maxBounceAngle = 75f;

    private void Awake() {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory() {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    private void CollisionEnter2D(Collision2D collision) {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoin =  collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoin.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset /  width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp (currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);
            this.rigidbody.AddForce(Vector2.up * this.speed * newAngle);
        }
    }
}
