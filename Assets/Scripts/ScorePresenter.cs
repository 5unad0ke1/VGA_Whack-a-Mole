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
        _handle = LMotion.Create(Vector3.one * 1.2f, Vector3.one, 0.05f)
            .WithEase(Ease.InCirc)
            .BindToLocalScale(_text.transform);
    }
}
