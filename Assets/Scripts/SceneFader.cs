using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
   public Action<FadeType> OnFadeIn;
   public Action<FadeType> OnFadeOut;
   
   public enum FadeType
   {
      Shutters,
      PlainBlack,
      Goop
   }
   
   public float FadeDuration = 1f;
   public FadeType CurrentFadeType;
   
   private int _fadeAmount = Shader.PropertyToID("_FadeAmount");
   
   private int _useShutters = Shader.PropertyToID("_UseShutters");
   private int _usePlainBlack = Shader.PropertyToID("_usePlainBlack");
   private int _useGoop = Shader.PropertyToID("_useGoop");

   private int? _lastEffect;

   private Image _image;
   private Material _material;

   private void OnEnable()
   {
      OnFadeIn += FadeIn;
      OnFadeOut += FadeOut;
   }

   private void OnDisable()
   {
      OnFadeIn -= FadeIn;
      OnFadeOut -= FadeOut;
   }

   private void Awake()
   {
      _image = GetComponent<Image>();
      Material mat = _image.material;
      _image.material = new Material(mat);
      _material = _image.material;

      _lastEffect = _useShutters;
   }

   public void FadeOut(FadeType fadeType)
   {
      ChangeFadeEffect(fadeType);
      StartFadeOut();
   }

   public void FadeIn(FadeType fadeType)
   {
      ChangeFadeEffect(fadeType);
      StartFadeIn();
   }

   public void ChangeFadeEffect(FadeType fadeType)
   {
      if (_lastEffect.HasValue)
      {
         _material.SetFloat(_lastEffect.Value, 0f);
      }

      switch (fadeType)
      {
         case FadeType.Goop:
            SwitchEffect(_useGoop);
            break;
         case FadeType.PlainBlack:
            SwitchEffect(_usePlainBlack);
            break;
         case FadeType.Shutters:
            SwitchEffect(_useShutters);
            break;
      }
   }

   private void SwitchEffect(int effectToTurnOn)
   {
      _material.SetFloat(effectToTurnOn, 1f);
      
      _lastEffect = effectToTurnOn;
   }

   private void StartFadeOut()
   {
      _material.SetFloat(_fadeAmount, 0f);

      StartCoroutine(HandleFade(1f, 0f));
   }
   private void StartFadeIn()
   {
      _material.SetFloat(_fadeAmount, 0f);

      StartCoroutine(HandleFade(0f, 1f));
   }

   private IEnumerator HandleFade(float targetAmount, float startAmount)
   {
      float elapsedTime = 0f;
      while (elapsedTime < FadeDuration)
      {
         elapsedTime += Time.deltaTime;
         
         float lerpedAmount = Mathf.Lerp(startAmount, targetAmount, elapsedTime / FadeDuration);
         _material.SetFloat(_fadeAmount, lerpedAmount);
         
         yield return null;
      }
      
      _material.SetFloat(_fadeAmount, targetAmount);
   }
}
