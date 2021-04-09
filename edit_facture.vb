Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports MySql.Data
Imports System.Drawing.Printing
Public Class edit_facture
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Dim inter As String
    Dim cumul As Decimal = 0
    Private Sub edit_facture_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader

            ComboBox2.Items.Clear()
            TextBox6.Clear()
            TextBox2.Text = cumul
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "select * from categories"
            dr = cmd.ExecuteReader
            While dr.Read

                ComboBox2.Items.Add(dr.GetValue(1))

            End While
            cn.Close()

            Dim cmd1 As New MySqlCommand
            Dim dr1 As MySqlDataReader

            ComboBox1.Items.Clear()
            cn.Open()
            cmd1.Connection = cn
            cmd1.CommandText = "select * from client"
            dr1 = cmd1.ExecuteReader
            While dr1.Read

                ComboBox1.Items.Add(dr1.GetValue(2))

            End While
            cn.Close()
            Dim rdm As New Random

            Dim num As Integer = rdm.Next(111111, 999999)
            ' cn.Open()
            ' cmd2.Connection = cn
            'cmd2.CommandText = "select * from numero"
            'dr2 = cmd2.ExecuteReader
            'int = dr2.GetValue(0)
            'While int = num
            'num = rdm.Next(111111, 999999)
            'End While
            TextBox1.Text = num
            'cn.Close()
        Catch ex As Exception
            MessageBox.Show("Probleme de connection au serveur")
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        ComboBox3.Items.Clear()
        ComboBox3.Text = ""
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

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "select id_article from articles where libelle = '" & ComboBox3.Text & "'"
        dr1 = cmd1.ExecuteReader

        While dr1.Read

            TextBox8.Text = dr1.GetValue(0)

        End While
        cn.Close()
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select prix from articles where libelle = '" & ComboBox3.Text & "'"
        dr = cmd.ExecuteReader

        While dr.Read

            TextBox6.Text = dr.GetValue(0)

        End While
        cn.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        If TextBox6.Text = "" Or ComboBox2.Text = "" Or ComboBox3.Text = "" Or ComboBox4.Text = "" Then
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
                    Dim liv As New ListViewItem
                    liv.Text = TextBox8.Text
                    liv.SubItems.Add(ComboBox2.Text)
                    liv.SubItems.Add(ComboBox3.Text)
                    liv.SubItems.Add(ComboBox4.Text)
                    liv.SubItems.Add(TextBox6.Text)
                    liv.SubItems.Add(TextBox6.Text * ComboBox4.Text)
                    cumul = cumul + TextBox6.Text * ComboBox4.Text
                    TextBox2.Text = cumul
                    TextBox4.Text = cumul
                    ListView1.Items.Add(liv)
                End If


            End While
            cn.Close()


            '''''''
            ComboBox2.Text = ""
            ComboBox3.Text = ""
            ComboBox4.Text = ""
            TextBox6.Clear()
            Dim rdm As New Random
            Dim num As Integer = rdm.Next(111111, 999999)
            TextBox1.Text = num
        End If

    End Sub

   
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not variable.chiffre.Contains(e.KeyChar) And Not Asc(e.KeyChar) = 8 Then
            e.Handled = True
        End If


    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        Dim text3 As Integer

        If TextBox3.Text = "" Or Not IsNumeric(TextBox3.Text) Then
            text3 = 0
        Else
            text3 = TextBox3.Text
        End If
        TextBox4.Text = TextBox2.Text - text3
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        Dim cmd2 As New MySqlCommand
        Dim dr2 As MySqlDataReader
        Dim cmd3 As New MySqlCommand
        Dim dr3 As MySqlDataReader
        Dim cmd4 As New MySqlCommand
        Dim dr4 As MySqlDataReader
        Dim id_fact As Integer
        If TextBox2.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("veuillez remplir tous les champs", 48)
        Else


            cn.Open()

            ' Format(CDate(), "aaaa-mm-jj")
            If TextBox3.Text = "" Then
                TextBox3.Text = 0
            End If

            cmd.Connection = cn
            cmd.CommandText = "insert into factures(`numero_facture`,`ID_CLIENT`,`DATE_FACTURE`,`heure`,`REMISE`,`MONTANT_PAYER`,`MONTANT_TOTAL`) values('" & TextBox1.Text & "','" & TextBox7.Text & "','" & DateTimePicker1.Text & "','" & TimeOfDay & "','" & TextBox3.Text & "','" & TextBox5.Text & "','" & TextBox4.Text & "')"
            dr = cmd.ExecuteReader
            cn.Close()

            ''''''''''''''''''''''''''''''''''''''
            cn.Open()
            cmd2.Connection = cn
            cmd2.CommandText = "select id_facture from factures where numero_facture = '" & TextBox1.Text & "'"
            dr2 = cmd2.ExecuteReader

            While dr2.Read

                id_fact = dr2.GetValue(0)

            End While
            cn.Close()
            ''''''''''''''''''''''''''''''''''''''

            For Each elem As ListViewItem In ListView1.Items
                cn.Open()
                cmd1.Connection = cn
                cmd1.CommandText = "insert into ligne_facture(`ID_FACTURE`,`ID_ARTICLE`,`QTE`,`TOTAL`) values('" & id_fact & "','" & elem.Text & "','" & elem.SubItems(3).Text & "','" & elem.SubItems(5).Text & "')"
                dr1 = cmd1.ExecuteReader
                cn.Close()
                cn.Open()
                cmd4.Connection = cn
                cmd4.CommandText = "update articles set qte_stock = qte_stock - '" & elem.SubItems(3).Text & "' where id_article = '" & elem.Text & "'"
                dr4 = cmd4.ExecuteReader
                cn.Close()
            Next
            Dim n As Decimal = TextBox5.Text - TextBox4.Text
            If n < 0 Then
                cn.Open()
                cmd3.Connection = cn
                cmd3.CommandText = "update client set dette = dette - '" & n & "' where id_client = '" & TextBox7.Text & "'"
                dr3 = cmd3.ExecuteReader
                cn.Close()
            End If
            MsgBox("Vente effectuée avec success", 64)
            TextBox2.Text = 0
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            ComboBox1.Text = ""
            ListView1.Items.Clear()

        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select id_client from client where nom_clt = '" & ComboBox1.Text & "' limit 1"
        dr = cmd.ExecuteReader

        While dr.Read

            TextBox7.Text = dr.GetValue(0)

        End While
        cn.Close()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        menu_principal.Show()
        Me.Close()
    End Sub

    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ComboBox2.Text = ""
        ComboBox3.Text = ""
        ComboBox4.Text = ""
        TextBox6.Clear()
        ListView1.Items.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
       
        'pour modifier la page
        Dim psDlg As New PageSetupDialog

        Dim LePageSettings As New PageSettings

        psDlg.PageSettings = LePageSettings

        PrintDocument1.DefaultPageSettings = LePageSettings
        If (psDlg.ShowDialog = System.Windows.Forms.DialogResult.OK) Then
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
        End If
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim H As Integer = 0
        Dim G As Integer = 0
        Dim blackPen As New Pen(Brushes.Black, 1)
        Dim police As New FontFamily("Consolas")
        Dim size As Integer = 12
        Dim siz As Integer = 10
        G = 200
        H = 20
        e.Graphics.DrawImage(PictureBox2.Image, 10, H + 60)
        H += 40

        e.Graphics.DrawString("Facture N° " & TextBox1.Text & "", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 100)
        e.Graphics.DrawString("Client : " & ComboBox1.Text & "", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 120)
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

            e.Graphics.DrawString(elem.SubItems(1).Text, New Drawing.Font(police, siz), Brushes.Black, 10, H)
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
        ' e.Graphics.DrawString(TextBox2.Text, New Drawing.Font(police, siz), Brushes.Black, 10, H)
        e.Graphics.DrawString("TOTAL", New Drawing.Font("Arial Black", 15), Brushes.Black, 450, H + 10)
        e.Graphics.DrawString(TextBox2.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 10)

        e.Graphics.DrawString("Remise", New Drawing.Font("Arial Black", 15), Brushes.Black, 450, H + 35)
        e.Graphics.DrawString(TextBox3.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 35)

        e.Graphics.DrawString("Montant payer", New Drawing.Font("Arial Black", 15), Brushes.Black, 370, H + 60)
        e.Graphics.DrawString(TextBox5.Text + " FCFA", New Drawing.Font("Old English Text", 15), Brushes.Black, 550, H + 60)


        ' e.Graphics.DrawString("NB: Les marchandises vendues et livrées ne sont ni reprises ni echangées", New Drawing.Font("Arial Black", 14), Brushes.Black, 50, H + 35)
        '  e.Graphics.DrawString("Caissière", New Drawing.Font("Consolas", 14), Brushes.Black, 50, H + 70)

    End Sub
End Class