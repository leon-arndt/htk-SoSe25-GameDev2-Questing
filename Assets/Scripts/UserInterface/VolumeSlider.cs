using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    [RequireComponent(typeof(Slider))]
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private string vcaPath = "vca:/VCA Name";
        [SerializeField] private TextMeshProUGUI valueText;
        
        private void Awake()
        {
            GetComponent<Slider>().onValueChanged.AddListener(SetVolume);
        }

        private void OnEnable()
        {
            var vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
            vca.getVolume(out var volume);
            GetComponent<Slider>().value = volume;
            valueText.text = $"{volume * 100:0}%";
        }

        private void OnDestroy()
        {
            GetComponent<Slider>().onValueChanged.RemoveListener(SetVolume);
        }

        private void SetVolume(float value)
        {
            var vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
            vca.setVolume(value);
            valueText.text = $"{value * 100:0}%";
        }
    }
}