using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Controll
{
    /// <summary>
    /// 使用image展示3d对象。原对象不受影响
    /// </summary>
    public class ImageViewer:EventTrigger
    {
        public int renderWidth = 256;
        public int renderHeight = 256;
        private Scene scene;
        private RenderTexture renderTexture;
        private Texture2D texture2D;
        private Image image;
        private Sprite imageSprite;
        private GameObject cameraObject;
        private new Camera camera;
        public LayerMask layerMask;
        public GameObject target;

        public Camera Camera
        {
            get
            {
                return cameraObject.GetComponent<Camera>();
            }
        }

        private void Start()
        {
            if (!(scene = SceneManager.GetSceneByName("imageViewer")).isLoaded)
            {
                scene = SceneManager.CreateScene("imageViewer");
            }

            cameraObject = new GameObject("imageViewerCamera");
            if (scene.IsValid())
            {
                SceneManager.MoveGameObjectToScene(cameraObject, scene);
            }
            cameraObject.transform.position = new Vector3Int(0, 0, -2);
            cameraObject.AddComponent<Camera>();
            cameraObject.AddComponent<CameraBehavior>();
            cameraObject.GetComponent<CameraBehavior>().ImageViewer = this;
            cameraObject.GetComponent<Camera>().cullingMask = layerMask;
            cameraObject.AddComponent<FlareLayer>();
            cameraObject.AddComponent<AudioListener>();

            renderTexture = new RenderTexture(renderWidth, renderHeight,(int)cameraObject.GetComponent<Camera>().depth);
            cameraObject.GetComponent<Camera>().targetTexture = renderTexture;
            texture2D = new Texture2D(renderWidth, renderHeight, TextureFormat.ARGB32, false);
            image = GetComponent<Image>();
        }

        private Vector3 startPos;
        private Vector3 offset = new Vector3();
        private Vector3 axis;
        private bool isDraging;
        private float speedRotate = -Mathf.PI;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            startPos = Input.mousePosition;
            isDraging = true;

        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isDraging = false;
        }

        private void Update()
        {
            if (isDraging)
            {
                offset = Input.mousePosition - startPos;
                startPos = Input.mousePosition;
                offset.z = 0;
                axis = Vector3.Cross(offset, Camera.transform.position);
                target.transform.Rotate(axis,speedRotate,Space.World);
                Debug.LogWarning("i am droging");
            }
        }

        private void OnDestroy()
        {
            if (scene.IsValid())
            {
                SceneManager.UnloadSceneAsync(scene);
            }
            Camera.Destroy(gameObject);
        }

        public void AddTarget(Transform trans)
        {

            target = GameObject.Instantiate(trans.gameObject);
            target.layer = SortingLayer.GetLayerValueFromName("ImageViewer");
            if (scene.IsValid())
            {
                SceneManager.MoveGameObjectToScene(target, scene);
            }
        }



        public void RemoveTarget(Transform trans)
        {
            Destroy(trans);
        }

        private class CameraBehavior : MonoBehaviour
        {
            private ImageViewer imageViewer;

            public ImageViewer ImageViewer
            {
                get
                {
                    return imageViewer;
                }

                set
                {
                    imageViewer = value;
                }
            }

            private CameraBehavior() { }
            public CameraBehavior(ImageViewer imageViewer)
            {
                this.imageViewer = imageViewer;
            }

            private void OnPostRender()
            {
                imageViewer.texture2D.ReadPixels(new Rect(0, 0, imageViewer.renderTexture.width, imageViewer.renderTexture.height), 0, 0);
                imageViewer.texture2D.Apply();
                if (imageViewer.imageSprite != null)
                {
                    Destroy(imageViewer.imageSprite);
                }
                imageViewer.imageSprite = Sprite.Create(imageViewer.texture2D, new Rect(0, 0, imageViewer.renderTexture.width, imageViewer.renderTexture.height), new Vector2(imageViewer.renderTexture.width / 2, imageViewer.renderTexture.height / 2));
                imageViewer.image.sprite = imageViewer.imageSprite;
            }
        }
    }
}
