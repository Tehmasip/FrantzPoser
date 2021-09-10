using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomizableAnimeGirl {
    public class FaceEditManager : MonoBehaviour {

        public SkinnedMeshRenderer eyeRenderer;
        public SkinnedMeshRenderer eyelashesRenderer;
        
        public SkinnedMeshRenderer eyebrowsRenderer;
        public SkinnedMeshRenderer faceRenderer;
        public enum BLENDSHAPES_TYPE {
            IRIS_X,
            IRIS_Y,
            IRIS_W,
            IRIS_H,
            EYE_INNER,
            EYE_OUTER,
            EYE_X,
            EYE_Y,
            EYE_HEIGHT,
            EYE_WIDTH,
            FACE_WIDTH,
            CHIN_LENGTH,
            CHIN_FRONT,
            MOUTH_Y,
            MOUTH_W,
            MOUTH_FRONT,
            NOSE_Y,
            NOSE_FRONT,
            EYEBROWS_X,
            EYEBROWS_Y,
            EYEBROWS_W,
            EYEBROWS_H

        }

        [System.Serializable]
        public class FaceEditData{
            public float[] blendShapeValues = new float[System.Enum.GetValues(typeof(BLENDSHAPES_TYPE)).Length];
        }
        FaceEditData currentFaceEditData = new FaceEditData();

        // Start is called before the first frame update
        void Start () {            
            /*
            ブレンドシェイプのインデックスを確認
            var mesh = eyeRenderer.sharedMesh;
            var shapeCount = mesh.blendShapeCount;
            Debug.LogFormat ("Mesh {0} has {1} shapes.", mesh.name, shapeCount);
            for (var i = 0; i < shapeCount; i++) {
                Debug.LogFormat ("\t{0}: {1}", i, mesh.GetBlendShapeName (i));
            }
            mesh = eyelashesRenderer.sharedMesh;
            shapeCount = mesh.blendShapeCount;
            Debug.LogFormat ("Mesh {0} has {1} shapes.", mesh.name, shapeCount);
            for (var i = 0; i < shapeCount; i++) {
                Debug.LogFormat ("\t{0}: {1}", i, mesh.GetBlendShapeName (i));
            }
            mesh = faceRenderer.sharedMesh;
            shapeCount = mesh.blendShapeCount;
            Debug.LogFormat ("Mesh {0} has {1} shapes.", mesh.name, shapeCount);
            for (var i = 0; i < shapeCount; i++) {
                Debug.LogFormat ("\t{0}: {1}", i, mesh.GetBlendShapeName (i));
            }
            */
        }

        public void initFaceData(){
            for(int i=0; i<currentFaceEditData.blendShapeValues.Length; i++){
                currentFaceEditData.blendShapeValues[i] = 100f;
                setFaceBlendShapes((BLENDSHAPES_TYPE)System.Enum.ToObject(typeof(BLENDSHAPES_TYPE),i),100f);
            }
        }

        public void setFaceBlendShapes (BLENDSHAPES_TYPE type, float value) {
            currentFaceEditData.blendShapeValues[(int)type] = value;
            value -= 100;
            switch (type) {
                case BLENDSHAPES_TYPE.IRIS_X:
                    setBlendShapesValue (eyeRenderer, 0, 1, value);
                    break;
                case BLENDSHAPES_TYPE.IRIS_Y:
                    setBlendShapesValue (eyeRenderer, 2, 3, value);
                    break;
                case BLENDSHAPES_TYPE.IRIS_W:
                    setBlendShapesValue (eyeRenderer, 4, 5, value);
                    break;
                case BLENDSHAPES_TYPE.IRIS_H:
                    setBlendShapesValue (eyeRenderer, 6, 7, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_INNER:
                    setBlendShapesValue (faceRenderer, 18, 19, value);
                    setBlendShapesValue (eyelashesRenderer, 7, 8, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_OUTER:
                    setBlendShapesValue (faceRenderer, 20, 21, value);
                    setBlendShapesValue (eyelashesRenderer, 9, 10, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_X:
                    setBlendShapesValue (faceRenderer, 22, 23, value);
                    setBlendShapesValue (eyelashesRenderer, 11, 12, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_Y:
                    setBlendShapesValue (faceRenderer, 24, 25, value);
                    setBlendShapesValue (eyelashesRenderer, 13, 14, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_HEIGHT:
                    setBlendShapesValue (faceRenderer, 26, 27, value);
                    setBlendShapesValue (eyelashesRenderer, 15, 16, value);
                    break;
                case BLENDSHAPES_TYPE.EYE_WIDTH:
                    setBlendShapesValue (faceRenderer, 28, 29, value);
                    setBlendShapesValue (eyelashesRenderer, 17, 18, value);
                    break;
                case BLENDSHAPES_TYPE.FACE_WIDTH:
                    setBlendShapesValue (faceRenderer, 30, 31, value);
                    break;
                case BLENDSHAPES_TYPE.CHIN_LENGTH:
                    setBlendShapesValue (faceRenderer, 32, 33, value);
                    break;
                case BLENDSHAPES_TYPE.CHIN_FRONT:
                    setBlendShapesValue (faceRenderer, 34, 35, value);
                    break;
                case BLENDSHAPES_TYPE.MOUTH_Y:
                    setBlendShapesValue (faceRenderer, 36, 37, value);
                    break;
                case BLENDSHAPES_TYPE.MOUTH_W:
                    setBlendShapesValue (faceRenderer, 38, 39, value);
                    break;
                case BLENDSHAPES_TYPE.MOUTH_FRONT:
                    setBlendShapesValue (faceRenderer, 40, 41, value);
                    break;
                case BLENDSHAPES_TYPE.NOSE_Y:
                    setBlendShapesValue (faceRenderer, 42, 43, value);
                    break;
                case BLENDSHAPES_TYPE.NOSE_FRONT:
                    setBlendShapesValue (faceRenderer, 44, 45, value);
                    break;
                case BLENDSHAPES_TYPE.EYEBROWS_X:
                    setBlendShapesValue (eyebrowsRenderer, 7, 8, value);
                    break;
                case BLENDSHAPES_TYPE.EYEBROWS_Y:
                    setBlendShapesValue (eyebrowsRenderer, 9, 10, value);
                    break;
                case BLENDSHAPES_TYPE.EYEBROWS_W:
                    setBlendShapesValue (eyebrowsRenderer, 11, 12, value);
                    break;
                case BLENDSHAPES_TYPE.EYEBROWS_H:
                    setBlendShapesValue (eyebrowsRenderer, 13, 14, value);
                    break;

            }
        }

        private void setBlendShapesValue (SkinnedMeshRenderer renderer, int index1, int index2, float value) {
            if (value > 0) {
                renderer.SetBlendShapeWeight (index1, value);
                renderer.SetBlendShapeWeight (index2, 0);
            } else if (value < 0) {
                renderer.SetBlendShapeWeight (index2, -value);
                renderer.SetBlendShapeWeight (index1, 0);
            } else {
                renderer.SetBlendShapeWeight (index1, 0);
                renderer.SetBlendShapeWeight (index2, 0);
            }

        }
        
        public FaceEditData getCurrentFaceEditData(){
            return currentFaceEditData;
        }

        public void setCurrentFaceEditData(FaceEditData data){
            for(int i=0; i<data.blendShapeValues.Length; i++){
                setFaceBlendShapes((BLENDSHAPES_TYPE)System.Enum.ToObject(typeof(BLENDSHAPES_TYPE),i),data.blendShapeValues[i]);
            }
        }

    }

}