Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class dette
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Dim n As Integer
    Private Sub dette_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            GroupBox1.Enabled = False
        End If
        Button1.Enabled = False
        Try
            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select * from client where dette > 0"
            dr1 = cmd1.ExecuteReader
            While dr1.Read

                Dim liv As New ListViewItem
                liv.Text = dr1.GetValue(1)
                liv.SubItems.Add(dr1.GetValue(2))
                liv.SubItems.Add(dr1.GetValue(3))
                liv.SubItems.Add(dr1.GetValue(6))
                ListView1.Items.Add(liv)
            End While
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur")
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        Dim elem As ListViewItem = ListView1.SelectedItems(0)
        If TextBox2.Text = "" Then
            MsgBox("Veuillez entrer un montant", 48)
        Else


            If TextBox2.Text >= elem.SubItems(3).Text Then
                ' ListView1.SelectedItems.Clear()
                elem.Remove()
                cn.Open()
                cmd1.Connection = cn
                cmd1.CommandText = "update client set dette = dette - '" & TextBox2.Text & "' where cni = '" & n & "'"
                dr1 = cmd1.ExecuteReader
                cn.Close()
                MsgBox("effectué", 64)
                TextBox1.Text = ""
                TextBox2.Text = ""
                Button1.Enabled = False
            Else

                elem.SubItems(3).Text = elem.SubItems(3).Text - TextBox2.Text

                cn.Open()
                cmd1.Connection = cn
                cmd1.CommandText = "update client set dette = dette - '" & TextBox2.Text & "' where cni = '" & n & "'"
                dr1 = cmd1.ExecuteReader
                cn.Close()
                MsgBox("effectué", 64)
                TextBox1.Text = ""
                TextBox2.Text = ""
                Button1.Enabled = False
            End If
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count = 1 Then
            Dim elem As ListViewItem = ListView1.SelectedItems(0)
            TextBox1.Text = elem.SubItems(1).Text & " " & elem.SubItems(2).Text
            n = elem.Text
            Button1.Enabled = True
        Else
            TextBox1.Text = ""
            TextBox2.Text = ""
            Button1.Enabled = False
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub label5_click(sender As Object, e As EventArgs) Handles Label5.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub textbox5_keyUp(sender As Object, e As EventArgs) Handles TextBox5.KeyUp
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        ListView1.Items.Clear()
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "select * from client where dette > 0 and cni like '" & TextBox5.Text & "%' or nom_clt like '" & TextBox5.Text & "'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read

            Dim liv As New ListViewItem
            liv.Text = dr1.GetValue(1)
            liv.SubItems.Add(dr1.GetValue(2))
            liv.SubItems.Add(dr1.GetValue(3))
            liv.SubItems.Add(dr1.GetValue(6))
            ListView1.Items.Add(liv)
        End While
        cn.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub
End Class