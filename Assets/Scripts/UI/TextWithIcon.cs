using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextWithIcon : Text
    {
        private List<Image> icons;
        private List<Vector3> positions = new List<Vector3>(); 
        private float _fontHeight;
        private float _fontWidth;
        public float ImageScalingFactor;
        
        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            base.OnPopulateMesh(toFill);
            List<UIVertex> vbo = new List<UIVertex>();
            toFill.GetUIVertexStream(vbo);

            positions.Clear();

            var indexes = getIndexes(this);
			icons = GetComponentsInChildren<Image>(true).ToList();

            for (var y = 0; y < indexes.Count; y++)
            {
                var vector3s = new Vector3[6];
                var startVertexIndex = indexes[y]*6;
                var endVertexIndex = startVertexIndex + 6;

                var j = 0;
                for (var i = startVertexIndex; i < endVertexIndex; i++)
                {
                    vector3s[j] = vbo[i].position;
                    j++;
                    if (j == 6) j = 0;
                }

                positions.Add(CenterOfVectors(vector3s));
                _fontHeight = Vector3.Distance(vector3s[0], vector3s[4]);
                _fontWidth = Vector3.Distance(vector3s[0], vector3s[1]);
            }
        }
			
        private void Update()
        {
			DeactivateImages();

			for (var i = 0; i < positions.Count; i++)
            {
				icons[i].gameObject.SetActive (true);
                icons[i].rectTransform.anchoredPosition = positions[i];
                icons[i].rectTransform.sizeDelta = new Vector2(_fontWidth * ImageScalingFactor, _fontHeight * ImageScalingFactor);

            }
				
        }
			
		private void DeactivateImages()
		{
			foreach (var icon in icons) 
				icon.gameObject.SetActive (false);
		}

        private Vector3 CenterOfVectors(Vector3[] vectors)
        {
            Vector3 sum = Vector3.zero;
            if (vectors == null || vectors.Length == 0)
            {
                return sum;
            }

            foreach (Vector3 vec in vectors)
            {
                sum += vec;
            }
            return sum / vectors.Length;
        }

        private List<int> getIndexes(Text textObject)
        {
            var indexes = new List<int>();
			foreach (Match match in Regex.Matches(textObject.text, @"\$"))
            {
                indexes.Add(match.Index);
            }
            return indexes;
        }
			
    }
}
