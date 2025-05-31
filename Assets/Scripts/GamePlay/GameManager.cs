using Frog;
using Input;
using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Core Settings")]
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Transform _boundTopLeft;
    [SerializeField] private Transform _boundBottomRight;

    [Header("Player Settings")]
    [SerializeField] private FrogView _frogView;
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Zones Settings")]
    [SerializeField] private Transform _spawnPosition;

    [Header("Timer Settings")]
    //[SerializeField] private TimerView timerView;
    [SerializeField] private float _levelStartTime = 60f;

    private FrogPresenter _frogPresenter;

    private void Start()
    {
        // Setup player
        var frogModel = new FrogModel(_spawnPosition.position, _boundTopLeft.position, _boundBottomRight.position);
        _frogPresenter = new FrogPresenter(frogModel, _frogView, _inputHandler, _moveSpeed);

        _frogPresenter.OnCarTriggerEntred += HandlePlayerRespawn;
        _frogPresenter.OnFinalZoneEntered += HandleWin;
    }

    private void Update()
    {
        _frogPresenter.Update();
    }

    [ContextMenu("Respawn Player")]
    private void HandlePlayerRespawn()
    {
        Debug.Log("Player Respawn");
        _frogPresenter.SetSpawnPosition(_spawnPosition.position);
    }

    private void HandleWin()
    {
        Debug.Log("Win");
    }
}