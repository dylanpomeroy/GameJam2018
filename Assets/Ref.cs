using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public static class Ref
    {
        public static List<List<string>> DialogOptions = new List<List<string>>
        {
            new List<string>{ "Yes", "No", "Are you Kidding me?!" },

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
