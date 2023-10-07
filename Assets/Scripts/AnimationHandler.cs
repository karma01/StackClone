using UnityEngine;
using DG.Tweening;
using TMPro;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI text;

    private void Start()
    {
        text.DOFade(0, 2f).SetEase(Ease.InOutExpo).SetLoops(-1);
        
    }
}