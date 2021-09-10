/*
AnimeGirlをカスタマイズするためのスクリプト
髪型や装備等のメッシュやマテリアル情報のリスト管理・差し替えを行う
*/

using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

namespace CustomizableAnimeGirl {
    public class CustomizeManager : MonoBehaviour {
        public GameObject animeGirlObject;
        private AnimeGirlManager animeGirlManager;
        public string MATERIALS_PATH = "Assets/CustomizableAnimeGirl/Models/Materials/";
        public string FBX_PATH = "Assets/CustomizableAnimeGirl/Models/FBX/";
        public string[] FBX_LIST = new string[] {
            "StandardEquipments.fbx",
            "StandardHairs.fbx",
        };

        [System.Serializable]
        public class AnimeGirlData {

            public int[] currentMeshes = new int[System.Enum.GetValues (typeof (MESH_TYPE)).Length];
            public int[] currentMaterials = new int[System.Enum.GetValues (typeof (MESH_TYPE)).Length];
            public SerializableColor[] currentMeshColors = new SerializableColor[System.Enum.GetValues (typeof (MESH_TYPE)).Length];
            public SerializableColor[] currentMaterialColors = new SerializableColor[System.Enum.GetValues (typeof (MESH_TYPE)).Length];
            public float currentBreastSize;
        }
        private AnimeGirlData currentAnimeGirlData;
        public float defaultBreastSize = 0.8f;

        [System.Serializable]
        public class InitialEquipment {
            public int hairFront = 0;
            public int hairSide = 0;
            public int hairBack = 0;
            public int upperBody = 1;
            public int lowerBody = 1;
            public int underwear = 1;
            public int undershirt = 1;
            public int shoes = 0;
            public int gloves = 0;
            public int headgear = 0;
            public int eye = 0;
            public int eyebrows = 0;
            public int eyelashes = 0;
            public int legs = 0;
            public int eyeshadow = 0;
            public int lips = 0;

            [ColorUsage (false, true)]
            public Color hairColor = new Color (1f, 0.8f, 0.8f);
            [ColorUsage (false, true)]
            public Color eyeColor = new Color (0.8f, 0.5f, 0.5f);
            [ColorUsage (false, true)]
            public Color eyelashesColor = new Color (0.15f, 0.12f, 0.1f);
            [ColorUsage (false, true)]
            public Color eyebrowsColor = new Color (0.15f, 0.12f, 0.1f);
            [ColorUsage (false, true)]
            public Color bodyColor = new Color (1f, 1f, 1f);

        }
        public InitialEquipment initialEquipment;

        [System.Serializable]
        public class MeshInfo {
            public string name;
            public Mesh mesh;
            public Material material;
            public int bodyType;
        }
        public List<MeshInfo> hairFrontList;
        public List<MeshInfo> hairSideList;
        public List<MeshInfo> hairBackList;
        public List<MeshInfo> upperBodyList;
        public List<MeshInfo> lowerBodyList;
        public List<MeshInfo> underwearList;
        public List<MeshInfo> undershirtList;
        public List<MeshInfo> shoesList;
        public List<MeshInfo> glovesList;
        public List<MeshInfo> headgearList;
        public List<Material> legsList;
        public List<Material> eyeList;
        public List<Material> eyebrowsList;
        public List<Material> eyelashesList;
        public List<Material> eyeShadowList;
        public List<Material> lipsList;

#if UNITY_EDITOR

