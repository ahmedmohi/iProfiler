using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
#if UNITY_5
using UnityEngine.Profiling;
#endif
//-----------------------------------------------------------------------------------------------------
public class StatsMan : MonoBehaviour
{

    public Color tx_Color = Color.white;

    GUIText gui;

    float updateInterval = 1.0f;
    float lastInterval; // Last interval end time
    float frames = 0; // Frames over current interval

    float framesavtick = 0;
    float framesav = 0.0f;

    // Use this for initialization
    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
        framesav = 0;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void OnDisable()
    {
        if (gui)
            DestroyImmediate(gui.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        ++frames;

        var timeNow = Time.realtimeSinceStartup;

        if (timeNow > lastInterval + updateInterval)
        {
            if (!gui)
            {
                GameObject go = new GameObject("FPS Display", typeof(GUIText));
                go.hideFlags = HideFlags.HideAndDontSave;
                go.transform.position = new Vector3(0, 0, 0);
                gui = go.GetComponent<GUIText>();
                gui.color = tx_Color;
                gui.pixelOffset = new Vector2(15, Screen.height - 15);
            }

            float fps = frames / (timeNow - lastInterval);
            float ms = 1000.0f / Mathf.Max(fps, 0.00001f);

            ++framesavtick;
            framesav += fps;
            float fpsav = framesav / framesavtick;

            gui.text = "Time : " + ms.ToString("f1") + "ms   " + "Current FPS : " + fps.ToString("f2") + "   AvgFps : " + fpsav.ToString("f2") +
            '\n';
            gui.text += '\n' + "GPU memory : " + SystemInfo.graphicsMemorySize + "   Sys Memory : " + SystemInfo.systemMemorySize;

            gui.text += '\n' + "TotalAllocatedMemory : " + Profiler.GetTotalAllocatedMemory() / 1048576 + "mb" + "   TotalReservedMemory : " + Profiler.GetTotalReservedMemory() / 1048576 + "mb" + "   TotalUnusedReservedMemory : " + Profiler.GetTotalUnusedReservedMemory() / 1048576 + "mb";
            
#if UNITY_EDITOR
            gui.text += "\nDrawCalls : " + UnityStats.drawCalls +
            '\n' +
            "Used Texture Memory : " + UnityStats.usedTextureMemorySize / 1024 / 1024 + "mb" +
            '\n' +
            "renderedTextureCount : " + UnityStats.usedTextureCount;
            #endif

            frames = 0;
            lastInterval = timeNow;
        }

    }
}


