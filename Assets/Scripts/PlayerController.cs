using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    Animator anim;
    public AudioMixerGroup MixerGroupMaster, MixerGroupMicrophone;
    public float playerY;
    public float loudness = 0;
    public float loudnessMax;

    public Slider slider;
    public Image Image;

    public Vector3 PlayerMove;
    private Rigidbody2D rb;
    public float _sensitivity = 100;
  [SerializeField]  TextMeshProUGUI text;
    public AudioSource _audio;
    [SerializeField] int timer;
    int time;

    bool isDie;
    bool isWin;

    [SerializeField] GameObject diePanel;
    [SerializeField] GameObject winPanel;
    private void Awake()
    {
        instance = this;

         anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        loudnessMax = PlayerPrefs.GetFloat("Loud",10f);
        slider.value = PlayerPrefs.GetFloat("Loud", 10f);
    }
        void Start()
    {
        Time.timeScale = 1;
        time = timer;
        text.text = time.ToString();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0))
        {
            _audio.Play();
        }
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        InvokeRepeating(nameof(decrease), 1, 1);
     
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        if (loudness > loudnessMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, playerY);
        }

        Image.fillAmount = loudness / 100;
    }
    void Update()
    {
        if (loudness > 0)
        
            _audio.outputAudioMixerGroup = MixerGroupMicrophone;
        
        else
        
            _audio.outputAudioMixerGroup = MixerGroupMaster;
        
        //
        loudness = GetAveragedVolume() * _sensitivity;
       
    }
    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
    void decrease()
    {

        time -= 1;
        text.text = time.ToString();
        if (time <= 0)
            Die();
    }

    public void Die()
    {
        if (isDie) return;

        
        CancelInvoke(nameof(decrease));
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        anim.SetTrigger("Die");
        diePanel.SetActive(true);
        isDie = true;


    }
   public void Win()
    {
        if (isWin) return;



        winPanel.SetActive(true);
        isWin = true;
    }
}
