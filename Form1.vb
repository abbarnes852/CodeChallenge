#Region "Imports"
Imports System.ComponentModel
#End Region

Public Class frmCodeChallenge

#Region "Loading"
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        FadeIn()
    End Sub

    Private Sub FadeIn()
        pbSplash.BringToFront()
        Me.FormBorderStyle = FormBorderStyle.None : Me.Width = 700 : Me.Height = 150
        For FadeIn As Integer = 0 To 100 Step 10
            Me.Opacity = FadeIn / 100 : Me.Refresh() : Threading.Thread.Sleep(130)
        Next
        pbSplash.SendToBack() : pbSplash.Visible = False : Me.FormBorderStyle = FormBorderStyle.Sizable : Me.Width = 700 : Me.Height = 700 : pnlTop.Visible = True
    End Sub

#End Region

#Region "Choose Directory"
    Private Sub txtCSVFile_Click(sender As Object, e As EventArgs) Handles txtCSVFile.Click
        ChooseFile()
    End Sub

    Private Sub btnChooseFile_Click(sender As Object, e As EventArgs) Handles btnChooseFile.Click
        ChooseFile()
    End Sub

    Private Sub ChooseFile()
        Dim fd As New OpenFileDialog
        fd.Title = "Search for CSV File"
        fd.Filter = "Excel files|*.XLSX;*.XLS;*.CSV"

        If fd.ShowDialog() = DialogResult.OK Then
            txtCSVFile.Text = fd.FileName
            If Not My.Computer.FileSystem.FileExists(txtCSVFile.Text) Then
                MsgBox("Cannot find file to be imported.")
                Return
            End If
            pbLoading.Visible = True : lstResults.Items.Clear() : lstResults.Columns.Clear()
            bg1.RunWorkerAsync(txtCSVFile.Text)
        End If
        fd.Dispose()
    End Sub
#End Region

