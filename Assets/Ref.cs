using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Ref
    {
        public static List<string> LevelOnePieceQueue = new List<string>
        {
            "PieceHorizontal",
            "PieceCornerLeftDown",
            "PieceCornerRightUp",
            "PieceHorizontal",
            "PieceHorizontal",
            "PieceVertical",
            "PieceCornerRightDown",
            "PieceCornerLeftUp",
            "PieceHorizontal",
        };
        public static List<string> LevelTwoPieceQueue = new List<string>
        {
            "PieceTLeft",
            "PieceVertical",
            "PieceVertical",
            "PieceCornerRightUp",
            "PieceCornerRightUp",
            "PieceCornerLeftDown",
            "PieceCornerLeftDown",
            "PieceCornerLeftDown",
            "PieceCornerRightDown",
            "PieceCornerRightUp",
            "PieceHorizontal"
        };
        public static List<string> LevelThreePieceQueue = new List<string>
        {
            "PieceTRight",
            "PieceVertical",
            "PieceHorizontal",
            "PieceCornerRightDown",
            "PieceCornerLeftDown",
            "PieceCornerLeftUp",
            "PieceHorizontal",
            "PieceCornerLeftUp",
            "PieceHorizontal",
            "PieceCornerRightDown"
        };
        public static List<string> LevelFourPieceQueue = new List<string>
        {
            "PieceTUp",
            "PieceCornerLeftDown",
            "PieceHorizontal",
            "PieceVertical",
            "PieceCornerRightUp",
            "PieceCornerLeftUp",
            "PieceCornerRightUp",
            "PieceVertical",
            "PieceHorizontal",
            "PieceCornerLeftDown",
            "PieceCornerRightDown",
            "PieceCornerLeftDown"
        };
        public static List<string> LevelFivePieceQueue = new List<string>
        {
            "PieceCornerRightUp",
            "PieceCornerLeftUp",
            "PieceTLeft",
            "PieceHorizontal",
            "PieceVertical",
            "PieceTRight",
            "PieceCornerLeftDown",
            "PieceCornerLeftDown",
            "PieceCornerRightUp",
            "PieceCornerRightDown",
            "PieceHorizontal"
        };
        public static List<string> LevelOneExternalPieces = new List<string>
        {
            "ExternalPiece1",
            "ExternalPiece9"
        };
        public static List<string> LevelTwoExternalPieces = new List<string>
        {
            "ExternalPiece1",
            "ExternalPiece7",
            "ExternalPiece11"
        };
        public static List<string> LevelThreeExternalPieces = new List<string>
        {
            "ExternalPiece1",
            "ExternalPiece6",
            "ExternalPiece11"
        };
        public static List<string> LevelFourExternalPieces = new List<string>
        {
            "ExternalPiece4",
            "ExternalPiece7",
            "ExternalPiece15"
        };
        public static List<string> LevelFiveExternalPieces = new List<string>
        {
            "ExternalPiece2",
            "ExternalPiece10"
        };

        public static List<bool> SelectorSectionsFull = new List<bool>
        {
            false,
            false,
            false
        };
        public static List<Vector3> SelectorPeicePlacements = new List<Vector3>
        {
            new Vector3(8.75f, 3.75f, 0),
            new Vector3(8.75f, 1.25f, 0),
            new Vector3(8.75f, -1.25f, 0),
        };
        
        public static List<List<string>> DialogOptions = new List<List<string>>
        {
            new List<string>{ "Something strage is going on", "I wonder what happens after the event horizon of a black hole?" },
            new List<string>{ "Kick him in the knee", "Haha, let me take a look at that" },
            new List<string>{ "Haha, let me take a look at that", "Touch the wall; feel it with passion" },
            new List<string>{ "Orange", "Blue", "Both" },
            new List<string>{ "Examine binoculars", "Examine cellphone", "Examine watch" },
            new List<string>{ "Check for texts", "Order pasta" },
            new List<string>{ "Offer throat soother", "Spray with anti-bacterial cleaning solution" },
            new List<string>{ "*Fake cough* You infected me!", "Give me your shoe please" },
            new List<string>{ "Thou hath been cleasned", "Get it together" },
            new List<string>{ "Approach", "Run away" }
        };

        public static List<List<long>> FrameRepeatRanges = new List<List<long>>
        {
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 }, // 5
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 }, // 10
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 }, // 15
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
            new List<long>{ 730, 850 },
        };

        public static List<string> VideoFiles = new List<string>
        {
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video3.mp4",
        };

        public static List<string> AudioFiles = new List<string>
        {
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
            "audio3",
        };
        
    }
}
