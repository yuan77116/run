using UnityEngine;

public class 隱藏路效果 : MonoBehaviour
{
    public AudioClip 進入音效;
    public AudioClip 出去音效;
    private AudioSource aud;
    private SpriteRenderer 圖;
    private bool 一次;
    private void Start()
    {
        aud = GetComponent<AudioSource>();
        圖 = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player"&& !一次)
        {
            一次 = true;
            圖.enabled = false;
            aud.PlayOneShot(進入音效, 0.8f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            一次 = false;
            圖.enabled = true;
            aud.PlayOneShot(出去音效, 0.8f);
        }
    }
}
