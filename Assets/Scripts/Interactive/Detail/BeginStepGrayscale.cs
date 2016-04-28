using Map;
using Path;
using System.Collections.Generic;
using UnityEngine;

namespace Interactive.Detail {

    public class BeginStepGrayscale : BeginStepGameBase
    {
        [SerializeField]
        private Shader grayscaleShader;

        private Dictionary<int, Shader> shaderDictionary = new Dictionary<int, Shader>(); 

        private Renderer[] renderers;
        private ITotem boss;
        private bool isActive = false;

        public override void StartStep()
        {
            renderers = FindObjectsOfType<Renderer>();
            SetGrayscale();
            boss = GameManager.Instance.Totems.Find(totem => totem.IsBoss);
            if (EndStep != null)
                EndStep();
        }

        private void SetGrayscale()
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].GetComponentInParent<BossTotem>() == null)
                {
                    MapObject mp = renderers[i].gameObject.GetComponent<MapObject>();

                    if (mp != null)
                    {
                        if (mp.Type != MapObjectType.LauncherNormal && mp.Type != MapObjectType.LauncherSticky && mp.Type != MapObjectType.BossJail)
                            ApplyShader(renderers[i], i);
                    }
                    else
                    {
                        ApplyShader(renderers[i], i);
                    }
                }
            }
            isActive  = true;
        }

        private void ApplyShader(Renderer renderer, int position)
        {
            shaderDictionary.Add(position, renderer.material.shader);
            renderer.material.shader = grayscaleShader;
        }

        private void Update()
        {
            if (isActive && boss.IsJailed)
            {
                isActive = false;
                DeactivateGrayScale();
            }
        }

        public void DeactivateGrayScale()
        {
            foreach (KeyValuePair<int, Shader> entry in shaderDictionary)
            {
                renderers[entry.Key].material.shader = entry.Value;
            }
        }
    }
}