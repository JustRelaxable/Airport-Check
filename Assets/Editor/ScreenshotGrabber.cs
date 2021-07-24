#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ScreenshotGrabber
{
    static int screenshotCounter = 0;
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        ScreenCapture.CaptureScreenshot($"Screenshot{screenshotCounter}.png", 1);
        screenshotCounter += 1;
    }
}
#endif