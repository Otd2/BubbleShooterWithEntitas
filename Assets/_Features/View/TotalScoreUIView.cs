using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using TMPro;
using UnityEngine;

public class TotalScoreUIView : View, ITotalScoreListener
{
    [SerializeField] private TextMeshProUGUI totalScoreText;
    public override void Link(IEntity entity)
    {
        base.Link(entity);
        _linkedEntity.AddTotalScoreListener(this);
    }

    public void OnTotalScore(GameEntity entity, int score)
    {
        if (score > 0)
        {
            totalScoreText.transform.DOKill();
            totalScoreText.DOKill();
            var flashColor = Color.green;
            transform.localScale = Vector3.one;
            totalScoreText.color = Color.white;
            totalScoreText.transform.DOScale(1.3f, 0.2f).SetEase(Ease.InCirc).SetLoops(2, LoopType.Yoyo);
            totalScoreText.DOColor(flashColor, 0.2f).SetEase(Ease.Flash).SetLoops(2, LoopType.Yoyo);
        }
        totalScoreText.text = "" + score;
    }
}
