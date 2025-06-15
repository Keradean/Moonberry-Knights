using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    [Header("Confiq")]
    [SerializeField] private GameObject selectorSprite;

    private EnemyBrain enemyBrain;
    private void Awake()
    {
        enemyBrain = GetComponent<EnemyBrain>();
    }
    private void EnemySelectedCallback(EnemyBrain enemySelected)
    {
        if (enemySelected == enemyBrain)
        {
            selectorSprite.SetActive(true);

        }
        else 
        {
            selectorSprite.SetActive(false);
        }
    }

    private void NoSelectedCallBack()
    {
        selectorSprite.SetActive(false);
    }
    private void OnEnable()
    {
        SelectionManager.OnEnemySelectedEvent += EnemySelectedCallback;
        SelectionManager.OnNoSelectedEvent += NoSelectedCallBack; 

        
    }

    private void OnDisable()
    {
        SelectionManager.OnEnemySelectedEvent -= EnemySelectedCallback;
        SelectionManager.OnNoSelectedEvent -= NoSelectedCallBack;
    }
}
