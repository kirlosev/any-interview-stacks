using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Size", menuName = "L/Ball/Ball Size", order = 0)]
public class BallSize : ScriptableObject {
    [SerializeField] private Sprite ballSprite;
    public Sprite BallSprite => ballSprite;
    
    [SerializeField] private float colliderRadius = 1f;
    public float ColliderRadius => colliderRadius;
}
