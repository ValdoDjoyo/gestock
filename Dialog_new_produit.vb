Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data

Public Class Dialog_new_produit
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Dim inter As String
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OK_Button_Click_1(sender As Object, e As EventArgs) Handles OK_Button.Click
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("Veuillez remplir tous les champs", 48)
        Else

            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "insert into articles(`ID_CAT`,`LIBELLE`,`QTE_STOCK`,`PRIX`) values('" & inter & "', '" & TextBox1.Text & "','0', '" & TextBox2.Text & "')"
            dr = cmd.ExecuteReader
            cn.Close()
            stock.Close()
            stock.Show()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End If
       
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader

        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select id_cat from categories where description_cat = '" & ComboBox1.Text & "'"
        dr = cmd.ExecuteReader
        While dr.Read
            inter = dr.GetValue(0)
        End While
        cn.Close()

    End Sub

    Private Sub Dialog_new_produit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Clear()
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ComboBox1.Items.Clear()
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from categories"
        dr = cmd.ExecuteReader
        While dr.Read

            ComboBox1.Items.Add(dr.GetValue(1))

        End While
        cn.Close()
    End Sub
End Class