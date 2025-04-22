#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WarArea))]
public class WarAreaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        WarArea testWarArea = (WarArea)target;

        // Add a button to the inspector
        if (GUILayout.Button("Make War Area"))
        {
            // Instantiate the war area prefab at the origin
            for (int i = -4; i < 4; i++)
            {
                for (int j = -4; j < 4; j++)
                {
                    if (i == 0 && j == 0) continue; // Skip the center position
                    Vector3 position = new Vector3(i * 100, j * 100, 0); // Adjust the spacing as needed
                    GameObject newImage = Instantiate(testWarArea.gameObject, position, Quaternion.identity);
                    newImage.transform.SetParent(testWarArea.transform.parent); // Set the parent to the original object
                    newImage.transform.localPosition = position; // Set the local position to the new position
                }
            }
        }

        // Draw the default inspector
        DrawDefaultInspector();
    }
}
#endif