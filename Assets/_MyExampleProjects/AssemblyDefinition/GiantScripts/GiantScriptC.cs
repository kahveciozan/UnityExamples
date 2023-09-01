using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using TMPro;
//using UnityEngine.ResourceManagement.AsyncOperations;
//using UnityEngine.ResourceManagement.ResourceProviders;
//using UnityEngine.AddressableAssets;
namespace GiantNamespace
{

    public class GiantScriptC : MonoBehaviour
    {
        // Panels
        [Header("PANELS")]
        [SerializeField] GameObject introPanel;
        [SerializeField] private GameObject finishRatingPanel;
        [SerializeField] GameObject pausePanel;
        [SerializeField] GameObject resultPanel;
        [SerializeField] RectTransform panelBottomUI;
        [SerializeField] GameObject ratingScenePanel;

        // Breathing Tutorial
        [Header("Breathing Introduction")]
        [SerializeField] GameObject panelBreathingTutorial;
        [SerializeField] GameObject panel1;
        [SerializeField] GameObject panel2;

        // External Scene Variables
        [Header("External Scene Variables")]
        private bool isExternalSceneTimer;
        private float externalSceneTimer = 0;
        private float externalSceneMin = 1f;

        // OVR Camera Rig Objects
        [Header("OVR Cam Objects")]
        [SerializeField] private GameObject centerEye;

        // UI Animation Objects
        [Header("UI Animation Objects")]
        public List<GameObject> laylaLogoSprites;
        public ParticleSystem laylaParticleSytem;
        public RectTransform goodafttVsImage;
        public RectTransform immerzaLogoSeperateParent;
        public RectTransform panelCategoriesParent;
        public List<RectTransform> categories;
        public GameObject focusGameObj;   // For Testing. This will be delete after new PC.

        // Internal Scene Loading Variables
        private AsyncOperation asyncLoad;
        //private AsyncOperationHandle<SceneInstance> handle;
        public static bool isInternalScene = false;

        //DontDestroy Objects
        public GameObject panelLoading;
        public Image loadingBar;
        public GameObject panelMoodTracker;
        private int downloadProgressInput;

        public List<GameObject> moods;              // ---- ---- ---- ---- ---- ---- ----MOODS

        public static string beforeMood;
        public static string afterMood;
        public static string beforeRating;              // - -- - - - - - - - - - - -MOODSSSSSS
        public static int afterRating;
        private bool isClickedRatingB = false;
        private bool isClickedMoodB = false;
        private bool isClickedRatingA = false;
        private bool isClickedMoodA = false;
        private bool isFinishMoofTracker = false;

        //public ParticleSystem moodTrackerParticle;

        //public TextMeshProUGUI beforeMood1;
        //public TextMeshProUGUI beforeNumber1;

        [Header("Buttons Silder")]
        //public RectTransform panelSliding;
        public RectTransform rightButton_F, rightButton_C, rightButton_E;
        public RectTransform leftButton_F, leftButton_C, leftButton_E;

        [SerializeField] GameObject breathingDetectionObject;
        [SerializeField] bool isBreathingDetectiohnActiveOnScene = true;
        [SerializeField] Toggle toogleBreathingDetection;
        [SerializeField] Toggle toogleBreathingDetectionPause;
        [SerializeField] GameObject virtualBelly;

        [Header("Other Scripts")]
        [SerializeField] private GameObject authhenticationManager;
        public GameObject jsonManagerSlider;
        public GameObject cameraChooser; // 563, 564

        #region POST DATA 
        // Scene's variables for POST data
        [HideInInspector] public static string sceneImageURL;
        [HideInInspector] public static string sceneID;
        [HideInInspector] public static string sceneDescription;
        [HideInInspector] public static string categoryNameForMood;
        [HideInInspector] public static string sceneNameForMood;
        #endregion

        //[SerializeField] private HideManager hideManagerScript;
        //[SerializeField] private FindAndAttachHandCanvas findAndAttachHand;
        //[SerializeField] private FindAndAttachCornerCanvas findAndAttachCorner;

        // Slider Values
        private int sliderMaxValue = 1000;
        [HideInInspector] public int sliderMinValue = 600;

        // External Scene Breathing Exercise
        [Header("External Breathing Exercises")]
        [SerializeField] private List<GameObject> breathingExerciseList = new List<GameObject>();

        private bool isBreathFix = true;

        // Start is called before the first frame update
        void Start()
        {
            rightButton_F.DOScale(0f, 0.1f); rightButton_C.DOScale(0f, 0.1f); rightButton_E.DOScale(0f, 0.1f);
            leftButton_F.DOScale(0f, 0.1f); leftButton_C.DOScale(0f, 0.1f); leftButton_E.DOScale(0f, 0.1f);

            PlayerPrefs.SetInt("FinishGame", 0);

            //SoundManager.instance.LoadingSound1();

            DontDestroyOnLoad(this.gameObject);
            InitStartConfig();

            //StartCoroutine(DenemeMetodu());                                                              // TEST 1 - ANA MENU ICIN (Default : Pasif)

            panelBottomUI.DOScaleX(0f, 0.2f);
        }

