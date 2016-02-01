using UnityEngine;
using UnityEditor;
using System.Collections;

public class PersistenceUtil : MonoBehaviour {

    [MenuItem("Madbricks/Persistence/Lock Levels")]
    public static void LockLevels()
    {
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "1");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "1");
    }

    [MenuItem("Madbricks/Persistence/Unlock Levels")]
    public static void UnlockLevels()
    {
        PlayerPrefs.SetString(PrefsProperties.CLEARED_AREA, "6");
        PlayerPrefs.SetString(PrefsProperties.CLEARED_LEVEL, "3");
    }
}
