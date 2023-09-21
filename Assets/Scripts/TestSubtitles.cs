using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSubtitles : MonoBehaviour
{
    public SubtitleDisplayer displayer;

    public bool captionsEnabled = true;

    Coroutine coroutine = null;

    private void Start()
    {
        if (captionsEnabled) 
            coroutine= StartCoroutine(displayer.Begin());// narration (default)
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //"bang"
        {
            if (!captionsEnabled) return;

            if(coroutine!=null) StopCoroutine(coroutine); // stop/start to reload new srt file
            displayer.subtitleIndex = 1; //0=narration, 1=bang in this example
            displayer.resetTime = true; // start from beginning for the bang sequence
            coroutine = StartCoroutine(displayer.Begin());
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            captionsEnabled = !captionsEnabled;
            //todo: enable/disable game object or mesh renderer
        }
        else if(Input.GetKeyDown(KeyCode.L))
        {
            if(!captionsEnabled) return;
            int currentLang = Subtitles.SelectedLanguage;
            currentLang++;
            if (currentLang > ((int)Subtitles.Language.NUMTYPES - 1))
                currentLang = 0;
            Subtitles.SelectedLanguage = currentLang;
            if(coroutine!=null)
            {
                StopCoroutine(coroutine); // stop/start to load new srt language file
                displayer.resetTime = false; // continue where you were if already running
                StartCoroutine(displayer.Begin());
            }
        }
    }
}
