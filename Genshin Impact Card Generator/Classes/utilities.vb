Public Class utilities

#Region "Shuffle Image Arrays" 'Swap the array of images into random indexes
    Public Sub ShuffleArray(list As List(Of Image))
        Dim n As Integer = list.Count
        Dim ran As New Random

        For i = 0 To list.Count - 1
            Swap(list, i, i + ran.Next(n - i))
        Next
    End Sub
    Private Sub Swap(li As List(Of Image), a As Integer, b As Integer)
        Dim temp As Image = li(a)
        li(a) = li(b)
        li(b) = temp
    End Sub
#End Region

    Public Sub Sort(Of T)(ByVal list As IList(Of T))
        Dim tmp As New List(Of T)(list)
        tmp.Sort()
        For i As Integer = 0 To tmp.Count - 1
            list(i) = tmp(i)
        Next i
    End Sub

End Class
