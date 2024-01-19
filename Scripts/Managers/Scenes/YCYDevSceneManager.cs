using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YCYDevSceneManager : BaseSceneManager
{
    protected override bool Init()
    {
        if (!base.Init()) return false;

        SceneType = Scenes.YCYDevScene;

        return true;
    }

    public override void Clear()
    {
    }
}
