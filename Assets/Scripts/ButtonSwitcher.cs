using UnityEngine;
using UnityEngine.UI;

public class ButtonSwitcher : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;

    void Start()
    {
        // Call the SwitchPositions method during Start to initially randomize the positions
        SwitchPositions();
    }

    public void SwitchPositions()
    {
        // Store the current positions of the buttons
        Vector3 tempPos1 = button1.transform.position;
        Vector3 tempPos2 = button2.transform.position;
        Vector3 tempPos3 = button3.transform.position;
        Vector3 tempPos4 = button4.transform.position;

        // Create an array to hold the positions
        Vector3[] positions = { tempPos1, tempPos2, tempPos3, tempPos4 };

        // Shuffle the positions array randomly
        ShuffleArray(positions);

        // Assign the shuffled positions to the buttons
        button1.transform.position = positions[0];
        button2.transform.position = positions[1];
        button3.transform.position = positions[2];
        button4.transform.position = positions[3];
    }

    // Function to shuffle an array randomly
    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < (n - 1); i++)
        {
            int r = i + Random.Range(0, n - i);
            T temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
    }
}
