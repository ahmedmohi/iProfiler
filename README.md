# iProfiler
Moniter game stats and fps for your game , including memory usage and draw calls

Hi,

This is alittle profiling tools to help in monitering your game.

![alt tag](https://s30.postimg.org/6s86yeyq9/iprofiler.png)

It includes to 2 things:

- Resouce checker
https://www.assetstore.unity3d.com/en/#!/content/3224
I just added this tool , it's a must use to know how much your assets are using.
All credit goes to the awesome guy who made it and made it free.

- StatsMan
Its a script that you just put on your camera and will show you
  - Current FPS and frame time
  - Average FPS
  - GPU Memory Usage
  - Total Allocated Memory
  - Total Reserved Memory
  - Total UnusedReserved Memory
  For UnityEditor only It will show  DrawCalls , used texture memory , renderedTextureCount
  
  Its test on Android And IOS
  
  Note :
  All this stats you can easily get if you run the profiler on your target device
  But i wanted a faster way to always have the most important stats on my screen without the need to connect the profiler everytime and also needles to say the overhead that a companies connecting with profiler and older devices so its faster to have the stats this way.
  
  How to use :
  - Just add the script Stats man to your camera
  - For Resource Checker you will find it in  window -> Resource Checker
