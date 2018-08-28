//using System.Collections.Generic;
//using GoogleARCore;
//using UnityEngine;

//#if UNITY_EDITOR
//using Input = GoogleARCore.InstantPreviewInput;
//#endif

//public class GameManagerARTest : CustomMonoBehaviour
//{
//    public Camera FirstPersonCamera;

//    //public GameObject AndyAndroidPrefab;
//    public GameObject objects;

//    public GameObject DetectedPlanePrefab;

//    public GameObject SearchingForPlaneUI;

//    private const float k_ModelRotation = 180.0f;

//    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

//    private bool m_IsQuitting = false;

//    public void Update()
//    {
//        _UpdateApplicationLifecycle();

//        Session.GetTrackables(m_AllPlanes);
//        bool showSearchingUI = true;
//        for (int i = 0; i < m_AllPlanes.Count; i++)
//        {
//            if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
//            {
//                showSearchingUI = false;
//                break;
//            }
//        }

//        SearchingForPlaneUI.SetActive(showSearchingUI);

//        Touch touch;
//        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
//        {
//            return;
//        }

//        TrackableHit hit;
//        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
//            TrackableHitFlags.FeaturePointWithSurfaceNormal;

//        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
//        {
//            ShowObjects(hit);

//            //if ((hit.Trackable is DetectedPlane) &&
//            //    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
//            //        hit.Pose.rotation * Vector3.up) < 0)
//            //{
//            //    Debug.Log("Hit at back of the current DetectedPlane");
//            //}
//            //else
//            //{
//            //    var andyObject = Instantiate(AndyAndroidPrefab, hit.Pose.position, hit.Pose.rotation);

//            //    andyObject.transform.Rotate(0, k_ModelRotation, 0, Space.Self);

//            //    var anchor = hit.Trackable.CreateAnchor(hit.Pose);

//            //    andyObject.transform.parent = anchor.transform;
//            //}
//        }
//    }

//    void ShowObjects(TrackableHit hit)
//    {
//        objects.SetActive(true);
//        objects.transform.position = hit.Pose.position;

//        var anchor = hit.Trackable.CreateAnchor(hit.Pose);

//        if ((hit.Flags & TrackableHitFlags.PlaneWithinPolygon) != TrackableHitFlags.None)
//        {
//            Vector3 cameraPositionSameY = FirstPersonCamera.transform.position;
//            cameraPositionSameY.y = hit.Pose.position.y;

//            objects.transform.LookAt(cameraPositionSameY, objects.transform.up);
//        }

//        objects.transform.parent = anchor.transform;
//    }

//    private void _UpdateApplicationLifecycle()
//    {
//        if (Input.GetKey(KeyCode.Escape))
//        {
//            Application.Quit();
//        }

//        if (Session.Status != SessionStatus.Tracking)
//        {
//            const int lostTrackingSleepTimeout = 15;
//            Screen.sleepTimeout = lostTrackingSleepTimeout;
//        }
//        else
//        {
//            Screen.sleepTimeout = SleepTimeout.NeverSleep;
//        }

//        if (m_IsQuitting)
//        {
//            return;
//        }

//        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
//        {
//            _ShowAndroidToastMessage("Camera permission is needed to run this application.");
//            m_IsQuitting = true;
//            Invoke("_DoQuit", 0.5f);
//        }
//        else if (Session.Status.IsError())
//        {
//            _ShowAndroidToastMessage("ARCore encountered a problem connecting.  Please start the app again.");
//            m_IsQuitting = true;
//            Invoke("_DoQuit", 0.5f);
//        }
//    }

//    private void _DoQuit()
//    {
//        Application.Quit();
//    }

//    private void _ShowAndroidToastMessage(string message)
//    {
//        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

//        if (unityActivity != null)
//        {
//            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
//            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
//            {
//                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
//                    message, 0);
//                toastObject.Call("show");
//            }));
//        }
//    }
//}