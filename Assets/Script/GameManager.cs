using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    public GridSystem GridSystem { get; private set; }
    public RoadCreationManager RoadCreationManager { get; private set; }
    public TileSelectionSystem TileSelectionSystem { get; private set; }

    private void Awake()
    {
        GridSystem = GameObject.FindObjectOfType<GridSystem>();
        RoadCreationManager = GameObject.FindObjectOfType<RoadCreationManager>();
        TileSelectionSystem = GameObject.FindObjectOfType<TileSelectionSystem>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
