Public Class utilities

#Region "Shuffle Image Arrays" 'Swap the array of images into random indexes
    Public Sub shuffleArray(list As List(Of Image))
        Dim n As Integer = list.Count
        Dim ran As New Random

        For i = 0 To list.Count - 1
            swap(list, i, i + ran.Next(n - i))
        Next
    End Sub
    Private Sub swap(li As List(Of Image), a As Integer, b As Integer)
        Dim temp As Image = li(a)
        li(a) = li(b)
        li(b) = temp
    End Sub
#End Region

End Class
