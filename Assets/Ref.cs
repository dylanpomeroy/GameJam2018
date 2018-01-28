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
            new List<string>{ "Yes", "No", "Are you Kidding me?!" },

        };

        public static List<List<long>> FrameRepeatRanges = new List<List<long>>
        {
            new List<long>{ 730, 850 },
        };

        public static List<string> VideoFiles = new List<string>
        {
            Application.dataPath + "/video3.mp4",
            Application.dataPath + "/video.mp4",
            Application.dataPath + "/video2.mp4",

        };

        public static List<string> AudioFiles = new List<string>
        {
            "audio3",

        };
        
    }
}