        // FBX内にあるメッシュを自動検出するメソッド
        [ContextMenu ("AutoDetectMeshes")]
        public void AutoDetectMeshes () {
            //リストを初期化する
            MeshInfo noneMeshInfo = new MeshInfo ();
            noneMeshInfo.name = "None";
            upperBodyList = new List<MeshInfo> ();
            upperBodyList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            lowerBodyList = new List<MeshInfo> ();
            lowerBodyList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            underwearList = new List<MeshInfo> ();
            underwearList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            undershirtList = new List<MeshInfo> ();
            undershirtList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            shoesList = new List<MeshInfo> ();
            shoesList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            glovesList = new List<MeshInfo> ();
            glovesList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            headgearList = new List<MeshInfo> ();
            headgearList.Add (noneMeshInfo); //先頭に裸用の空メッシュ情報を追加
            hairFrontList = new List<MeshInfo> ();
            hairSideList = new List<MeshInfo> ();
            hairBackList = new List<MeshInfo> ();

            if(!this.FBX_PATH.EndsWith("/"))this.FBX_PATH += "/";
            if(!this.MATERIALS_PATH.EndsWith("/"))this.MATERIALS_PATH += "/";

            Regex numRegex = new Regex (@"[^0-9]");

            foreach (string fbxName in this.FBX_LIST) {
                foreach (Object obj in AssetDatabase.LoadAllAssetRepresentationsAtPath (this.FBX_PATH + fbxName)) {
                    if (obj.name.StartsWith ("Hair") && obj.GetType ().Equals (typeof (Mesh))) {
                        MeshInfo meshInfo = new MeshInfo ();
                        meshInfo.mesh = (Mesh) obj;
                        if (obj.name.StartsWith ("Hair_Front")) {
                            // マテリアルのパスを計算
                            string path = this.MATERIALS_PATH + obj.name.Replace ("Front_", "") + ".mat";
                            meshInfo.material = AssetDatabase.LoadAssetAtPath<Material> (path);
                            meshInfo.name = obj.name.Replace ("Hair_Front_", "");
                            hairFrontList.Add (meshInfo);
                        } else
                        if (obj.name.StartsWith ("Hair_Side")) {
                            string path = this.MATERIALS_PATH + obj.name.Replace ("Side_", "") + ".mat";
                            meshInfo.material = AssetDatabase.LoadAssetAtPath<Material> (path);
                            meshInfo.name = obj.name.Replace ("Hair_Side_", "");
                            hairSideList.Add (meshInfo);
                        } else
                        if (obj.name.StartsWith ("Hair_Back")) {
                            string path = this.MATERIALS_PATH + obj.name.Replace ("Back_", "") + ".mat";
                            meshInfo.material = AssetDatabase.LoadAssetAtPath<Material> (path);
                            meshInfo.name = obj.name.Replace ("Hair_Back_", "");
                            hairBackList.Add (meshInfo);
                        }
                    } else if (obj.name.StartsWith ("UP_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string start = "";
                        int bodyType = 0;
                        if (obj.name.StartsWith ("UP_LONG_")) {
                            start = "UP_LONG_";
                            bodyType = 2;
                        } else if (obj.name.StartsWith ("UP_SHORT_")) {
                            start = "UP_SHORT_";
                            bodyType = 1;
                        } else if (obj.name.StartsWith ("UP_NONE_")) {
                            start = "UP_NONE_";
                            bodyType = 0;
                        } else {
                            start = "UP_";
                            bodyType = 0;
                        }
                        string name = obj.name.Replace (start, "");

                        addMeshInfoToList (upperBodyList, (Mesh) obj, name, bodyType);
                    } else if (obj.name.StartsWith ("LO_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string start = "";
                        int bodyType = 0;
                        if (obj.name.StartsWith ("LO_LONG_")) {
                            start = "LO_LONG_";
                            bodyType = 2;
                        } else if (obj.name.StartsWith ("LO_SHORT_")) {
                            start = "LO_SHORT_";
                            bodyType = 1;
                        } else if (obj.name.StartsWith ("LO_NONE_")) {
                            start = "LO_NONE_";
                            bodyType = 0;
                        } else {
                            start = "LO_";
                            bodyType = 0;
                        }
                        string name = obj.name.Replace (start, "");

                        addMeshInfoToList (lowerBodyList, (Mesh) obj, name, bodyType);
                    } else if (obj.name.StartsWith ("UN_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string name = obj.name.Replace ("UN_", "");
                        addMeshInfoToList (underwearList, (Mesh) obj, name, 0);
                    } else if (obj.name.StartsWith ("US_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string name = obj.name.Replace ("US_", "");
                        addMeshInfoToList (undershirtList, (Mesh) obj, name, 0);
                    } else if (obj.name.StartsWith ("SH_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string name = obj.name.Replace ("SH_", "");
                        addMeshInfoToList (shoesList, (Mesh) obj, name, 0);
                    } else if (obj.name.StartsWith ("GL_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string name = obj.name.Replace ("GL_", "");
                        addMeshInfoToList (glovesList, (Mesh) obj, name, 0);
                    } else if (obj.name.StartsWith ("HD_") && obj.GetType ().Equals (typeof (Mesh))) {
                        string name = obj.name.Replace ("HD_", "");
                        addMeshInfoToList (headgearList, (Mesh) obj, name, 0);
                    }
                }
            }
        }

        // 該当するマテリアルを検索し、装備のメッシュ情報リストに追加する
        private void addMeshInfoToList (List<MeshInfo> list, Mesh mesh, string name, int bodyType) {
            bool isMaterialFound = false;

            foreach (Material mat in NonResources.LoadAll<Material> (this.MATERIALS_PATH)) {
                if (mat.name.StartsWith (name)) {
                    MeshInfo meshInfo = new MeshInfo ();
                    meshInfo.mesh = mesh;
                    meshInfo.material = mat;
                    meshInfo.name = mat.name;
                    meshInfo.bodyType = bodyType;
                    list.Add (meshInfo);
                    isMaterialFound = true;
                }
            }
            if (!isMaterialFound) print (name + " material not found.");
        }

        // FBX内のマテリアルを自動検出するメソッド
        [ContextMenu ("AutoDetectMaterials")]
        public void AutoDetectMaterials () {

            eyeList = new List<Material> ();
            eyebrowsList = new List<Material> ();
            eyelashesList = new List<Material> ();
            legsList = new List<Material> ();
            eyeShadowList = new List<Material> ();
            lipsList = new List<Material> ();
            
            if(!this.FBX_PATH.EndsWith("/"))this.FBX_PATH += "/";
            if(!this.MATERIALS_PATH.EndsWith("/"))this.MATERIALS_PATH += "/";
            
            foreach (Material mat in NonResources.LoadAll<Material> (this.MATERIALS_PATH)) {
                if (mat.name.StartsWith ("Eye_")) {
                    eyeList.Add (mat);
                } else if (mat.name.StartsWith ("Eyebrows_")) {
                    eyebrowsList.Add (mat);
                } else if (mat.name.StartsWith ("Eyelashes_")) {
                    eyelashesList.Add (mat);
                } else if (mat.name.StartsWith ("Legs_")) {
                    legsList.Add (mat);
                } else if (mat.name.StartsWith ("EyeShadow_")) {
                    eyeShadowList.Add (mat);
                } else if (mat.name.StartsWith ("Lips_")) {
                    lipsList.Add (mat);
                }
            }
        }

#endif

        // Start is called before the first frame update
        void Start () {
            animeGirlManager = animeGirlObject.GetComponent<AnimeGirlManager> ();
            initCustomize ();
        }

        //カスタマイズ項目をすべて初期化する
        public void initCustomize () {
            currentAnimeGirlData = new AnimeGirlData ();

            //初期カラーを設定
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT] = initialEquipment.hairColor;
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_SIDE] = initialEquipment.hairColor;
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_BACK] = initialEquipment.hairColor;
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.UPPER_BODY] = animeGirlManager.getMeshColor (MESH_TYPE.UPPER_BODY);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.LOWER_BODY] = animeGirlManager.getMeshColor (MESH_TYPE.LOWER_BODY);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.UNDERWEAR] = animeGirlManager.getMeshColor (MESH_TYPE.UNDERWEAR);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.UNDERSHIRT] = animeGirlManager.getMeshColor (MESH_TYPE.UNDERSHIRT);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.SHOES] = animeGirlManager.getMeshColor (MESH_TYPE.SHOES);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.GLOVES] = animeGirlManager.getMeshColor (MESH_TYPE.GLOVES);
            currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HEADGEAR] = animeGirlManager.getMeshColor (MESH_TYPE.HEADGEAR);
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYE] = initialEquipment.eyeColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYEBROWS] = initialEquipment.eyebrowsColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYELASHES] = initialEquipment.eyelashesColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.LEGS] = animeGirlManager.getMaterialColor (MATERIAL_TYPE.LEGS);
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.UBODY] = initialEquipment.bodyColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.LBODY] = initialEquipment.bodyColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.FACE] = initialEquipment.bodyColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYESHADOW] = initialEquipment.bodyColor;
            currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.LIPS] = initialEquipment.bodyColor;
            //初期メッシュを設定
            setMesh (initialEquipment.hairFront, MESH_TYPE.HAIR_FRONT);
            setMesh (initialEquipment.hairSide, MESH_TYPE.HAIR_SIDE);
            setMesh (initialEquipment.hairBack, MESH_TYPE.HAIR_BACK);
            setMesh (initialEquipment.upperBody, MESH_TYPE.UPPER_BODY);
            setMesh (initialEquipment.lowerBody, MESH_TYPE.LOWER_BODY);
            setMesh (initialEquipment.underwear, MESH_TYPE.UNDERWEAR);
            setMesh (initialEquipment.undershirt, MESH_TYPE.UNDERSHIRT);
            setMesh (initialEquipment.shoes, MESH_TYPE.SHOES);
            setMesh (initialEquipment.gloves, MESH_TYPE.GLOVES);
            setMesh (initialEquipment.headgear, MESH_TYPE.HEADGEAR);
            //初期マテリアルを設定
            setMaterial (initialEquipment.eye, MATERIAL_TYPE.EYE);
            setMaterial (initialEquipment.eyebrows, MATERIAL_TYPE.EYEBROWS);
            setMaterial (initialEquipment.eyelashes, MATERIAL_TYPE.EYELASHES);
            setMaterial (initialEquipment.legs, MATERIAL_TYPE.LEGS);
            setMaterial (initialEquipment.eyeshadow, MATERIAL_TYPE.EYESHADOW);
            setMaterial (initialEquipment.lips, MATERIAL_TYPE.LIPS);

            animeGirlManager.setBreastsSize (defaultBreastSize);
            currentAnimeGirlData.currentBreastSize = defaultBreastSize;
        }

        // Update is called once per frame
        void Update () {

        }

        public AnimeGirlData getCurrentAnimeGirlData () {
            return currentAnimeGirlData;
        }

        public void setCurrentAnimeGirlData (AnimeGirlData animeGirlData) {
            currentAnimeGirlData = animeGirlData;

            //色を設定
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.HAIR_FRONT);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.HAIR_SIDE);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.HAIR_BACK);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.UPPER_BODY);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.LOWER_BODY);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.UNDERWEAR);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.UNDERSHIRT);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.SHOES);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.GLOVES);
            setColor (currentAnimeGirlData.currentMeshColors[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.HEADGEAR);
            setColor (currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYE], MATERIAL_TYPE.EYE);
            setColor (currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYEBROWS], MATERIAL_TYPE.EYEBROWS);
            setColor (currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.EYELASHES], MATERIAL_TYPE.EYELASHES);
            setColor (currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.LEGS], MATERIAL_TYPE.LEGS);
            setColor (currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.UBODY], MATERIAL_TYPE.UBODY);

            //メッシュを設定
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.HAIR_FRONT], MESH_TYPE.HAIR_FRONT);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.HAIR_SIDE], MESH_TYPE.HAIR_SIDE);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.HAIR_BACK], MESH_TYPE.HAIR_BACK);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.UPPER_BODY], MESH_TYPE.UPPER_BODY);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.LOWER_BODY], MESH_TYPE.LOWER_BODY);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.UNDERWEAR], MESH_TYPE.UNDERWEAR);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.UNDERSHIRT], MESH_TYPE.UNDERSHIRT);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.SHOES], MESH_TYPE.SHOES);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.GLOVES], MESH_TYPE.GLOVES);
            setMesh (currentAnimeGirlData.currentMeshes[(int) MESH_TYPE.HEADGEAR], MESH_TYPE.HEADGEAR);
            //マテリアルを設定
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.EYE], MATERIAL_TYPE.EYE);
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.EYEBROWS], MATERIAL_TYPE.EYEBROWS);
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.EYELASHES], MATERIAL_TYPE.EYELASHES);
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.LEGS], MATERIAL_TYPE.LEGS);
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.EYESHADOW], MATERIAL_TYPE.EYESHADOW);
            setMaterial (currentAnimeGirlData.currentMaterials[(int) MATERIAL_TYPE.LIPS], MATERIAL_TYPE.LIPS);

            animeGirlManager.setBreastsSize (currentAnimeGirlData.currentBreastSize);
        }

        public void setBreastsSize (float size) {
            animeGirlManager.setBreastsSize (size);
            currentAnimeGirlData.currentBreastSize = size;
        }

        public float getBreastSize () {
            return currentAnimeGirlData.currentBreastSize;
        }

        // 現在設定されているメッシュのIDを取得
        public int getCurrent (MESH_TYPE meshType) {
            return currentAnimeGirlData.currentMeshes[(int) meshType];
        }
        // 現在設定されているマテリアルのIDを取得
        public int getCurrent (MATERIAL_TYPE materialType) {
            return currentAnimeGirlData.currentMaterials[(int) materialType];
        }

        // 髪型、装備のメッシュを設定する
        public void setMesh (int id, MESH_TYPE meshType) {
            Mesh mesh = null;
            Material material = null;
            List<MeshInfo> meshInfoList = getListFromMeshType (meshType);
            if(meshInfoList.Count == 0)return;
            mesh = meshInfoList[id].mesh;
            material = meshInfoList[id].material;
            if (meshType == MESH_TYPE.UPPER_BODY) {
                animeGirlManager.setBodyMesh (MATERIAL_TYPE.UBODY, upperBodyList[id].bodyType, currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.UBODY]);
            } else if (meshType == MESH_TYPE.LOWER_BODY) {
                animeGirlManager.setBodyMesh (MATERIAL_TYPE.LBODY, lowerBodyList[id].bodyType, currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.UBODY]);
            }

            currentAnimeGirlData.currentMeshes[(int) meshType] = id;
            animeGirlManager.setMesh (mesh, material, meshType);
            switch (meshType) {
                case MESH_TYPE.HAIR_FRONT:
                case MESH_TYPE.HAIR_SIDE:
                case MESH_TYPE.HAIR_BACK:
                    animeGirlManager.setMeshColor (meshType, currentAnimeGirlData.currentMeshColors[(int) meshType]);
                    break;
                default:
                    break;
            }
        }

        private List<MeshInfo> getListFromMeshType (MESH_TYPE meshType) {
            switch (meshType) {
                case MESH_TYPE.HAIR_FRONT:
                    return hairFrontList;
                case MESH_TYPE.HAIR_SIDE:
                    return hairSideList;
                case MESH_TYPE.HAIR_BACK:
                    return hairBackList;
                case MESH_TYPE.UPPER_BODY:
                    return upperBodyList;
                case MESH_TYPE.LOWER_BODY:
                    return lowerBodyList;
                case MESH_TYPE.UNDERWEAR:
                    return underwearList;
                case MESH_TYPE.UNDERSHIRT:
                    return undershirtList;
                case MESH_TYPE.SHOES:
                    return shoesList;
                case MESH_TYPE.GLOVES:
                    return glovesList;
                case MESH_TYPE.HEADGEAR:
                    return headgearList;
            }
            return null;
        }

        // 登録されているメッシュの数を取得する
        public int getMeshListCount (MESH_TYPE meshType) {
            return getListFromMeshType (meshType).Count;
        }

        // 髪型、装備を次のメッシュへ変更する
        public void nextMesh (MESH_TYPE meshType) {
            currentAnimeGirlData.currentMeshes[(int) meshType]++;
            if (currentAnimeGirlData.currentMeshes[(int) meshType] >= getMeshListCount (meshType)) {
                currentAnimeGirlData.currentMeshes[(int) meshType] = 0;
            }
            setMesh (currentAnimeGirlData.currentMeshes[(int) meshType], meshType);
        }

        // 髪型、装備を前のメッシュへ変更する
        public void prevMesh (MESH_TYPE meshType) {
            currentAnimeGirlData.currentMeshes[(int) meshType]--;
            if (currentAnimeGirlData.currentMeshes[(int) meshType] < 0) {
                currentAnimeGirlData.currentMeshes[(int) meshType] = getMeshListCount (meshType) - 1;
            }
            setMesh (currentAnimeGirlData.currentMeshes[(int) meshType], meshType);

        }
        public void nextMesh (int meshType) {
            nextMesh ((MESH_TYPE) meshType);
        }
        public void prevMesh (int meshType) {
            prevMesh ((MESH_TYPE) meshType);
        }

        // 登録されているマテリアルの数を取得する
        public int getMaterialListCount (MATERIAL_TYPE materialType) {
            return getListFromMaterialType (materialType).Count;
        }

        // 目、眉毛、まつ毛、脚のマテリアルを設定する
        public void setMaterial (int id, MATERIAL_TYPE materialType) {
            List<Material> materialList = getListFromMaterialType (materialType);
            if(materialList.Count == 0)return;
            Material material = materialList[id];
            currentAnimeGirlData.currentMaterials[(int) materialType] = id;
            animeGirlManager.setMaterial (material, materialType);
            if (materialType == MATERIAL_TYPE.LIPS || materialType == MATERIAL_TYPE.EYESHADOW) {
                animeGirlManager.setMaterialColor (materialType, currentAnimeGirlData.currentMaterialColors[(int) MATERIAL_TYPE.UBODY]);
            } else {
                animeGirlManager.setMaterialColor (materialType, currentAnimeGirlData.currentMaterialColors[(int) materialType]);
            }
        }

        private List<Material> getListFromMaterialType (MATERIAL_TYPE materialType) {
            switch (materialType) {
                case MATERIAL_TYPE.EYE:
                    return eyeList;
                case MATERIAL_TYPE.EYEBROWS:
                    return eyebrowsList;
                case MATERIAL_TYPE.EYELASHES:
                    return eyelashesList;
                case MATERIAL_TYPE.LEGS:
                    return legsList;
                case MATERIAL_TYPE.EYESHADOW:
                    return eyeShadowList;
                case MATERIAL_TYPE.LIPS:
                    return lipsList;
            }
            return null;
        }

        public void setMaterial (string name, MATERIAL_TYPE materialType) {
            List<Material> materialList = getListFromMaterialType (materialType);
            setMaterial (materialList.FindIndex (mat => mat.name == name), materialType);
        }

        // 目、眉毛、まつ毛、脚を次のマテリアルへ変更する
        public void nextMaterial (MATERIAL_TYPE materialType) {
            currentAnimeGirlData.currentMaterials[(int) materialType]++;
            if (currentAnimeGirlData.currentMaterials[(int) materialType] >= getMaterialListCount (materialType)) {
                currentAnimeGirlData.currentMaterials[(int) materialType] = 0;
            }
            setMaterial (currentAnimeGirlData.currentMaterials[(int) materialType], materialType);
        }
        // 目、眉毛、まつ毛、脚を前のマテリアルへ変更する
        public void prevMaterial (MATERIAL_TYPE materialType) {
            currentAnimeGirlData.currentMaterials[(int) materialType]--;
            if (currentAnimeGirlData.currentMaterials[(int) materialType] < 0) {
                currentAnimeGirlData.currentMaterials[(int) materialType] = getMaterialListCount (materialType) - 1;
            }
            setMaterial (currentAnimeGirlData.currentMaterials[(int) materialType], materialType);

        }
        public void nextMaterial (int materialType) {
            nextMaterial ((MATERIAL_TYPE) materialType);
        }
        public void prevMaterial (int materialType) {
            prevMaterial ((MATERIAL_TYPE) materialType);
        }

        public void setColor (Color color, MESH_TYPE meshType) {
            animeGirlManager.setMeshColor (meshType, color);
            currentAnimeGirlData.currentMeshColors[(int) meshType] = color;
        }
        public void setColor (Color color, MATERIAL_TYPE materialType) {
            animeGirlManager.setMaterialColor (materialType, color);
            currentAnimeGirlData.currentMaterialColors[(int) materialType] = color;
        }
        public Color getColor (MESH_TYPE meshType) {
            return currentAnimeGirlData.currentMeshColors[(int) meshType];
        }
        public Color getColor (MATERIAL_TYPE materialType) {
            return currentAnimeGirlData.currentMaterialColors[(int) materialType];
        }
    }
}

[System.Serializable]
public class SerializableColor {

    public float[] colorStore = new float[4] { 1F, 1F, 1F, 1F };
    public Color Color {
        get { return new Color (colorStore[0], colorStore[1], colorStore[2], colorStore[3]); }
        set { colorStore = new float[4] { value.r, value.g, value.b, value.a }; }
    }

    //makes this class usable as Color, Color normalColor = mySerializableColor;
    public static implicit operator Color (SerializableColor instance) {
        return instance.Color;
    }

    //makes this class assignable by Color, SerializableColor myColor = Color.white;
    public static implicit operator SerializableColor (Color color) {
        return new SerializableColor { Color = color };
    }
}