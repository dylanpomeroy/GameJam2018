using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Ref
    {
        public static List<string> LevelOnePieceQueue = new List<string>()
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

        public static List<bool> SelectorSectionsFull = new List<bool>
        {
            false,
            false,
            false
        };

        public static List<Vector3> SelectorPeicePlacements = new List<Vector3>
        {
            new Vector3(8.25f, 3.75f, -0.6f),
            new Vector3(8.25f, 1.25f, -0.6f),
            new Vector3(8.25f, -1.25f, -0.6f),
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
            Application.dataPath + "/audio.mp3",

        };
        
    }
}
