Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Imports System.Drawing.Printing

Public Class Dialog_consult_fact
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Dim id_fact As Integer
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Dialog_consult_fact_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.Items.Clear()
        TextBox1.Text = liste_factures.num_fact
        TextBox2.Text = liste_factures.nom_client

        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader

        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "Select id_facture,remise,montant_payer,montant_total from factures where numero_facture = '" & TextBox1.Text & "' "

        dr = cmd.ExecuteReader

        While dr.Read
            id_fact = dr.GetValue(0)
            TextBox3.Text = dr.GetValue(3)
            TextBox4.Text = dr.GetValue(1)
            TextBox5.Text = dr.GetValue(2)
        End While
        cn.Close()

        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader

        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "Select description_cat,libelle,qte,prix,total from articles,ligne_facture,categories where articles.id_article = ligne_facture.id_article and id_facture = '" & id_fact & "' and articles.id_cat = categories.id_cat "
        dr1 = cmd1.ExecuteReader

        While dr1.Read

            Dim liv As New ListViewItem
            liv.Text = dr1.GetValue(0)
            liv.SubItems.Add(dr1.GetValue(1))
            liv.SubItems.Add(dr1.GetValue(2))
            liv.SubItems.Add(dr1.GetValue(3))
            liv.SubItems.Add(dr1.GetValue(4))
            ListView1.Items.Add(liv)

        End While
        cn.Close()


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
        e.Graphics.DrawImage(PictureBox1.Image, 10, H + 60)
        H += 40

        e.Graphics.DrawString("Facture N° " & TextBox1.Text & "", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 100)
        e.Graphics.DrawString("Client : " & TextBox2.Text & "", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 120)
        H += 230

        e.Graphics.DrawString("Categorie", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 10, H)
        e.Graphics.DrawString("Designation", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 140, H)
        e.Graphics.DrawString("Quantité", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 360, H)
        e.Graphics.DrawString("Prix unitaire", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 500, H)
        e.Graphics.DrawString("Prix total", New Drawing.Font(police, size, FontStyle.Bold), Brushes.Black, 635, H)

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
            e.Graphics.DrawString(elem.SubItems(1).Text, New Drawing.Font(police, siz), Brushes.Black, 140, H)
            e.Graphics.DrawString(elem.SubItems(2).Text, New Drawing.Font(police, siz), Brushes.Black, 360, H)
            e.Graphics.DrawString(elem.SubItems(3).Text, New Drawing.Font(police, siz), Brushes.Black, 500, H)
            e.Graphics.DrawString(elem.SubItems(4).Text, New Drawing.Font(police, siz), Brushes.Black, 635, H)
            e.Graphics.DrawLine(blackPen, 5, H, 5, H + 20)
            e.Graphics.DrawLine(blackPen, 135, H, 135, H + 20)
            e.Graphics.DrawLine(blackPen, 355, H, 355, H + 20)
            e.Graphics.DrawLine(blackPen, 495, H, 495, H + 20)
            e.Graphics.DrawLine(blackPen, 630, H, 630, H + 20)
            e.Graphics.DrawLine(blackPen, 800, H, 800, H + 20)
            H += 20
            e.Graphics.DrawLine(blackPen, 5, H, 800, H)
        Next
        ' e.Graphics.DrawString(TextBox2.Text, New Drawing.Font(police, siz), Brushes.Black, 10, H)
        e.Graphics.DrawString("TOTAL", New Drawing.Font("Arial Black", 15), Brushes.Black, 450, H + 10)
        e.Graphics.DrawString(TextBox3.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 10)

        e.Graphics.DrawString("Remise", New Drawing.Font("Arial Black", 15), Brushes.Black, 450, H + 35)
        e.Graphics.DrawString(TextBox4.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 35)

        e.Graphics.DrawString("Montant payer", New Drawing.Font("Arial Black", 15), Brushes.Black, 370, H + 60)
        e.Graphics.DrawString(TextBox5.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 60)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
       
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
