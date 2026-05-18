using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelClearControler : MonoBehaviour
{
    [Tooltip("By Default, Loads MainMenu")]
    [SerializeField] int sceneToLoadWhenClicked = 1;
    [SerializeField] float textSlideInSpeed = 2;
    [SerializeField] float backgroundFadeInSpeed = 2;
    [SerializeField] float buttonFadeInSpeed = 2;
    [Header("References")]
    [SerializeField] Image backGroundFadeIn;
    [SerializeField] GameObject textSlideIn1;
    [SerializeField] GameObject textSlideIn2;
    [SerializeField] Image buttonFadeIn;
    static Canvas canvas;
    static bool runLevelClearAnimation = false;
    Vector2 textSlideIn1StartPosition;
    Vector2 textSlideIn2StartPosition;
    TextMeshProUGUI buttonTextFadeIn;
    static CanvasGroup canvasGroup;
    int animationStage = 0;
    void Start()
    {
        //component retrieval
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        buttonTextFadeIn = buttonFadeIn.GetComponentInChildren<TextMeshProUGUI>();
        //gets starting positions of text & sets their offsets
        textSlideIn1StartPosition = textSlideIn1.transform.position;
        textSlideIn2StartPosition = textSlideIn2.transform.position;
        textSlideIn1.transform.position = new Vector2(textSlideIn1.transform.position.x, textSlideIn1.transform.position.y + 700);
        textSlideIn2.transform.position = new Vector2(textSlideIn2.transform.position.x, textSlideIn2.transform.position.y - 700);
        //set UI elements to be transparent
        backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, 0);
        buttonFadeIn.color = new Color(1, 1, 1, 0);
        buttonTextFadeIn.color = new Color(1, 1, 1, 0);
        //makes canvas not interactable
        canvasGroup.interactable = false;

        //READ ME
        //Disables canvase on start. The canvas component NEEDS to be enabled, or position retrieval will return incorrect values.
        canvas.enabled = false;
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoadWhenClicked);
    }
    public static void TriggerLevelClearScrene()
    {
        //diables the pause menu from being opened
        PauseMenuControler.DisablePauseMenuOpening();
        canvas.enabled = true;
        Time.timeScale = 0;
        //starts animation
        runLevelClearAnimation = true;
        //makes canvas interactable
        canvasGroup.interactable = true;
    }
    private void Update()
    {
        LevelClearScreenAnimation();
    }
    void LevelClearScreenAnimation()
    {
        //The level clear screen animation is fully animated via code
        if (runLevelClearAnimation)
        {
            //animation stage variable is what determins what part of the animation is currently being played
            if (animationStage == 0)
            {
                //background fade in
                backGroundFadeIn.color = new Color(backGroundFadeIn.color.r, backGroundFadeIn.color.g, backGroundFadeIn.color.b, backGroundFadeIn.color.a + (backgroundFadeInSpeed * Time.unscaledDeltaTime));
                if (backGroundFadeIn.color.a >= 1)
                    animationStage++;
            }
            else if (animationStage == 1)
            {
                //"LEVEL" & "CLEAR" text slide in
                textSlideIn1.transform.position = Vector2.MoveTowards(textSlideIn1.transform.position, textSlideIn1StartPosition, textSlideInSpeed * Time.unscaledDeltaTime * 100);
                textSlideIn2.transform.position = Vector2.MoveTowards(textSlideIn2.transform.position, textSlideIn2StartPosition, textSlideInSpeed * Time.unscaledDeltaTime * 100);
                if (Vector2.Distance(textSlideIn1.transform.position, textSlideIn1StartPosition) == 0)
                    animationStage++;
            }
            else if (animationStage == 2)
            {
                //button & button text fade in
                Color currentColor = new Color(1, 1, 1, buttonFadeIn.color.a + buttonFadeInSpeed * Time.unscaledDeltaTime);
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
