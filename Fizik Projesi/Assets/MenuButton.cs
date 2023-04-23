using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class MenuButton : MonoBehaviour
{
    public enum SelectedSubject
    {
        none,
        yatay,
        egik,
        serbest
    }

    public SelectedSubject selectedSubject;
    public VideoLinks videoLinks;
    public GameObject menu;
    public float openTime, closeTime;
    
    [Serializable]
    public struct VideoLinks
    {
        public string Yatay;
        public string Egik;
        public string Serbest;
    }

    public void SetSelectedSubject(string subject)
    {
        selectedSubject = subject switch
        {
            "yatay" => SelectedSubject.yatay,
            "eğik" => SelectedSubject.egik,
            "serbest" => SelectedSubject.serbest,
            _ => SelectedSubject.none
        };
    }

    public void StartSimulation()
    {
        switch (selectedSubject)
        {
            case SelectedSubject.none:
                Debug.LogError("There is no selected subject");
                break;
            case SelectedSubject.yatay:
                SceneManager.LoadScene("yatayAtis");
                break;
            case SelectedSubject.egik:
                SceneManager.LoadScene("aciliAtis");
                break;
            case SelectedSubject.serbest:
                SceneManager.LoadScene("serbest");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void StartVideo()
    {
        switch (selectedSubject)
        {
            case SelectedSubject.none:
                Debug.LogError("There is no selected subject");
                break;
            case SelectedSubject.yatay:
                Process.Start(videoLinks.Yatay);
                break;
            case SelectedSubject.egik:
                Process.Start(videoLinks.Egik);
                break;
            case SelectedSubject.serbest:
                Process.Start(videoLinks.Serbest);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OpenMenu() => StartCoroutine(FindObjectOfType<MenuAnimations>().OpenObject(menu,openTime));
    public void CloseMenu() => StartCoroutine(FindObjectOfType<MenuAnimations>().CloseObject(menu,closeTime));
}
