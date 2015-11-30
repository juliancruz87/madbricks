using System;
using Assets.Scripts.Util;
using UnityEngine;

namespace Interactive.Detail
{
    [RequireComponent(typeof(GUITexture))]
    public class BeginStepScreenFader : BeginStepGameBase {

        [SerializeField]
        private float fadeTime = 5.0f;
        [SerializeField]
        private float elapsedTime = 0;
        [SerializeField]
        private float starterAlpha = 0;
        [SerializeField]
        private float endAlpha = 0;

        private GUITexture fadeTexture;
        private AlphaLerp timeAlphaLerp;
        private AlphaLerp fadeAlphaLerp;
        private bool stopped;

        private void Awake() {
            StartStep();
        }

        public override void StartStep() {
            elapsedTime = 0;
            InitFadeTexture();
            InitAlphaLerps();
            stopped = false;
        }

        private void Update() {
            if (!stopped)
                Fade();
        }

        private void InitFadeTexture() {
            fadeTexture = GetComponent<GUITexture>();
            fadeTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
            SetTextureAlpha(starterAlpha);
        }

        private void InitAlphaLerps()
        {
            timeAlphaLerp = new AlphaLerp(0, fadeTime);
            fadeAlphaLerp = new AlphaLerp(starterAlpha, endAlpha);
        }

        private void Fade()
        {
            elapsedTime += Time.deltaTime;

            float currentAlpha = timeAlphaLerp.GetAlpha(elapsedTime);

            if (currentAlpha >= 1)
            {
                currentAlpha = 1;
                stopped = true;
            }

            float fadeAlpha = fadeAlphaLerp.GetValue(currentAlpha);

            SetTextureAlpha(fadeAlpha);

            if (stopped &&
                EndStep != null)
                EndStep();
        }

        private void SetTextureAlpha(float fadeAlpha)
        {
            Color newTextureColor = fadeTexture.color;
            newTextureColor.a = fadeAlpha;
            fadeTexture.color = newTextureColor;
        }
    }
}