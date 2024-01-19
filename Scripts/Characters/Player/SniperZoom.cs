using UnityEngine;
using UnityEngine.UI;

public class SniperZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    
    [SerializeField] LayerMask Culling;
    [SerializeField] LayerMask ZoomCulling;

    [SerializeField] Image Crosshair;
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Image img3;
    [SerializeField] Image img4;
    [SerializeField] Image ZoomCrosshair;


    private void OnEnable()
    {
        mainCamera.cullingMask = ZoomCulling;

        if (Crosshair && ZoomCrosshair)
        {
            Crosshair.gameObject.SetActive(false);
            img1.gameObject.SetActive(false);
            img2.gameObject.SetActive(false);
            img3.gameObject.SetActive(false);
            img4.gameObject.SetActive(false);

            ZoomCrosshair.gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        mainCamera.cullingMask = Culling;
        if (Crosshair && ZoomCrosshair)
        {
            Crosshair.gameObject.SetActive(true);
            img1.gameObject.SetActive(true);
            img2.gameObject.SetActive(true);
            img3.gameObject.SetActive(true);
            img4.gameObject.SetActive(true);

            ZoomCrosshair.gameObject.SetActive(false);
        }
    }

}
