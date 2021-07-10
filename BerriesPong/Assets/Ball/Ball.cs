using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public static event System.Action<Collider2D> BlowUpEvent;

    [Header("Parameters")]
    [SerializeField] private BallConfig defaultConfig;
    [SerializeField] private float maxLeanForce = 1f;

    [Header("References")]
    [SerializeField] private TrailRenderer tail;
    [SerializeField] private SpriteRenderer ballRend;
    [SerializeField] private CircleCollider2D ballCollider;
    [SerializeField] private MaterialVariable paletteMaterialRef;

    private BallConfig currentConfig;
    private float moveSpeed;
    private bool isMoving = false;

    private Rigidbody2D rbody;
    private Vector3 prevVelocity;

    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();

        currentConfig = defaultConfig;
        ResetBallParameters();
    }

    private void ResetBallParameters() {
        moveSpeed = currentConfig.Speed;

        tail.startColor = tail.endColor = currentConfig.TailColor;
        tail.widthMultiplier = currentConfig.Size.ColliderRadius * 2f;

        ballCollider.radius = currentConfig.Size.ColliderRadius;

        ballRend.sprite = currentConfig.Size.BallSprite;
        ballRend.material = paletteMaterialRef.value;
    }

    public void SetConfig(BallConfig config) {
        currentConfig = config;

        ResetBallParameters();
    }

    void FixedUpdate() {
        if (rbody.velocity.sqrMagnitude < moveSpeed * moveSpeed)
            rbody.velocity = rbody.velocity.normalized * moveSpeed;
        prevVelocity = rbody.velocity;
    }

    public void Shoot() {
        var shootDir = Random.insideUnitCircle;

        if (Mathf.Abs(Vector3.Dot(shootDir, Vector3.up)) < 0.8f) {
            shootDir.y = 1 * Mathf.Sign(shootDir.y);
            shootDir.x = 0.2f * Mathf.Sign(shootDir.x);
            shootDir.Normalize();
        }

        rbody.velocity = shootDir * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var v = LosevHelper.GetReflectedDirection(prevVelocity, other.contacts[0].normal, 1f);

        if (other.collider.CompareTag("Player")) {
            var hitPointOnCollider = (Vector3) other.contacts[0].point - other.transform.position +
                                     (Vector3) other.collider.offset;
            var sidenessPercent = Mathf.Abs(hitPointOnCollider.x) / other.collider.bounds.extents.x;
            v.x = sidenessPercent * maxLeanForce * Mathf.Sign(hitPointOnCollider.x);
            v = v.normalized * moveSpeed;
        }

        rbody.velocity = v;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Game Over")) {
            BlowUpEvent?.Invoke(other);

            Destroy(gameObject);
        }
    }
}