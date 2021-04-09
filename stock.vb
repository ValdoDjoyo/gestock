Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class stock
    Public cat As String
    Public libe As String
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")

    Sub refreshh()
        Try
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            ListView1.Items.Clear()
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select libelle,description_cat,qte_stock from articles,categories where articles.id_cat = categories.id_cat order by libelle"
            dr = cmd.ExecuteReader

            While dr.Read

                Dim liv As New ListViewItem
                liv.Text = dr.GetValue(0)
                liv.SubItems.Add(dr.GetValue(1))
                liv.SubItems.Add(dr.GetValue(2))
                If (dr(2) <= 5) Then
                    liv.BackColor = Color.LightCoral
                End If
                ListView1.Items.Add(liv)

            End While
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur")
        End Try

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dialog_ravitaillement.ShowDialog()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dialog_new_produit.ShowDialog()
    End Sub

    Public Sub stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            Button1.Enabled = False
            Button2.Enabled = False
        End If
        Try


            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader

            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select libelle,description_cat,qte_stock from articles,categories where articles.id_cat = categories.id_cat order by libelle"
            dr = cmd.ExecuteReader

            While dr.Read

                Dim liv As New ListViewItem
                liv.Text = dr.GetValue(0)
                liv.SubItems.Add(dr.GetValue(1))
                liv.SubItems.Add(dr.GetValue(2))
                If (dr(2) <= 5) Then
                    liv.BackColor = Color.LightCoral
                End If
                ListView1.Items.Add(liv)

            End While
            cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur")
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedItems.Count >= 1 Then
            Dim elem As ListViewItem = ListView1.SelectedItems(0)
            libe = elem.Text
            cat = elem.SubItems(1).Text
        Else
            libe = ""
            cat = ""
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
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ListView1.Items.Clear()
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "Select libelle,description_cat,qte_stock from articles,categories where articles.id_cat = categories.id_cat and (libelle like '" & TextBox5.Text & "%' or description_cat like '" & TextBox5.Text & "%')"
        dr = cmd.ExecuteReader

        While dr.Read

            Dim liv As New ListViewItem
            liv.Text = dr.GetValue(0)
            liv.SubItems.Add(dr.GetValue(1))
            liv.SubItems.Add(dr.GetValue(2))
            If (dr(2) <= 5) Then
                liv.BackColor = Color.LightCoral
            End If
            ListView1.Items.Add(liv)

        End While
        cn.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub
End Class