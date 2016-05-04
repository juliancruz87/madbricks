using UnityEngine;
using System.Collections;
using LevelLoaderController.Detail;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene(settings.LevelLoader);
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
            yield return SceneManager.LoadSceneAsync(pendingScene); 
        }

        public static string GetTutorialOrLevelName(int area, int level)
        {
            string sceneName = "";
            int clearedTutorial = int.Parse(SaveManager.Instance.GetClearedTutorial());

            if (area == 1 && level == 1 && clearedTutorial == 0)
                sceneName = "Tutorial_1";
            else if (area == 1 && level == 2 && clearedTutorial == 1)
                sceneName = "Tutorial_2";
            else if (area == 1 && level == 3 && clearedTutorial <= 2)
                sceneName = "Tutorial_3";
            else if (area == 2 && level == 1 && clearedTutorial <= 3)
                sceneName = "Tutorial_4";
            else if (area == 3 && level == 1 && clearedTutorial <= 4)
                sceneName = "Tutorial_5";
            else if (area == 3 && level == 2 && clearedTutorial <= 5)
                sceneName = "Tutorial_6";
            else if (area == 5 && level == 2 && clearedTutorial <= 6)
                sceneName = "Tutorial_7";
            else
                sceneName = "W" + area + "_L" + level;

            return sceneName;
        }

        public void LoadNextLevel()
        {
            string levelName = "";
            Interactive.GameManager.LevelInfo info = FindObjectOfType<Interactive.GameManager>().levelInfo;
            int area = info.area;
            int level = info.level;

            Debug.Log("[LevelLoader] Getting level info " + area + " - " + level);
            
            //TODO: Awful code, try to refactor
            if (area == 0)
            {
                SaveManager.Instance.SetClearedTutorial(level.ToString());

				if (level == 1)
				{
					area = 1;
					level = 1;
				}

                else if (level == 2)
                {
                    area = 1;
                    level = 2;
                }
                else if (level == 3)
                {
                    area = 1;
                    level = 3;
                }
                else if (level == 4)
                {
                    area = 2;
                    level = 1;
                }
                else if (level == 5)
                {
                    area = 3;
                    level = 1;
                }

                else if (level == 6)
                {
                    area = 3;
                    level = 2;
                }
                else if (level == 7)
                {
                    area = 5;
                    level = 2;
                }
                
            }
            else if (area < 6)
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
            else if (level == 1 && info.area != 0)
                levelName = SceneProperties.SCENE_LOADER_AREA;
            else
                levelName = GetTutorialOrLevelName(area, level);

			SaveContent (area, level);

            Debug.Log("[LevelLoader] Loading " + levelName);
            LoadScene(levelName);

        }

		private void SaveContent(int area, int level)
		{
			int previousClearedArea = int.Parse (SaveManager.Instance.GetClearedArea ());
			int previousClearedLevel = int.Parse (SaveManager.Instance.GetClearedLevel ());

			SaveManager.Instance.SetSelectedArea (area.ToString ());
			SaveManager.Instance.SetSelectedLevel (level.ToString ());

			if (area > previousClearedArea) 
			{
				SaveManager.Instance.SetClearedArea (area.ToString ());
				SaveManager.Instance.SetClearedLevel (level.ToString ());
			}
			else if (area == previousClearedArea && level > previousClearedLevel) 
			{
				SaveManager.Instance.SetClearedLevel (level.ToString ());
			}
		}
    }
}