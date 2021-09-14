using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CustomizationUIController : MonoBehaviour {
	int RotateAmount;
	public Transform CharPivot;
	public SkinnedMeshRenderer Body,Face;


	public Scrollbar BustSize,BustProximity,BustPosition,ShoulderBroadness,WaistSize;
	float _BustSize,_BustProximity,_BustPosition,_ShoulderBroadness,_WaistSize;


	public Scrollbar NosePositon,MouthSize,MouthPosition,EarSize,EarElf,Face1,Face2,Face3;
	float _NosePositon,_MouthSize,_MouthPosition,_EarSize,_EarElf,_Face1,_Face2,_Face3;


	public Scrollbar UpperELidPos,LowerELidPos,EyePos,OuterESlant,InnerESlant,IrisWidth,IrisHeight,EyeBrowUP,EyeBrowClose;
	float _UpperELidPos,_LowerELidPos,_EyePos,_OuterESlant,_InnerESlant,_IrisWidth,_IrisHeight,_EyeBrowUP,_EyeBrowClose;

	public Animator ModelAnim, CamAnim;
	public GameObject [] StatsPanels;
	public ClothPreset[] Clothes;
	public GameObject [] FrontHairModels;
	public GameObject [] BackHairModels;
	public GameObject [] ExtensionModels;
	public Texture [] EyeTextures1;
	public Texture [] EyeTextures2;

	int C_Front,C_Back,C_Extension,C_Iris,ClothNum;
	public ColorPicker HairColorPicker;
	public ColorPicker EyeColorPicker;
	public ColorPicker SkinColorPicker;

	public Material HairMat;
	public Material IrisMat;
	public Material IrisShadeMat;
	public Material BodyMat;
	public Material FaceMat;

	public Material TopMat, BotMat, ShoeMat;

	public Animator RightPanelAnimator;
	public GameObject[] ColorPickers;
	public Text FrontHairText,BackHairText,CostumeName;
	Color HairCol,SkinCol,EyeCol;
	// Use this for initialization
	void Start () {
		C_Front = C_Back = C_Extension = C_Iris = 0;
		foreach (GameObject go in ColorPickers)
			go.SetActive (false);
		ResetValues ();
		IrisMat.color = EyeColorPicker.CurrentColor;
		EyeCol = EyeColorPicker.CurrentColor;
		ClothNum = -1;
		NextCostume();

	}
	public void PlayDemo()
	{
		Save ();
		SceneManager.LoadScene (1);
	}
	public void RotateCharacter (int direction)
	{
		RotateAmount = direction;
	}
	public void StopRotate ()
	{
		RotateAmount = 0;
	}
	public void OpenPanel(int num)
	{
		foreach (GameObject go in ColorPickers) {
			go.SetActive (false);
		}
		foreach (GameObject go in StatsPanels)
			go.SetActive (false);
		StatsPanels [num].SetActive (true);
		ModelAnim.SetInteger ("EditFace", num);
		CamAnim.SetInteger ("EditFace", num);
		ModelAnim.transform.localPosition = new Vector3 (0, 0, 0);

	}

	public void ToggleRightPanel()
	{
		RightPanelAnimator.SetTrigger ("Toggle");
	}
	void Update()
	{
		CharPivot.eulerAngles = new Vector3 (0, CharPivot.eulerAngles.y + (RotateAmount * 80 * Time.deltaTime), 0);
	}
	public void Save()
	{
		SavedStats._NosePositon = _NosePositon;
		SavedStats._MouthSize = _MouthSize;
		SavedStats._MouthPosition = _MouthPosition;
		SavedStats._EarSize = _EarSize;
		SavedStats._EarElf = _EarElf;
		SavedStats._Face1 = _Face1;
		SavedStats._Face2 = _Face2;
		SavedStats._Face3 = _Face3;
		SavedStats._BustSize = _BustSize;
		SavedStats._BustProximity = _BustProximity;
		SavedStats._BustPosition = _BustPosition;
		SavedStats._ShoulderBroadness = _ShoulderBroadness;
		SavedStats._WaistSize = _WaistSize;
		SavedStats._UpperELidPos=_UpperELidPos;
		SavedStats._LowerELidPos=_LowerELidPos;
		SavedStats._EyePos=_EyePos;
		SavedStats._OuterESlant=_OuterESlant;
		SavedStats._InnerESlant=_InnerESlant;
		SavedStats._IrisWidth=_IrisWidth;
		SavedStats._IrisHeight=_IrisHeight;
		SavedStats._EyeBrowUP=_EyeBrowUP;
		SavedStats._EyeBrowClose=_EyeBrowClose;
		SavedStats.FrontHairModel = C_Front;
		SavedStats.BackHairModel = C_Back;
		SavedStats.HairColor=HairCol;
		SavedStats.EyeColor=EyeCol;
		SavedStats.SkinColor=SkinCol;
		SavedStats.EyeTexture1=EyeTextures1[C_Iris];
		SavedStats.EyeTexture2=EyeTextures2[C_Iris];
	}
	public void ResetValues()
	{
		BustSize.value = 0.56f;
		BustProximity.value = 0.37f;
		BustPosition.value = 0.26f;
		ShoulderBroadness.value = 0.13f;
		WaistSize.value = 0.35f;
		UpperELidPos.value = 1f;
		LowerELidPos.value = 0f;
		NosePositon.value = 0.47f;
		MouthSize.value = 0.78f;
		MouthPosition.value = 0.43f;
		Face1.value = 0.59f;
		EyePos.value = 0.35f;
		OuterESlant.value = 0f;
		IrisWidth.value = 0.48f;
		InnerESlant.value = 0.43f;
		IrisHeight.value = 0.24f;
		EyeBrowUP.value = 0.80f;
		EyeBrowClose.value = 0.75f;
		EarSize.value = 0.10f;
		Face2.value = 0.28f;
		Face3.value = 0.68f;

		ChangeBustSize ();
		ChangeBustProximity ();
		ChangeBustPosition ();
		ChangeShoulderBroadness ();
		ChangeWaistSize ();

		ChangeNosePosition ();
		ChangeMouthSize ();
		ChangeMouthPosition ();
		ChangeEarSize ();
		SetFaceShape1 ();
		SetFaceShape2 ();
		SetFaceShape3 ();

		ChangeUpperEyeLidPos ();
		ChangeLowerEyeLidPos ();
		ChangeEyePos ();
		InnerEyeSlant ();
		OuterEyeSlant ();
		ChangeIrisWidth ();
		ChangeIrisHeight ();
		ChangeEyeBrowHeight ();
		ChangeEyeBrowCloseness ();

		IrisMat.SetTexture ("_MainTex", EyeTextures1 [C_Iris]);
		IrisShadeMat.SetTexture ("_MainTex", EyeTextures2 [C_Iris]);

	}
	//CostumeSelection
	public void NextCostume()
	{
		ClothNum++;
		if (ClothNum == Clothes.Length)
			ClothNum = 0;
		TopMat.SetTexture ("_MainTex", Clothes [ClothNum].Top);
		BotMat.SetTexture ("_MainTex", Clothes [ClothNum].Bottom);
		ShoeMat.SetTexture ("_MainTex", Clothes [ClothNum].Shoe);
		BodyMat.SetTexture ("_OverLayTex", Clothes [ClothNum].Under);
		CostumeName.text = Clothes [ClothNum].Name;
	}
	public void PrevCostume ()
	{
		ClothNum--;
		if (ClothNum == -1)
			ClothNum = Clothes.Length - 1;
		TopMat.SetTexture ("_MainTex", Clothes [ClothNum].Top);
		BotMat.SetTexture ("_MainTex", Clothes [ClothNum].Bottom);
		ShoeMat.SetTexture ("_MainTex", Clothes [ClothNum].Shoe);
		BodyMat.SetTexture ("_OverLayTex", Clothes [ClothNum].Under);
		CostumeName.text = Clothes [ClothNum].Name;
	}
	//Hair Stats

	public void ChangeHairColor()
	{
		HairMat.color = HairColorPicker.CurrentColor;
		HairCol = HairColorPicker.CurrentColor;
	}
	public void NextFront()
	{
		C_Front++;
		if (C_Front == FrontHairModels.Length)
			C_Front = 0;
		foreach (GameObject go in FrontHairModels) {
			go.SetActive (false);
		}
		FrontHairModels [C_Front].SetActive (true);
		FrontHairText.text = "Style " + (C_Front + 1).ToString ();
	}
	public void NextBack()
	{
		C_Back++;
		if (C_Back == BackHairModels.Length)
			C_Back = 0;
		foreach (GameObject go in BackHairModels) {
			go.SetActive (false);
		}
		BackHairModels [C_Back].SetActive (true);
		BackHairText.text = "Style " + (C_Back + 1).ToString ();

	}
	public void NextExtension()
	{
		C_Extension++;
		if (C_Extension == ExtensionModels.Length)
			C_Extension = 0;
		foreach (GameObject go in ExtensionModels) {
			go.SetActive (false);
		}
		ExtensionModels [C_Extension].SetActive (true);
	}
	public void PrevFront()
	{
		C_Front--;
		if (C_Front == -1)
			C_Front = FrontHairModels.Length-1;
		foreach (GameObject go in FrontHairModels) {
			go.SetActive (false);
		}
		FrontHairModels [C_Front].SetActive (true);
		FrontHairText.text = "Style " + (C_Front + 1).ToString ();

	}
	public void PrevBack()
	{
		C_Back--;
		if (C_Back == -1)
			C_Back = BackHairModels.Length-1;
		foreach (GameObject go in BackHairModels) {
			go.SetActive (false);
		}
		BackHairModels [C_Back].SetActive (true);
		BackHairText.text = "Style " + (C_Back + 1).ToString ();

	}
	public void PrevExtension()
	{
		C_Extension--;
		if (C_Extension == -1)
			C_Extension = ExtensionModels.Length-1;
		foreach (GameObject go in ExtensionModels) {
			go.SetActive (false);
		}
		ExtensionModels [C_Extension].SetActive (true);
	}



	//BodyStats
	public void ChangeBodyColor()
	{
		FaceMat.color = SkinColorPicker.CurrentColor;
		BodyMat.color = SkinColorPicker.CurrentColor;
		SkinCol = SkinColorPicker.CurrentColor;

	}
	public void ChangeBustSize()
	{
		_BustSize = (1-BustSize.value) * 100;
		Body.SetBlendShapeWeight (1, _BustSize);
	}
	public void ChangeBustProximity()
	{
		_BustProximity = BustProximity.value * 100;
		Body.SetBlendShapeWeight (0, _BustProximity);
	}
	public void ChangeBustPosition()
	{
		_BustPosition = BustPosition.value * 100;
		Body.SetBlendShapeWeight (2, _BustPosition);
	}
	public void ChangeShoulderBroadness()
	{
		_ShoulderBroadness = ShoulderBroadness.value * 100;
		Body.SetBlendShapeWeight (3, _ShoulderBroadness);
	}
	public void ChangeWaistSize()
	{
		_WaistSize = WaistSize.value * 100;
		Body.SetBlendShapeWeight (4, _WaistSize);
	}



	//FaceStats
	public void ChangeNosePosition()
	{
		_NosePositon = NosePositon.value * 100;
		Face.SetBlendShapeWeight (2, _NosePositon);
	}
	public void ChangeMouthSize()
	{
		_MouthSize = MouthSize.value * 100;
		Face.SetBlendShapeWeight (3, _MouthSize);
	}
	public void ChangeMouthPosition()
	{
		_MouthPosition = MouthPosition.value * 100;
		Face.SetBlendShapeWeight (4, _MouthPosition);
	}
	public void ChangeEarSize()
	{
		_EarSize = EarSize.value * 100;
		Face.SetBlendShapeWeight (13, _EarSize);
	}
	public void ChangeEarElf()
	{
		_EarElf = EarElf.value * 100;
		Face.SetBlendShapeWeight (16, _EarElf);
	}
	public void SetFaceShape1()
	{
		_Face1 = Face1.value * 100;
		Face.SetBlendShapeWeight (5, _Face1);
	}
	public void SetFaceShape2()
	{
		_Face2 = Face2.value * 100;
		Face.SetBlendShapeWeight (14, _Face2);
	}
	public void SetFaceShape3()
	{
		_Face3 = Face3.value * 100;
		Face.SetBlendShapeWeight (15, _Face3);
	}


	//EyeStats
	public void ChangeEyeColor()
	{
		IrisMat.color = EyeColorPicker.CurrentColor;
		EyeCol = EyeColorPicker.CurrentColor;
	}
	public void NextIris()
	{
		C_Iris++;
		if (C_Iris == EyeTextures1.Length)
			C_Iris = 0;
		Debug.Log (C_Iris);
		IrisMat.SetTexture ("_MainTex", EyeTextures1 [C_Iris]);
		IrisShadeMat.SetTexture ("_MainTex", EyeTextures2 [C_Iris]);

	}
	public void PrevIris()
	{
		C_Iris--;
		if (C_Iris == -1)
			C_Iris = EyeTextures1.Length-1;
		Debug.Log (C_Iris);
		IrisMat.SetTexture ("_MainTex", EyeTextures1 [C_Iris]);
		IrisShadeMat.SetTexture ("_MainTex", EyeTextures2 [C_Iris]);
	}
	public void ChangeUpperEyeLidPos()
	{
		_UpperELidPos = UpperELidPos.value * 100;
		Face.SetBlendShapeWeight (0, _UpperELidPos);
	}
	public void ChangeLowerEyeLidPos()
	{
		_LowerELidPos = LowerELidPos.value * 100;
		Face.SetBlendShapeWeight (1, _LowerELidPos);
	}
	public void ChangeEyePos()
	{
		_EyePos = EyePos.value * 100;
		Face.SetBlendShapeWeight (6, _EyePos);
	}
	public void OuterEyeSlant()
	{
		_OuterESlant = OuterESlant.value * 100;
		Face.SetBlendShapeWeight (7, _OuterESlant);
	}
	public void InnerEyeSlant()
	{
		_InnerESlant = InnerESlant.value * 100;
		Face.SetBlendShapeWeight (9, _InnerESlant);
	}
	public void ChangeIrisWidth()
	{
		_IrisWidth = IrisWidth.value * 100;
		Face.SetBlendShapeWeight (8, _IrisWidth);
	}
	public void ChangeIrisHeight()
	{
		_IrisHeight = IrisHeight.value * 100;
		Face.SetBlendShapeWeight (10, _IrisHeight);
	}
	public void ChangeEyeBrowHeight()
	{
		_EyeBrowUP = EyeBrowUP.value * 100;
		Face.SetBlendShapeWeight (11, _EyeBrowUP);
	}
	public void ChangeEyeBrowCloseness()
	{
		_EyeBrowClose = EyeBrowClose.value * 100;
		Face.SetBlendShapeWeight (12, _EyeBrowClose);
	}

}
