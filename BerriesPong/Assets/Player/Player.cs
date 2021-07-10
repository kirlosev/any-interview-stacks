using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static event System.Action GoalEvent;

    [SerializeField] private PlayerAvatar topAvatarInstance;
    [SerializeField] private PlayerAvatar bottomAvatarInstance;
    [SerializeField] private BoxCollider2D myWall;
    [SerializeField] private PlayerSide side = PlayerSide.Bottom;
    [SerializeField] private IntVariable scoreRef;
    [SerializeField] private BestScoreData bestScoreRef;

    private PlayerAvatar currAvatar;
    private bool IsBottom => side.Equals(PlayerSide.Bottom);

    private int score;

    private void Start() {
        score = 0;
        scoreRef.value = 0;
        CreatePlayerAvatar();
        SetupWall();
    }

    private void SetupWall() {
        myWall.size = new Vector2(Cam.Instance.Width * 2f + 6f, 1f);
        var isBottom = side.Equals(PlayerSide.Bottom);
        myWall.transform.position = Vector3.up * ((isBottom ? -1f : 1f) * (Cam.Instance.TopEdge + 1.5f));
    }

    void CreatePlayerAvatar() {
        if (currAvatar != null) Destroy(currAvatar.gameObject);

        var pos = Vector3.up * (Cam.Instance.TopEdge * (IsBottom ? -1f : 1f));

        PlayerAvatar avatarInst;
        if (IsBottom) {
            avatarInst = bottomAvatarInstance;
        }
        else {
            avatarInst = topAvatarInstance;
        }

        currAvatar = Instantiate(avatarInst, pos, Quaternion.Euler(0f, 0f, IsBottom ? 0f : 180f));
    }

    void OnEnable() {
        Ball.BlowUpEvent += OnBallBlowUp;
    }

    void OnDisable() {
        Ball.BlowUpEvent -= OnBallBlowUp;
    }

    void OnBallBlowUp(Collider2D other) {
        if (other != myWall) {
            score++;
            scoreRef.value = score;
            GoalEvent?.Invoke();
        }
    }
}