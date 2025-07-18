using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeonsGeheimeScripts
{
    [RequireComponent(typeof(Slider))]
    public class FinishedVolumeSliderView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private string vcaPath = "vca:/VCA Name";

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

        private void SetVolume(float volume)
        {
            valueText.text = $"{volume * 100:0}%";
            var vca = FMODUnity.RuntimeManager.GetVCA(vcaPath);
            vca.setVolume(volume);
        }
    }
}