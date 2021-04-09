Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data

Public Class Dialog_ravitaillement
    Dim inter As String
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim cmd As New MySqlCommand

        Dim dr As MySqlDataReader

        cn.Open()
        cmd.Connection = cn
        ' cmd.CommandText = "insert into produit values('','" & ComboBox1.Text & "', '" & ComboBox2.Text & "', '" & TextBox2.Text & "')"
        cmd.CommandText = "update articles set QTE_STOCK = QTE_STOCK + '" & TextBox2.Text & "' where LIBELLE = '" & ComboBox2.Text & "'"
        dr = cmd.ExecuteReader
        cn.Close()

        stock.refreshh()
        stock.Show()
        menu_principal.Hide()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ComboBox2.Items.Clear()
        ComboBox2.Text = ""
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select id_cat from categories where description_cat = '" & ComboBox1.Text & "'"
        dr = cmd.ExecuteReader
        While dr.Read
            inter = dr.GetValue(0)
        End While
        cn.Close()

        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from articles where id_cat = '" & inter & "' order by libelle"
        dr = cmd.ExecuteReader

        While dr.Read

            ComboBox2.Items.Add(dr.GetValue(2))

        End While
        cn.Close()
    End Sub

    Private Sub Dialog_ravitaillement_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ComboBox1.Items.Clear()
        ComboBox1.Text = stock.cat
        ComboBox2.Text = stock.libe
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from categories order by description_cat"
        dr = cmd.ExecuteReader
        While dr.Read

            ComboBox1.Items.Add(dr.GetValue(1))

        End While
        cn.Close()
    End Sub


End Class
