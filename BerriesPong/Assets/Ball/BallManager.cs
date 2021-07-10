using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {
    [SerializeField] private Ball ballInstance;

    private Ball currentBall;
    private BallConfig[] ballConfigs;

    private void Awake() {
        ballConfigs = Resources.LoadAll<BallConfig>("Ball Configs/");
        Debug.Log($"Loaded {ballConfigs.Length} ball configs");
    }
    
    private void Start() {
        CreateBall();
        ShootBall();
    }

    private void ShootBall() {
        currentBall.Shoot();
    }

    private void CreateBall() {
        if (currentBall != null) {
            Destroy(currentBall.gameObject);
        }
        
        currentBall = Instantiate(ballInstance, Vector3.zero, Quaternion.identity);
        var config = ballConfigs[Random.Range(0, ballConfigs.Length)];
        Debug.Log($"Using ball config: {config.name}");
        currentBall.SetConfig(config);
    }

    private void OnEnable() {
        Ball.BlowUpEvent += OnBallBlowUp;
    }

    private void OnDisable() {
        Ball.BlowUpEvent -= OnBallBlowUp;
    }

    private void OnBallBlowUp(Collider2D other) {
        StartCoroutine(ResetBall());
    }

    private IEnumerator ResetBall() {
        yield return new WaitForSeconds(0.5f);
        
        CreateBall();
        
        yield return new WaitForSeconds(1f);
        
        ShootBall();
    }
}