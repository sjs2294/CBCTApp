using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

/// SceneHandler is in charge of handling the necessary changes to go from the UI to the x-ray result and vise versa.
public class SceneHandler : MonoBehaviour
{
    /// The camera capturing the x-ray head.
    public Camera xRayCam;
    /// The camera capturing the front view of the head.
    public Camera mainCam;
    /// The camera capturing the side view of the head.
    public Camera sideCam;

    float viewPortX = 0.775f;
    float viewPortY = 0.025f;
    float viewPortW = 0.2f;
    float viewPortH = 0.2f;

    //the buttons that overlay ontop of the mini screen
    /// This button is overlayed on top of the mini view on the bottom right when the UI page is loaded.
    public GameObject xRayButtonCanvas;
    /// This button is overlayed on the left half of the mini view on the bottom right when the x-ray page is loaded.
    public GameObject mainButtonCanvas;
    /// This button is overlayed on the right half of the mini view on the bottom right when the x-ray page is loaded.
    public GameObject sideButtonCanvas;

    /// True if the x-ray scene is loaded. False if the UI scene is loaded.
    static public bool isXRay;

    /// Button to change into and out of AR mode.
    public GameObject ARButton;

    /// Reference to the FlipCamera script attached to the cameras.
    public FlipCamera flipScript;

    /// The XRay model of the head
    public GameObject XRayHead;

    /// The preview button controlling visibility 
    public Button preview; 
    /// Boolean to indicate if preview currently visible
    private bool show = true; 

    // Start is called before the first frame update
    void Start()
    {
        SetUIAsMainPage();
        Button btn = preview.GetComponent<Button>();
        btn.onClick.AddListener(ChangeMode);
    }

    // This function toggles whether the preview is visible
     private void ChangeMode() 
    {
        if (show) {
            show = false; 
            XRayHead.SetActive(false); 
        } else {
            show = true; 
            XRayHead.SetActive(true);
        }
    }

    /// 
    /// Performs the necessary actions to set the UI as the main page. It sets the xRayButtonCanvas
    /// to be true and the other buttons to be false. The viewport of the three cameras and their depths are 
    /// adjusted to show the UI cameras as the big screen and the others as the small overlayed screen.
    /// Deactivates scaling and tranlsation of XRay head in preview and sets visibility of preview to previous
    /// state. 
    ///
    public void SetUIAsMainPage()
    {
        isXRay = false;

        flipScript.resetView();

        xRayButtonCanvas.SetActive(true);

        mainButtonCanvas.SetActive(false);
        sideButtonCanvas.SetActive(false);

        // ARButton.GetComponent<ARHandler>().NonARMode();
        ARButton.SetActive(false);

        sideCam.rect = new Rect(0.5f, 0, 0.5f, 1);
        mainCam.rect = new Rect(0, 0, 0.5f, 1);

        xRayCam.rect = new Rect(viewPortX, viewPortY, viewPortW, viewPortH);

        xRayCam.tag = "Untagged";
        mainCam.tag = "MainCamera";

        sideCam.depth = 0;
        mainCam.depth = 0;
        xRayCam.depth = 2;

        // set visibility to what is was before main page was entered
        XRayHead.SetActive(show);
        // switch off pinch scaling and translating while in selection mode
        XRayHead.GetComponent<Lean.Touch.LeanPinchScale>().enabled = false; 
        XRayHead.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = false; 
    }
    /// 
    /// Performs the necessary actions to set the x-ray as the main page. It sets the xRayButtonCanvas
    /// to be false and the other buttons to be true. The viewport of the three cameras and their depths are 
    /// adjusted to show the xRay camera as the big screen and the others as the small overlayed screen.
    /// Activates scaling and translation of XRay head and ensures the model is visible. 
    ///
    public void SetXRayAsMainPage()
    {
        isXRay = true;

        flipScript.resetView();

        xRayButtonCanvas.SetActive(false);

        mainButtonCanvas.SetActive(true);
        sideButtonCanvas.SetActive(true);

        ARButton.SetActive(true);

        sideCam.rect = new Rect(viewPortX + viewPortW/2, viewPortY, viewPortW/2, viewPortH);
        mainCam.rect = new Rect(viewPortX, viewPortY, viewPortW/2, viewPortH);

        xRayCam.rect = new Rect(0,0,1,1);

        mainCam.tag = "Untagged";
        xRayCam.tag = "MainCamera";

        sideCam.depth = 2;
        mainCam.depth = 2;
        xRayCam.depth = 0;

        // make sure xRay head is visible
        XRayHead.SetActive(true); 
        // activate pinch scaling and drag translate to manipulate head
        XRayHead.GetComponent<Lean.Touch.LeanPinchScale>().enabled = true; 
        XRayHead.GetComponent<Lean.Touch.LeanDragTranslate>().enabled = true; 
    }


}
