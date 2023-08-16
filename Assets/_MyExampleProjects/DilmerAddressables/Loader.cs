using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class Loader : Singleton<Loader>
{
    [SerializeField] private Ease easeType = Ease.OutQuad;
    [SerializeField] private float alphaTweenFrom = 0.0f;
    [SerializeField] private float alphaTweenTo = 1.0f;

    private RawImage loaderConteiner;
    private TextMeshProUGUI loaderText;

    public bool IsLoading { get; set; } = false;

    private List<TweenerCore<Color, Color, ColorOptions>> tweeners = new List<TweenerCore<Color, Color, ColorOptions>>();


    private void Awake()
    {
        loaderConteiner = GetComponent<RawImage>();
        loaderText = loaderConteiner.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void StartLoader()
    {
        Logger.Instance.LogWarning("Starting Loader");

        if(tweeners.Count == 0)
        {
            TweenerCore<Color, Color, ColorOptions> x = loaderConteiner.DOFade(alphaTweenFrom, alphaTweenTo).From()
                .SetEase(easeType).SetLoops(-1);


            TweenerCore<Color , Color, ColorOptions> y = loaderText.DOFade(alphaTweenFrom, alphaTweenTo).From()
                .SetEase(easeType).SetLoops(-1);

            tweeners.Add(x);
            tweeners.Add(y);
 
        }
        else
        {
            foreach (var t in tweeners)
            {
                t.Restart();
            }
        }

        IsLoading = true;
        gameObject.SetActive(true);

    }

    public void StopLoader()
    {
        Logger.Instance.LogWarning("Stopping Loader");

        foreach(var t in tweeners)
        {
            t.Pause();
        }

        IsLoading= false;
        gameObject.SetActive(IsLoading);
    }
}
