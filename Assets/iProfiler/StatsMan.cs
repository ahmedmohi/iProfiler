using UnityEngine;
using System.Collections;
using System.Text;
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
    StringBuilder tx;
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
        tx = new StringBuilder();
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

            tx.Length = 0;
            tx.Capacity = 0;

            tx.Append("Time : ").Append(ms.ToString("f1")).Append("ms   ")
            .Append("Current FPS : ").Append(fps.ToString("f2"))
            .Append("   AvgFps : ").Append(fpsav.ToString("f2")).Append('\n')
            .Append('\n').Append("GPU memory : ").Append(SystemInfo.graphicsMemorySize)
            .Append("   Sys Memory : ").Append(SystemInfo.systemMemorySize)
            .Append('\n').Append("TotalAllocatedMemory : ").Append(Profiler.GetTotalAllocatedMemory() / 1048576).Append("mb")
            .Append("   TotalReservedMemory : ").Append(Profiler.GetTotalReservedMemory() / 1048576).Append("mb")
            .Append("   TotalUnusedReservedMemory : ").Append(Profiler.GetTotalUnusedReservedMemory() / 1048576).Append("mb");
            
#if UNITY_EDITOR
            tx.Append("\nDrawCalls : ").Append(UnityStats.drawCalls).Append('\n')
            .Append("Used Texture Memory : ").Append(UnityStats.usedTextureMemorySize / 1024 / 1024).Append("mb").Append('\n')
            .Append("renderedTextureCount : ").Append(UnityStats.usedTextureCount);
            #endif

            gui.text = tx.ToString();
            frames = 0;
            lastInterval = timeNow;
        }

    }
}


