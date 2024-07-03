using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FindPlayers : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cinemachine;
    [SerializeField] private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();

        // Ensure the CinemachineTargetGroup is not null
        if (cinemachine == null)
        {
            cinemachine = GameObject.FindAnyObjectByType<CinemachineTargetGroup>();
        }

        // Clear any existing targets
        cinemachine.m_Targets = new CinemachineTargetGroup.Target[0];

        // Add players to the CinemachineTargetGroup
        foreach (var player in gameManager.playersInGame)
        {
            var target = new CinemachineTargetGroup.Target
            {
                target = player.transform,
                weight = 1f,  // Set the weight as desired
                radius = 2f   // Set the radius as desired
            };

            // Use a temporary list to add the new target
            var tempList = new List<CinemachineTargetGroup.Target>(cinemachine.m_Targets)
            {
                target
            };
            cinemachine.m_Targets = tempList.ToArray();
        }
    }
}
