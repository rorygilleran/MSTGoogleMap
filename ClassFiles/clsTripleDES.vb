Imports System
Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Public Class clsTripleDES

    ' Addapted from article by Wrox team
    ' http://www.devarticles.com/c/a/VB.Net/String-Encryption-With-Visual-Basic-.NET/3/


    Private key() As Byte = {201, 102, 43, 14, 65, 56, 70, 18, 19, 103, 110, 121, 133, 144, 150, 163, 172, 184, 191, 20, 21, 22, 23, 24}
    Private iv() As Byte = {65, 110, 68, 26, 69, 178, 200, 219}

    Public Function Encrypt(ByVal plainText As String) As Byte()
        ' Declare a UTF8Encoding object so we may use the GetByte 
        ' method to transform the plainText into a Byte array. 
        Dim utf8encoder As UTF8Encoding = New UTF8Encoding()
        Dim inputInBytes() As Byte = utf8encoder.GetBytes(plainText)

        ' Create a new TripleDES service provider 
        Dim tdesProvider As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()

        ' The ICryptTransform interface uses the TripleDES 
        ' crypt provider along with encryption key and init vector 
        ' information 
        Dim cryptoTransform As ICryptoTransform = tdesProvider.CreateEncryptor(Me.key, Me.iv)

        ' All cryptographic functions need a stream to output the 
        ' encrypted information. Here we declare a memory stream 
        ' for this purpose. 
        Dim encryptedStream As MemoryStream = New MemoryStream()
        Dim cryptStream As CryptoStream = New CryptoStream(encryptedStream, cryptoTransform, CryptoStreamMode.Write)

        ' Write the encrypted information to the stream. Flush the information 
        ' when done to ensure everything is out of the buffer. 
        cryptStream.Write(inputInBytes, 0, inputInBytes.Length)
        cryptStream.FlushFinalBlock()
        encryptedStream.Position = 0

        ' Read the stream back into a Byte array and return it to the calling 
        ' method. 
        Dim result(encryptedStream.Length - 1) As Byte
        encryptedStream.Read(result, 0, encryptedStream.Length)
        cryptStream.Close()
        Return result
    End Function


    Public Function Decrypt(ByVal inputInBytes() As Byte) As String
        ' UTFEncoding is used to transform the decrypted Byte Array 
        ' information back into a string. 
        Dim utf8encoder As UTF8Encoding = New UTF8Encoding()
        Dim tdesProvider As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider()

        ' As before we must provide the encryption/decryption key along with 
        ' the init vector. 
        Dim cryptoTransform As ICryptoTransform = tdesProvider.CreateDecryptor(Me.key, Me.iv)

        ' Provide a memory stream to decrypt information into 
        Dim decryptedStream As MemoryStream = New MemoryStream()
        Dim cryptStream As CryptoStream = New CryptoStream(decryptedStream, cryptoTransform, CryptoStreamMode.Write)
        cryptStream.Write(inputInBytes, 0, inputInBytes.Length)
        cryptStream.FlushFinalBlock()
        decryptedStream.Position = 0

        ' Read the memory stream and convert it back into a string 
        Dim result(decryptedStream.Length - 1) As Byte
        decryptedStream.Read(result, 0, decryptedStream.Length)
        cryptStream.Close()
        Dim myutf As UTF8Encoding = New UTF8Encoding()
        Return myutf.GetString(result)
    End Function

    Public Function EncryptToHexString(ByVal theStr As String) As String

        Dim i As Integer

        Dim theEncryptBytes() As Byte

        EncryptToHexString = ""

        Try
            theEncryptBytes = Encrypt(theStr)

            theStr = ""
            ' Create string
            For i = 0 To theEncryptBytes.Length - 1
                theStr = theStr & Chr(theEncryptBytes(i))
            Next

            ' Convert to Hex couplets

            EncryptToHexString = ASCII2Hex(theStr)

        Catch ex As Exception

        End Try
    End Function

    Public Function DecryptFromHexStr(ByVal theStr As String) As String

        Dim i As Integer
        Dim LenBytes As Integer

        Dim theEncryptBytes(1024) As Byte

        Dim myChar As Char

        DecryptFromHexStr = ""

        Try
            theStr = HexToASCIIString(theStr)
            LenBytes = Len(theStr) - 1
            ReDim theEncryptBytes(LenBytes)

            For i = 0 To LenBytes
                myChar = CType(Mid(theStr, i + 1, 1), Char)
                theEncryptBytes(i) = CType(Asc(myChar), Byte)
            Next

            DecryptFromHexStr = Decrypt(theEncryptBytes)
        Catch ex As Exception

        End Try

    End Function
    Private Function ASCII2Hex(ByVal Ascii_string As String) As String
        '   Routine to convert an ASCII string to a string of HEX values

        Dim l, i As Int16
        Dim a As String
        Dim b As String
        Dim HEXString As String

        ASCII2Hex = ""

        Try
            l = Len(Ascii_string)

            HEXString = ""
            For i = 1 To l
                a = Mid(Ascii_string, i, 1)
                b = Hex(Asc(a))
                If Len(b) < 2 Then
                    b = "0" & b
                End If
                HEXString = HEXString & b
            Next i

            ASCII2Hex = HEXString
        Catch exc As Exception

        End Try

    End Function    ' ASCII2Hex

    Private Function HexToASCIIString(ByVal In_Data As String) As String
        ' Convert Hex Couplets to Text Characters

        Dim i As Int16
        Dim wrkH As String

        HexToASCIIString = ""
        For i = 1 To Len(In_Data$) Step 2
            wrkH = Mid(In_Data, i, 2)
            HexToASCIIString = HexToASCIIString & Chr(Convert.ToInt32(wrkH, 16))
        Next i

        HexToASCIIString = HexToASCIIString


    End Function

End Class
