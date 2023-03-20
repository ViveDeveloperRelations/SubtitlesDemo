using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

// modified from https://github.com/roguecode/Unity-Simple-SRT
// Copyrigth 2017 roguecode MIT License
public class SubtitleDisplayer : MonoBehaviour
{
    public TextMesh textMesh;
    public TextMeshPro textMeshPro; //preferred
    public int subtitleIndex;
    //private string path = "srt";// System.IO.Path.Combine(Application.persistentDataPath, "srt");
    private string path = System.IO.Path.Combine(Application.streamingAssetsPath, "srt");
    private string fileContent;
    private GameObject obj;
    private float startTime;
    public bool resetTime = true;

    public IEnumerator Begin()
    {
        if(textMesh!=null)
        textMesh.gameObject.SetActive(true);
        else
            textMeshPro.gameObject.SetActive(true);

        string file = Subtitles.Files[subtitleIndex, Subtitles.SelectedLanguage];
        file = System.IO.Path.Combine(path, file);
        Debug.Log("srt file = " + file);

//        TextAsset txtAsset = Resources.Load<TextAsset>("srt/"+file);

        StreamReader reader = new StreamReader(file);
        fileContent  = "";

        while (!reader.EndOfStream)
        {
            fileContent += reader.ReadLine() + "\n";
        }
        reader.Close();

        SRTParser parser = new SRTParser(fileContent);

        if(resetTime)
            startTime = Time.time;
        SubtitleBlock currentSubtitle = null;

        if (textMesh != null) obj = textMesh.gameObject;
        else obj = textMeshPro.gameObject;

        while (obj.activeSelf)
        {
            float elapsed = Time.time - startTime;
            SubtitleBlock subtitle = parser.GetForTime(elapsed);
            if (subtitle != null)
            {
                if (!subtitle.Equals(currentSubtitle))
                {
                    currentSubtitle = subtitle;
                    if (textMesh != null)
                        textMesh.text = currentSubtitle.Text;
                    else textMeshPro.text = currentSubtitle.Text;
                }
                yield return null;
            }
            else
            {
                obj.SetActive(false);
                if (textMesh != null) textMesh.text = "";
                else textMeshPro.text = "";
                yield break;
            }
        }
        if (textMesh != null) textMesh.text = "";
        else textMeshPro.text = "";
        resetTime = true;
    }
}
