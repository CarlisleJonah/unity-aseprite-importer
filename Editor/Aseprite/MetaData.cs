using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Aseprite
{
    public enum MetaDataType { UNKNOWN, TRANSFORM };

    public class MetaData
    {
        static public string MetaDataChar = "@";

		static string[] MetaKeywords = new string[]
		{
			"@transform",
		};

		static public bool IsMetaLayer(string layerName)
		{
			foreach(var keyword in MetaKeywords)
			{
				if (layerName.StartsWith(keyword))
					return true;
			}
			return false;
		}

		public MetaDataType Type { get; private set; }
        //Average position per frames
        public Dictionary<int, Vector2> Transforms { get; private set; }
        public List<string> Args { get; private set; }

        public MetaData(string layerName)
        {
            var regex = new Regex("@transform\\(\"(.*)\"\\)");
            var match = regex.Match(layerName);
            if (match.Success)
            {
                Type = MetaDataType.TRANSFORM;
                Args = new List<string>();
                Args.Add(match.Groups[1].Value);
                Transforms = new Dictionary<int, Vector2>();
            }
            else
                Debug.LogWarning($"Unsupported aseprite metadata {layerName}");
        }
    }
}
