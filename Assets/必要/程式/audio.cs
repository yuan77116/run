using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audio : MonoBehaviour
{
    public AudioMixer �I��AudioMixer;
    public AudioMixer �ĪGAudioMixer;
    public Slider �I������slider;
    public Slider ���ĭ���slider;
    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.GetFloat("�I�����q����", data.�I�����q����)!=0)
        {
            �I��AudioMixer.SetFloat("�I�����q", PlayerPrefs.GetFloat("�I�����q����", data.�I�����q����));
            �I������slider.value = PlayerPrefs.GetFloat("�I�����q����", data.�I�����q����);
        }
        if (PlayerPrefs.GetFloat("�I�ĭ��q����", data.�I�ĭ��q����) != 0)
        {
            �ĪGAudioMixer.SetFloat("�I�����q", PlayerPrefs.GetFloat("�I�ĭ��q����", data.�I�ĭ��q����));
            ���ĭ���slider.value = PlayerPrefs.GetFloat("�I�ĭ��q����", data.�I�ĭ��q����);
        }
    }
    public void �C�����q(float �ƭ�)
    {
        �I��AudioMixer.SetFloat("�I�����q", �ƭ�);
        data.�I�����q���� = �ƭ�;
        PlayerPrefs.SetFloat("�I�����q����", data.�I�����q����);
    }
    public void �ĪG���q(float �ƭ�)
    {
        �ĪGAudioMixer.SetFloat("�ĪG���q", �ƭ�);
        data.�I�ĭ��q���� = �ƭ�;
        PlayerPrefs.SetFloat("�I�ĭ��q����", data.�I�ĭ��q����);
    }
    [SerializeField]
    public playerdata data;
    [System.Serializable]
    public class playerdata
    {
        public float �I�����q���� = 0;
        public float �I�ĭ��q���� = 0;
    }
}
