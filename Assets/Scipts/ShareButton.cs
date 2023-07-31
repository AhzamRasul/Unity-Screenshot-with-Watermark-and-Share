using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class ShareButton : MonoBehaviour
{
   public void ClickShare()
   {
        StartCoroutine(TakeScreenshotAndShare());
   }

private IEnumerator TakeScreenshotAndShare()
{
	yield return new WaitForEndOfFrame();

	Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
	ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
	ss.Apply();

	string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
	File.WriteAllBytes( filePath, ss.EncodeToPNG() );

	// To avoid memory leaks
	Destroy( ss );

	new NativeShare().AddFile( filePath )
		.SetSubject( "arthing" ).SetText( "Sharing new content from arthing" )
		.Share();

}

	public GameObject WaterMark;
	
	public void Trigger()
	{
		if(WaterMark.activeInHierarchy == false)
		{
			WaterMark.SetActive(true);
		}
		else
		{
			StartCoroutine(Hide());
		}
	}

IEnumerator Hide()
{
	yield return new WaitForSeconds(2f);
	WaterMark.SetActive(false);
}	
}



