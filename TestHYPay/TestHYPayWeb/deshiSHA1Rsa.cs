﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

/// <summary>
/// deshiSHA1Rsa 的摘要说明
/// </summary>
public class deshiSHA1Rsa
{

    /// <summary>
    /// 私钥加密
    /// </summary>
    /// <param name="content">待签名字符串</param>
    /// <param name="privateKey">私钥</param>
    /// <param name="input_charset">编码格式</param>
    /// <returns>签名后字符串</returns>
    public static string sign(string content, string privateKey, string input_charset)
    {
        byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(SHA1(content, Encoding.UTF8).ToLower());
        RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
        SHA1 sh = new SHA1CryptoServiceProvider();
        byte[] signData = rsa.SignData(Data, sh);
        return BitConverter.ToString(signData).Replace("-", string.Empty);
    }
    private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
    {
        byte[] pkcs8privatekey;
        pkcs8privatekey = Convert.FromBase64String(pemstr);
        if (pkcs8privatekey != null)
        {
            RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
            return rsa;
        }
        else
            return null;
    }
    private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
    {
        byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        byte[] seq = new byte[15];

        MemoryStream mem = new MemoryStream(pkcs8);
        int lenstream = (int)mem.Length;
        BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
        byte bt = 0;
        ushort twobytes = 0;

        try
        {
            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                binr.ReadByte();    //advance 1 byte
            else if (twobytes == 0x8230)
                binr.ReadInt16();   //advance 2 bytes
            else
                return null;

            bt = binr.ReadByte();
            if (bt != 0x02)
                return null;

            twobytes = binr.ReadUInt16();

            if (twobytes != 0x0001)
                return null;

            seq = binr.ReadBytes(15);       //read the Sequence OID
            if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                return null;

            bt = binr.ReadByte();
            if (bt != 0x04) //expect an Octet string 
                return null;

            bt = binr.ReadByte();       //read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
            if (bt == 0x81)
                binr.ReadByte();
            else
                if (bt == 0x82)
                binr.ReadUInt16();
            //------ at this stage, the remaining sequence should be the RSA private key

            byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
            RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
            return rsacsp;
        }

        catch (Exception)
        {
            return null;
        }

        finally { binr.Close(); }

    }
    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }
        return true;
    }
    private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
    {
        byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

        // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
        MemoryStream mem = new MemoryStream(privkey);
        BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
        byte bt = 0;
        ushort twobytes = 0;
        int elems = 0;
        try
        {
            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                binr.ReadByte();    //advance 1 byte
            else if (twobytes == 0x8230)
                binr.ReadInt16();   //advance 2 bytes
            else
                return null;

            twobytes = binr.ReadUInt16();
            if (twobytes != 0x0102) //version number
                return null;
            bt = binr.ReadByte();
            if (bt != 0x00)
                return null;


            //------  all private key components are Integer sequences ----
            elems = GetIntegerSize(binr);
            MODULUS = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            E = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            D = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            P = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            Q = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DP = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            DQ = binr.ReadBytes(elems);

            elems = GetIntegerSize(binr);
            IQ = binr.ReadBytes(elems);

            // ------- create RSACryptoServiceProvider instance and initialize with public key -----
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSAParameters RSAparams = new RSAParameters();
            RSAparams.Modulus = MODULUS;
            RSAparams.Exponent = E;
            RSAparams.D = D;
            RSAparams.P = P;
            RSAparams.Q = Q;
            RSAparams.DP = DP;
            RSAparams.DQ = DQ;
            RSAparams.InverseQ = IQ;
            RSA.ImportParameters(RSAparams);
            return RSA;
        }
        catch (Exception)
        {
            return null;
        }
        finally { binr.Close(); }
    }
    private static int GetIntegerSize(BinaryReader binr)
    {
        byte bt = 0;
        byte lowbyte = 0x00;
        byte highbyte = 0x00;
        int count = 0;
        bt = binr.ReadByte();
        if (bt != 0x02)     //expect integer
            return 0;
        bt = binr.ReadByte();

        if (bt == 0x81)
            count = binr.ReadByte();    // data size in next byte
        else
            if (bt == 0x82)
        {
            highbyte = binr.ReadByte(); // data size in next 2 bytes
            lowbyte = binr.ReadByte();
            byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
            count = BitConverter.ToInt32(modint, 0);
        }
        else
        {
            count = bt;     // we already have the data size
        }



        while (binr.ReadByte() == 0x00)
        {   //remove high order zeros in data
            count -= 1;
        }
        binr.BaseStream.Seek(-1, SeekOrigin.Current);       //last ReadByte wasn't a removed zero, so back up a byte
        return count;
    }

    /// <summary>
    /// SHA1 加密，返回大写字符串
    /// </summary>
    /// <param name="content">需要加密字符串</param>
    /// <param name="encode">指定加密编码</param>
    /// <returns>返回40位大写字符串</returns>
    public static string SHA1(string content, Encoding encode)
    {
        try
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_in = encode.GetBytes(content);
            byte[] bytes_out = sha1.ComputeHash(bytes_in);
            sha1.Dispose();
            string result = BitConverter.ToString(bytes_out);
            result = result.Replace("-", string.Empty);
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("SHA1加密出错：" + ex.Message);
        }
    }

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="content">待验签字符串</param>
    /// <param name="signedString">签名</param>
    /// <param name="publicKey">公钥</param>
    /// <param name="input_charset">编码格式</param>
    /// <returns>true(通过)，false(不通过)</returns>
    public static bool checkSign(string content, string signedString, string publicKey, string input_charset)
    {
        bool result = false;
        byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(SHA1(content, Encoding.UTF8).ToLower());
        //byte[] data = Encoding.GetEncoding(input_charset).GetBytes(signedString);
        byte[] data  = strToToHexByte(signedString);
        RSAParameters paraPub = ConvertFromPublicKey(publicKey);
        RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
        rsaPub.ImportParameters(paraPub);
        SHA1 sh = new SHA1CryptoServiceProvider();
        result = rsaPub.VerifyData(Data, sh, data);
        return result;
    }
    /// <summary> 
    /// 字符串转16进制字节数组 
    /// </summary> 
    /// <param name="hexString"></param> 
    /// <returns></returns> 
    private static byte[] strToToHexByte(string hexString)
    {
        hexString = hexString.Replace(" ", "");
        if ((hexString.Length % 2) != 0)
            hexString += " ";
        byte[] returnBytes = new byte[hexString.Length / 2];
        for (int i = 0; i < returnBytes.Length; i++)
            returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
        return returnBytes;
    }
    #region 解析.net 生成的Pem
    private static RSAParameters ConvertFromPublicKey(string pemFileConent)
    {

        byte[] keyData = Convert.FromBase64String(pemFileConent);
        if (keyData.Length < 162)
        {
            throw new ArgumentException("pem file content is incorrect.");
        }
        byte[] pemModulus = new byte[128];
        byte[] pemPublicExponent = new byte[3];
        Array.Copy(keyData, 29, pemModulus, 0, 128);
        Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
        RSAParameters para = new RSAParameters();
        para.Modulus = pemModulus;
        para.Exponent = pemPublicExponent;
        return para;
    }

    private static RSAParameters ConvertFromPrivateKey(string pemFileConent)
    {
        byte[] keyData = Convert.FromBase64String(pemFileConent);
        if (keyData.Length < 609)
        {
            throw new ArgumentException("pem file content is incorrect.");
        }

        int index = 11;
        byte[] pemModulus = new byte[128];
        Array.Copy(keyData, index, pemModulus, 0, 128);

        index += 128;
        index += 2;//141
        byte[] pemPublicExponent = new byte[3];
        Array.Copy(keyData, index, pemPublicExponent, 0, 3);

        index += 3;
        index += 4;//148
        byte[] pemPrivateExponent = new byte[128];
        Array.Copy(keyData, index, pemPrivateExponent, 0, 128);

        index += 128;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//279
        byte[] pemPrime1 = new byte[64];
        Array.Copy(keyData, index, pemPrime1, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//346
        byte[] pemPrime2 = new byte[64];
        Array.Copy(keyData, index, pemPrime2, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//412/413
        byte[] pemExponent1 = new byte[64];
        Array.Copy(keyData, index, pemExponent1, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//479/480
        byte[] pemExponent2 = new byte[64];
        Array.Copy(keyData, index, pemExponent2, 0, 64);

        index += 64;
        index += ((int)keyData[index + 1] == 64 ? 2 : 3);//545/546
        byte[] pemCoefficient = new byte[64];
        Array.Copy(keyData, index, pemCoefficient, 0, 64);

        RSAParameters para = new RSAParameters();
        para.Modulus = pemModulus;
        para.Exponent = pemPublicExponent;
        para.D = pemPrivateExponent;
        para.P = pemPrime1;
        para.Q = pemPrime2;
        para.DP = pemExponent1;
        para.DQ = pemExponent2;
        para.InverseQ = pemCoefficient;
        return para;
    }
    #endregion






    public deshiSHA1Rsa()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
}