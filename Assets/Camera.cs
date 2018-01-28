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
        }
    }

    void Stage0()
    {
        HandlePuzzleStuff(Ref.LevelOnePieceQueue, Ref.LevelOneExternalPieces);

        switch (CurrentDialogLevel)
        {
            case 0:
                SetNewVideo(Ref.VideoFiles[0], Ref.AudioFiles[0]); // construction worker intro
                CurrentDialogLevel++;
                break;
            case 1:
                if (VideoPlayer.frame > Ref.FrameRepeatRanges[0].First()) // "Something is blocking my signal"
                {
                    if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[0], Ref.DialogOptions[0]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 2:
                if (OptionASelected || OptionBSelected)
                {
                    if (OptionASelected)
                    {
                        SetNewVideo(Ref.VideoFiles[1], Ref.AudioFiles[1]); // "Your face?"
                        OptionASelected = false;
                        OptionASelectedHistory = true;
                    }
                    else
                    {
                        SetNewVideo(Ref.VideoFiles[2], Ref.AudioFiles[2]); // "You sound smart"
                        OptionBSelected = false;
                        OptionBSelectedHistory = true;
                    }
                    CurrentDialogLevel++;
                }
                break;
            case 3:
                if (OptionASelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[1].First()
                        && HandleOptionsWithLoop(Ref.FrameRepeatRanges[1], Ref.DialogOptions[1]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                else
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[2].First()
                        && HandleOptionsWithLoop(Ref.FrameRepeatRanges[2], Ref.DialogOptions[2]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 4:
                if (OptionASelected && OptionASelectedHistory)
                {
                    SetNewVideo(Ref.VideoFiles[3], Ref.AudioFiles[3]); // Falls in pain
                }
                else if (OptionBSelected && OptionASelectedHistory)
                {
                    SetNewVideo(Ref.VideoFiles[4], Ref.AudioFiles[4]); // Appreciative of fixed walkie
                }
                else
                {
                    SetNewVideo(Ref.VideoFiles[5], Ref.AudioFiles[5]); // Touching wall
                }
                CurrentDialogLevel++;
                break;
            case 5:
                if (OptionASelected && OptionASelectedHistory)
                {
                    if (VideoPlayer.frame == Ref.FrameRepeatRanges[3].First())
                    {
                        CurrentDialogLevel++;
                    }
                }
                else if (OptionBSelected && OptionASelectedHistory)
                {
                    if (VideoPlayer.frame == Ref.FrameRepeatRanges[4].First())
                    {
                        CurrentDialogLevel++;
                    }
                }
                else
                {
                    if (VideoPlayer.frame == Ref.FrameRepeatRanges[5].First())
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 6:
                InitializePuzzle();
                CurrentDialogLevel++;
                break;
            case 7:
                if (CompleteSelected)
                {
                    PuzzleCompleted();
                    PlayVideo();
                    CurrentDialogLevel++;
                }
                break;
            default:
                CurrentStage++;
                OptionASelected = false;
                OptionBSelected = false;
                OptionASelectedHistory = false;
                OptionBSelectedHistory = false;
                CompleteSelected = false;
                CurrentDialogLevel = 0;
                Ref.SelectorSectionsFull[0] = false;
                Ref.SelectorSectionsFull[1] = false;
                Ref.SelectorSectionsFull[2] = false;
                break;
        }
    }

    List<GameObject> clonedObjects = new List<GameObject>();
    void HandlePuzzleStuff(List<string> pieceQueue, List<string> externalPieces)
    {
        if (puzzleInitialized && pieceQueue.Count > 0)
        {
            if (!Ref.SelectorSectionsFull[0])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                clonedObjects.Add(newObject);
                newObject.transform.position = Ref.SelectorPeicePlacements[0];
                Ref.SelectorSectionsFull[0] = true;
            }
            else if (!Ref.SelectorSectionsFull[1])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                clonedObjects.Add(newObject);
                newObject.transform.position = Ref.SelectorPeicePlacements[1];
                Ref.SelectorSectionsFull[1] = true;
            }
            else if (!Ref.SelectorSectionsFull[2])
            {
                var nextObjectName = pieceQueue[0];
                pieceQueue.RemoveAt(0);
                var newObject = Instantiate(GameObject.Find(nextObjectName));
                clonedObjects.Add(newObject);
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

        if (VideoPlayer.frame > frameRepeatRange.Last())
        {
            SetVideoToFrame(frameRepeatRange.First());
        }
        return false;
    }

    void Stage1()
    {
        HandlePuzzleStuff(Ref.LevelTwoPieceQueue, Ref.LevelTwoExternalPieces);

        switch (CurrentDialogLevel)
        {
            case 0:
                CurrentDialogLevel++;
                break;
            case 1:
                if (VideoPlayer.frame > Ref.FrameRepeatRanges[6].First()) // offers liquids
                {
                    if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[6], Ref.DialogOptions[3]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 2:
                if (OptionASelected || OptionBSelected || OptionCSelected)
                {
                    if (OptionASelected)
                    {
                        SetNewVideo(Ref.VideoFiles[6], Ref.AudioFiles[6]); // Orange
                        OptionASelected = false;
                        OptionASelectedHistory = true;
                    }
                    else if (OptionBSelected)
                    {
                        SetNewVideo(Ref.VideoFiles[7], Ref.AudioFiles[7]); // Blue
                        OptionBSelected = false;
                        OptionBSelectedHistory = true;
                    }
                    else
                    {
                        SetNewVideo(Ref.VideoFiles[8], Ref.AudioFiles[8]); // Both
                        OptionCSelected = false;
                        OptionCSelectedHistory = true;
                    }
                    CurrentDialogLevel++;
                }
                break;
            case 3:
                if (OptionASelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[7].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                if (OptionBSelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[8].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                if (OptionCSelectedHistory)
                {
                    // skipping puzzle
                    CurrentDialogLevel += 2;
                }
                break;
            case 4:
                if (CompleteSelected)
                {
                    PuzzleCompleted();
                    PlayVideo();
                    CurrentDialogLevel++;
                }
                break;
            default:
                CurrentStage++;
                OptionASelected = false;
                OptionBSelected = false;
                OptionCSelected = false;
                OptionASelectedHistory = false;
                OptionBSelectedHistory = false;
                OptionCSelectedHistory = false;
                CompleteSelected = false;
                CurrentDialogLevel = 0;
                Ref.SelectorSectionsFull[0] = false;
                Ref.SelectorSectionsFull[1] = false;
                Ref.SelectorSectionsFull[2] = false;
                break;
        }
    }

    void Stage2()
    {
        HandlePuzzleStuff(Ref.LevelThreePieceQueue, Ref.LevelThreeExternalPieces);

        switch (CurrentDialogLevel)
        {
            case 0:
                if (VideoPlayer.frame > Ref.FrameRepeatRanges[9].First()) // show 3 items
                {
                    if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[9], Ref.DialogOptions[4]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 1:
                if (OptionASelected || OptionBSelected || OptionCSelected)
                {
                    if (OptionASelected)
                    {
                        SetNewVideo(Ref.VideoFiles[9], Ref.AudioFiles[9]); // Binoculars
                        OptionASelected = false;
                        OptionASelectedHistory = true;
                    }
                    else if (OptionBSelected)
                    {
                        SetNewVideo(Ref.VideoFiles[10], Ref.AudioFiles[10]); // Cellphone
                        OptionBSelected = false;
                        OptionBSelectedHistory = true;
                    }
                    else
                    {
                        SetNewVideo(Ref.VideoFiles[11], Ref.AudioFiles[11]); // Watch
                        OptionCSelected = false;
                        OptionCSelectedHistory = true;
                    }
                    CurrentDialogLevel++;
                }
                break;
            case 2:
                if (OptionASelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[10].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel += 3;
                    }
                }
                if (OptionBSelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[11].First())
                    {
                        if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[11], Ref.DialogOptions[5]))
                        {
                            CurrentDialogLevel++;
                        }
                    }
                }
                if (OptionCSelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[12].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel += 3;
                    }
                }
                break;
            case 3: // this only runs when Option B history hits above
                if (OptionASelected)
                {
                    OptionASelected = false;
                    OptionASelectedHistory = true;
                    SetNewVideo(Ref.VideoFiles[12], Ref.AudioFiles[12]); // check texts
                }
                else
                {
                    OptionBSelected = false;
                    OptionBSelectedHistory = true;
                    SetNewVideo(Ref.VideoFiles[13], Ref.AudioFiles[13]); // order pasta
                }
                CurrentDialogLevel++;
                break;
            case 4:
                if (OptionASelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[13].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                else
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[14].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 5:
                if (CompleteSelected)
                {
                    PuzzleCompleted();
                    PlayVideo();
                    CurrentDialogLevel++;
                }
                break;
            default:
                CurrentStage++;
                OptionASelected = false;
                OptionBSelected = false;
                OptionCSelected = false;
                OptionASelectedHistory = false;
                OptionBSelectedHistory = false;
                OptionCSelectedHistory = false;
                CompleteSelected = false;
                CurrentDialogLevel = 0;
                Ref.SelectorSectionsFull[0] = false;
                Ref.SelectorSectionsFull[1] = false;
                Ref.SelectorSectionsFull[2] = false;
                break;
        }
    }

    void Stage3()
    {
        HandlePuzzleStuff(Ref.LevelFourPieceQueue, Ref.LevelFourExternalPieces);

        switch (CurrentDialogLevel)
        {
            case 0:
                if (VideoPlayer.frame > Ref.FrameRepeatRanges[15].First()) // show 3 items
                {
                    if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[15], Ref.DialogOptions[6]))
                    {
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 1:
                if (OptionASelected)
                {
                    SetNewVideo(Ref.VideoFiles[14], Ref.AudioFiles[14]); // Binoculars
                    OptionASelected = false;
                    OptionASelectedHistory = true;
                }
                else if (OptionBSelected)
                {
                    SetNewVideo(Ref.VideoFiles[15], Ref.AudioFiles[15]); // Cellphone
                    OptionBSelected = false;
                    OptionBSelectedHistory = true;
                }
                CurrentDialogLevel++;
                break;
            case 2:
                if (OptionASelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[16].First())
                    {
                        if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[16], Ref.DialogOptions[7]))
                        {
                            CurrentDialogLevel++;
                        }
                    }
                }
                if (OptionBSelectedHistory)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[17].First())
                    {
                        if (HandleOptionsWithLoop(Ref.FrameRepeatRanges[17], Ref.DialogOptions[8]))
                        {
                            CurrentDialogLevel++;
                        }
                    }
                }
                break;
            case 3:
                if (OptionASelectedHistory)
                {
                    if (OptionASelected)
                    {
                        SetNewVideo(Ref.VideoFiles[16], Ref.AudioFiles[16]);
                    }

                    if (OptionBSelected)
                    {
                        SetNewVideo(Ref.VideoFiles[17], Ref.AudioFiles[17]);
                    }
                }
                else
                {
                    if (OptionASelected)
                    {
                        SetNewVideo(Ref.VideoFiles[18], Ref.AudioFiles[18]);
                    }

                    if (OptionBSelected)
                    {
                        SetNewVideo(Ref.VideoFiles[19], Ref.AudioFiles[19]);
                    }
                }
                CurrentDialogLevel++;
                break;
            case 4:
                if (OptionASelectedHistory && OptionASelected)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[18].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                else if (OptionASelectedHistory && OptionBSelected)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[19].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                else if (OptionBSelectedHistory && OptionASelected)
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[20].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                else
                {
                    if (VideoPlayer.frame > Ref.FrameRepeatRanges[21].First())
                    {
                        InitializePuzzle();
                        CurrentDialogLevel++;
                    }
                }
                break;
            case 5:
                if (CompleteSelected)
                {
                    PuzzleCompleted();
                    PlayVideo();
                    CurrentDialogLevel++;
                }
                break;
            default:
                CurrentStage++;
                OptionASelected = false;
                OptionBSelected = false;
                OptionCSelected = false;
                OptionASelectedHistory = false;
                OptionBSelectedHistory = false;
                OptionCSelectedHistory = false;
                CompleteSelected = false;
                CurrentDialogLevel = 0;
                Ref.SelectorSectionsFull[0] = false;
                Ref.SelectorSectionsFull[1] = false;
                Ref.SelectorSectionsFull[2] = false;
                break;
        }
    }

    void Stage4()
    {
    }

    void Stage5()
    {
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

    bool OptionASelectedHistory = false;
    bool OptionASelected = false;
    void OptionAClicked()
    {
        OptionASelected = true;
    }

    bool OptionBSelectedHistory = false;
    bool OptionBSelected = false;
    void OptionBClicked()
    {
        OptionBSelected = true;
    }

    bool OptionCSelectedHistory = false;
    bool OptionCSelected = false;
    void OptionCClicked()
    {
        OptionCSelected = true;
    }

    bool OptionDSelectedHistory = false;
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
        this.addVideoCancelWhenReachedAlpha = 0.9f;
        InvokeRepeating("AddVideoAlpha", 0, 0.1f);

        puzzleInitialized = false;
        CompleteButton.SetActive(false);
        CompleteSelected = false;

        clonedObjects.ForEach(gameObject => Destroy(gameObject));
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