#Region "Importing CSV"

    Private Sub bg1_DoWork(sender As Object, e As DoWorkEventArgs) Handles bg1.DoWork
        Dim cvs As CSVHeaders = Nothing : Dim rowCount As Integer = 0 : Dim sValidationErrors As String = "" : Dim sLoanAmount As String = "" : Dim sOrigIntRate As String = "" : Dim sLoanOrigDate As String = "" : Dim iRowErrors As Int16 = 0
        Dim CsvReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(e.Argument.ToString) : CsvReader.TextFieldType = FileIO.FieldType.Delimited : CsvReader.SetDelimiters(",")

        While Not CsvReader.EndOfData
            rowCount += 1 : Dim rowData() As String = CsvReader.ReadFields

            If rowCount = 1 Then 'Lets do the headers first
                cvs = New CSVHeaders(rowData) : bg1.ReportProgress(rowCount, rowData) : Continue While
            End If

            If rowData(0).ToString = "" Then Exit Sub 'Potentially the end, bail

            cvs.LoadRow(rowData) : sValidationErrors = "" 'Load the row for processing, clear the errors

            '--Validation--
            If cvs.GetVal("servicer_name") = "" Then sValidationErrors += "Blank Servicer Name, "
            If cvs.GetVal("servicer_loan_number") = "" Then sValidationErrors += "Blank Loan Number, "

            'Loan Amount Validation
            sLoanAmount = cvs.GetVal("loan_amount")
            If sLoanAmount = "" Then
                sValidationErrors += "Blank Loan Amount, "
            Else
                If IsNumeric(sLoanAmount) Then cvs.SetVal("loan_amount", CDec(sLoanAmount).ToString("C")) Else sValidationErrors += "Invalid Loan Amount of " + sLoanAmount + ", "
            End If

            'Interest Rate Validation
            sOrigIntRate = cvs.GetVal("original_interest_rate")
            If sOrigIntRate = "" Then
                sValidationErrors += "Blank Interest Rate, "
            Else
                If IsNumeric(sOrigIntRate) Then
                    If CDec(sOrigIntRate) > 0.2 Then
                        sValidationErrors += "Invalid Interest Rate of " + sOrigIntRate + ", "
                    End If
                Else
                    sValidationErrors += "Invalid Interest Rate of " + sOrigIntRate + ", "
                End If
            End If

            'Origination date validation
            sLoanOrigDate = cvs.GetVal("loan_origination_date")
            If sLoanOrigDate = "" Then
                sValidationErrors += "Blank Loan Date, "
            Else
                Dim dtLoanOrigDate As DateTime = Nothing
                If Not DateTime.TryParseExact(sLoanOrigDate, "yyyy-MM-dd", Nothing, Globalization.DateTimeStyles.None, dtLoanOrigDate) Then
                    sValidationErrors += "Invalid Origination Date of " + sLoanOrigDate + ", "
                End If
            End If
            '--Validation Complete--


            Dim ls As New ListViewItem((rowCount - 1).ToString) 'Item # as first column
            For Each rw As String In rowData
                ls.SubItems.Add(rw) 'Add to a listitem
            Next

            'Add the validation errors on the last column
            If sValidationErrors <> "" Then sValidationErrors = Mid(sValidationErrors, 1, Len(sValidationErrors) - 2) : iRowErrors += 1
            If ls.SubItems.Count < 7 Then ls.SubItems.Add(sValidationErrors) Else ls.SubItems(6).Text = sValidationErrors

            'Hand listview item to backgroundworker progress to add
            bg1.ReportProgress(rowCount, ls)
        End While

        e.Result = iRowErrors
    End Sub

    Private Sub bg1_ProgressChanged(sender As Object, e As ProgressChangedEventArgs) Handles bg1.ProgressChanged
        If e.ProgressPercentage = 1 Then 'This is setting up columns
            lstResults.Columns.Add("Item") 'Add a column at the beginning for a better count

            For Each rw As String In e.UserState
                lstResults.Columns.Add(rw.ToString) 'Add all excel columns to columns in our listview
            Next
            lstResults.Columns.Add("Results") 'Add a column at the end for results
        Else
            lblStatus.Text = "Processing row# " + e.ProgressPercentage.ToString 'Add the item to the list
            lstResults.Items.Add(e.UserState)
        End If
    End Sub

    Private Sub bg1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles bg1.RunWorkerCompleted
        pbLoading.Visible = False : Dim iTotal As Decimal = 0 : Dim iCount As Int16 = 0 : Dim ratevalues As New Dictionary(Of Decimal, CSVTotals)
        lblStatus.Text = "Done Processing.  Found " + lstResults.Items.Count.ToString + " rows of data.  " + (lstResults.Items.Count - CInt(e.Result)).ToString + " valid rows.  " + e.Result.ToString + " rows in error."

        For Each rw As ListViewItem In lstResults.Items
            If rw.SubItems(6).Text = "" Then 'No errors on that line, count it in the stats
                iTotal += rw.SubItems(3).Text : iCount += 1
                If ratevalues.ContainsKey(rw.SubItems(4).Text) Then
                    Dim tot As CSVTotals = ratevalues(rw.SubItems(4).Text)
                    tot.LoanCount += 1 : tot.LoanAmount += rw.SubItems(3).Text
                    ratevalues(rw.SubItems(4).Text) = tot
                Else
                    ratevalues.Add(rw.SubItems(4).Text, New CSVTotals With {.LoanCount = 1, .LoanAmount = rw.SubItems(3).Text})
                End If
            End If
        Next

        Dim ls As New ListViewItem With {.Text = ""} : lstResults.Items.Add(ls)
        Dim lsTotals As New ListViewItem With {.Text = ""}
        lsTotals.SubItems.Add("Totals") : lsTotals.SubItems.Add("Interest Rate") : lsTotals.SubItems.Add("Total Loans") : lsTotals.SubItems.Add("Total Loan Values")
        lstResults.Items.Add(lsTotals)

        For Each keyValuePair As KeyValuePair(Of Decimal, CSVTotals) In ratevalues
            Dim lsTotalLine As New ListViewItem("") : Dim tot As CSVTotals = keyValuePair.Value
            lsTotalLine.SubItems.Add("") : lsTotalLine.SubItems.Add(keyValuePair.Key) : lsTotalLine.SubItems.Add(tot.LoanCount) : lsTotalLine.SubItems.Add(CDec(tot.LoanAmount).ToString("C"))
            lstResults.Items.Add(lsTotalLine)
        Next

        Dim lsFinal As New ListViewItem With {.Text = ""}
        lsFinal.SubItems.Add("") : lsFinal.SubItems.Add("") : lsFinal.SubItems.Add(iCount.ToString) : lsFinal.SubItems.Add(CDec(iTotal).ToString("C"))
        lstResults.Items.Add(lsFinal)
        SetAlternateColor()
        lstResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Private Sub SetAlternateColor()
        For j = 0 To lstResults.Items.Count - 1
            If (j Mod 2) = 0 Then
                lstResults.Items(j).BackColor = Color.AliceBlue
            Else
                lstResults.Items(j).BackColor = Color.White

            End If
        Next
    End Sub

#End Region

End Class

#Region "CSVTotals"
Public Class CSVTotals
    Public Property LoanCount As Int16
    Public Property LoanAmount As Decimal
End Class
#End Region

#Region "CSVHeaders"
Public Class CSVHeaders
    Private _ar As New ArrayList
    Private _s() As String

    Public Sub New(ByVal s() As String)
        For q As Int16 = 0 To s.Length - 1
            _ar.Add(s(q))
        Next
    End Sub
    Public Sub LoadRow(ByVal s() As String)
        _s = s
    End Sub

    Public Function GetVal(ByVal sColumnName As String) As String
        For i As Int16 = 0 To _ar.Count - 1
            If _ar(i).ToString.ToUpper = sColumnName.ToUpper Then
                Return Trim(_s(i))
            End If
        Next

        Return ""
    End Function

    Public Sub SetVal(ByVal sColumnName As String, ByVal sValue As String)
        For i As Int16 = 0 To _ar.Count - 1
            If _ar(i).ToString.ToUpper = sColumnName.ToUpper Then
                _s(i) = sValue : Exit Sub
            End If
        Next
    End Sub
End Class
#End Region