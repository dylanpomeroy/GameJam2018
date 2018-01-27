using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Camera : MonoBehaviour {

    VideoPlayer videoPlayer;
    private GameObject buttonPrefab;
    private List<GameObject> OptionButtons;


    // Use this for initialization
    void Start () {
        var camera = GameObject.Find("Main Camera");
        videoPlayer = camera.AddComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        videoPlayer.targetCameraAlpha = 0.99f;
        videoPlayer.url = Application.dataPath + "/video3.mp4";
        videoPlayer.isLooping = true;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
        videoPlayer.Play();

        OptionButtons = new List<GameObject>()
        {
            GameObject.Find("ButtonA"),
            GameObject.Find("ButtonB"),
            GameObject.Find("ButtonC"),
            GameObject.Find("ButtonD")
        };
        OptionButtons.ForEach(optionButton =>
        {
            optionButton.SetActive(false);
        });
        OptionButtons[0].GetComponent<Button>().onClick.AddListener(OptionAClicked);
    }

    bool hitPoint0 = false;
    bool hitPoint1 = false;
    // Update is called once per frame
    void Update () {
        if (!hitPoint0 && videoPlayer.frame > 730)
        {
            InitializeDialogOptions("Yes", "No", "Are you kidding!?");
            videoPlayer.frame = 500;
        }
        if (!hitPoint1 && videoPlayer.frame > 1500)
        {
            hitPoint1 = true;
            InitializePuzzle();
        }

		if (Input.GetKeyDown(KeyCode.W))
        {
            videoPlayer.targetCameraAlpha += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            videoPlayer.targetCameraAlpha -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (videoPlayer.isPlaying)
                videoPlayer.Pause();
            else videoPlayer.Play();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InitializePuzzle();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            PuzzleCompleted();
        }
	}

    void OptionAClicked()
    {
        hitPoint0 = true;
        OptionButtons.ForEach(optionButton =>
        {
            optionButton.SetActive(false);
        });
    }
    
    void InitializeDialogOptions(params string[] options)
    {
        int i;
        for (i = 0; i < options.Length; i++)
        {
            OptionButtons[i].SetActive(true);
            OptionButtons[i].GetComponentInChildren<Text>().text = options[i];
        }
    }

    void InitializePuzzle()
    {
        this.fadeVideoAlpha = 0.1f;
        this.fadeVideoCancelWhenReachedAlpha = 0.1f;
        InvokeRepeating("FadeVideoAlpha", 0, 0.1f);
        this.videoPlayer.Pause();
    }

    void PuzzleCompleted()
    {
        this.addVideoAlpha = 0.1f;
        this.addVideoCancelWhenReachedAlpha = 1.0f;
        InvokeRepeating("AddVideoAlpha", 0, 0.1f);
        this.videoPlayer.Play();
    }

    private float addVideoAlpha;
    private float addVideoCancelWhenReachedAlpha;
    void AddVideoAlpha()
    {
        var remainingAlphaToChange = addVideoCancelWhenReachedAlpha - this.videoPlayer.targetCameraAlpha;
        if (remainingAlphaToChange < addVideoAlpha)
        {
            this.videoPlayer.targetCameraAlpha = addVideoCancelWhenReachedAlpha;
            CancelInvoke("AddVideoAlpha");
        }
        else
        {
            this.videoPlayer.targetCameraAlpha += addVideoAlpha;
        }
    }

    private float fadeVideoAlpha;
    private float fadeVideoCancelWhenReachedAlpha;
    void FadeVideoAlpha()
    {
        var remainingAlphaToChange = this.videoPlayer.targetCameraAlpha - fadeVideoCancelWhenReachedAlpha;
        if (remainingAlphaToChange < fadeVideoAlpha)
        {
            this.videoPlayer.targetCameraAlpha = fadeVideoCancelWhenReachedAlpha;
            CancelInvoke("FadeVideoAlpha");
        }
        else
        {
            this.videoPlayer.targetCameraAlpha -= fadeVideoAlpha;
        }
    }
}
