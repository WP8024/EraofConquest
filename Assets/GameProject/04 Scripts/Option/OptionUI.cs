using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class OptionUI : MonoBehaviour
{
    [SerializeField]
    InputField inputField;
    [SerializeField]
    Slider slider;

    [SerializeField]
    AudioSource _audioSource;
    AudioSource audioSource;
    [SerializeField]
    Slider audioSlider;
    public void Save()
    {
        PlayerPrefs.SetString("StringA", inputField.text);
        PlayerPrefs.SetFloat("SliderA", slider.value);

        PlayerPrefs.DeleteAll();//모든 데이터 삭제
        PlayerPrefs.DeleteKey("key"); //key값을 삭제
        PlayerPrefs.HasKey("key"); //key값 존재 여부 확인 bool형 
    }

    public void Load()
    {
        inputField.text = PlayerPrefs.GetString("StrignA");
        slider.value = PlayerPrefs.GetFloat("SliderA");
    }

    public void audioVolumeCon()
    {
        audioSource.volume = audioSlider.value;
    }
}

