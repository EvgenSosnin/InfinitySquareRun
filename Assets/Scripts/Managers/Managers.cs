using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(BlockManager))]
[RequireComponent(typeof(TeleportManager))]
[RequireComponent(typeof(WorldRotation))]

public class Managers : MonoBehaviour
{
    public static GameManager GameManager { get; private set; }
    public static BlockManager BlockManager { get; private set; }
    public static TeleportManager TeleportManager { get; private set; }
    public static WorldRotation WorldRotation { get; private set; }

    public static ManaController ManaController { get; private set; }

    void Awake()
    {
        BlockManager = transform.GetComponent<BlockManager>();
        GameManager = transform.GetComponent<GameManager>();
        TeleportManager = transform.GetComponent<TeleportManager>();
        WorldRotation = transform.GetComponent<WorldRotation>();
        ManaController = transform.GetComponent<ManaController>();
    }
}
