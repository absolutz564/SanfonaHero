using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int Difficult = 0;
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public List<NoteObject> NotesA;
    public List<NoteObject> NotesS;
    public List<NoteObject> NotesD;
    public List<NoteObject> NotesJ;
    public List<NoteObject> NotesK;
    public BeatScroller theBSHard;
    public BeatScroller theBSEasy;
    // Start is called before the first frame update
    public AudioSource WrongSound;
    public static GameManager Instance;
    public GameObject Menu;
    public GameObject First;
    public GameObject popup; // Referência ao popup que você quer exibir
    public Text countdownText; // Texto dentro do popup para exibir a contagem regressiva
    public Slider slider; // Referência ao Slider que você quer controlar
    private float duration = 31f; // Duração em segundos para a animação
    public GameObject End;
    public Text EndScoreText;

    //public DontDestroy DDestroy;

    public int currentScore;
    public int scorePerNote;
    public int scorePerGoodNote;
    public int scorePerPerfectNote;

    public Text scoreText;
    public Text MultiText;

    public int currentMultiplier = 1;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    public RectTransform CanvasTransform;

    public void SetPlayAgain()
    {
        //DDestroy.isPlayAgain = true;
    }
    private IEnumerator AnimateSlider()
    {
        float currentTime = 0f;

        // Enquanto o tempo atual for menor que a duração, atualize o valor do Slider
        while (currentTime < duration)
        {
            float sliderValue = Mathf.Lerp(1f, 0f, currentTime / duration);
            slider.value = sliderValue;

            currentTime += Time.deltaTime;
            yield return null; // Espera o próximo frame
        }

        startPlaying = false;
        slider.value = 0f;
        yield return new WaitForSeconds(2);
        End.SetActive(true);
    }
    public void ShowPopupWithCountdown()
    {
        // Ativa o popup
        popup.SetActive(true);

        // Inicia a contagem regressiva
        StartCoroutine(CountdownCoroutine());
    }
    public Image countdownImage; // Referência ao componente Image que exibirá as imagens
    public Sprite[] countdownSprites;
    private IEnumerator CountdownCoroutine()
    {
        int countdownValue = 3;

        // Exibe a contagem regressiva na imagem
        countdownImage.sprite = countdownSprites[countdownValue - 1];

        // Aguarda 1 segundo antes de diminuir o contador
        yield return new WaitForSeconds(1f);

        countdownValue--;

        // Continua a contagem regressiva até chegar a 0
        while (countdownValue > 0)
        {
            countdownImage.sprite = countdownSprites[countdownValue - 1];
            yield return new WaitForSeconds(1f);
            countdownValue--;
        }

        // Aguarda mais 1 segundo antes de fechar o popup
        yield return new WaitForSeconds(1f);

        // Desativa o popup e ativa o jogo
        theBS.gameObject.SetActive(true); // Certifique-se de que este é o gameObject correto
        popup.SetActive(false);
        startPlaying = true;
        theBS.hasStarted = true;
        StartCoroutine(DelayToPlay());
        StartCoroutine(AnimateSlider());
    }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //if (DDestroy.isPlayAgain)
        //{
        //    First.SetActive(false);
        //    Menu.SetActive(true);
        //}
    }
    IEnumerator WaitToSetFirst()
    {
        yield return new WaitForSeconds(1);
    }

    public void GoToDifficulty()
    {
        Menu.SetActive(true);
        First.SetActive(false);
    }

    public void GoToPopup()
    {
        Menu.SetActive(false);
        ShowPopupWithCountdown();
    }

    public void SetDifficult(int value)
    {
        Difficult = value;
        if (Difficult == 0)
        {
            theBS = theBSEasy;
        }
        else
        {
            theBS = theBSHard;
        }
    }

    public void PlayWrongSound()
    {
        WrongSound.Play();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("SanfonaHero");
    }

    // Update is called once per frame
    public TMP_Text textDisplay; // Referência ao componente TextMeshPro
    private string currentText = ""; // Armazena o texto atual

    void Update()
    {
        // Verifica se qualquer tecla foi pressionada
        //if (Input.anyKeyDown)
        //{
        //    // Verifica cada tecla específica
        //    foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        //    {
        //        if (Input.GetKeyDown(kcode))
        //        {
        //            currentText += "Tecla pressionada: " + kcode.ToString() + "\n";
        //        }
        //    }
        //}

        //// Atualiza o texto no TextMeshPro
        //textDisplay.text = currentText;



        MultiText.text = "x" + currentMultiplier.ToString();
        scoreText.text = currentScore.ToString();
        EndScoreText.text = currentScore.ToString();
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton8) && End.activeSelf && !startPlaying)
        {
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9) && !startPlaying && !Menu.activeSelf && First.activeSelf)
        {
            GoToDifficulty();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton9) && !startPlaying && Menu.activeSelf && !popup.activeSelf)
        {
            SetDifficult(0);
            GoToPopup();
        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton8) && !startPlaying && Menu.activeSelf && !popup.activeSelf)
        {
            SetDifficult(1);
            GoToPopup();
        }
    }
    IEnumerator DelayToPlay()
    {
        yield return new WaitForSeconds(1.2f);
        theMusic.Play();
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteHit() {
        if (currentMultiplier -1 < multiplierThresholds.Length)
        {
            multiplierTracker++;

            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        //currentScore += scorePerNote * currentMultiplier;
    }

    public void NoteMissed() {
        currentMultiplier = 1;
        multiplierTracker = 0;
    }
}
