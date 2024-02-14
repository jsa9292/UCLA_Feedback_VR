using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VIVE.OpenXR.Samples.Hand
{
    public class Show_Hand : MonoBehaviour
    {
        /// <summary>
        /// This is a comment
        /// </summary>
        public GameObject jointPrefab;
        public Transform leftHand;
        public Transform rightHand;
        public Joint_Movement[] leftJM;
        public Joint_Movement[] rightJM;
        public bool showLeft;
        public bool showRight;
        void Start()
        {
            if(leftJM.Length == 0) leftJM = leftHand.GetComponentsInChildren<Joint_Movement>();
            if(rightJM.Length == 0) rightJM = rightHand.GetComponentsInChildren<Joint_Movement>();
            SkinnedMeshRenderer leftSMR = leftHand.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer rightSMR = rightHand.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            for (int i = 0; i < 26; i++)
            {
                leftJM[i].jointNum = i;
                leftJM[i].isLeft = true;

                rightJM[i].jointNum = i;
                rightJM[i].isLeft = false;
            }
            LH_mat = leftSMR.material;
            RH_mat = rightSMR.material;
            startColor = LH_mat.color;
        }
        public Color startColor;
        public Material LH_mat;
        public Material RH_mat;
        private float LH_vis;
        private float RH_vis;
        public float appearSpeed;
        public GameObject leftTouch;
        public GameObject rightTouch;
        private void Update()
        {
            showLeft = true;
            showRight = true;
            for (int i = 0; i < 26; i++)
            {
                showLeft = showLeft && leftJM[i].showHand;
                showRight = showRight && rightJM[i].showHand;
            }

            leftTouch.SetActive(showLeft);
            rightTouch.SetActive(showRight);
            LH_vis += Time.deltaTime * (showLeft ? appearSpeed : -appearSpeed);
            LH_vis = Mathf.Clamp01(LH_vis);
            RH_vis += Time.deltaTime * (showRight ? appearSpeed : -appearSpeed);
            RH_vis = Mathf.Clamp01(RH_vis);
            LH_mat.color = Color.Lerp(Color.clear, startColor, LH_vis);
            RH_mat.color = Color.Lerp(Color.clear, startColor, RH_vis);


        }
    }
}
