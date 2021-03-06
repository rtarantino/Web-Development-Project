﻿Imports System.IO
Imports System.Net.Mail

Partial Class Controls_ContactForm
    Inherits System.Web.UI.UserControl

    Protected Sub CustomValidator1_ServerValidate(source As Object, args As ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        If Not String.IsNullOrEmpty(PhoneHome.Text) Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
    End Sub

    Protected Sub SendButton_Click(sender As Object, e As EventArgs) Handles SendButton.Click
        If Page.IsValid Then
            Dim fileName As String = Server.MapPath("~/App_Data/ContactForm.txt")
            Dim mailBody As String = File.ReadAllText(fileName)

            mailBody = mailBody.Replace("##Name##", Name.Text)
            mailBody = mailBody.Replace("##Email##", EmailAddress.Text)
            mailBody = mailBody.Replace("##HomePhone##", PhoneHome.Text)

            mailBody = mailBody.Replace("##Comments##", Comments.Text)

            Dim myMessage As MailMessage = New MailMessage()
            myMessage.Subject = "Response from web site"
            myMessage.Body = mailBody

            myMessage.From = New MailAddress("you@example.com", "Planet Wrox")
            myMessage.To.Add(New MailAddress("you@example.com", "Planet Wrox"))
            myMessage.ReplyToList.Add(New MailAddress(EmailAddress.Text))

            Dim mySmtpClient As SmtpClient = New SmtpClient()
            mySmtpClient.Send(myMessage)

            Message.Visible = True
            FormTable.Visible = False
        End If
    End Sub
End Class
