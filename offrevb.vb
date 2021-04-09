Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class offrevb
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")

    Private Sub offrevb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select cni,nom_clt,prenom_clt,libelle,quantite,date from offre,client,articles where client.id_client=offre.id_clt and articles.id_article=offre.id_produit"
            dr1 = cmd1.ExecuteReader
            While dr1.Read

                Dim liv As New ListViewItem
                liv.Text = dr1.GetValue(0)
                liv.SubItems.Add(dr1.GetValue(1) + " " + dr1.GetValue(2))
                liv.SubItems.Add(dr1.GetValue(3))
                liv.SubItems.Add(dr1.GetValue(4))
                liv.SubItems.Add(dr1.GetValue(5))
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

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub
End Class