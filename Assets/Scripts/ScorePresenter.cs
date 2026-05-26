using LitMotion;
using LitMotion.Extensions;
using R3;
using TMPro;
using UnityEngine;

public sealed class ScorePresenter : MonoBehaviour
{
    [SerializeField] private InGameManager _inGameManager;
    [SerializeField] private TMP_Text _text;

    private MotionHandle _handle;


    private static readonly Vector3 TEXT_ANIMATION_FROM = Vector3.one * 1.2f;
    private static readonly Vector3 TEXT_ANIMATION_TO = Vector3.one;
    private static readonly float TEXT_ANIMATION_DURATION = 0.05f;
    private static readonly Ease TEXT_ANIMATION_EASE = Ease.InCirc;

    void Start()
    {
        _inGameManager.Score.Subscribe(OnUpdate)
            .RegisterTo(destroyCancellationToken);
    }
    private void OnDestroy()
    {
        _handle.TryCancel();
    }
    public void OnUpdate(int score)
    {
        _text.SetText(score.ToString());

        _handle.TryCancel();
        _handle = LMotion.Create(TEXT_ANIMATION_FROM, TEXT_ANIMATION_TO, TEXT_ANIMATION_DURATION)
            .WithEase(TEXT_ANIMATION_EASE)
            .BindToLocalScale(_text.transform);
    }
}
