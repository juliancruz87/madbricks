namespace Assets.Scripts.Util {

    public class AlphaLerp {

        private float min;
        private float max;

        public AlphaLerp(float min, float max) {
            this.min = min;
            this.max = max;
        }

        public float GetValue(float alpha) {
            float value = ((max - min) * alpha) + min;
            return value;
        }

        public float GetAlpha(float value) {
            float alpha = (value - min) / (max - min);
            return alpha;
        }

        public static float GetValue(float min, float max, float alpha) {
            return new AlphaLerp(min, max).GetValue(alpha);
        }
    }
}