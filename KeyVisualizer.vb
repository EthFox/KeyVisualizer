Imports KGySoft.Drawing

Public Class KeyVisualizer

    ' KEY FORM INFO - made form invisible using the TransparencyKey property, set form background to that property's color. TopMost property allows app to stay on top of all other programs
    ' Definitely not a keylogger I promise I wont steal your passwords

    Private WithEvents kbHook As New KeyboardHook ' custom class found on the internet, allows for key presses to be captured even when form is not in focus
    ' each key declared
    Private WRect As New Rectangle
    Private ARect As New Rectangle
    Private SRect As New Rectangle
    Private DRect As New Rectangle
    Private EscRect As New Rectangle
    ' colors for each key
    Private WKeyFillColor As Color = Color.DarkGray
    Private AKeyFillColor As Color = Color.DarkGray
    Private SKeyFillColor As Color = Color.DarkGray
    Private DKeyFillColor As Color = Color.DarkGray
    Private EscKeyFillColor As Color = Color.DarkGray
    ' for moving the keys
    Private IsMouseDown As Boolean ' only set to true when left click is held down
    Private MouseOffset As Point ' distance from the top left of the form and the mouse location, otherwise the top left of the form snaps to the mouse

    Private Sub KeyVisualizer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim KeySize As New Size(50, 50)

        WRect.Location = New Point(70, 3) ' set the locations and sizes of the keys on the form
        WRect.Size = KeySize

        ARect.Location = New Point(10, 68)
        ARect.Size = KeySize

        SRect.Location = New Point(75, 68)
        SRect.Size = KeySize

        DRect.Location = New Point(140, 68)
        DRect.Size = KeySize

        EscRect.Location = New Point(5, 3)
        EscRect.Size = KeySize
    End Sub

    Private Sub KeyVisualizer_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
        Dim graphics As Graphics = e.Graphics
        ' W
        GraphicsExtensions.FillRoundedRectangle(graphics, New SolidBrush(WKeyFillColor), WRect, 8, 8, 8, 8) ' create the key
        GraphicsExtensions.DrawRoundedRectangle(graphics, New Pen(Color.Black), WRect, 8, 8, 8, 8) ' create the border of the key
        e.Graphics.DrawString("W", New Font("Gadugi", 24, FontStyle.Bold), New SolidBrush(Color.Black), New Point(74, 6)) ' draw the letter on the key
        ' A
        GraphicsExtensions.FillRoundedRectangle(graphics, New SolidBrush(AKeyFillColor), ARect, 8, 8, 8, 8)
        GraphicsExtensions.DrawRoundedRectangle(graphics, New Pen(Color.Black), ARect, 8, 8, 8, 8)
        e.Graphics.DrawString("A", New Font("Gadugi", 24, FontStyle.Bold), New SolidBrush(Color.Black), New Point(19, 70))
        ' S
        GraphicsExtensions.FillRoundedRectangle(graphics, New SolidBrush(SKeyFillColor), SRect, 8, 8, 8, 8)
        GraphicsExtensions.DrawRoundedRectangle(graphics, New Pen(Color.Black), SRect, 8, 8, 8, 8)
        e.Graphics.DrawString("S", New Font("Gadugi", 24, FontStyle.Bold), New SolidBrush(Color.Black), New Point(86, 70))
        ' D
        GraphicsExtensions.FillRoundedRectangle(graphics, New SolidBrush(DKeyFillColor), DRect, 8, 8, 8, 8)
        GraphicsExtensions.DrawRoundedRectangle(graphics, New Pen(Color.Black), DRect, 8, 8, 8, 8)
        e.Graphics.DrawString("D", New Font("Gadugi", 24, FontStyle.Bold), New SolidBrush(Color.Black), New Point(149, 70))
        ' Esc
        GraphicsExtensions.FillRoundedRectangle(graphics, New SolidBrush(EscKeyFillColor), EscRect, 8, 8, 8, 8)
        GraphicsExtensions.DrawRoundedRectangle(graphics, New Pen(Color.Black), EscRect, 8, 8, 8, 8)
        e.Graphics.DrawString("Esc", New Font("Gadugi", 18, FontStyle.Bold), New SolidBrush(Color.Black), New Point(8, 11))
    End Sub

    Private Sub kbHook_KeyDown(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyDown
        Dim KeyPressed As String = Key.ToString() ' get the key pressed
        If KeyPressed = "W" Then ' when key pressed...
            WKeyFillColor = Color.Gray ' make the background darker (yes gray is darker than dark gray, don't question it)
        ElseIf KeyPressed = "A" Then
            AKeyFillColor = Color.Gray
        ElseIf KeyPressed = "S" Then
            SKeyFillColor = Color.Gray
        ElseIf KeyPressed = "D" Then
            DKeyFillColor = Color.Gray
        ElseIf KeyPressed = "Escape" Then
            EscKeyFillColor = Color.Gray
        End If

        Invalidate() ' calls the paint event, essentially refreshes the GUI
    End Sub

    Private Sub kbHook_KeyUp(ByVal Key As System.Windows.Forms.Keys) Handles kbHook.KeyUp
        Dim KeyPressed As String = Key.ToString()
        If KeyPressed = "W" Then
            WKeyFillColor = Color.DarkGray ' when the key is released, make it lighter again
        ElseIf KeyPressed = "A" Then
            AKeyFillColor = Color.DarkGray
        ElseIf KeyPressed = "S" Then
            SKeyFillColor = Color.DarkGray
        ElseIf KeyPressed = "D" Then
            DKeyFillColor = Color.DarkGray
        ElseIf KeyPressed = "Escape" Then
            EscKeyFillColor = Color.DarkGray
        End If

        Invalidate()
    End Sub

    Private Sub KeyVisualizer_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then ' if the mouse clicked was left click...
            IsMouseDown = True ' use boolean variable to tell the mouse move event that left click is being held on the form

            MouseOffset = New Point(-e.X, -e.Y) ' Mouse Offset explained above, e.X is the X value of the mouse, and negative as we want it equal to the form location
        End If
    End Sub

    Private Sub KeyVisualizer_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If IsMouseDown Then ' if left click is being held...
            Dim mousePosition As Point = Control.MousePosition ' get point of mouse position so offset can be used

            mousePosition.Offset(MouseOffset) ' offset the mouse location
            Me.Location = mousePosition ' set form location to offset mouse location
        End If
    End Sub

    Private Sub KeyVisualizer_MouseUp(sender As Object, e As MouseEventArgs) Handles MyBase.MouseUp
        If e.Button = MouseButtons.Left Then ' if left click was let go...
            IsMouseDown = False ' tell mouse move to stop moving the form
        End If
    End Sub
End Class
