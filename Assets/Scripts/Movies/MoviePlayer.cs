using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour
{
#if !UNITY_ANDROID
    private Renderer movieRenderer;
    public bool useLocalTexture;

    public bool isUsingRawTexture;
    // Use this for initialization
    void Start()
    {
        if (useLocalTexture)
        {
            StartCoroutine(PlayMovieLocal());

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

    public void SetMovie(MovieTexture movie)
    {
        GetComponent<RawImage>().texture = movie;
    }

    IEnumerator PlayMovieLocal()
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

        while (movie.isPlaying)
              yield return null;

        //transform.parent.gameObject.SetActive(false);
        FindObjectOfType<TutorialContainer>().CloseVideo();
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

        Debug.Log("" + movie.isPlaying);


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

            Debug.Log("" + movie.isPlaying);

            while (movie.isPlaying)
            {
                Debug.Log("" + movie.isPlaying);
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }
#endif
}
