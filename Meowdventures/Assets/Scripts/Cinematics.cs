using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Cinematics : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public TMP_Text skipButtonText;

    void Start()
    {
        //skipButtonText.onClick.AddListener(delegate { SkipVideo(); });


        videoPlayer.loopPointReached += EndReached;
    }
    //public void ButtonSkip()
    //{

    //    skipButtonText.AddListener(delegate { SkipVideo(); });

    //}




    public void SkipVideo()
    {
        // Saltar al final del video manualmente
        EndReached(videoPlayer);
    }

    void EndReached(VideoPlayer vp)
    {
        // El evento se activa al finalizar el video
        // Puedes cargar la siguiente escena aquí
        SceneManager.LoadScene("Main");
    }
}
