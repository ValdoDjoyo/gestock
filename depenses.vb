Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class depenses
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       


        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Veuillez remplir tous les champs", 48)
        Else
            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "insert into depense(intitule,montant,date) values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & DateTimePicker1.Text & "')"
            dr1 = cmd1.ExecuteReader
            cn.Close()
            Dim liv As New ListViewItem
            liv.Text = Now.Date
            liv.SubItems.Add(TextBox1.Text)
            liv.SubItems.Add(TextBox2.Text)

            MsgBox("Effectué", 64)
            ListView1.Items.Add(liv)
            clear()

        End If
    End Sub

    Private Sub depenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            GroupBox1.Enabled = False
        End If
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        Try
            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select * from depense"
            dr1 = cmd1.ExecuteReader
            While dr1.Read

                Dim liv As New ListViewItem
                liv.Text = dr1.GetValue(3)
                liv.SubItems.Add(dr1.GetValue(1))
                liv.SubItems.Add(dr1.GetValue(2))
                ListView1.Items.Add(liv)

            End While
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur")
        End Try

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub pictureBox1_click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub
End Class