        private void LateUpdate()
        {
            if (isExternalSceneTimer)
            {
                externalSceneTimer += Time.deltaTime;

                if (externalSceneTimer >= externalSceneMin * 60f)
                {
                    isExternalSceneTimer = false;
                    PlayerPrefs.SetInt("FinishGame", 1);
                    Debug.Log("FINISHED EXTERNAL SCENES");
                }
            }


            #region isSceneFinished
            // Yüklenen Sahnenin bittigini kontrol ediyor
            if (PlayerPrefs.GetInt("FinishGame") == 1)
            {

                // If user exit before 60 seconds from scene
                /*

                if (!authhenticationManager.isSixtySecond) 
                    authhenticationManager.BreathDetectionBOnClick();

                authhenticationManager.BreathDetectionAOnClick();

                */

                if (isBreathFix)
                {
                    isBreathFix = false;
                    MyBreathDayaFixLater();
                }




                // OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
                //PlayerPrefs.SetInt("FinishGame", 0);
                StartCoroutine(FinishConfig());

            }
            #endregion


            //SliderWithJoystick();
            PauseAndResume();
        }

        private void MyBreathDayaFixLater()
        {
            /*
            if (!authhenticationManager.isSixtySecond)
                authhenticationManager.BreathDetectionBOnClick();

            authhenticationManager.BreathDetectionAOnClick();
            */
        }

        IEnumerator FinishConfig()
        {
            Debug.Log("-F1-");
            //centerEye = GameObject.FindWithTag("MainCamera");
            //centerEye = GameObject.FindGameObjectWithTag("MainCamera");
            centerEye = GameObject.Find("CenterEyeAnchor");
            Debug.Log("-F2-");
            yield return new WaitUntil(() => centerEye != null);
            Debug.Log("CENTER EYE ANCHOR:" + centerEye.gameObject.name);
            //yield return new WaitForSeconds(5f);
            Debug.Log("-F2.1-");

            isBreathingDetectiohnActiveOnScene = false;
            Debug.Log("-F2.2-");
            BreathingDetectionActivation();                                         // -- BD Activation
            Debug.Log("-F2.3-");
            //OVRScreenFade.instance.FadeIn();
            Debug.Log("-F2.4-");
            centerEye.GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
            Debug.Log("-F3-");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("-F4-");


            isFinishMoofTracker = true;
            OpenFinishRatingPanel();
            PlayerPrefs.SetInt("FinishGame", 0);

        }

        private void InitStartConfig()
        {
            panelCategoriesParent.localScale = Vector2.zero;
            StartCoroutine(LaylaLogoAnimation());
        }

