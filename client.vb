Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class client
    Dim n As Integer
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Sub clear()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub client_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            Button2.Visible = False
            Button3.Visible = False
            Button5.Visible = False
        End If
        Button2.Enabled = False
        Try
            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select * from client"
            dr1 = cmd1.ExecuteReader
            While dr1.Read

                Dim liv As New ListViewItem
                liv.Text = dr1.GetValue(2)
                liv.SubItems.Add(dr1.GetValue(3))
                liv.SubItems.Add(dr1.GetValue(4))
                liv.SubItems.Add(dr1.GetValue(1))

                ListView1.Items.Add(liv)

            End While
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur" & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Veuillez remplir tous les champs", 48)
        Else
            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "insert into client(id_client,cni,nom_clt,prenom_clt,telephone) values('','" & TextBox4.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
            dr1 = cmd1.ExecuteReader
            cn.Close()
            Dim liv As New ListViewItem
            liv.Text = TextBox1.Text
            liv.SubItems.Add(TextBox2.Text)
            liv.SubItems.Add(TextBox3.Text)
            liv.SubItems.Add(TextBox4.Text)
            MsgBox("Client ajouté avec succès", 64)
            ListView1.Items.Add(liv)
            Clear()
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Button2.Enabled = True
        If ListView1.SelectedItems.Count = 1 Then
            Dim elem As ListViewItem = ListView1.SelectedItems(0)
            TextBox1.Text = elem.Text
            TextBox2.Text = elem.SubItems(1).Text
            TextBox3.Text = elem.SubItems(2).Text
            TextBox4.Text = elem.SubItems(3).Text

            n = elem.SubItems(3).Text
        Else
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""


        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click, Button5.Click

        Dim elem As ListViewItem = ListView1.SelectedItems(0)
        elem.Text = TextBox1.Text
        elem.SubItems(1).Text = TextBox2.Text
        elem.SubItems(2).Text = TextBox3.Text
        elem.SubItems(3).Text = TextBox4.Text
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader

        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "update client set cni= '" & TextBox4.Text & "', nom_clt = '" & TextBox1.Text & "', prenom_clt = '" & TextBox2.Text & "', telephone = '" & TextBox3.Text & "' where cni = '" & n & "'"
        dr1 = cmd1.ExecuteReader
        cn.Close()
        MsgBox("Element modifié", 64)
        Button2.Enabled = False
        clear()

    End Sub


    Private Sub ListView1_MouseMove(sender As Object, e As MouseEventArgs) Handles ListView1.MouseMove
        If ListView1.SelectedItems.Count = 1 Then
            Button2.Enabled = True
        Else
            Button2.Enabled = False
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub TextBox5_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyUp
        ListView1.Items.Clear()
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader

        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "select * from client where nom_clt like '" & TextBox5.Text & "%' or cni like '" & TextBox5.Text & "%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read

            Dim liv As New ListViewItem
            liv.Text = dr1.GetValue(2)
            liv.SubItems.Add(dr1.GetValue(3))
            liv.SubItems.Add(dr1.GetValue(4))
            liv.SubItems.Add(dr1.GetValue(1))
            ListView1.Items.Add(liv)

        End While
        cn.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dialog_offres.ShowDialog()
    End Sub
End Class

















































