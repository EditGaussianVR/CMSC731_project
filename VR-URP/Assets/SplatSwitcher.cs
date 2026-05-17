using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GaussianSplatting.Runtime;
using UnityEngine.XR;

public class SplatSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class SplatEntry
    {
        public string displayName;
        public GaussianSplatAsset asset;
    }

    [Header("Target")]
    public GaussianSplatRenderer splatRenderer;

    [Header("Splats")]
    public List<SplatEntry> splats = new List<SplatEntry>();

    [Header("UI (optional)")]
    public TMP_Text label;
    public GameObject labelContainer;
    public float labelDistance = 1.5f;
    public float labelVerticalOffset = 0.2f;

    [Header("Input")]
    [Tooltip("Minimum seconds between swaps (prevents accidental rapid-fire)")]
    public float swapCooldown = 0.5f;

    private int currentIndex = 0;
    private bool bWasPressed;
    private float lastSwapTime = -999f;
    private Camera cachedCam;
    private InputDevice rightController;

    void Start()
    {
        cachedCam = Camera.main;
        if (splatRenderer != null && splats.Count > 0)
            Load(0);
    }

    public void Load(int index)
    {
        if (splatRenderer == null || splats.Count == 0) return;

        int newIndex = Mathf.Clamp(index, 0, splats.Count - 1);
        var entry = splats[newIndex];
        if (entry.asset == null) return;

        if (splatRenderer.m_Asset == entry.asset)
        {
            currentIndex = newIndex;
            ShowLabel(entry.displayName, entry.asset.name);
            return;
        }

        currentIndex = newIndex;
        lastSwapTime = Time.time;

        splatRenderer.m_Asset = entry.asset;

        ShowLabel(entry.displayName, entry.asset.name);
    }

    public void Next()
    {
        if (Time.time - lastSwapTime < swapCooldown) return;
        Load((currentIndex + 1) % splats.Count);
    }

    public void Previous()
    {
        if (Time.time - lastSwapTime < swapCooldown) return;
        Load((currentIndex - 1 + splats.Count) % splats.Count);
    }

    void ShowLabel(string displayName, string fallbackName)
    {
        if (label != null)
            label.text = string.IsNullOrEmpty(displayName) ? fallbackName : displayName;

        if (labelContainer == null) return;

        if (cachedCam == null) cachedCam = Camera.main;

        if (cachedCam != null)
        {
            Vector3 pos = cachedCam.transform.position
                + cachedCam.transform.forward * labelDistance
                + cachedCam.transform.up * labelVerticalOffset;
            labelContainer.transform.position = pos;
            labelContainer.transform.rotation = Quaternion.LookRotation(cachedCam.transform.forward);
        }

        labelContainer.SetActive(true);
    }

    void Update()
    {
        if (!rightController.isValid)
            rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (rightController.isValid &&
            rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool bPressed))
        {
            if (bPressed && !bWasPressed) Next();
            bWasPressed = bPressed;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) Next();
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Previous();
    }
}