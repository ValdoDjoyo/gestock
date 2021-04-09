Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Public Class Form1
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Dim path As String = IO.File.ReadAllText("path.txt")

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = False Then
            ' show password
            TextBox2.UseSystemPasswordChar = True
        Else
            ' hide password
            TextBox2.UseSystemPasswordChar = False
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Process.Start("C:\ProgramData\Microsoft\Windows\Start Menu\Programs\WampServer\start WampServer.lnk")
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        cn.Open()
        Dim command As New MySqlCommand
        '  Dim command As New MySqlCommand("SELECT `login`, `password`,fonction FROM `utilisateur` WHERE `login` = @login AND `password` = @password", cn)
        command.Connection = cn
        command.CommandText = "SELECT COUNT(login),fonction FROM `utilisateur` WHERE `login` = @login AND `password` = @password "
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = TextBox1.Text
        command.Parameters.Add("@password", MySqlDbType.VarChar).Value = TextBox2.Text

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        Dim dr As MySqlDataReader
        dr = command.ExecuteReader

        ' adapter.Fill(table)
        While dr.Read
            If dr.GetValue(0) = 0 Then
                MsgBox("Login ou mot de passe incorrect", 48)
            Else
                If dr.GetValue(1) = "admin" Then
                    connect.IsAdmin = True
                Else
                    connect.IsAdmin = False
                End If
                TextBox1.Clear()
                TextBox2.Clear()
                Me.Hide()
                menu_principal.Show()
            End If
        End While
        cn.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start(path)
    End Sub
End Class
