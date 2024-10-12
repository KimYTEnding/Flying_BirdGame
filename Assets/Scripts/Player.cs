using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Gravity = 10f;
    public float Accel = 10f;
    float v = 0;

    public AudioClip UpSound;
    public AudioClip DownSound;


    void Start()
    {
        v = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(UpSound);
        }
        if(Input.GetButtonUp("Jump"))
        {
            GetComponent<AudioSource>().PlayOneShot(DownSound);
        }
        if (Input.GetButton("Jump"))
        {
            v -= Accel * Time.deltaTime;
        }
        else
        {
            v += Gravity * Time.deltaTime;
        }

    }

    private void FixedUpdate() // 똑같은 시간으로 호출되는 것, 물리적인 계산을 넣을 때 사용
    {//물리 연산이 돌아가는 부분
        transform.Translate(Vector2.down * v * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            int score = (int)GameManager.Instance.Score;
            PlayerPrefs.SetInt("Score", score);
            SceneManager.LoadScene("GameOverScene");
        }
    }
}