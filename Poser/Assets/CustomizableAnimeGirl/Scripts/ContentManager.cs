﻿/*
GUIのボタンやテキストを管理するスクリプト
 */
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace CustomizableAnimeGirl {
    //CustomizeManagerが初期化された後にテキストを設定するためにオーダーを変更する
    [DefaultExecutionOrder (1)]
    public class ContentManager : MonoBehaviour {
        public CustomizeManager customizeManager;
        public FaceEditManager faceEditManager;
        public GameObject colorPicker;
        public GameObject faceEditor;

        public Text hairFrontText;
        public Text hairSideText;
        public Text hairBackText;
        public Text eyeText;
        public Text eyebrowsText;
        public Text eyelashesText;
        public Text upperBodyText;
        public Text lowerBodyText;
        public Text underwearText;
        public Text undershirtText;
        public Text legsText;
        public Text shoesText;
        public Text glovesText;
        public Text headgearText;
        public Text lipsText;
        public Text eyeShadowText;
        public Slider breastSlider;
        public Slider e_faceWidthSlider;
        public Slider e_chinLengthSlider;
        public Slider e_chinFrontSlider;
        public Slider e_mouthYSlider;
        public Slider e_mouthWidthSlider;
        public Slider e_mouthFrontSlider;
        public Slider e_noseYSlider;
        public Slider e_noseFrontSlider;
        public Slider e_eyeXSlider;
        public Slider e_eyeYSlider;
        public Slider e_eyeWidthSlider;
        public Slider e_eyeHeightSlider;
        public Slider e_eyeInnerSlider;
        public Slider e_eyeOuterSlider;
        public Slider e_irisXSlider;
        public Slider e_irisYSlider;
        public Slider e_irisWidthSlider;
        public Slider e_irisHeightSlider;
        public Slider e_eyebrowsXSlider;
        public Slider e_eyebrowsYSlider;
        public Slider e_eyebrowsWidthSlider;
        public Slider e_eyebrowsHeightSlider;

        private int currentPicker;

        public Button hairColorButton;
        public Button eyeColorButton;
        public Button eyebrowsColorButton;
        public Button eyelashesColorButton;
        public Button bodyColorButton;
        private int currentSaveData;
        void Start () {
            currentSaveData = 0;
            loadAnimeGirl (currentSaveData);
        }

        void OnAplicationQuit () {
            saveCurrentData ();
        }

        private ColorBlock getColorBlock (Color color) {
            ColorBlock cb = ColorBlock.defaultColorBlock;
            cb.normalColor = color;
            cb.highlightedColor = color;
            cb.pressedColor = color;
            return cb;
        }

        public void onClickPrevButton (int num) {
            switch (num) {
                case 0:
                    customizeManager.prevMesh (MESH_TYPE.HAIR_FRONT);
                    break;
                case 1:
                    customizeManager.prevMesh (MESH_TYPE.HAIR_SIDE);
                    break;
                case 2:
                    customizeManager.prevMesh (MESH_TYPE.HAIR_BACK);
                    break;
                case 3:
                    customizeManager.prevMaterial (MATERIAL_TYPE.EYE);
                    break;
                case 4:
                    customizeManager.prevMaterial (MATERIAL_TYPE.EYEBROWS);
                    break;
                case 5:
                    customizeManager.prevMaterial (MATERIAL_TYPE.EYELASHES);
                    break;
                case 6:
                    customizeManager.prevMesh (MESH_TYPE.UPPER_BODY);
                    break;
                case 7:
                    customizeManager.prevMesh (MESH_TYPE.LOWER_BODY);
                    break;
                case 8:
                    customizeManager.prevMesh (MESH_TYPE.UNDERWEAR);
                    break;
                case 9:
                    customizeManager.prevMesh (MESH_TYPE.UNDERSHIRT);
                    break;
                case 10:
                    customizeManager.prevMaterial (MATERIAL_TYPE.LEGS);
                    break;
                case 11:
                    customizeManager.prevMesh (MESH_TYPE.SHOES);
                    break;
                case 12:
                    customizeManager.prevMesh (MESH_TYPE.GLOVES);
                    break;
                case 13:
                    customizeManager.prevMesh (MESH_TYPE.HEADGEAR);
                    break;
                case 14:
                    customizeManager.prevMaterial (MATERIAL_TYPE.EYESHADOW);
                    break;
                case 15:
                    customizeManager.prevMaterial (MATERIAL_TYPE.LIPS);
                    break;
            }
            refreshText ();
        }
        public void onClickNextButton (int num) {
            switch (num) {
                case 0:
                    customizeManager.nextMesh (MESH_TYPE.HAIR_FRONT);
                    break;
                case 1:
                    customizeManager.nextMesh (MESH_TYPE.HAIR_SIDE);
                    break;
                case 2:
                    customizeManager.nextMesh (MESH_TYPE.HAIR_BACK);
                    break;
                case 3:
                    customizeManager.nextMaterial (MATERIAL_TYPE.EYE);
                    break;
                case 4:
                    customizeManager.nextMaterial (MATERIAL_TYPE.EYEBROWS);
                    break;
                case 5:
                    customizeManager.nextMaterial (MATERIAL_TYPE.EYELASHES);
                    break;
                case 6:
                    customizeManager.nextMesh (MESH_TYPE.UPPER_BODY);
                    break;
                case 7:
                    customizeManager.nextMesh (MESH_TYPE.LOWER_BODY);
                    break;
                case 8:
                    customizeManager.nextMesh (MESH_TYPE.UNDERWEAR);
                    break;
                case 9:
                    customizeManager.nextMesh (MESH_TYPE.UNDERSHIRT);
                    break;
                case 10:
                    customizeManager.nextMaterial (MATERIAL_TYPE.LEGS);
                    break;
                case 11:
                    customizeManager.nextMesh (MESH_TYPE.SHOES);
                    break;
                case 12:
                    customizeManager.nextMesh (MESH_TYPE.GLOVES);
                    break;
                case 13:
                    customizeManager.nextMesh (MESH_TYPE.HEADGEAR);
                    break;
                case 14:
                    customizeManager.nextMaterial (MATERIAL_TYPE.EYESHADOW);
                    break;
                case 15:
                    customizeManager.nextMaterial (MATERIAL_TYPE.LIPS);
                    break;
            }
            refreshText ();
        }

        public void onClickColorButton (int num) {
            currentPicker = num;

            colorPicker.SetActive (true);
            ColorPicker cp = colorPicker.GetComponentInChildren<ColorPicker> ();
            switch (num) {
                case 0:
                    cp.CurrentColor = customizeManager.getColor (MESH_TYPE.HAIR_FRONT);
                    break;
                case 1:
                    cp.CurrentColor = customizeManager.getColor (MATERIAL_TYPE.EYE);
                    break;
                case 2:
                    cp.CurrentColor = customizeManager.getColor (MATERIAL_TYPE.EYEBROWS);
                    break;
                case 3:
                    cp.CurrentColor = customizeManager.getColor (MATERIAL_TYPE.EYELASHES);
                    break;
                case 4:
                    cp.CurrentColor = customizeManager.getColor (MATERIAL_TYPE.UBODY);
                    break;

            }
        }

        public void closeColorPicker () {
            colorPicker.SetActive (false);
        }

        public void onColorPickerChanged (Color color) {
            switch (currentPicker) {
                case 0:
                    customizeManager.setColor (color, MESH_TYPE.HAIR_FRONT);
                    customizeManager.setColor (color, MESH_TYPE.HAIR_BACK);
                    customizeManager.setColor (color, MESH_TYPE.HAIR_SIDE);
                    hairColorButton.colors = getColorBlock (color);
                    break;
                case 1:
                    customizeManager.setColor (color, MATERIAL_TYPE.EYE);
                    eyeColorButton.colors = getColorBlock (color);
                    break;
                case 2:
                    customizeManager.setColor (color, MATERIAL_TYPE.EYEBROWS);
                    eyebrowsColorButton.colors = getColorBlock (color);
                    break;
                case 3:
                    customizeManager.setColor (color, MATERIAL_TYPE.EYELASHES);
                    eyelashesColorButton.colors = getColorBlock (color);
                    break;
                case 4:
                    customizeManager.setColor (color, MATERIAL_TYPE.UBODY);
                    customizeManager.setColor (color, MATERIAL_TYPE.LBODY);
                    customizeManager.setColor (color, MATERIAL_TYPE.FACE);
                    bodyColorButton.colors = getColorBlock (color);
                    break;
            }
        }

        public void setIrisX (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.IRIS_X, value);
        }
        public void setIrisY (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.IRIS_Y, value);
        }
        public void setIrisW (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.IRIS_W, value);
        }
        public void setIrisH (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.IRIS_H, value);
        }
        public void setEeyInner (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_INNER, value);
        }
        public void setEeyOuter (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_OUTER, value);
        }
        public void setEeyX (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_X, value);
        }
        public void setEeyY (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_Y, value);
        }
        public void setEeyHeight (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_HEIGHT, value);
        }
        public void setEeyWidth (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYE_WIDTH, value);
        }
        public void setFaceWidth (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.FACE_WIDTH, value);
        }
        public void setChinLength (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.CHIN_LENGTH, value);
        }
        public void setChinFront (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.CHIN_FRONT, value);
        }
        public void setMouthY (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.MOUTH_Y, value);
        }
        public void setMouthW (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.MOUTH_W, value);
        }
        public void setMouthFront (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.MOUTH_FRONT, value);
        }
        public void setNoseY (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.NOSE_Y, value);
        }
        public void setNoseFront (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.NOSE_FRONT, value);
        }
        public void setEeybrowsX (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_X, value);
        }
        public void setEeybrowsY (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_Y, value);
        }
        public void setEeybrowsHeight (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_H, value);
        }
        public void setEeybrowsWidth (float value) {
            faceEditManager.setFaceBlendShapes (FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_W, value);
        }

        public void onClickFaceEditorButton () {
            faceEditor.SetActive (true);
        }
        public void closeFaceEditor () {
            faceEditor.SetActive (false);
        }

        public void setBreastSize (float size) {
            customizeManager.setBreastsSize (size);
        }

        public void saveCurrentData () {
            saveAnimeGirl (currentSaveData);
        }

        public void initCurrentSaveData () {
            customizeManager.initCustomize ();
            faceEditManager.initFaceData ();
        }

        public void onClickLoadButton (int index) {
            saveCurrentData ();
            currentSaveData = index;
            loadAnimeGirl (index);
        }

        public void saveAnimeGirl (int index) {
            BinaryFormatter bf = new BinaryFormatter ();
            if (!Directory.Exists (getSecureDataPath () + "/Save/")) {
                Directory.CreateDirectory (getSecureDataPath () + "/Save/");
            }
            using (FileStream fs = new FileStream (getSecureDataPath () + "/Save/animegirldata" + index + ".dat", FileMode.Create)) {
                CustomizeManager.AnimeGirlData animeGirlData = customizeManager.getCurrentAnimeGirlData ();
                FaceEditManager.FaceEditData faceEditData = faceEditManager.getCurrentFaceEditData ();
                bf.Serialize (fs, animeGirlData);
                bf.Serialize (fs, faceEditData);
            }
        }

        public void loadAnimeGirl (int index) {
            BinaryFormatter bf = new BinaryFormatter ();
            try {
                using (FileStream fs = new FileStream (getSecureDataPath () + "/Save/animegirldata" + index + ".dat", FileMode.Open)) {
                    CustomizeManager.AnimeGirlData animeGirlData = bf.Deserialize (fs) as CustomizeManager.AnimeGirlData;
                    FaceEditManager.FaceEditData faceEditData = bf.Deserialize (fs) as FaceEditManager.FaceEditData;
                    customizeManager.setCurrentAnimeGirlData (animeGirlData);
                    faceEditManager.setCurrentFaceEditData (faceEditData);
                }
            } catch (IOException) {
                initCurrentSaveData ();
            }
            refreshText ();
            refreshSlider ();
        }

        public void refreshText () {

            //テキストを更新する
            hairFrontText.text = (customizeManager.getCurrent (MESH_TYPE.HAIR_FRONT)).ToString ();
            hairSideText.text = (customizeManager.getCurrent (MESH_TYPE.HAIR_SIDE)).ToString ();
            hairBackText.text = (customizeManager.getCurrent (MESH_TYPE.HAIR_BACK)).ToString ();
            eyeText.text = (customizeManager.getCurrent (MATERIAL_TYPE.EYE)).ToString ();
            eyebrowsText.text = (customizeManager.getCurrent (MATERIAL_TYPE.EYEBROWS)).ToString ();
            eyelashesText.text = (customizeManager.getCurrent (MATERIAL_TYPE.EYELASHES)).ToString ();
            eyeShadowText.text = (customizeManager.getCurrent (MATERIAL_TYPE.EYESHADOW)).ToString ();
            lipsText.text = (customizeManager.getCurrent (MATERIAL_TYPE.LIPS)).ToString ();
            upperBodyText.text = (customizeManager.getCurrent (MESH_TYPE.UPPER_BODY)).ToString ();
            lowerBodyText.text = (customizeManager.getCurrent (MESH_TYPE.LOWER_BODY)).ToString ();
            underwearText.text = (customizeManager.getCurrent (MESH_TYPE.UNDERWEAR)).ToString ();
            undershirtText.text = (customizeManager.getCurrent (MESH_TYPE.UNDERSHIRT)).ToString ();
            legsText.text = (customizeManager.getCurrent (MATERIAL_TYPE.LEGS)).ToString ();
            shoesText.text = (customizeManager.getCurrent (MESH_TYPE.SHOES)).ToString ();
            glovesText.text = (customizeManager.getCurrent (MESH_TYPE.GLOVES)).ToString ();
            headgearText.text = (customizeManager.getCurrent (MESH_TYPE.HEADGEAR)).ToString ();

            //色変更ボタンの色を更新
            hairColorButton.colors = getColorBlock (customizeManager.getColor (MESH_TYPE.HAIR_FRONT));
            eyeColorButton.colors = getColorBlock (customizeManager.getColor (MATERIAL_TYPE.EYE));
            eyebrowsColorButton.colors = getColorBlock (customizeManager.getColor (MATERIAL_TYPE.EYEBROWS));
            eyelashesColorButton.colors = getColorBlock (customizeManager.getColor (MATERIAL_TYPE.EYELASHES));
            bodyColorButton.colors = getColorBlock (customizeManager.getColor (MATERIAL_TYPE.UBODY));
        }

        public void refreshSlider () {

            breastSlider.value = customizeManager.getBreastSize ();
            e_faceWidthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.FACE_WIDTH];
            e_chinLengthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.CHIN_LENGTH];
            e_chinFrontSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.CHIN_FRONT];
            e_mouthYSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.MOUTH_Y];
            e_mouthWidthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.MOUTH_W];
            e_mouthFrontSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.MOUTH_FRONT];
            e_noseYSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.NOSE_Y];
            e_noseFrontSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.NOSE_FRONT];
            e_eyeXSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_X];
            e_eyeYSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_Y];
            e_eyeWidthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_WIDTH];
            e_eyeHeightSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_HEIGHT];
            e_eyeInnerSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_INNER];
            e_eyeOuterSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYE_OUTER];
            e_irisXSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.IRIS_X];
            e_irisYSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.IRIS_Y];
            e_irisWidthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.IRIS_W];
            e_irisHeightSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.IRIS_H];
            e_eyebrowsXSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_X];
            e_eyebrowsYSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_Y];
            e_eyebrowsWidthSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_W];
            e_eyebrowsHeightSlider.value = faceEditManager.getCurrentFaceEditData ().blendShapeValues[(int) FaceEditManager.BLENDSHAPES_TYPE.EYEBROWS_H];
        }

        private string getSecureDataPath () {
#if UNITY_EDITOR
            return Application.dataPath;
#elif UNITY_ANDROID
            using (var unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity"))
            using (var getFilesDir = currentActivity.Call<AndroidJavaObject> ("getFilesDir")) {
                string secureDataPathForAndroid = getFilesDir.Call<string> ("getCanonicalPath");
                return secureDataPathForAndroid;
            }
#else
            return Application.persistentDataPath;
#endif
        }
    }
}