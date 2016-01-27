using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour
{

    private Renderer movieRenderer;
    public bool useLocalTexture;

    public bool isUsingRawTexture;
    // Use this for initialization
    void Start()
    {
        if (useLocalTexture)
        {
            MovieTexture movie = null;
            if (!isUsingRawTexture)
            {
                movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
            }
            else
            {
                movie = GetComponent<RawImage>().mainTexture as MovieTexture;
            }
            movie.Play();
        }
        else
        {
            movieRenderer = GetComponent<Renderer>();
            StartCoroutine(PlayMovie());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator PlayMovie()
    {
        var www = new WWW("file://" + Application.streamingAssetsPath + "/T1.mov");
        MovieTexture movie = null;
        AudioSource audio = null;

        if (FindObjectOfType<AudioListener>() == null)
        {
            gameObject.AddComponent<AudioListener>();
        }

        if (www != null)
        {

            yield return www;

            //doGUI = 0.0f;

            // error comes at this line! 
            Debug.Log("1");
            movie = (MovieTexture)www.movie as MovieTexture;

            while (!movie.isReadyToPlay)
                yield return null;

            Debug.Log("2");

            movieRenderer.material.mainTexture = movie;
            movieRenderer.enabled = true;
            audio = gameObject.AddComponent<AudioSource>();
            //audio.clip = movie.audioClip;

            //Debug.Log("Yeah play the moviE");

            movie.Play();
            audio.Play();
            Debug.Log("3");

            while (movie.isPlaying && !Input.anyKeyDown)
                yield return null;
        }
    }
}
