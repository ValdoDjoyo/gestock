Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Public Class Dialog_offres
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        Dim qte As Integer
        If ComboBox1.Text = "" Or ComboBox2.Text = "" Or ComboBox3.Text = "" Or ComboBox4.Text = "" Then
            MsgBox("veuillez remplir tous les champs", 48)
        Else

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select qte_stock from articles where libelle = '" & ComboBox3.Text & "' limit 1"
            dr1 = cmd1.ExecuteReader
            While dr1.Read
                If dr1.GetValue(0) < ComboBox4.Text Then
                    MsgBox("Quantité insuffisante en stock", 48)
                Else

                End If
            End While
        End If

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dialog_offres_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ComboBox1.Items.Clear()
        ' ComboBox1.Text = stock.cat
        ' ComboBox2.Text = stock.libe
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from categories"
        dr = cmd.ExecuteReader
        While dr.Read

            ComboBox2.Items.Add(dr.GetValue(1))

        End While
        cn.Close()
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        Dim inter As Integer
        ComboBox2.Items.Clear()
        ComboBox2.Text = ""
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select id_cat from categories where description_cat = '" & ComboBox2.Text & "'"
        dr = cmd.ExecuteReader
        While dr.Read
            inter = dr.GetValue(0)
        End While
        cn.Close()

        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from articles where id_cat = '" & inter & "'"
        dr = cmd.ExecuteReader

        While dr.Read

            ComboBox3.Items.Add(dr.GetValue(2))

        End While
        cn.Close()
    End Sub
    Dim id_client As Integer
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select id_client from client where nom_clt = '" & ComboBox1.Text & "' limit 1"
        dr = cmd.ExecuteReader

        While dr.Read

            id_client = dr.GetValue(0)

        End While
        cn.Close()
    End Sub
    Dim id_article As Integer
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "select id_article from articles where libelle = '" & ComboBox3.Text & "'"
        dr1 = cmd1.ExecuteReader

        While dr1.Read

            id_article = dr1.GetValue(0)

        End While
        cn.Close()
    End Sub
End Class
