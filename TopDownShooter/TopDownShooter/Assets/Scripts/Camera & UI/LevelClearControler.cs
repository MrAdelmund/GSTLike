using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelClearControler : MonoBehaviour
{
    [SerializeField] float textSlideInSpeed = 2;
    [SerializeField] float backgroundFadeInSpeed = 2;
    [SerializeField] float buttonFadeInSpeed = 2;
    [Header("References")]
    [SerializeField] Image backGroundFadeIn;
    [SerializeField] GameObject textSlideIn1;
    [SerializeField] GameObject textSlideIn2;
    [SerializeField] Image buttonFadeIn;
    static LevelClearControler SelfReference; //a workaround for not being able to call a non static funciton without an object reference
    static Canvas canvas;
    static bool runLevelClearAnimation = false;
    Vector2 textSlideIn1StartPosition;
    Vector2 textSlideIn2StartPosition;
    TextMeshProUGUI buttonTextFadeIn;
    int animationStage = 0;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        SelfReference = this;
        buttonTextFadeIn = buttonFadeIn.GetComponentInChildren<TextMeshProUGUI>();
        textSlideIn1StartPosition = textSlideIn1.transform.position;
        textSlideIn2StartPosition = textSlideIn2.transform.position;
        textSlideIn1.transform.position = new Vector2(textSlideIn1.transform.position.x, textSlideIn1.transform.position.y + 700);
        textSlideIn2.transform.position = new Vector2(textSlideIn2.transform.position.x, textSlideIn2.transform.position.y - 700);
        backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, 0);
        buttonFadeIn.color = new Color(1, 1, 1, 0);
        buttonTextFadeIn.color = new Color(1, 1, 1, 0);
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public static void TriggerLevelClearScrene()
    {
        PauseMenuControler.DisablePauseMenuOpening();
        canvas.enabled = true;
        Time.timeScale = 0;
        runLevelClearAnimation = true;
    }
    private void Update()
    {
        if (runLevelClearAnimation)
        {
            if (animationStage == 0)
            {
                Debug.Log(backGroundFadeIn.color.a);
                backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, backGroundFadeIn.color.a + (backgroundFadeInSpeed * Time.deltaTime));
                if (backGroundFadeIn.color.a >= 1)
                    animationStage++;
            }
            else if (animationStage == 1)
            {
                textSlideIn1.transform.position = Vector2.MoveTowards(textSlideIn1.transform.position, textSlideIn1StartPosition, textSlideInSpeed * Time.deltaTime);
                textSlideIn2.transform.position = Vector2.MoveTowards(textSlideIn2.transform.position, textSlideIn2StartPosition, textSlideInSpeed * Time.deltaTime);
                if (Vector2.Distance(textSlideIn1.transform.position, textSlideIn1StartPosition) == 0)
                    animationStage++;
            }
            else if (animationStage == 2)
            {
                Color currentColor = new Color(1, 1, 1, buttonFadeIn.color.a + (buttonFadeInSpeed * 0.01f) * Time.deltaTime);
                buttonFadeIn.color = currentColor;
                buttonTextFadeIn.color = currentColor;
                if (buttonFadeIn.color.a >= 1)
                    animationStage++;
            }
            //stops animation check when animation complete
            if (animationStage == 3)
                runLevelClearAnimation = false;
        }
    }
}
