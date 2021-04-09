Module connect
    Public serveur As String = IO.File.ReadAllText("url.txt")
    Public bd As String = "og_club"
    Public user As String = IO.File.ReadAllText("user.txt")
    Public password = IO.File.ReadAllText("password.txt")
    Public IsAdmin As Boolean
End Module
