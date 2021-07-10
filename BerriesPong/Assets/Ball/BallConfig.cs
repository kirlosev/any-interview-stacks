using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Config", menuName = "L/Ball/Ball Config", order = 0)]
public class BallConfig : ScriptableObject {
    [SerializeField] private float speed = 1f;
    public float Speed => speed;

    [SerializeField] private BallSize size;
    public BallSize Size => size;

    [SerializeField] private Color tailColor = Color.white;
    public Color TailColor => tailColor;
}