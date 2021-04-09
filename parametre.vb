Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data.DataTable
Public Class parametre
    Dim cn As New MySqlConnection("server='" & connect.serveur & "';user id='" & connect.user & "';password='" & connect.password & "';database='" & connect.bd & "'")
    Sub recharger()
        ListView1.Items.Clear()
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "select * from utilisateur"
        dr = cmd.ExecuteReader
        While dr.Read
            Dim liv As New ListViewItem
            liv.Text = dr.GetValue(0)
            liv.SubItems.Add(dr.GetValue(1))
            liv.SubItems.Add(dr.GetValue(3))
            ListView1.Items.Add(liv)
        End While
        cn.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
      
    End Sub

    Private Sub parametre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            GroupBox4.Enabled = False
            GroupBox3.Visible = False
        End If
        Try
            TextBox1.Text = IO.File.ReadAllText("url.txt")
            TextBox2.Text = IO.File.ReadAllText("user.txt")
            TextBox3.Text = IO.File.ReadAllText("password.txt")
        Catch ex As Exception
            MessageBox.Show("Erreur fichier introuvable")
        End Try
        recharger()
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim fonction As String
        If CheckBox1.Checked = False Then
            fonction = "user"
        Else
            fonction = "admin"
        End If
        If TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Veuillez remplir tous les champs", 48)
        ElseIf TextBox5.Text <> TextBox6.Text Then
            MsgBox("Les mots de passes ne correspondent pas", 48)
        Else
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "insert into utilisateur(login,password,fonction) value('" & TextBox4.Text & "','" & TextBox5.Text & "','" & fonction & "')"
            dr = cmd.ExecuteReader
            MsgBox("Effectuer", 64)
            cn.Close()
            TextBox4.Clear()
            TextBox5.Clear()
            TextBox6.Clear()
            recharger()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim fonc As String
        cn.Open()
        Dim command As New MySqlCommand
        '  Dim command As New MySqlCommand("SELECT `login`, `password`,fonction FROM `utilisateur` WHERE `login` = @login AND `password` = @password", cn)
        command.Connection = cn
        command.CommandText = "SELECT id, COUNT(login),login,fonction FROM `utilisateur` WHERE `login` = @login AND `password` = @password "
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = TextBox7.Text
        command.Parameters.Add("@password", MySqlDbType.VarChar).Value = TextBox8.Text
        Dim dr As MySqlDataReader
        dr = command.ExecuteReader

        ' adapter.Fill(table)
        While dr.Read
            If dr.GetValue(1) = 0 Then
                MsgBox("Login ou mot de passe incorrect", 48)
            Else
                GroupBox4.Enabled = True
                TextBox9.Text = dr.GetValue(2)
                If dr.GetValue(3) = "admin" Then
                    CheckBox2.Checked = True
                Else
                    CheckBox2.Checked = False
                End If
                If dr.GetValue(0) = 1 Then
                    Button6.Enabled = False
                    CheckBox2.Enabled = False
                Else
                    Button6.Enabled = True
                    CheckBox2.Enabled = True
                End If
            End If
        End While
        cn.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If TextBox9.Text = "" Or TextBox10.Text = "" Or TextBox11.Text = "" Then
            MsgBox("Veuillez remplir tous les champs", 48)
        ElseIf TextBox10.Text <> TextBox11.Text Then
            MsgBox("Les mots de passes ne correspondent pas", 48)
        Else
            Dim cmd As New MySqlCommand
            Dim dr As MySqlDataReader
            Dim fonc As String
            If CheckBox2.Checked = True Then
                fonc = "admin"
            Else
                fonc = "user"
            End If
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "update utilisateur set login = '" & TextBox9.Text & "', password= '" & TextBox10.Text & "' , fonction='" & fonc & "' where login = '" & TextBox9.Text & "' "
            dr = cmd.ExecuteReader
            MsgBox("Effectuer", 64)
            cn.Close()
            TextBox7.Clear()
            TextBox8.Clear()
            TextBox9.Clear()
            TextBox10.Clear()
            TextBox11.Clear()
            GroupBox4.Enabled = False
            recharger()
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            IO.File.WriteAllText("url.txt", TextBox1.Text)
            IO.File.WriteAllText("user.txt", TextBox2.Text)
            IO.File.WriteAllText("password.txt", TextBox3.Text)
            connect.serveur = TextBox1.Text
            connect.user = TextBox2.Text
            connect.password = TextBox3.Text
            MessageBox.Show("configuration effectuée avec succes")
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Erreur fichier introuvable")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim cmd As New MySqlCommand
        Dim dr As MySqlDataReader
        cn.Open()
        cmd.Connection = cn
        cmd.CommandText = "delete from utilisateur where login= '" & TextBox7.Text & "' and password = '" & TextBox8.Text & "'"
        dr = cmd.ExecuteReader
        MsgBox("Effectuer", 64)
        cn.Close()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        GroupBox4.Enabled = False
        recharger()
    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
        TextBox11.Clear()
        GroupBox4.Enabled = False
    End Sub
End Class