using Frog;
using Input;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private Transform boundTopLeft;
    [SerializeField] private Transform boundBottomRight;

    [Header("Player Settings")]
    [SerializeField] private FrogView _frogView;
    [SerializeField] private float moveSpeed = 5f;

    [Header("Zones Settings")]
    [SerializeField] private Transform spawnPosition;

    [Header("Timer Settings")]
    //[SerializeField] private TimerView timerView;
    [SerializeField] private float levelStartTime = 60f;

    private FrogPresenter _frogPresenter;

    private void Start()
    {
        // Setup player
        var frogModel = new FrogModel(Vector2.zero, boundTopLeft.position, boundBottomRight.position);
        _frogPresenter = new FrogPresenter(frogModel, _frogView, inputHandler, moveSpeed);
    }

    private void Update()
    {
        _frogPresenter.Update();
    }

    [ContextMenu("Respawn Player")]
    private void HandlePlayerRespawn()
    {
        Debug.Log("Player Respawn");
    }

    private void HandleWin()
    {
        Debug.Log("Win");
    }
}