using UnityEngine;

public class AutoRecalibrator : MonoBehaviour
{
    public Transform leftController;      // 왼손 컨트롤러
    public Transform worldAnchorRoot;     // 월드 앵커 루트

    private Vector3 initialLeftPos;       // 초기 왼손 컨트롤러 위치
    private Vector3 initialLeftRot;       // 초기 왼손 컨트롤러 오일러 회전
    private Vector3 initialWorldPos;      // 초기 월드 앵커 위치
    private Vector3 initialWorldRot;      // 초기 월드 앵커 오일러 회전

    public bool initialize = false;       // 초기화 플래그

    void Update()
    {
        if (initialize)
        {
            initialLeftPos = leftController.position;
            initialLeftRot = leftController.eulerAngles;
            initialWorldPos = worldAnchorRoot.position;
            initialWorldRot = worldAnchorRoot.eulerAngles;
            initialize = false;
        }

        // 위치 차이 계산
        Vector3 positionDifference = leftController.position - initialLeftPos;
        worldAnchorRoot.position = initialWorldPos + positionDifference;

        // 회전 차이 계산 (오일러 기준)
        Vector3 currentRot = leftController.eulerAngles;
        Vector3 rotationDifference = currentRot - initialLeftRot;
        worldAnchorRoot.eulerAngles = initialWorldRot + rotationDifference;
    }
}
