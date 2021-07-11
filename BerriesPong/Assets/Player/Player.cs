using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static event System.Action GoalEvent;

    [SerializeField] private PlayerAvatar topAvatarInstance;
    [SerializeField] private PlayerAvatar bottomAvatarInstance;
    [SerializeField] private BoxCollider2D myWallCollider;
    [SerializeField] private SpriteRenderer myWallRenderer;
    [SerializeField] private PlayerSide side = PlayerSide.Bottom;
    [SerializeField] private IntVariable scoreRef;
    [SerializeField] private BestScoreData bestScoreRef;
    [SerializeField] private FloatVariable heightOffsetRef;
    [SerializeField] private FloatVariable widthOffsetRef;

    private PlayerAvatar currAvatar;
    private bool IsBottom => side.Equals(PlayerSide.Bottom);

    private int score;

    private void Start() {
        score = 0;
        scoreRef.value = 0;
        CreatePlayerAvatar();
        SetupWall();
    }
    
    private void CreatePlayerAvatar() {
        if (currAvatar != null) Destroy(currAvatar.gameObject);

        var pos = Vector3.up * ((IsBottom ? -1f : 1f) * (Cam.Instance.TopEdge - heightOffsetRef.value));

        PlayerAvatar avatarInst;
        if (IsBottom) {
            avatarInst = bottomAvatarInstance;
        }
        else {
            avatarInst = topAvatarInstance;
        }

        currAvatar = Instantiate(avatarInst, pos, Quaternion.Euler(0f, 0f, IsBottom ? 0f : 180f));
    }

    private void SetupWall() {
        var isBottom = side.Equals(PlayerSide.Bottom);

        myWallCollider.size = new Vector2(Cam.Instance.Width * 2f - widthOffsetRef.value * 2f, 0.5f);
        myWallCollider.offset = Vector2.up * (myWallCollider.size.y / 2f * (isBottom ? -1f : 1f));
        myWallRenderer.size = new Vector2(Cam.Instance.Width * 2f - widthOffsetRef.value * 2f + 2f/32, 0.5f);
        
        myWallCollider.transform.position = Vector3.up * ((isBottom ? -1f : 1f) * (Cam.Instance.TopEdge - heightOffsetRef.value + myWallCollider.size.y/2f));
    }

    private void OnEnable() {
        Ball.BlowUpEvent += OnBallBlowUp;
    }

    private void OnDisable() {
        Ball.BlowUpEvent -= OnBallBlowUp;
    }

    private void OnBallBlowUp(Collider2D other) {
        if (other != myWallCollider) {
            score++;
            scoreRef.value = score;
            GoalEvent?.Invoke();
        }
    }
}