'La ruta de los archivos es D:/VBNodeServer
'Es necesario que el archivo de node.js se llame main.js
'Este programa esta hecho al cien por cien por JoanTheGamer


Imports System.Text

Public Class Form1
    Dim fs As IO.FileStream,
        file As IO.StreamWriter,
        extensions As String

    Public Function Start()
        If My.Computer.FileSystem.FileExists("D:/VBNodeServer/start.bat") Then
            Process.Start("CMD", "/K cd D:/VBNodeServer/ & start.bat")
        Else
            file = My.Computer.FileSystem.OpenTextFileWriter("D:/VBNodeServer/start.bat", True)
            file.WriteLine(":a")
            file.WriteLine("cls")
            file.WriteLine("node main.js")
            file.WriteLine("goto a")
            file.Close()
            Start()
        End If
        Return 0
    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ProgressBar1.Value = 100
        Label1.Text = "0/0"
        If My.Computer.FileSystem.FileExists("D:/VBNodeServer/npm_Complements.txt") = False Then
            Using fs As IO.FileStream = IO.File.Create("D:/VBNodeServer/npm_Complements.txt")
                fs.Close()
            End Using
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If My.Computer.FileSystem.FileExists(TextBox2.Text) Then
            ProgressBar1.Maximum = My.Computer.FileSystem.ReadAllText(TextBox2.Text).Length
            If ProgressBar1.Value < My.Computer.FileSystem.ReadAllText(TextBox2.Text).Length - 1 Then
                Label1.Show()
                Label1.Text = (ProgressBar1.Value * 58.1).ToString + "/" + My.Computer.FileSystem.ReadAllText(TextBox2.Text).Length.ToString
                ProgressBar1.Value += 1
            ElseIf ProgressBar1.Value = My.Computer.FileSystem.ReadAllText(TextBox2.Text).Length - 1 Then
                Label1.Hide()
                ProgressBar1.Value += 1
            End If
            If My.Computer.FileSystem.ReadAllText(TextBox2.Text).Length > 100 Then
                ProgressBar1.Maximum = 100
            End If
            If TextBox1.Text = My.Computer.FileSystem.ReadAllText(TextBox2.Text) Then
                Timer1.Stop()
            ElseIf ProgressBar1.Value = ProgressBar1.Maximum Then
                TextBox1.Text = My.Computer.FileSystem.ReadAllText(TextBox2.Text)
            End If
        Else
            ProgressBar1.Value = ProgressBar1.Maximum
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        If My.Computer.FileSystem.FileExists(TextBox2.Text) Then
            TextBox2.ForeColor = Color.Green
            Dim response = MsgBox("You want use this File", MsgBoxStyle.YesNo, "File Confirmation")
            If response = MsgBoxResult.Yes Then
                ProgressBar1.Value = 0
                Timer1.Start()
            Else
                TextBox2.Text = ""
            End If
        Else
            TextBox2.ForeColor = Color.Red
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        extensions = My.Computer.FileSystem.ReadAllText("D:/VBNodeServer/npm_Complements.txt")
        If extensions.Contains(TextBox3.Text) Then
            MsgBox("Este complemento ya esta instalado.")
        Else
            extensions = extensions + TextBox3.Text + ", "
            Using fs As IO.FileStream = IO.File.Create("D:/VBNodeServer/npm_Complements.txt")
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(extensions)
                fs.Write(info, 0, info.Length)
                fs.Close()
            End Using
            Process.Start("CMD", "/K cd D:/VBNodeServer/ & npm install " + TextBox3.Text)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text <> My.Computer.FileSystem.ReadAllText(TextBox2.Text) Then
            Dim response = MsgBox("Save?", MsgBoxStyle.YesNo, "Save Confirmation")
            If response = MsgBoxResult.Yes Then
                Using fs As IO.FileStream = IO.File.Create(TextBox2.Text)
                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(TextBox1.Text)
                    fs.Write(info, 0, info.Length)
                    fs.Close()
                End Using
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Start()
    End Sub
End Class
