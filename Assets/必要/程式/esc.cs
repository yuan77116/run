using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class esc : MonoBehaviour
{
    player���� e;
    ���� ����;
    private AudioSource aud;
    //
    public GameObject �����qui����;
    public GameObject esc����;
    public GameObject npc��ܵ���;
    public GameObject �G�Ƶ���;
    public GameObject �i�ܵP����;
    //
    private bool �}��UI;
    //
    private void Start()
    {
        aud = GameObject.Find("�I������").GetComponent<AudioSource>();
        ���� = GameObject.Find("�����I�޲z").GetComponent<����>();
        Time.timeScale = 1;
        e = GameObject.Find("�D��").GetComponent<player����>();
        //esc���� = GameObject.Find("�C�����]�w");
        //�i�ܵP���� = GameObject.Find("�i�ܵP����");
        //�G�Ƶ��� = GameObject.Find("�G�Ƶ���");
        //npc��ܵ��� = GameObject.Find("��ܵ���");
        //�����qui���� = GameObject.Find("��qUi");
        //
        if (esc���� != null)
            esc����.SetActive(false);
        if (�i�ܵP���� != null)
            �i�ܵP����.SetActive(false);
        if (�G�Ƶ��� != null)
            �G�Ƶ���.SetActive(false);
        if (npc��ܵ��� != null)
            npc��ܵ���.SetActive(false);
        if (�����qui���� != null)
            �����qui����.SetActive(true);
        //
        �}��UI = false;
    }
    private void Update()
    {
        ���Uesc();
    }
    //
    private void ���Uesc()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !�}��UI)
        {
            esc����.SetActive(true);
            �����qui����.SetActive(false);
            �}��UI = true;
            e.�i�ϥ�E = false;
            Time.timeScale=0;
            PlayerPrefs.SetFloat("���֮ɶ�", ����.data.���֮ɶ�);
            aud.Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && �}��UI)
        {
            esc����.SetActive(false);
            �i�ܵP����.SetActive(false);
            �G�Ƶ���.SetActive(false);
            if(npc��ܵ���!=null)
            npc��ܵ���.SetActive(false);
            �����qui����.SetActive(true);
            �}��UI = false;
            e.�i�ϥ�E = true;
            Time.timeScale = 1;
            aud.Play();
        }
    }
}
