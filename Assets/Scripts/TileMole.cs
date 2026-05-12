using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public sealed class TileMole : MonoBehaviour
{
    [SerializeField] private InGameManager _gameManager;
    [SerializeField] private MoleButton _mole;

    private float _timer;
    private Transform _cacheMoleT;

    private MotionHandle _handle;
    private bool _isMoleActive;

    private static readonly Vector3 DIVE_POSITION = Vector3.down;
    private static readonly Vector3 SHOW_POSITION = Vector3.zero;
    private const Ease EASING = Ease.InOutCirc;
    private const float DURATION = 0.2f;
    private const int SCORE_PER_HIT = 1;

    private const float HIT_EXIT_DURATION = 0.05f;
    private const float HIT_PUNCH_DURATION = 0.05f;
    private const float HIT_PUNCH_STRENGTH = 0.1f;
    private const int HIT_PUNCH_WAVE_MIN = 2;
    private const int HIT_PUNCH_WAVE_MAX = 5;

    void Start()
    {
        ResetTimer();
        _isMoleActive = false;
        _cacheMoleT = _mole.transform;

        _cacheMoleT.localPosition = DIVE_POSITION;

        _mole.OnClicked += Hit;
    }
    private void Update()
    {
        _timer = Mathf.Max(0f, _timer - Time.deltaTime);
        if (_timer > 0f)
            return;

        ResetTimer();
        _isMoleActive ^= true;
        if (_isMoleActive)
        {
            PlayEnterAnimation();
        }
        else
        {
            PlayExitAnimation();
        }
    }
    private void Hit()
    {
        if (!_isMoleActive)
            return;
        _isMoleActive = false;
        ResetTimer();
        PlayHitAnimation();

        _gameManager.AddScore(SCORE_PER_HIT);
        Debug.Log("Hit!", gameObject);
    }
    private void OnDestroy()
    {
        _handle.TryCancel();
        _mole.OnClicked -= Hit;
    }

    private void ResetTimer()
    {
        _timer = Random.Range(1f, 3f);
    }
    private void PlayEnterAnimation()
    {
        _handle.TryCancel();
        _handle = LMotion.Create(DIVE_POSITION, SHOW_POSITION, DURATION)
            .WithEase(EASING)
            .BindToLocalPosition(_cacheMoleT);
    }
    private void PlayExitAnimation()
    {
        _handle.TryCancel();
        _handle = LMotion.Create(SHOW_POSITION, DIVE_POSITION, DURATION)
            .WithEase(EASING)
            .BindToLocalPosition(_cacheMoleT);
    }
    private void PlayHitAnimation()
    {
        _handle.TryCancel();
        _handle = LSequence.Create()
            .Append(LMotion.Punch.Create(0, HIT_PUNCH_STRENGTH, HIT_PUNCH_DURATION)
                .WithFrequency(Random.Range(HIT_PUNCH_WAVE_MIN, HIT_PUNCH_WAVE_MAX))
                .BindToLocalPositionX(_cacheMoleT))

            .Join(LMotion.Punch.Create(0, HIT_PUNCH_STRENGTH, HIT_PUNCH_DURATION)
                .WithFrequency(Random.Range(HIT_PUNCH_WAVE_MIN, HIT_PUNCH_WAVE_MAX))
                .BindToLocalPositionY(_cacheMoleT))

            .Insert(0.1f, LMotion.Create(SHOW_POSITION, DIVE_POSITION, HIT_EXIT_DURATION)
                .WithEase(EASING)
                .BindToLocalPosition(_cacheMoleT))
            .Run();
    }
}
