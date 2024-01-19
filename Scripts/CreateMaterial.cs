using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateMaterial : MonoBehaviour
{
    public Camera renderCamera;
    public int textureWidth = 1024;
    public int textureHeight = 1024;

    void Start()
    {
        // 카메라 배경을 투명으로 설정
        renderCamera.clearFlags = CameraClearFlags.Depth;
        renderCamera.backgroundColor = new Color(0f, 0f, 0f, 0f);

        // 렌더 텍스쳐 생성
        RenderTexture rt = new RenderTexture(textureWidth, textureHeight, 24);
        renderCamera.targetTexture = rt;
        Texture2D renderTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);

        // 렌더링
        renderCamera.Render();
        RenderTexture.active = rt;
        renderTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        renderTexture.Apply();

        // 스프라이트로 변환
        Sprite sprite = Sprite.Create(renderTexture, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0.5f, 0.5f));
        Texture2D texture = sprite.texture;
        byte[] bytes = texture.EncodeToPNG();
        string path = "Assets/Resources/images/ItemIcon/SavedSprite.png";
        File.WriteAllBytes(path, bytes);
        Debug.Log("Sprite saved to Resources folder!");
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}

