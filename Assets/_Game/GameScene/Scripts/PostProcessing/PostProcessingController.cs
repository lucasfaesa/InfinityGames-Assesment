using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private AnimationsEventChannelSO animationsEventChannel;
    
    [Header("Components")]
    [SerializeField] private VolumeProfile postProcessingVolumeProfile;

    private readonly float _effectDuration = 0.3f; 
    
    private readonly float _lensDistortionTargetIntensity = 0.8f; 
    private readonly float _chromaticAberrationTargetIntensity = 1f; 
    private readonly (float postExposure, float contrast, float saturation) _colorAdjustmentValues = (0.20f,100f,-100f); 
    private readonly float _filmGrainIntensity = 0.347f; 
    
    private LensDistortion _lensDistortion;
    private ChromaticAberration _chromaticAberration;
    private ColorAdjustments _colorAdjustments;
    private FilmGrain _filmGrain;
    
    private void OnEnable()
    {
        levelEventsChannel.LevelFinished += OnLevelFinished;
        levelEventsChannel.RequestToLoadNextLevel += ResetEffects;
    }

    private void OnDisable()
    {
        levelEventsChannel.LevelFinished -= OnLevelFinished;
        levelEventsChannel.RequestToLoadNextLevel -= ResetEffects;
        _filmGrain.intensity.value = 0f;
    }

    private void Start()
    {
        postProcessingVolumeProfile.TryGet<LensDistortion>(out _lensDistortion);
        postProcessingVolumeProfile.TryGet<ChromaticAberration>(out _chromaticAberration);
        postProcessingVolumeProfile.TryGet<ColorAdjustments>(out _colorAdjustments);
        postProcessingVolumeProfile.TryGet<FilmGrain>(out _filmGrain);

        _filmGrain.intensity.value = 0f;
    }

    private void OnLevelFinished()
    {
        animationsEventChannel.OnFinishLevelAnimationsStarted();
        animationsEventChannel.OnFinishLevelPostProcessingAnimationsStarted();
        
        LensDistortionAnimation();
    }

    private void LensDistortionAnimation()
    {
        //lens distortion
        StartCoroutine(LerpValueAndBack(_lensDistortion.intensity.value, _lensDistortionTargetIntensity, value =>
        {
            _lensDistortion.intensity.value = value;
        }));
        
        //chromatic aberration
        StartCoroutine(LerpValueAndBack(_chromaticAberration.intensity.value, _chromaticAberrationTargetIntensity, value =>
        {
            _chromaticAberration.intensity.value = value;
        }));
        
        //color adjustment
        StartCoroutine(LerpValueAndBack(_colorAdjustments.postExposure.value, _colorAdjustmentValues.postExposure, value =>
        {
            _colorAdjustments.postExposure.value = value;
        }));
        StartCoroutine(LerpValueAndBack(_colorAdjustments.contrast.value, _colorAdjustmentValues.contrast, value =>
        {
            _colorAdjustments.contrast.value = value;
        }));
        StartCoroutine(LerpValueAndBack(_colorAdjustments.saturation.value, _colorAdjustmentValues.saturation, value =>
        {
            _colorAdjustments.saturation.value = value;
        }));
        
        //film Grain
        _filmGrain.intensity.value = _filmGrainIntensity;

        StartCoroutine(WaitForEffectsToEnd());
    }

    private IEnumerator LerpValueAndBack(float valueToLerp, float targetValue, Action<float> value)
    {
        float elapsedTime = 0;
        float initialValue = valueToLerp;

        while (elapsedTime < _effectDuration)
        {
            elapsedTime += Time.deltaTime;
            valueToLerp = Mathf.Lerp(initialValue, targetValue, elapsedTime / _effectDuration);
            
            value?.Invoke(valueToLerp);
            yield return null;
        }

        elapsedTime = 0;
        initialValue = valueToLerp;

        while (elapsedTime < _effectDuration)
        {
            elapsedTime += Time.deltaTime;
            valueToLerp = Mathf.Lerp(initialValue, 0f, elapsedTime / _effectDuration);
            
            value?.Invoke(valueToLerp);
            yield return null;
        }
    }

    private IEnumerator WaitForEffectsToEnd()
    {
        yield return new WaitForSeconds(_effectDuration + 0.1f);
        
        animationsEventChannel.OnFinishLevelPostProcessingAnimationsEnded();
    }

    private void ResetEffects()
    {
        _filmGrain.intensity.value = 0f;
    }
}
