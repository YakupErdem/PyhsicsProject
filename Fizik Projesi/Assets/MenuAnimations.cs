using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DG.Tweening;
using UnityEngine;

public class MenuAnimations : MonoBehaviour
{

    public GameObject[] animationObjects;
    public float animationTime;
    public float delayBetweenAnimation;
    public bool animate;

    private void Start()
    {
        if (animate) StartCoroutine(StartAnimation());
    }

    public IEnumerator StartAnimation()
    {
        foreach (var animationObject in animationObjects)
        {
            animationObject.transform.localScale = Vector3.zero;
        }
        //
        foreach (var animationObject in animationObjects)
        {
            animationObject.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), animationTime);
            StartCoroutine(AnimateObjectScaleBack(animationObject, animationTime));
            yield return new WaitForSeconds(delayBetweenAnimation);
        }
    }

    private IEnumerator AnimateObjectScaleBack(GameObject g, float time)
    {
        yield return new WaitForSeconds(time);
        g.transform.DOScale(Vector3.one, .1f);
    }

    public IEnumerator OpenObject(GameObject g, float time)
    {
        g.SetActive(true);
        g.transform.localScale = new Vector3(.6f, .6f, .6f);
        g.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), time);
        yield return new WaitForSeconds(time);
        g.transform.DOScale(Vector3.one, .1f);
    }
    public IEnumerator CloseObject(GameObject g, float time)
    {
        g.transform.DOScale(new Vector3(.6f, .6f, .6f), time);
        yield return new WaitForSeconds(time);
        g.SetActive(false);
    }
}
