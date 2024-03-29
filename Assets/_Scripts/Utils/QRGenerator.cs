﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

public class QRGenerator : MonoBehaviour
{
    private static Color32[] Encode(string textForEncoding, int width, int height) {
        var writer = new BarcodeWriter {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions {
                Height = height,
                Width = width
            }
        };
        return writer.Write(textForEncoding);
    }

    public static Texture2D GenerateQRTexture(string text) {
        var encoded = new Texture2D (256, 256);
        var color32 = Encode(text, encoded.width, encoded.height);
        encoded.SetPixels32(color32);
        encoded.Apply();
        return encoded;
    }
    public static Sprite GenerateQRSprite(string text)
    {
        if (string.IsNullOrEmpty(text)) return null;
        
        return Sprite.Create(GenerateQRTexture(text), new Rect(0, 0, 256, 256), Vector2.zero);
    }
}
