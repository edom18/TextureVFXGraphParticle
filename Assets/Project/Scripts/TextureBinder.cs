using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class TextureBinder : MonoBehaviour
{
    [SerializeField] private Texture _texture = null;
    [SerializeField] private float _duration = 1f;

    private VisualEffect _vfx = null;
    private bool _isStarted = false;
    private float _time = 0;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        SetupParticle();
    }

    private void Update()
    {
        if (_isStarted)
        {
            UpdateParticle();
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 130, 30), "Burst"))
        {
            BurstParticle();
        }

        if (GUI.Button(new Rect(10, 50, 130, 30), "Reset"))
        {
            ResetParticle();
        }
    }

    private void SetupParticle()
    {
        _vfx.SetInt("Width", _texture.width);
        _vfx.SetInt("Height", _texture.height);
        _vfx.SetTexture("ColorMap", _texture);
        _vfx.SetFloat("Duration", _duration);
    }

    private void UpdateParticle()
    {
        _time = Mathf.Clamp(_time + Time.deltaTime, 0, _duration);
        
        _vfx.SetFloat("Time", _time);
    }

    private void BurstParticle()
    {
        _isStarted = true;
    }

    private void ResetParticle()
    {
        _isStarted = false;
        _vfx.SetFloat("Time", 0);
        _vfx.SendEvent("OnPlay");
    }
}