        //Loading image dissappear AND Layla Logo Image appear slowly
        IEnumerator LaylaLogoAnimation()
        {

            foreach (GameObject l in laylaLogoSprites)
            {
                yield return new WaitForSeconds(0.4f);
                //l.GetComponent<Image>().DOFade(0f, 0.32f);
            }

            laylaParticleSytem.Play();

            foreach (GameObject l in laylaLogoSprites)
            {
                yield return new WaitForSeconds(0.4f);
                //l.GetComponent<Image>().DOFade(1f, 0.3f);
            }

            panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y + 3000f);
            panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector2(panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y + 3000f);
            panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y + 3000f);
            panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.y + 3000f);

            panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().DOScale(0, 0.1f);
            panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().DOScale(0, 0.1f);
            panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().DOScale(0, 0.1f);
            panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().DOScale(0, 0.1f);

            yield return new WaitForSeconds(1f);
            CategoriesAnimations();
        }

        private void CategoriesAnimations()
        {
            //panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().DOAnchorPos(new Vector2(panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y - 3000f), 2f); // LOGIN
            //panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition.y - 3000f), 1f); // FOCUS
            //panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().DOAnchorPos(new Vector2(panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition.y - 3000f), 2f); // CALM
            //panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().DOAnchorPos(new Vector2(panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.x, panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition.y - 3000f), 2.5f); // EXCITEMENT

            panelCategoriesParent.transform.GetChild(0).GetComponent<RectTransform>().DOScale(1, 1f);       // LOGIN PANEL
            panelCategoriesParent.transform.GetChild(1).GetComponent<RectTransform>().DOScale(1.1765f, 1f); // FOCUS
            panelCategoriesParent.transform.GetChild(2).GetComponent<RectTransform>().DOScale(1.1765f, 2f); // CALM
            panelCategoriesParent.transform.GetChild(3).GetComponent<RectTransform>().DOScale(1.1765f, 2.5f); // EXCITEMENT

            //SoundManager.instance.AnimationSoundFX();
            //laylaLogo.DOScale(0.7f, 2f);
            //laylaLogo.DOAnchorPos(new Vector3(0, 400f, 0), 2f);     // -637.2     285.1
            immerzaLogoSeperateParent.DOScale(0.5f, 3f);
            //immerzaLogoSeperateParent.DOAnchorPos(new Vector3(-15, 244f, 0f), 2.1f).OnComplete(()=>immerzaLogoSeperateParent.DOAnchorPos(new Vector3(-474f, 254f, 0f), 1.2f).OnComplete(() => immerzaLogoSeperateParent.DOScale(0.67f, 1f)));     // ROBOTIC MOVEMENT
            //immerzaLogoSeperateParent.DOAnchorPos(new Vector3(-394.6f, 172f, 0f), 1.2f).OnComplete(() => immerzaLogoSeperateParent.DOScale(0.67f, 1f));



            panelCategoriesParent.DOScale(new Vector3(1f, 1f, 1f), 3f);                                             //REEL (Default : Aktif)
                                                                                                                    //panelCategoriesParent.DOScale(new Vector3(1f,1f,1f), 3f).OnComplete(CategoriesAnimationsTEST);          //TEST 2 - KAtegori icin (Default : Pasif)
        }

        /* ------------------------------------------------------  Select Category OnClick--------------------------------------------------- */
        public void SelectCategory()
        {
            panelBottomUI.DOScaleX(1f, 0.5f);

            goodafttVsImage.DOScale(0f, 1f);
            immerzaLogoSeperateParent.DOScale(1.2f, 3f);
            //immerzaLogoSeperateParent.DOAnchorPosX(0, 1f).OnComplete(()=> immerzaLogoSeperateParent.DOAnchorPosY(500f, 1f));          //ROBOTIC MOVEMENT
            //immerzaLogoSeperateParent.DOAnchorPos(new Vector2(0f, 500f), 1f);

            // TO DO Cliced SOUND
            GameObject tempButton = EventSystem.current.currentSelectedGameObject;         // Get the selected button's Game Object
            categoryNameForMood = tempButton.name;
            foreach (RectTransform r in categories)
            {
                //r.GetComponent<InteractLevelSelect>().enabled = false;
                r.GetComponent<Button>().interactable = false;

                if (tempButton.name == r.name)
                {
                    r.GetComponent<RectTransform>().DOScale(0, 0.7f).OnComplete(() => OpenSelectedSceneMenu(tempButton));
                    //SoundManager.instance.ChoosingSoundFX();
                }
                else
                {
                    r.GetComponent<RectTransform>().DOScale(0, 0.7f).OnComplete(() => r.gameObject.SetActive(false));
                }
            }
        }

        public void OpenSelectedSceneMenu(GameObject selectedCategory)
        {
            //rightButton.DOScale(1f, 1f); 
            //leftButton.DOScale(1f, 1f);

            selectedCategory.transform.GetChild(5).GetComponent<RectTransform>().DOScale(1f, 1f);
            selectedCategory.transform.GetChild(6).GetComponent<RectTransform>().DOScale(1f, 1f);

            selectedCategory.GetComponent<Image>().enabled = false;                               //30122022
            selectedCategory.transform.GetChild(4).GetComponent<Image>().enabled = false;         //30122022
            selectedCategory.transform.GetChild(0).gameObject.SetActive(false);                     // Category Name Text
                                                                                                    //selectedCategory.GetComponent<RectTransform>().Rotate(0, 25, 0);    // <------------------------------------------ Rotate

            selectedCategory.GetComponent<RectTransform>().DOScale(1, 0.5f);                                          //REEL (Default : Aktif)
                                                                                                                      //selectedCategory.GetComponent<RectTransform>().DOScale(1, 1f).OnComplete(SceneSelectTEST);          //TEST 3 - Sahne Yukleme (Default : Pasif)
                                                                                                                      //selectedCategory.GetComponent<RectTransform>().DOScale(1, 1f).OnComplete(AddreesableSceneTEST);         // ADDRESSABLE TEST

            RectTransform[] tempScenes = selectedCategory.transform.GetChild(1).GetComponentsInChildren<RectTransform>();

            //ACTIVETE SCENES
            selectedCategory.transform.GetChild(1).gameObject.SetActive(true);
            SceneAnimation(selectedCategory);
        }

        private void SceneAnimation(GameObject selectedCategory)
        {

            for (int i = 1; i < selectedCategory.transform.GetChild(1).GetChild(0).childCount; i++)
            {
                Vector2 zeroPos = selectedCategory.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>().anchoredPosition;
                float tempPos = selectedCategory.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<RectTransform>().anchoredPosition.x;

                selectedCategory.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<RectTransform>().anchoredPosition = zeroPos;

                //selectedCategory.transform.GetChild(1).GetChild(0).GetChild(i).GetComponent<RectTransform>().DOAnchorPosX(tempPos, (0.5f + (i - i * 9 / 10)));
            }
        }

        public void SceneOnClick()
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
            //categoryNameForMood = clickedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            clickedObject.transform.GetChild(6).gameObject.SetActive(true);
            panelBottomUI.DOScaleX(1f, 0.5f);
        }

        #region Internal Scene Loading
        // Internal Scene Loading OnClick
        public void SelectSceneInternal(string sceneName)
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
            //sceneID = clickedObject.transform.GetChild(1).GetComponent<>().text;
            //sceneDescription = clickedObject.transform.parent.GetChild(9).GetComponent<>().text;
            //sceneImageURL = clickedObject.transform.GetChild(2).GetComponent<>().text;

            // Control if scene static. For controller vibration
            if (clickedObject.tag == "StaticScene")
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().isStaticScene = true;
                Debug.Log(" ---STATIC-- ");
            }
            else
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().isStaticScene = false;
                Debug.Log(" ---DYNAMIC-- ");
            }

            //Sound
            //SoundManager.instance.ChoosingSoundFX();
            //SoundManager.instance.Sound3();
            isInternalScene = true;
            panelCategoriesParent.DOScale(0f, 0.5f);
            StartCoroutine(LoadYourAsyncScene(sceneName));
        }

        IEnumerator LoadYourAsyncScene(string sceneName)
        {
            sceneNameForMood = sceneName;
            asyncLoad = SceneManager.LoadSceneAsync(sceneName);             // Start Scene Loading
            asyncLoad.allowSceneActivation = false;
            LoadingPanel();

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                //Debug.Log("YÜKLENÝYOR");
                yield return null;

            }
            Debug.Log("Inrenal Sahne YÜKLENDÝ");

        }
        #endregion

        //Opens Loading Panel
        public void LoadingPanel()
        {
            isBreathFix = true;


            panelCategoriesParent.DOScale(0f, 0.5f);
            immerzaLogoSeperateParent.gameObject.SetActive(false);
            panelLoading.SetActive(true);
            //loadingBar.DOFillAmount(1, 8).OnComplete(OpenMoodTracker);
            //loadingBar.DOFillAmount(1, 8).OnComplete(LoadingPanelComplate);
            panelMoodTracker.GetComponent<RectTransform>().DOScale(0f, 1f);
        }

        private void LoadingPanelComplate()
        {
            if (introPanel.activeInHierarchy)
            {
                panel1.SetActive(true);
            }
            else
            {
                OpenMoodTracker();
            }

            // Active after BD Intro
            panelLoading.SetActive(false);
            //OpenMoodTracker();                  // Deactive after BD Intro
        }

        // Opens MoodTracker Panel
        private void OpenMoodTracker()
        {

            panelMoodTracker.SetActive(true);
            panelMoodTracker.GetComponent<RectTransform>().DOScale(1.5f, 1f);
        }

        // ----------------------------------------------------------------OnClick MoodTracker Next

        public void NewMotionTrackingOnClick()
        {
            if (isFinishMoofTracker) isClickedMoodA = true;
            else isClickedMoodB = true;

            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            beforeMood = clickedButton.gameObject.name;

            //SoundManager.instance.ChoosingSoundFX();

            if (isClickedRatingB)
            {
                // TO DO MOOD TRACKER BEFORE 
                //authhenticationManager.MoodTrackerOnClickB();

                //hideManagerScript.enabled = true;
                StartCoroutine(StartTheScene());

                //panel2.SetActive(true);
                isClickedMoodB = false;
                isClickedRatingB = false;
            }

            if (isClickedRatingA)
            {
                // TO DO MOOD TRACKER AFTER
                //authhenticationManager.MoodTrackerOnClickA();

                //authhenticationManager.BreathDetectionAOnClick();

                StartCoroutine(OpenResultPanel());
                isFinishMoofTracker = false;
            }

            //SoundManager.instance.Sound1();

        }

        public void NewRatingOnClick()
        {
            if (isFinishMoofTracker) isClickedRatingA = true;
            else isClickedRatingB = true;


            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            beforeRating = clickedButton.gameObject.name;

            //SoundManager.instance.ChoosingSoundFX();

            if (isClickedMoodB)
            {
                // TO DO MOOD TRACKER BEFORE
                //authhenticationManager.MoodTrackerOnClickB();

                //hideManagerScript.enabled = true;
                StartCoroutine(StartTheScene());
                //panel2.SetActive(true);
                isClickedMoodB = false;
                isClickedRatingB = false;
            }

            if (isClickedMoodA)
            {
                // TO DO MOOD TRACKER AFTER
                //authhenticationManager.MoodTrackerOnClickA();

                StartCoroutine(OpenResultPanel());
                isFinishMoofTracker = false;
            }

            //SoundManager.instance.MoodTrackerSound1();
        }

        IEnumerator OpenResultPanel()
        {

            yield return new WaitForSeconds(3f);
            panelMoodTracker.SetActive(false);
            resultPanel.SetActive(true);
        }

        IEnumerator StartTheScene()
        {
            //moodTrackerParticle.Play();

            yield return new WaitForSeconds(2f);
            //moodTrackerParticle.Stop();
            immerzaLogoSeperateParent.gameObject.SetActive(false);
            panelMoodTracker.SetActive(false);

            BreathingDetectionActivation();

            //findAndAttachHand.enabled = true;
            //findAndAttachCorner.enabled = true;
            if (isInternalScene)
            {

                //panelBreathingTutorial.SetActive(true);
                asyncLoad.allowSceneActivation = true;
                Destroy(cameraChooser.gameObject);

            }
            else
            {
                isExternalSceneTimer = true;
                externalSceneTimer = 0;

                //handle.Task.Result.ActivateAsync();                                                                   // Start The External Addressable Scene Play
                //jsonManagerSlider.ActivateAddressableScene();
                //cameraChooser.enabled = true;                                                                             // -- Geçici 08022023

                // Add breathing exercise to external scenes
                float waitTime = 15f;
                yield return new WaitForSeconds(waitTime);
                int randomNumber = Random.Range(0, breathingExerciseList.Count);
                //breathingExerciseList[randomNumber].SetActive(true);                                  // ADD BREATHING EXERCISE TO EXTERNAL SCENES
            }



        }

        /*
        public void MoodTrackerOnClick()
        {
            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            // GET Clicked mood's name
            beforeMood = clickedButton.name.Substring(12);

            foreach (GameObject g in moods)
            {
                if (g.name == clickedButton.GetComponent<InteractMoodTrackerCircle>().parentObject.name)
                {
                    clickedButton.transform.GetChild(0).gameObject.SetActive(true);
                    clickedButton.GetComponent<InteractMoodTrackerCircle>().enabled = false;
                    clickedButton.GetComponent<InteractMoodTrackerCircle>().parentObject.GetComponent<RectTransform>().DOScale(1f, 0.5f);
                }
                else
                {
                    g.GetComponent<Image>().DOFade(0.2f, 0.5f);
                    g.GetComponent<RectTransform>().DOScale(0.8f, 0.5f);
                }
            }

            SoundManager.instance.ChoosingSoundFX();
        }
        public void RatingOnClick()
        {
            SoundManager.instance.ChoosingSoundFX();

            immerzaLogoSeperateParent.gameObject.SetActive(false);
            panelRating.SetActive(false);
            panelMoodTracker.GetComponent<RectTransform>().DOScale(0, 1f);

            Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            //int.TryParse(clickedButton.name, out beforeRating);

            if (isInternalScene)
            {
                //panelBreathingTutorial.SetActive(true);
                asyncLoad.allowSceneActivation = true;
                Destroy(cameraChooser.gameObject);
            }
            else
            {
                //handle.Task.Result.ActivateAsync();             // Start The External Addressable Scene Play
                jsonManagerSlider.ActivateAddressableScene();
                cameraChooser.enabled = true;
            }
        }
        */

        #region Addressable Scene Loading
        public void LoadAddressableScene(string sceneName)
        {
            //Sound
            //SoundManager.instance.ChoosingSoundFX();

            isInternalScene = false;
            // TO DO Button Click Sound;
            StartCoroutine(DownloadScene(sceneName));
        }

        IEnumerator DownloadScene(string sceneName)
        {
            //Button clickedButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

            /*-------------------------------------------------------------------------------------------------*/
            //TextMeshProUGUI clickedText = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>();
            //TextMeshProUGUI clickedText = EventSystem.current.currentSelectedGameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();


            //var scn = Addressables.GetDownloadSizeAsync(sceneName);
            //downloadScene.Completed += SceneDownloadComplete;
            //clickedButton.interactable = false;
            Debug.Log("IFFFFF UPPP");
            if (true)
            {
                panelCategoriesParent.DOScale(0f, 0.5f);

                //handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, false);
                LoadingPanel();
                Debug.Log("IFFFFF ICI");

                yield return new WaitForSeconds(10f);
                //handle.Task.Result.ActivateAsync();
                //cameraChooser.enabled = true;
            }
            else
            {
                //var downloadScene = Addressables.DownloadDependenciesAsync(sceneName);
                //addressableTestText.text = "EVET DEÐÝÞÝYOR";
                /*
                while (!downloadScene.IsDone)
                {
                    Debug.Log("WHILEEE ---- ");
                    var status = downloadScene.GetDownloadStatus();
                    float progress = status.Percent;
                    downloadProgressInput = (int)(progress * 100);

                    //addressableTestText.text = downloadProgressInput.ToString() + " % Downloading";
                    yield return null;
                }
            */
                //addressableTestText.text = "PLAY";
                //clickedButton.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                //clickedButton.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                //clickedButton.interactable = true;
                downloadProgressInput = 100;
                Debug.Log("--100-- ");
            }

            //textInfo.text = "Sahneye Gidiliyor";

        }
        #endregion


        // OPEN FINISH RATING PANEL
        public void OpenFinishRatingPanel()
        {

            //beforeMood1.text = beforeMood;
            //beforeNumber1.text = beforeRating.ToString();

            //finishRatingPanel.SetActive(true);

            panelMoodTracker.SetActive(true);                                                   // CALISMA ALANI 12.02.2023
                                                                                                //InteractibleActive();
        }

        // BackButton OnClick
        public void BackButtonOnClick()
        {
            // TO DO
        }

        #region Editor TEST CODES
        private void CategoriesAnimationsTEST()
        {
            goodafttVsImage.DOScale(0f, 1f);

            GameObject tempButton = focusGameObj;


            foreach (RectTransform r in categories)
            {
                //r.GetComponent<InteractLevelSelect>().enabled = false;
                r.GetComponent<Button>().interactable = false;

                if (tempButton.name == r.name)
                {
                    r.GetComponent<RectTransform>().DOScale(0, 1f).OnComplete(() => OpenSelectedSceneMenu(tempButton));

                }
                else
                {
                    r.GetComponent<RectTransform>().DOScale(0, 1f).OnComplete(() => r.gameObject.SetActive(false));
                }
            }



            /*
            for (int i = 0; i < categories.Count; i++)
            {
                categories[i].DOScale(1f, 3f);                                       // All Categories scale are getting bigger             // DEÐÝÞTÝR
                categories[i].DOAnchorPos(categoriesOriginalPos[i], 3f);             // All Categories are going their original position     

                if (i == 2)
                {
                    SelectSceneInternal("Water2");
                }
                else
                {

                }
            */
        }

        private void SceneSelectTEST()
        {
            //SoundManager.instance.ChoosingSoundFX();

            isInternalScene = true;
            panelCategoriesParent.DOScale(0f, 0.5f);
            StartCoroutine(LoadYourAsyncScene("LightsInForest"));
            asyncLoad.allowSceneActivation = true;

            //cameraChooser.enabled = true;
            Destroy(cameraChooser.gameObject);

            //findAndAttachHand.enabled = true;
            //findAndAttachCorner.enabled = true;

            BreathingDetectionActivation();


            // Send example data
            //authhenticationManager.BreathDetectionBOnClick();

        }


        private void AddreesableSceneTEST()
        {

            //SoundManager.instance.ChoosingSoundFX();

            isInternalScene = false;
            StartCoroutine(DownloadScene("Assets/Scenes/CozyPolarHut.unity"));
            //findAndAttachHand.enabled = true;
            //findAndAttachCorner.enabled = true;
            Debug.Log("LASTTTTTTTTTTTTTTTTTTTTTTTTT");
            //handle.Task.Result.ActivateAsync();

        }
        #endregion

        public void SliderRight(RectTransform panelSliding)
        {
            float interval = 229f;
            //int sliderMinValue = -1100;

            if (panelSliding.anchoredPosition.x > sliderMinValue)
            {
                // Týklandýðý sürece kaysýn vs.
                float tempX = panelSliding.anchoredPosition.x - interval;
                panelSliding.DOLocalMoveX(tempX, 0.7f);

                // TO DO - Button OnClick sound
                //SoundManager.instance.SliderSoundFX();
            }
            else
            {
                // TO DO - Blocked Sound
            }

        }

        public void SliderLeft(RectTransform panelSliding)
        {
            float interval = 229f;
            //int sliderMaxValue = 1130;

            if (panelSliding.anchoredPosition.x < sliderMaxValue)
            {
                // Týklandýðý sürece kaysýn vs.
                float tempX = panelSliding.anchoredPosition.x + interval;
                panelSliding.DOLocalMoveX(tempX, 0.7f);

                // TO DO - Button OnClick sound
                //SoundManager.instance.SliderSoundFX();
            }
            else
            {
                // TO DO - Blocked Sound
            }
        }
        /*
        private void SliderWithJoystick()
        {
            //int sliderMinValue = -1100;
            //int sliderMaxValue = 1130;

            if (panelSliding.parent.gameObject.activeSelf)
            {
                // Slider With Controller
                Vector2 primaryData = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                Vector2 secondaryData = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

                if (panelSliding.anchoredPosition.x <= sliderMaxValue && panelSliding.anchoredPosition.x >= sliderMinValue)
                {
                    if (primaryData.x > 0 || primaryData.x < 0)
                    {
                        panelSliding.anchoredPosition += new Vector2(primaryData.x * 10, 0);
                    }
                    else
                    {
                        panelSliding.anchoredPosition += new Vector2(secondaryData.x * 5, 0);
                    }
                }
                else
                {
                    // TO DO - Block Sound
                }

                if (panelSliding.anchoredPosition.x > sliderMaxValue)
                {
                    //panelSliding.anchoredPosition = new Vector2(sliderMaxValue, panelSliding.anchoredPosition.y);
                    panelSliding.DOAnchorPosX(sliderMaxValue, 0.2f);
                }

                if (panelSliding.anchoredPosition.x < sliderMinValue)
                {
                    //panelSliding.anchoredPosition = new Vector2(sliderMinValue, panelSliding.anchoredPosition.y);
                    panelSliding.DOAnchorPosX(sliderMinValue, 0.2f);
                }
            }

        }
        */
        private void PauseAndResume()
        {
            Scene scene = SceneManager.GetActiveScene();

            if (scene.name == "MainMenu7Curved" || ratingScenePanel.activeInHierarchy || panelMoodTracker.activeInHierarchy || resultPanel.activeInHierarchy)
            {
                // Do not anything for now. We can add feateure in future
            }
            else
            {
                /*
                if (OVRInput.GetDown(OVRInput.Button.Two))
                {
                    PauseGame();
                }

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    ResumeGame();

                }
                */
            }
        }

        private void PauseGame()
        {
            pausePanel.SetActive(true);
            isBreathingDetectiohnActiveOnScene = false;
            BreathingDetectionActivation();                                              // -- BD Activation
            Time.timeScale = 0;
            //OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);

        }

        // Resume Gameplay and Exit(Pause Panel) OnClick Function
        public void ResumeGame()
        {
            pausePanel.SetActive(false);
            isBreathingDetectiohnActiveOnScene = true;
            BreathingDetectionActivation();                                      // -- BD Activation
            Time.timeScale = 1;
        }
        public void ReturnMainMenu()
        {
            resultPanel.SetActive(false);
            //OVRScreenFade.instance.FadeOut();
            DestroyAllDontDestroyOnLoadObjects();
            SceneManager.LoadScene("MainMenu7Curved");

            //SoundManager.instance.Sound3();
        }


        public void UserExitTheSceneOnPausePanelOnclick()
        {
            Time.timeScale = 1;

            //StartCoroutine(USerExit());
            //OVRScreenFade.instance.FadeIn();
            pausePanel.SetActive(false);
            //panelMoodTracker.SetActive(true);

            //authhenticationManager.BreathDetectionBOnClick();

            PlayerPrefs.SetInt("FinishGame", 1);
        }

        IEnumerator USerExit()
        {
            yield return new WaitForSeconds(15f);
            Time.timeScale = 0;

        }
        public void ReturnMainMenuFromPausePanel()
        {
            Time.timeScale = 1;
            DestroyAllDontDestroyOnLoadObjects();
            SceneManager.LoadScene("MainMenu7Curved");
            //SoundManager.instance.Sound3();
        }

        public void RestartGame()
        {
            StartCoroutine(IRestartGame());
        }

        IEnumerator IRestartGame()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            //SoundManager.instance.Sound3();

            if (isInternalScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                //jsonManagerSlider.RestartScene();
                //cameraChooser.m_FindCamera(); // -- Geçici 08022023


                foreach (GameObject be in breathingExerciseList)
                {
                    be.SetActive(false);
                }

                float waitTime = 15f;
                yield return new WaitForSeconds(waitTime);
                int randomNumber = Random.Range(0, breathingExerciseList.Count);
                breathingExerciseList[randomNumber].SetActive(true);
            }
        }

        // BACK Button OnClick -- Bunu Bottom PAnel Scriptine aktarmayi dusunuyorum
        public void TurnCategoriesFromScenes()
        {
            goodafttVsImage.DOScale(1f, 1f);
            rightButton_F.DOScale(0f, 0.1f); leftButton_F.DOScale(0f, 0.1f);
            rightButton_C.DOScale(0f, 0.1f); leftButton_C.DOScale(0f, 0.1f);
            rightButton_E.DOScale(0f, 0.1f); leftButton_E.DOScale(0f, 0.1f);

            foreach (RectTransform r in categories)
            {
                r.GetChild(1).gameObject.SetActive(false);
                //r.GetComponent<InteractLevelSelect>().enabled = true;
                if (r.name == "FOCUS") r.GetComponent<Button>().interactable = true;
                r.GetComponent<RectTransform>().DOScale(1, 1f);
                r.gameObject.SetActive(true);

                r.GetComponent<Image>().enabled = true;                               //30122022
                r.transform.GetChild(4).GetComponent<Image>().enabled = true;         //30122022
                r.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        public void DestroyAllDontDestroyOnLoadObjects()
        {
            var go = new GameObject("Sacrificial Lamb");
            DontDestroyOnLoad(go);

            var tempDestroys = GameObject.FindGameObjectsWithTag("Destroy");
            foreach (var r in tempDestroys)
            {
                Destroy(r);
            }

            foreach (var root in go.scene.GetRootGameObjects())
            {
                //Destroy(root);
            }
        }

        IEnumerator DenemeMetodu()
        {
            yield return new WaitForSeconds(10f);
            Debug.Log("-20-");
            yield return new WaitForSeconds(10f);
            Debug.Log("-40-");
            yield return new WaitForSeconds(10f);
            Debug.Log("-60-");
            //ReturnMainMenu();
            UserExitTheSceneOnPausePanelOnclick();
            yield return new WaitForSeconds(10f);
            Debug.Log("-80-");
        }

        #region Breathing Tutorials Continue OnClick() methods
        public void Continue1()
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        public void Continue2()
        {
            OpenMoodTracker();
            panel2.SetActive(false);
            //asyncLoad.allowSceneActivation = true;        
        }

        #endregion


        public void GoToSceneRatingOnClick()
        {
            resultPanel.SetActive(false);
            ratingScenePanel.SetActive(true);
        }

        /* ------------------------- Optional Breating Detection ------------------------------------ */
        public void TogleBreathingDetectionOnClick()
        {
            if (toogleBreathingDetection.isOn)
            {
                isBreathingDetectiohnActiveOnScene = true;
                toogleBreathingDetectionPause.isOn = true;
            }
            else
            {
                isBreathingDetectiohnActiveOnScene = false;
                toogleBreathingDetectionPause.isOn = false;
            }

        }

        private void BreathingDetectionActivation()
        {
            if (isBreathingDetectiohnActiveOnScene)
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().enabled = true;
                if (virtualBelly != null) virtualBelly.SetActive(true);
            }
            else
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().enabled = false;

                if (virtualBelly != null) virtualBelly.SetActive(false);
            }
        }

        public void PausePanelBreathingDetectionSwitcher()
        {


            if (toogleBreathingDetectionPause.isOn)
            {
                isBreathingDetectiohnActiveOnScene = true;
            }
            else
            {
                isBreathingDetectiohnActiveOnScene = false;
            }
        }









        public void RestartGa2me()
        {
            StartCoroutine(IRestartGame());
        }

        IEnumerator IRestartGam2e()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            //SoundManager.instance.Sound3();

            if (isInternalScene)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                //jsonManagerSlider.RestartScene();
                //cameraChooser.m_FindCamera(); // -- Geçici 08022023


                foreach (GameObject be in breathingExerciseList)
                {
                    be.SetActive(false);
                }

                float waitTime = 15f;
                yield return new WaitForSeconds(waitTime);
                int randomNumber = Random.Range(0, breathingExerciseList.Count);
                breathingExerciseList[randomNumber].SetActive(true);
            }
        }

        // BACK Button OnClick -- Bunu Bottom PAnel Scriptine aktarmayi dusunuyorum
        public void TurnCategori2esFromScenes()
        {
            goodafttVsImage.DOScale(1f, 1f);
            rightButton_F.DOScale(0f, 0.1f); leftButton_F.DOScale(0f, 0.1f);
            rightButton_C.DOScale(0f, 0.1f); leftButton_C.DOScale(0f, 0.1f);
            rightButton_E.DOScale(0f, 0.1f); leftButton_E.DOScale(0f, 0.1f);

            foreach (RectTransform r in categories)
            {
                r.GetChild(1).gameObject.SetActive(false);
                //r.GetComponent<InteractLevelSelect>().enabled = true;
                if (r.name == "FOCUS") r.GetComponent<Button>().interactable = true;
                r.GetComponent<RectTransform>().DOScale(1, 1f);
                r.gameObject.SetActive(true);

                r.GetComponent<Image>().enabled = true;                               //30122022
                r.transform.GetChild(4).GetComponent<Image>().enabled = true;         //30122022
                r.transform.GetChild(0).gameObject.SetActive(true);
            }
        }

        public void DestroyAllD2ontDestroyOnLoadObjects()
        {
            var go = new GameObject("Sacrificial Lamb");
            DontDestroyOnLoad(go);

            var tempDestroys = GameObject.FindGameObjectsWithTag("Destroy");
            foreach (var r in tempDestroys)
            {
                Destroy(r);
            }

            foreach (var root in go.scene.GetRootGameObjects())
            {
                //Destroy(root);
            }
        }

        IEnumerator DenemeMeto2du()
        {
            yield return new WaitForSeconds(10f);
            Debug.Log("-20-");
            yield return new WaitForSeconds(10f);
            Debug.Log("-40-");
            yield return new WaitForSeconds(10f);
            Debug.Log("-60-");
            //ReturnMainMenu();
            UserExitTheSceneOnPausePanelOnclick();
            yield return new WaitForSeconds(10f);
            Debug.Log("-80-");
        }

        #region Breathing Tutorials Continue OnClick() methods
        public void Continue12()
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
        }
        public void Continue22()
        {
            OpenMoodTracker();
            panel2.SetActive(false);
            //asyncLoad.allowSceneActivation = true;        
        }

        #endregion


        public void GoToSceneRatingOnClic2k()
        {
            resultPanel.SetActive(false);
            ratingScenePanel.SetActive(true);
        }

        /* ------------------------- Optional Breating Detection ------------------------------------ */
        public void TogleBreathingDetectionOnClic2k()
        {
            if (toogleBreathingDetection.isOn)
            {
                isBreathingDetectiohnActiveOnScene = true;
                toogleBreathingDetectionPause.isOn = true;
            }
            else
            {
                isBreathingDetectiohnActiveOnScene = false;
                toogleBreathingDetectionPause.isOn = false;
            }

        }

        private void BreathingDetectionActivatio2n()
        {
            if (isBreathingDetectiohnActiveOnScene)
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().enabled = true;
                if (virtualBelly != null) virtualBelly.SetActive(true);
            }
            else
            {
                //breathingDetectionObject.GetComponent<BreathDetection>().enabled = false;

                if (virtualBelly != null) virtualBelly.SetActive(false);
            }
        }

        public void PausePanelBreathingDetectionSwitcher2()
        {


            if (toogleBreathingDetectionPause.isOn)
            {
                isBreathingDetectiohnActiveOnScene = true;
            }
            else
            {
                isBreathingDetectiohnActiveOnScene = false;
            }
        }

    }
}