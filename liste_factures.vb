Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Public Class liste_factures
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Public num_fact As Integer
    Public nom_client As String
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListView1.SelectedItems.Count = 1 Then
            Dim elem As ListViewItem = ListView1.SelectedItems(0)

            num_fact = elem.Text
            nom_client = elem.SubItems(2).Text
            Dialog_consult_fact.ShowDialog()
        End If

    End Sub

    Private Sub liste_factures_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client "
            dr = cmd.ExecuteReader

            While dr.Read

                Dim liv As New ListViewItem
                liv.Text = dr.GetValue(0)
                liv.SubItems.Add(dr.GetValue(1))
                liv.SubItems.Add(dr.GetValue(2) + " " + dr.GetValue(3))
                liv.SubItems.Add(dr.GetValue(4))
                liv.SubItems.Add(dr.GetValue(5))
                liv.SubItems.Add(dr.GetValue(6))
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

    Private Sub TextBox5_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyUp
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ListView1.Items.Clear()
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and( cni like'" & TextBox5.Text & "%' or nom_clt like'" & TextBox5.Text & "%' ) "
        dr = cmd.ExecuteReader

        While dr.Read

            Dim liv As New ListViewItem
            liv.Text = dr.GetValue(0)
            liv.SubItems.Add(dr.GetValue(1))
            liv.SubItems.Add(dr.GetValue(2) + " " + dr.GetValue(3))
            liv.SubItems.Add(dr.GetValue(4))
            liv.SubItems.Add(dr.GetValue(5))
            liv.SubItems.Add(dr.GetValue(6))
            ListView1.Items.Add(liv)

        End While
        cn.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim H As Integer = 0
        Dim G As Integer = 0
        Dim blackPen As New Pen(Brushes.Black, 1)
        Dim police As New FontFamily("Consolas")
        Dim size As Integer = 12
        Dim siz As Integer = 10
        G = 200
        H = 20
        'e.Graphics.DrawImage(PictureBox1.Image, 10, H + 60)
        H += 40

        e.Graphics.DrawString("Liste des factures ", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 100)
        ' e.Graphics.DrawString("Client : " & TextBox2.Text & "", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 120)
        H += 230

        e.Graphics.DrawString("N°Facture", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 10, H)
        e.Graphics.DrawString("Noms et prenoms", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 140, H)
        e.Graphics.DrawString("Date", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 360, H)
        e.Graphics.DrawString("Heure", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 500, H)
        e.Graphics.DrawString("Montant total", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 635, H)

        e.Graphics.DrawLine(blackPen, 5, H, 800, H)
        e.Graphics.DrawLine(blackPen, 5, H + 20, 800, H + 20)

        e.Graphics.DrawLine(blackPen, 5, H, 5, H + 20)
        e.Graphics.DrawLine(blackPen, 135, H, 135, H + 20)
        e.Graphics.DrawLine(blackPen, 355, H, 355, H + 20)
        e.Graphics.DrawLine(blackPen, 495, H, 495, H + 20)
        e.Graphics.DrawLine(blackPen, 630, H, 630, H + 20)
        e.Graphics.DrawLine(blackPen, 800, H, 800, H + 20)
        H += 20
        For Each elem As ListViewItem In ListView1.Items

            e.Graphics.DrawString(elem.SubItems(0).Text, New Drawing.Font(police, siz), Brushes.Black, 10, H)
            e.Graphics.DrawString(elem.SubItems(2).Text, New Drawing.Font(police, siz), Brushes.Black, 140, H)
            e.Graphics.DrawString(elem.SubItems(3).Text, New Drawing.Font(police, siz), Brushes.Black, 360, H)
            e.Graphics.DrawString(elem.SubItems(4).Text, New Drawing.Font(police, siz), Brushes.Black, 500, H)
            e.Graphics.DrawString(elem.SubItems(5).Text, New Drawing.Font(police, siz), Brushes.Black, 635, H)
            e.Graphics.DrawLine(blackPen, 5, H, 5, H + 20)
            e.Graphics.DrawLine(blackPen, 135, H, 135, H + 20)
            e.Graphics.DrawLine(blackPen, 355, H, 355, H + 20)
            e.Graphics.DrawLine(blackPen, 495, H, 495, H + 20)
            e.Graphics.DrawLine(blackPen, 630, H, 630, H + 20)
            e.Graphics.DrawLine(blackPen, 800, H, 800, H + 20)
            H += 20
            e.Graphics.DrawLine(blackPen, 5, H, 800, H)
        Next
       

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'pour choisir l'imprimante
        Dim dlg As New PrintDialog

        dlg.Document = PrintDocument1

        Dim result As DialogResult = dlg.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            'afficher un apercu avant impression
            Dim dllg As New PrintPreviewDialog

            dllg.Document = PrintDocument1

            dllg.ShowDialog()
            PrintDocument1.Print()
        End If
    End Sub
End Class