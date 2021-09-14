using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBuilder : MonoBehaviour {
	public SkinnedMeshRenderer Body,Face;
	public GameObject [] FrontHairModels;
	public GameObject [] BackHairModels;
	public Material HairMat;
	public Material IrisMat;
	public Material IrisShadeMat;
	public Material BodyMat;
	public Material FaceMat;

	// Use this for initialization
	void Start () {
		Body.SetBlendShapeWeight (0, SavedStats._BustProximity);
		Body.SetBlendShapeWeight (2, SavedStats._BustPosition);
		Body.SetBlendShapeWeight (3, SavedStats._ShoulderBroadness);
		Body.SetBlendShapeWeight (4, SavedStats._WaistSize);
		Face.SetBlendShapeWeight (2, SavedStats._NosePositon);
		Face.SetBlendShapeWeight (3, SavedStats._MouthSize);
		Face.SetBlendShapeWeight (4, SavedStats._MouthPosition);
		Face.SetBlendShapeWeight (13, SavedStats._EarSize);
		Face.SetBlendShapeWeight (16, SavedStats._EarElf);
		Face.SetBlendShapeWeight (5, SavedStats._Face1);
		Face.SetBlendShapeWeight (14, SavedStats._Face2);
		Face.SetBlendShapeWeight (15, SavedStats._Face3);
		Face.SetBlendShapeWeight (0, SavedStats._UpperELidPos);
		Face.SetBlendShapeWeight (1, SavedStats._LowerELidPos);
		Face.SetBlendShapeWeight (6, SavedStats._EyePos);
		Face.SetBlendShapeWeight (7, SavedStats._OuterESlant);
		Face.SetBlendShapeWeight (9, SavedStats._InnerESlant);
		Face.SetBlendShapeWeight (8, SavedStats._IrisWidth);
		Face.SetBlendShapeWeight (10, SavedStats._IrisHeight);
		Face.SetBlendShapeWeight (11, SavedStats._EyeBrowUP);
		Face.SetBlendShapeWeight (12, SavedStats._EyeBrowClose);
		Body.SetBlendShapeWeight (1, SavedStats._BustSize);
		foreach (GameObject go in FrontHairModels)
			go.SetActive (false);
		foreach (GameObject go in BackHairModels)
			go.SetActive (false);
		FrontHairModels[SavedStats.FrontHairModel].SetActive(true);
		BackHairModels[SavedStats.BackHairModel].SetActive(true);
		HairMat.color = SavedStats.HairColor;
		BodyMat.color = SavedStats.SkinColor;
		FaceMat.color = SavedStats.SkinColor;
		IrisMat.color = SavedStats.EyeColor;
		IrisMat.SetTexture ("_MainTex", SavedStats.EyeTexture1);
		IrisShadeMat.SetTexture ("_MainTex", SavedStats.EyeTexture2);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
