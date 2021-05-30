Public Class Form1
    Dim enemy(49) As PictureBox
    Dim speed As Integer = 5
    Dim shot(19) As Label
    Dim shot2(6) As Label
    Dim shotno As Integer = 0
    Dim shotno2 As Integer = 0
    Dim enemyno As Integer = 0
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Dim myenemy As PictureBox
        Dim mylabel As Label
        Randomize()
        For x = 0 To 9
            For y = 0 To 4
                myenemy = New PictureBox
                myenemy.Image = PictureBox1.Image
                myenemy.SizeMode = PictureBoxSizeMode.StretchImage
                myenemy.Top = 10 + y * 40
                myenemy.Left = x * 80
                myenemy.Height = 40
                myenemy.Width = 50
                Controls.Add(myenemy)
                enemy(10 * y + x) = myenemy
            Next
        Next

        For x = 0 To 19
            mylabel = New Label
            mylabel.Top = 550
            mylabel.Left = x * 300
            mylabel.Visible = False
            mylabel.Height = 10
            mylabel.Width = 5
            mylabel.BackColor = Color.Green
            mylabel.AutoSize = False
            mylabel.BorderStyle = BorderStyle.Fixed3D
            Controls.Add(mylabel)
            shot(x) = mylabel
        Next

        For x = 0 To 5
            mylabel = New Label
            mylabel.Top = 50
            mylabel.Left = Int(Rnd() * 700)
            mylabel.Visible = False
            mylabel.Height = 10
            mylabel.Width = 5
            mylabel.BackColor = Color.Red
            mylabel.AutoSize = False
            mylabel.BorderStyle = BorderStyle.Fixed3D
            Controls.Add(mylabel)
            shot2(x) = mylabel
        Next
    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Dim switch As Boolean
        Randomize()
        shotno2 += 1
        If shotno2 = 6 Then
            shotno2 = 0
        End If
        shot2(shotno2).Visible = True
        For i = 0 To 5
            If shot2(i).Visible = True Then
                shot2(i).Top = shot2(i).Top + 10
                If shot2(i).Top > Me.Height Then
                    shot2(i).Top = 50
                    shot2(i).Left = Int(Rnd() * 700)
                End If
            End If
        Next

        For i = 0 To 5
            If shot2(i).Visible = True And shot2(i).Bounds.IntersectsWith(ship.Bounds) Then
                Timer1.Stop()
                MsgBox("you lose")
                End
            End If
        Next
        For i = 0 To 49
            If enemy(i).Visible = True And (enemy(i).Left > Me.Width - 50 Or enemy(i).Left < 0) Then
                switch = True
            End If
        Next
        If switch = True Then
            speed = -speed
            For i = 0 To 49
                enemy(i).Top = enemy(i).Top + 10
            Next
            switch = False
        End If
        For i = 0 To 49
            enemy(i).Left += speed
            If enemy(i).Visible And ship.Bounds.IntersectsWith(enemy(i).Bounds) Then
                Timer1.Stop()
                MsgBox("you lose")
                End

            End If
        Next
    End Sub


    Private Sub Form1_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Left Then
            ship.Left -= 15
        ElseIf e.KeyCode = Keys.Right Then
            ship.Left += 15
        ElseIf e.KeyCode = Keys.Space Then
            shotno += 1
            If shotno = 20 Then
                shotno = 0
            End If
            shot(shotno).visible = True
            shot(shotno).top = ship.Top - 15
            shot(shotno).left = ship.Left + 46
        End If
        If ship.Left < 0 Then
            ship.Left = 10
        End If
        If ship.Left > Me.Width - 50 Then
            ship.Left = Me.Width - 60
        End If
    End Sub

    Private Sub Timer2_Tick(sender As System.Object, e As System.EventArgs) Handles Timer2.Tick
        For i = 0 To 19
            If shot(i).Visible = True Then
                shot(i).Top = shot(i).Top - 10
            End If
        Next

        For i = 0 To 19
            For j = 0 To 49
                If shot(i).Visible = True And enemy(j).Visible And shot(i).Bounds.IntersectsWith(enemy(j).Bounds) Then
                    enemy(j).Visible = False
                    enemyno += 1
                End If
            Next
        Next
        If enemyno = 50 Then
            Timer2.Stop()
            MsgBox("you win")
            End
        End If
    End Sub
End Class
