Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Imports System.Drawing.Printing
Imports System.IO

Public Class historique
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")

    Private Sub TabPage1_Click(sender As Object, e As EventArgs)

    End Sub
    Private Sub form_close(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        menu_principal.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dr1 As MySqlDataReader
        Dim cmd1 As New MySqlCommand
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "Select sum(montant_total) from factures where date_facture like '" & periode & "%'"
        dr1 = cmd1.ExecuteReader

        While dr1.Read
            If IsDBNull(dr1.GetValue(0)) Then
                Label12.Text = "NaN"
            Else
                Label12.Text = dr1.GetValue(0) & " FCFA"
            End If

        End While
        cn.Close()
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "Select sum(montant) from depense where date like '" & periode & "%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            If IsDBNull(dr1.GetValue(0)) Then
                Label7.Text = "NaN"
            Else
                Label7.Text = dr1.GetValue(0) & " FCFA"
            End If
        End While
        cn.Close()
    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub
    Dim annee As String
    Dim firstDayWeek As Date
    Dim lastDayWeek As Date
    Private Sub historique_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = Month(DateTimePicker1.Text)
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        Dim cmd1 As New MySqlCommand
        Dim dr1 As MySqlDataReader
        '''''''''''''
        'Chart2.Titles.Add("Titre1")

        ' Chart2.Titles(0).Text = "Graphique" + ControlChars.Lf + "affichant 2 series"

        ' Chart2.Titles(0).Alignment = System.Drawing.ContentAlignment.BottomRight

        '''''''''''
        firstDayWeek = DateAdd("d", 1 - Weekday(Today, vbMonday), Today)
        lastDayWeek = DateAdd("d", 7 - Weekday(Today, vbMonday), Today)
        DateTimePicker2.Text = firstDayWeek
        DateTimePicker3.Text = lastDayWeek
        annee = Year(DateTimePicker1.Text)
        Try

            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and date_facture ='" & DateTimePicker1.Text & "' "
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
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "Select sum(montant_total) from factures"
        dr1 = cmd1.ExecuteReader

        While dr1.Read
            Label12.Text = dr1.GetValue(0) & " FCFA"
        End While
        cn.Close()
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "Select sum(montant) from depense"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Label7.Text = dr1.GetValue(0) & " FCFA"
        End While
        cn.Close()
        Dim cumul As Integer
        For Each elem As ListViewItem In ListView1.Items

            cumul = cumul + elem.SubItems(5).Text

        Next
        Label9.Text = cumul & " FCFA"
        '''''''''''''''''''''''''''''''''''''''''''
        'Chart2.Series("Ventes").Points.AddXY("Jan", 0)
        'Chart2.Series("Ventes").Points.AddXY("Fev", 0)
        'Chart2.Series("Ventes").Points.AddXY("Mars", 0)
        'Chart2.Series("Ventes").Points.AddXY("Avr", 0)
        'Chart2.Series("Ventes").Points.AddXY("Mai", 0)
        'Chart2.Series("Ventes").Points.AddXY("Jui", 0)
        'Chart2.Series("Ventes").Points.AddXY("Juil", 0)
        'Chart2.Series("Ventes").Points.AddXY("Aout", 0)
        'Chart2.Series("Ventes").Points.AddXY("Sept", 0)
        'Chart2.Series("Ventes").Points.AddXY("Oct", 0)
        'Chart2.Series("Ventes").Points.AddXY("Nov", 0)
        'Chart2.Series("Ventes").Points.AddXY("", 0)

        ''''''''''''''''''''''''''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-01%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Jan", dr1.GetValue(0))
        End While
        cn.Close()
        ''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-02%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Fev", dr1.GetValue(0))
        End While
        cn.Close()
        ''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-03%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("mars", dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-04%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Avr", dr1.GetValue(0))
        End While
        cn.Close()
        ''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-05%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Mai", dr1.GetValue(0))
        End While

        cn.Close()
        '''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-06%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Jui", dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-07%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Juil", dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-08%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Aout", dr1.GetValue(0))
        End While
        cn.Close()
        '''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-09%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Sept", dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-10%'"
        dr1 = cmd1.ExecuteReader

        While dr1.Read

            Chart2.Series("Ventes").Points.AddXY("Oct", dr1.GetValue(0))

        End While
        cn.Close()
        ''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-11%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Nov", dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT_TOTAL` ) FROM factures WHERE `DATE_FACTURE` LIKE '2020-12%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Ventes").Points.AddXY("Dec", dr1.GetValue(0))
        End While
        cn.Close()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''' depenses '''''''''''''''''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-01%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        ''''''
        cn.Open()

        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-02%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-03%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-04%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-05%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-06%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-07%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-08%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-09%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        '''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-10%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-11%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()
        ''''''''''''''
        cn.Open()
        cmd1.Connection = cn
        cmd1.CommandText = "SELECT sum( `MONTANT` ) FROM depense WHERE `DATE` LIKE '2020-12%'"
        dr1 = cmd1.ExecuteReader
        While dr1.Read
            Chart2.Series("Depenses").Points.AddY(dr1.GetValue(0))
        End While
        cn.Close()

        ''''''''''''''''''''''''''''''''''''''''
        Dim stream As New System.IO.MemoryStream()

        ' Entregistrer l'image du chart dans le  stream    
        Chart2.SaveImage(stream, System.Drawing.Imaging.ImageFormat.Bmp)

        ' Créer un BitMap et le remplir avec le stream    
        ' Dim bmp As New Bitmap(stream)

        img = Image.FromStream(stream)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListView1.SelectedItems.Count = 1 Then
            Dim elem As ListViewItem = ListView1.SelectedItems(0)
            liste_factures.num_fact = elem.Text
            liste_factures.nom_client = elem.SubItems(2).Text
            Dialog_consult_fact.ShowDialog()
        End If
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click
        menu_principal.Show()
        Me.Dispose()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        menu_principal.Show()
        Me.Dispose()
    End Sub
    Dim periode As String
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

        Select Case ComboBox2.Text
            Case "Janvier"
                periode = "2020-01"
            Case "Fevrier"
                periode = "2020-02"
            Case "Mars"
                periode = "2020-03"
            Case "Avril"
                periode = "2020-04"
            Case "Mai"
                periode = "2020-05"
            Case "Juin"
                periode = "2020-06"
            Case "Juillet"
                periode = "2020-07"
            Case "Aout"
                periode = "2020-08"
            Case "Septembre"
                periode = "2020-09"
            Case "Octobre"
                periode = "2020-10"
            Case "Novembre"
                periode = "2020-11"
            Case "Decembre"
                periode = "2020-12"
        End Select
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked = True Then
            ' Try
            ListView1.Items.Clear()
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and date_facture like '" & annee + "-0" + TextBox1.Text & "%' "
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
            Dim cumul As Integer
            For Each elem As ListViewItem In ListView1.Items

                cumul = cumul + elem.SubItems(5).Text

            Next
            Label9.Text = cumul & " FCFA"
            ' Catch ex As Exception
            '     MessageBox.Show(ex.Message)
            ' End Try
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked = True Then
            ' Try
            ListView1.Items.Clear()
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and date_facture like '" & annee & "%' "
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
            Dim cumul As Integer
            For Each elem As ListViewItem In ListView1.Items

                cumul = cumul + elem.SubItems(5).Text

            Next
            Label9.Text = cumul & " FCFA"
            ' Catch ex As Exception
            '     MessageBox.Show(ex.Message)
            ' End Try
        End If
       
    End Sub
    Dim img As Image
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked = True Then
            ' Try
            ListView1.Items.Clear()
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and date_facture = '" & DateTimePicker1.Text & "' "
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
            Dim cumul As Integer
            For Each elem As ListViewItem In ListView1.Items

                cumul = cumul + elem.SubItems(5).Text

            Next
            Label9.Text = cumul & " FCFA"
            ' Catch ex As Exception
            '     MessageBox.Show(ex.Message)
            ' End Try
        End If
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            ' Try
            ListView1.Items.Clear()
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select numero_facture,cni,nom_clt,prenom_clt,date_facture,heure,montant_total from factures,client where factures.id_client = client.id_client and date_facture between '" & DateTimePicker2.Text & "' and '" & DateTimePicker3.Text & "'"
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
            Dim cumul As Integer
            For Each elem As ListViewItem In ListView1.Items

                cumul = cumul + elem.SubItems(5).Text

            Next
            Label9.Text = cumul & " FCFA"
            ' Catch ex As Exception
            '     MessageBox.Show(ex.Message)
            ' End Try
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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
        Dim radio As String
        Dim blackPen As New Pen(Brushes.Black, 1)
        Dim police As New FontFamily("Times New Roman")
        Dim size As Integer = 12
        Dim siz As Integer = 10
        G = 200
        H = 20
        If RadioButton1.Checked Then
            radio = "hebdomadaire"
        ElseIf RadioButton2.Checked Then
            radio = "mensuelle"
        ElseIf RadioButton3.Checked Then
            radio = "journaliere"
        ElseIf RadioButton4.Checked Then
            radio = "annuelle"
        End If
        'e.Graphics.DrawImage(PictureBox1.Image, 10, H + 60)
        H += 40
        ' e.Graphics.DrawPath(blackPen, Chart2.Printi)
        e.Graphics.DrawString("Liste des factures " & radio & " ", New Drawing.Font(police, 19, FontStyle.Bold), Brushes.Black, G, H + 100)
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
        ' e.Graphics.DrawImage(img, 10, H + 30)
        e.Graphics.DrawImage(img, 10, H + 30)

    End Sub
End Class