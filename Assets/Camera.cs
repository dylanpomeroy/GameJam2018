using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Assets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Camera : MonoBehaviour {

    VideoPlayer VideoPlayer;
    AudioSource AudioPlayer;
    private List<GameObject> OptionButtons;
    private GameObject CompleteButton;
    private List<GameObject> ExternalPieces;

    int CurrentStage;
    int CurrentDialogLevel;
    List<int> CurrentDialogOptionSelections;
    
    void Start() {
        InitializeVideoPlayer();
        PlayVideo();

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
        OptionButtons[1].GetComponent<Button>().onClick.AddListener(OptionBClicked);
        OptionButtons[2].GetComponent<Button>().onClick.AddListener(OptionCClicked);
        OptionButtons[3].GetComponent<Button>().onClick.AddListener(OptionDClicked);

        CompleteButton = GameObject.Find("ButtonComplete");
        CompleteButton.SetActive(false);
        CompleteButton.GetComponent<Button>().onClick.AddListener(CompleteClicked);

        CurrentStage = 0;
        CurrentDialogLevel = 0;
        CurrentDialogOptionSelections = new List<int>();

        ExternalPieces = new List<GameObject>();
        for (int i = 1; i <= 16; i++)
        {
            ExternalPieces.Add(GameObject.Find("ExternalPiece" + i));
        }
        ExternalPieces.ForEach(piece => piece.SetActive(false));
    }

    void Update()
    {
        HandleKeystrokes();

        HandleHitPoints();
    }

    private void OnMouseDown()
    {
        InitializeDialogOptions(new List<string> { "asdf", "asdf" });
        FadeVideoAlpha();
    }

    void HandleHitPoints()
    {
        if (!puzzleInitialized) CompleteButton.SetActive(false);
        else CompleteButton.SetActive(true);


        switch (CurrentStage)
        {
            case 0:
                Stage0();
                break;
            case 1:
                Stage1();
                break;
            case 2:
                Stage2();
                break;
            case 3:
                Stage3();
                break;
            case 4:
                Stage4();
                break;
            case 5:
                Stage5();
                break;
            case 6:
                Stage6();
                break;
            case 7:
                Stage7();
                break;
        }
    }

    void Stage0()
    {
        HandlePuzzleStuff(Ref.LevelFivePieceQueue, Ref.LevelFiveExternalPieces);

        switch (CurrentDialogLevel)
        {
            case 0:
                SetNewVideo(Ref.VideoFiles[0], Ref.AudioFiles[0]);
                CurrentDialogLevel++;
                break;
            case 1:
                if (VideoPlayer.frame > 730)
                {
                    if (HandleOptionsWithLoop(
                        Ref.FrameRepeatRanges[0],
                        Ref.DialogOptions[0]))
                    {
                        CurrentDialogLevel++;
                    }
                    CurrentDialogLevel++;
                }
                break;
            case 2:
                if (OptionASelected || OptionBSelected || OptionCSelected || OptionDSelected)
                {
                    OptionButtons.ForEach(optionButton => optionButton.SetActive(false));
                    CurrentDialogLevel++;
                }
                else if (VideoPlayer.frame > 850) SetVideoToFrame(730);
                break;
            case 3:
                SetNewVideo(Ref.VideoFiles[2], Ref.AudioFiles[0]);
                SetVideoToFrame(VideoPlayer.frame + 600); // temporary
                CurrentDialogLevel++;
                break;
            case 4:
                if (VideoPlayer.frame > 300)
                {
                    InitializePuzzle();
                    CurrentDialogLevel++;
                }
                break;
            case 5:
                if (CompleteSelected)
                {
                    PuzzleCompleted();
                    SetNewVideo(Ref.VideoFiles[1], Ref.AudioFiles[0]);
                    CurrentDialogLevel++;
                }
                break;
            case 6:
                if (OptionASelected)
                {
                    InitializeDialogOptions(new List<string> { "Great", "Job", "You", "Win!" });
                    OptionASelected = false;
                    CurrentDialogLevel++;

                }
                else if (OptionBSelected || OptionCSelected || OptionDSelected)
                {
                    InitializeDialogOptions(new List<string> { "Wrong", "Choice", "You", "Lose!" });
                    new List<bool>() { OptionBSelected, OptionCSelected, OptionDSelected }.ForEach(x => x = false);
                    CurrentDialogLevel++;
                }
                break;
            default:
                CurrentStage++;
                CurrentDialogLevel = 0;
                break;
        }
    }

    void HandlePuzzleStuff(List<string> pieceQueue, List<string> externalPieces)
    {
        if (puzzleInitialized && pieceQueue.Count > 0)
        {
            if (!Ref.SelectorSectionsFull[0])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                newObject.transform.position = Ref.SelectorPeicePlacements[0];
                Ref.SelectorSectionsFull[0] = true;
            }
            else if (!Ref.SelectorSectionsFull[1])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                newObject.transform.position = Ref.SelectorPeicePlacements[1];
                Ref.SelectorSectionsFull[1] = true;
            }
            else if (!Ref.SelectorSectionsFull[2])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                newObject.transform.position = Ref.SelectorPeicePlacements[2];
                Ref.SelectorSectionsFull[2] = true;
            }

            ExternalPieces
            .Where(piece => externalPieces.Contains(piece.name))
            .ToList().ForEach(piece => piece.SetActive(true));
        }
    }

    bool dialogOptionsInitialized = false;
    bool HandleOptionsWithLoop(List<long> frameRepeatRange, List<string> dialogOptions)
    {
        if (!dialogOptionsInitialized) InitializeDialogOptions(dialogOptions);
        dialogOptionsInitialized = true;

        if (OptionASelected || OptionBSelected || OptionCSelected || OptionDSelected)
        {
            UninitializeDialogOptions();
            dialogOptionsInitialized = false;
            return true;
        }
        return false;
    }

    void Stage1()
    {
    }

    void Stage2()
    {
    }

    void Stage3()
    {
    }

    void Stage4()
    {
    }

    void Stage5()
    {
    }

    void Stage6()
    {
    }

    void Stage7()
    {
    }

    void UnselectOptionButtons()
    {
        OptionASelected = false;
        OptionBSelected = false;
        OptionCSelected = false;
        OptionDSelected = false;
    }

    void SetNewVideo(string newVideoUrl, string newAudioFile)
    {
        PauseVideo();
        VideoPlayer.url = newVideoUrl;
        AudioPlayer.clip = Resources.Load<AudioClip>(newAudioFile);
        SetVideoToFrame(0);
        AudioPlayer.time = 0;
        PlayVideo();
    }

    void InitializeVideoPlayer()
    {
        var camera = GameObject.Find("Main Camera");
        VideoPlayer = camera.AddComponent<VideoPlayer>();
        VideoPlayer.playOnAwake = false;
        VideoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        VideoPlayer.targetCameraAlpha = 0.99f;
        VideoPlayer.isLooping = true;

        AudioPlayer = camera.AddComponent<AudioSource>();
        AudioPlayer.playOnAwake = false;
    }

    void SetVideoToFrame(long frame)
    {
        VideoPlayer.frame = frame;
        AudioPlayer.time = frame / VideoPlayer.frameRate;
    }

    void PlayVideo()
    {
        VideoPlayer.Play();
        AudioPlayer.Play();
    }

    void PauseVideo()
    {
        VideoPlayer.Pause();
        AudioPlayer.Pause();
    }

    void HandleKeystrokes()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            VideoPlayer.targetCameraAlpha += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            VideoPlayer.targetCameraAlpha -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (VideoPlayer.isPlaying)
                PauseVideo();
            else VideoPlayer.Play();
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

    bool CompleteSelected = false;
    void CompleteClicked()
    {
        CompleteSelected = true;
    }

    bool OptionASelected = false;
    void OptionAClicked()
    {
        OptionASelected = true;
    }

    bool OptionBSelected = false;
    void OptionBClicked()
    {
        OptionBSelected = true;
    }

    bool OptionCSelected = false;
    void OptionCClicked()
    {
        OptionCSelected = true;
    }

    bool OptionDSelected = false;
    void OptionDClicked()
    {
        OptionDSelected = true;
    }

    void InitializeDialogOptions(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            OptionButtons[i].SetActive(true);
            OptionButtons[i].GetComponentInChildren<Text>().text = options[i];
        }
    }

    void UninitializeDialogOptions()
    {
        OptionButtons.ForEach(button => button.SetActive(false));
    }

    bool puzzleInitialized = false;
    void InitializePuzzle()
    {
        puzzleInitialized = true;
        

        this.fadeVideoAlpha = 0.1f;
        this.fadeVideoCancelWhenReachedAlpha = 0.1f;
        InvokeRepeating("FadeVideoAlpha", 0, 0.1f);
        PauseVideo();
        
    }

    void PuzzleCompleted()
    {
        this.addVideoAlpha = 0.1f;
        this.addVideoCancelWhenReachedAlpha = 1.0f;
        InvokeRepeating("AddVideoAlpha", 0, 0.1f);

        puzzleInitialized = false;
        CompleteButton.SetActive(false);
        CompleteSelected = false;

        ExternalPieces.ForEach(piece => piece.SetActive(false));
    }

    private float addVideoAlpha;
    private float addVideoCancelWhenReachedAlpha;
    void AddVideoAlpha()
    {
        var remainingAlphaToChange = addVideoCancelWhenReachedAlpha - this.VideoPlayer.targetCameraAlpha;
        if (remainingAlphaToChange < addVideoAlpha)
        {
            this.VideoPlayer.targetCameraAlpha = addVideoCancelWhenReachedAlpha;
            CancelInvoke("AddVideoAlpha");
        }
        else
        {
            this.VideoPlayer.targetCameraAlpha += addVideoAlpha;
        }
    }

    private float fadeVideoAlpha;
    private float fadeVideoCancelWhenReachedAlpha;
    void FadeVideoAlpha()
    {
        var remainingAlphaToChange = this.VideoPlayer.targetCameraAlpha - fadeVideoCancelWhenReachedAlpha;
        if (remainingAlphaToChange < fadeVideoAlpha)
        {
            this.VideoPlayer.targetCameraAlpha = fadeVideoCancelWhenReachedAlpha;
            CancelInvoke("FadeVideoAlpha");
        }
        else
        {
            this.VideoPlayer.targetCameraAlpha -= fadeVideoAlpha;
        }
    }
}
