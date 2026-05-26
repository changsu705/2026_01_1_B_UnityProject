using DG.Tweening;
using UnityEngine;
using TMPro;

public class TweenSample : MonoBehaviour
{
    [Header("펀치 스케일 예시")]
    public RectTransform punchUITarget;
    public GameObject punchObjectTarget;

    [Header("숫자 연출 예시")]
    public TMP_Text countText;
    public int addValue = 0;
    public int currntValue = 100;

    private int targetValue = 0;

    [Header("색 변형 연출 예시")]
    public Color frashColor = Color.yellow;

    private Color OriginalColor;

    [Header("페이드 UI 그룹")]
    public CanvasGroup fadeTarget;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OriginalColor = countText.color;
        fadeTarget.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayPunckUIScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayPunckObjectScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIShake();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayCountUp();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayColorFlash();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFade();
        }
    }

    public void PlayPunckUIScale()
    {
        if (punchUITarget == null) return;

        punchUITarget.DOKill();
        punchUITarget.localScale = Vector3.one;
        punchUITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);
    }

    public void PlayPunckObjectScale()
    {
        if (punchObjectTarget == null) return;

        punchObjectTarget.transform.DOKill();
        punchObjectTarget.transform.localScale = Vector3.one;
        punchObjectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);
    }

    public void PlayUIShake()
    {
        if (punchUITarget == null) return;

        punchUITarget.DOKill();
        punchUITarget.DOShakeAnchorPos(0.3f, 20f, 20, 90f);
    }

    public void PlayCountUp()
    {
        if (countText == null) return;

        targetValue += addValue;

        DOTween.Kill("CountTween", true);

        //DOTween.to는 숫자처럼 직접 Tween 함수가 없는 값을 움직일 때 사용한다.
        DOTween.To(
            () => currntValue,
            value =>
            {
                currntValue = value;
                countText.text = currntValue.ToString();
            },
            targetValue,
            0.5f
            )
        .SetEase(Ease.OutCubic)
        .SetId("CountTween");
    }

    public void PlayColorFlash()
    {
        if (countText == null) return;

        countText.DOKill();

        countText.color = OriginalColor;

        countText.DOColor(frashColor, 0.1f)
            .OnComplete (() =>
            {
                countText.DOColor(OriginalColor, 0.2f);
            });
    }

    public void PlayFade()
    {
        if (fadeTarget == null) return;

        fadeTarget.DOKill();
        fadeTarget.alpha = 0f;

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeTarget.DOFade(1f, 0.2f)); ;   // 0.2초 동안 나타난다.
        seq.AppendInterval(0.5f); // 0.5초 동안 유지된다.
        seq.Append(fadeTarget.DOFade(0f, 0.3f)); // 0.2초 동안 사라진다.
    }
}
