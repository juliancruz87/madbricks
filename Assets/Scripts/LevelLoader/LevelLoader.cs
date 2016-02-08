using UnityEngine;
using System.Collections;
using LevelLoaderController.Detail;

namespace LevelLoaderController
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField]
        private LevelLoaderSettings settings;

        private string pendingScene = string.Empty;

        public static LevelLoader Instance
        {
            get;
            private set;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
        }

        public void LoadScene(string levelName)
        {
            pendingScene = levelName;
            Application.LoadLevel(settings.LevelLoader);
        }

        public void LoadPendingScene()
        {
            StartCoroutine(LoadLevelAsync());
        }

        private IEnumerator LoadLevelAsync()
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            yield return Resources.UnloadUnusedAssets();
            yield return Application.LoadLevelAsync(pendingScene);
        }

        public void LoadNextLevel()
        {
            string levelName = "";
            Interactive.GameManager.LevelInfo info = FindObjectOfType<Interactive.GameManager>().levelInfo;
            int area = info.area;
            int level = info.level;

            Debug.Log("[LevelLoader] Getting level info " + area + " - " + level);

            if (area < 6)
            {
                if (level < 3)
                    level++;
                else
                {
                    area++;
                    level = 1;
                }
            }
            else
            {
                if (level < 3)
                    level++;
                else
                    level = -1;
               
            }

            if (level == -1)
                levelName = SceneProperties.SCENE_MAIN_MENU;
            else
                levelName = "W" + area + "_L" + level;

            if(level == 1)
                levelName = SceneProperties.SCENE_LOADER_AREA;


            SaveManager.Instance.SetSelectedArea(area.ToString());

            SaveManager.Instance.SetSelectedLevel(level.ToString());
            SaveManager.Instance.SetClearedArea(area.ToString());
            SaveManager.Instance.SetClearedLevel(level.ToString());



            Debug.Log("[LevelLoader] Loading " + levelName);
            LoadScene(levelName);

        }
    }
}