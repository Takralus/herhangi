using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer musicmixer;
    [SerializeField] private Slider MusicSlider;

    public void SetMusicVolume()
    {
        float volume = MusicSlider.value;
        musicmixer.SetFloat("music", volume);
    }
}
