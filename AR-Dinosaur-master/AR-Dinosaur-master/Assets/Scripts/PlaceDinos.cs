using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent(typeof(ARPlaneManager), typeof(ARRaycastManager))]
public class PlaceDinos : MonoBehaviour
{
    [SerializeField] public GameObject raptor;
    [SerializeField] public GameObject pachycephalosaurus;
    [SerializeField] public GameObject stegosaurus;

    public GameObject selectedDino;

    private ARRaycastManager aRraycastManager;
    private ARPlaneManager aRplaneManager;
    private List<ARRaycastHit> aRaycastHitList = new List<ARRaycastHit>();
   

    private void Awake()
    {
        aRraycastManager = GetComponent<ARRaycastManager>();
        aRplaneManager = GetComponent<ARPlaneManager>();

        selectedDino = raptor;
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }

    private void FingerDown(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;

        if(aRraycastManager.Raycast(finger.currentTouch.screenPosition, aRaycastHitList, TrackableType.PlaneWithinPolygon))
        {
            foreach(ARRaycastHit hit in aRaycastHitList)
            {
                Pose pose = hit.pose;
                GameObject obj = Instantiate(selectedDino, pose.position, pose.rotation);
            }
        }
    }

    // Functions to be called from button's onClick listeners
    public void SelectRaptor()
    {
        selectedDino = raptor;
    }

    public void SelectPachycephalosaurus()
    {
        selectedDino = pachycephalosaurus;
    }

    public void SelectStegosaurus()
    {
        selectedDino = stegosaurus;
    }

}
