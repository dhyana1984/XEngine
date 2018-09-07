/**************************************************************************
*
* NAME        : AccountController.cs
*
* VERSION     : 1.0.0
*
* DATE        : 20-Mar-2016
*
* DESCRIPTION : 字符串加密解密
*
* MODIFICATION HISTORY
* Name             Date         Description
* ===============  ===========  =======================================
* Miro Yuan        21-Mar-2016  Initial Version
*
**************************************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace XEngine.Web.Utility
{
    public class EncryptDecrypt
    {
        private static string strEncrKey = "XEngine!@#";

        //字符串加密   
        public static string Encrypt(string strText)
        {
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = System.Text.Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                string sRet = Convert.ToBase64String(ms.ToArray());

                return sRet;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //字符串解密   
        public static string Decrypt(string strText)
        {
            strText = strText.Replace(' ', '+');
            Byte[] byKey = { };
            Byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            Byte[] inputByteArray = new byte[strText.Length];
            try
            {
                byKey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                string sRet = encoding.GetString(ms.ToArray());
                return sRet;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}