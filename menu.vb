Public Class menu_principal

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        client.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        offrevb.Show()
        Me.Hide()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        stock.Show()
        Me.Hide()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        dette.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        depenses.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        edit_facture.Show()
        Me.Hide()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        liste_factures.Show()
        Me.Hide()
    End Sub

    Private Sub b_parametre_Click(sender As Object, e As EventArgs) Handles b_parametre.Click
        parametre.Show()

    End Sub
    Private Sub menu_closing(sender As Object, e As EventArgs) Handles MyBase.FormClosing
        Form1.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        historique.Show()
        Me.Hide()
    End Sub

    Private Sub menu_principal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If connect.IsAdmin = False Then
            Button1.Enabled = False

        End If
    End Sub
End Class