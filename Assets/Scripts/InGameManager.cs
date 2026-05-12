using R3;
using UnityEngine;

public sealed class InGameManager : MonoBehaviour
{
    public ReadOnlyReactiveProperty<float> CurrentTime => _currentTime;
    public ReadOnlyReactiveProperty<int> Score => _score;

    [SerializeField] private float _defaultTimer;
    private readonly ReactiveProperty<float> _currentTime = new();
    private readonly ReactiveProperty<int> _score = new();
    void Start()
    {
        _currentTime.Value = _defaultTimer;
        _score.Value = 0;
    }
    private void Update()
    {
        _currentTime.Value = Mathf.Max(_currentTime.Value - Time.deltaTime, 0f);
    }
    private void OnDestroy()
    {
        _currentTime.Dispose();
        _score.Dispose();
    }
    public void AddScore(int amount)
    {
        if (_currentTime.Value <= 0f)
            return;
        _score.Value += amount;
    }
}
