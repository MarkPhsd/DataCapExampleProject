Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dsiEMVX As Object = CreateObject("DSIEMVXLib.DsiEMVX")
        Dim requestString = "<?xml version=""1.0""?>" & vbCrLf &
                    "<TStream>" & vbCrLf &
                    "    <Admin>" & vbCrLf &
                    "        <HostOrIP>dsl1.dsipscs.com</HostOrIP>" & vbCrLf &
                    "        <IpPort>9000</IpPort>" & vbCrLf &
                    "        <MerchantID>700000012262</MerchantID>" & vbCrLf &
                    "        <TerminalID>001</TerminalID>" & vbCrLf &
                    "        <OperatorID>TEST</OperatorID>" & vbCrLf &
                    "        <UserTrace>Dev1</UserTrace>" & vbCrLf &
                    "        <TranCode>EMVParamDownload</TranCode>" & vbCrLf &
                    "        <SecureDevice>EMV_VX805_PAYMENTECH</SecureDevice>" & vbCrLf &
                    "        <ComPort>1</ComPort>" & vbCrLf &
                    "        <SequenceNo>0010010010</SequenceNo>" & vbCrLf &
                    "    </Admin>" & vbCrLf &
                    "</TStream>"
        Dim resultString As String = dsiEMVX.ProcessTransaction(requestString)
        If (Not CheckResponse(resultString, "Success")) Then
            Return
        End If

    End Sub

    Function CheckResponse(toCheck As String, forWhat As String)
        If Not String.IsNullOrEmpty(toCheck) Then
            Dim cmdResponse As XElement = XElement.Parse(toCheck).Element("CmdResponse")
            Dim cmdStatus As String = cmdResponse.Element("CmdStatus").Value
            If (cmdStatus = forWhat) Then
                Return True
            End If
            Console.WriteLine(String.Format("{0} {1} {2} - {3}",
                cmdResponse.Element("ResponseOrigin").Value, cmdStatus,
                cmdResponse.Element("DSIXReturnCode").Value, cmdResponse.Element("TextResponse").Value))
        End If
        Return False
    End Function
End Class
