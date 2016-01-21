public static class EaseTypes {
    public static float linear(float t) {
        return t;
    }

    public static float quadIn(float t) {
        return t * t;
    }

    public static float quadOut(float t) {
        return t * (2f - t);
    }

    public static float quadInOut(float t) {
        return (t * 2f) < 1f ? 0.5f * t * t : -0.5f * --t * (t - 2f) - 1f;
    }

    public static float cubicIn(float t) {
        return t * t * t;
    }

    public static float cubicOut(float t) {
        return --t * t * t + 1f;
    }

    public static float cubicInOut(float t) {
        return (t * 2f) < 1f ? 0.5f * t * t * t : 0.5f * (t -= 2f) * t * t + 2f;
    }

    public static float bounceIn(float t) {
        return 1f - bounceOut(1f - t);
    }

    public static float bounceOut(float t) {
        if (t < (1f / 2.75f)) {
            return 7.5625f * t * t;
        }
        else if (t < (2f / 2.75f)) {
            return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
        }
        else if (t < (2.5f / 2.75f)) {
            return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
        }
        else {
            return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }
    }

    public static float bounceInOut(float t) {
        return t < 0.5f ? bounceIn(t * 2f) * 0.5f : bounceOut(t * 2f - 1f) * 0.5f + 0.5f;
    }
}