using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource source;
    public bool wait;
    int courrSong = -1;
    public List<string> sounds;

    private void Start()
    {
        string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
        Directory.CreateDirectory(path + @"\Music\");

        var allowedExtensions = new[] {".ogg"};
        sounds = Directory
            .GetFiles(path + @"\Music\")
            .Where(file => allowedExtensions.Any(file.ToLower().EndsWith))
            .ToList();

        print(sounds.Count);
    }

    private void Update()
    {
        if (Application.loadedLevel == 1)
        {
            if (source.isPlaying == false)
            {
                if (wait == false)
                {
                    if (sounds.Count == 0) return;

                    int newsong = Random.Range(0, sounds.Count);
                    while (newsong == courrSong && sounds.Count != 1)
                    {
                        newsong = Random.Range(0, sounds.Count);
                    }
                    courrSong = newsong;
                    StartCoroutine(StartSong(sounds[courrSong]));
                }
            }
        }
    }

    public IEnumerator StartSong(string path)
    {
        wait = true;
        var www = new WWW(path);
        if (www.error != null)
        {
            wait = false;
            Debug.Log(www.error);
        }
        else
        {
            source.clip = www.GetAudioClip(false, false);
            while (source.clip.loadState != AudioDataLoadState.Loaded)
                yield return new WaitForSeconds(0.1f);
            source.PlayOneShot(source.clip);
            wait = false;
        }
        wait = false;
    }
}
