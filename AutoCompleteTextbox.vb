
Imports System.Collections
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Namespace AutoCompleteTextBoxSample
    ' if you would like to use ArrayList insted
    ' here we use Generic Type List<string>

    ' the component is derived from a TextBox 
    ' and is therfore called TextBox, but ment is this class (AutoCompleteTextbox)
    Public Class AutoCompleteTextbox
        Inherits TextBox
#Region "Fields"

        ' the ListBox used for suggestions
        Private listBox As ListBox

        ' string to remember a former input
        Private oldText As String

        ' a Panel for displaying
        Private panel As Panel

#End Region

#Region "Constructors"

        ' the constructor
        Public Sub New()
            MyBase.New()
            ' assigning some default values
            ' minimum characters to be typed before suggestions are displayed
            Me.MinTypedCharacters = 2
            ' not cases sensitive
            Me.CaseSensitive = False
            ' the list for our suggestions database
            ' the sample dictionary en-EN.dic is stored here when form1 is loaded (see form1.Load event)
            Me.AutoCompleteList = New List(Of String)()

            ' the listbox used for suggestions
            Me.listBox = New ListBox()
            Me.listBox.Name = "SuggestionListBox"
            Me.listBox.Font = Me.Font
            Me.listBox.Visible = True

            ' the panel to hold the listbox later on
            Me.panel = New Panel()
            Me.panel.Visible = False
            Me.panel.Font = Me.Font
            ' to be able to fit to changing sizes of the parent form
            Me.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            ' initialize with minimum size to avoid overlaping or flickering problems
            Me.panel.ClientSize = New System.Drawing.Size(1, 1)
            Me.panel.Name = "SuggestionPanel"
            Me.panel.Padding = New System.Windows.Forms.Padding(0, 0, 0, 0)
            Me.panel.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
            Me.panel.BackColor = Color.Transparent
            Me.panel.ForeColor = Color.Transparent
            Me.panel.Text = ""
            Me.panel.PerformLayout()
            ' add the listbox to the panel if not already done
            If Not panel.Controls.Contains(listBox) Then
                Me.panel.Controls.Add(listBox)
            End If

            ' make the listbox fill the panel
            Me.listBox.Dock = DockStyle.Fill
            ' only one itme can be selected from the listbox
            Me.listBox.SelectionMode = SelectionMode.One
            ' the events to be fired if an item is selected
            AddHandler Me.listBox.KeyDown, AddressOf listBox_KeyDown
            AddHandler Me.listBox.MouseClick, AddressOf listBox_MouseClick
            AddHandler Me.listBox.MouseDoubleClick, AddressOf listBox_MouseDoubleClick

            '#Region "Excursus: ArrayList vs. List<string>"
            ' surpringly ArrayList is a little faster than List<string>
            ' to use ArrayList instead, replace every 'List<string>' with 'ArrayList'
            ' i will used List<string> cause it's generic type
            '#End Region
            ' the list of suggestions actually displayed in the listbox
            ' a subset of AutoCompleteList according to the typed in keyphrase
            Me.CurrentAutoCompleteList = New List(Of String)()

            '#Region "Excursus: DataSource vs. AddRange"
            ' using DataSource is faster than adding items (see
            ' uncommented listBox.Items.AddRange method below)
            '#End Region
            ' Bind the CurrentAutoCompleteList as DataSource to the listbox
            listBox.DataSource = CurrentAutoCompleteList

            ' set the input to remember, which is empty so far
            oldText = Me.Text
        End Sub

#End Region

#Region "Properties"

        ' the list for our suggestions database
        Public Property AutoCompleteList() As List(Of String)
            Get
                Return m_AutoCompleteList
            End Get
            Set(value As List(Of String))
                m_AutoCompleteList = value
            End Set
        End Property
        Private m_AutoCompleteList As List(Of String)

        ' case sensitivity
        Public Property CaseSensitive() As Boolean
            Get
                Return m_CaseSensitive
            End Get
            Set(value As Boolean)
                m_CaseSensitive = value
            End Set
        End Property
        Private m_CaseSensitive As Boolean

        ' minimum characters to be typed before suggestions are displayed
        Public Property MinTypedCharacters() As Integer
            Get
                Return m_MinTypedCharacters
            End Get
            Set(value As Integer)
                m_MinTypedCharacters = value
            End Set
        End Property
        Private m_MinTypedCharacters As Integer

        ' the index selected in the listbox
        ' maybe of intrest for other classes
        Public Property SelectedIndex() As Integer
            Get
                Return listBox.SelectedIndex
            End Get
            Set(value As Integer)
                ' musn't be null
                If listBox.Items.Count <> 0 Then
                    listBox.SelectedIndex = value
                End If
            End Set
        End Property

        ' the actual list of currently displayed suggestions
        Private Property CurrentAutoCompleteList() As List(Of String)
            Get
                Return m_CurrentAutoCompleteList
            End Get
            Set(value As List(Of String))
                m_CurrentAutoCompleteList = value
            End Set
        End Property
        Private m_CurrentAutoCompleteList As List(Of String)

        ' the parent Form of this control
        Private ReadOnly Property ParentForm() As Form
            Get
                Return Me.Parent.FindForm()
            End Get
        End Property

#End Region

#Region "Methods"

        ' hides the listbox
        Public Sub HideSuggestionListBox()
            If (ParentForm IsNot Nothing) Then
                ' hiding the panel also hides the listbox
                panel.Hide()
                ' now remove it from the ParentForm (Form1 in this example)
                If Me.ParentForm.Controls.Contains(panel) Then
                    Me.ParentForm.Controls.Remove(panel)
                End If
            End If
        End Sub

        Protected Overrides Sub OnKeyDown(args As KeyEventArgs)
            ' if user presses key.up
            If (args.KeyCode = Keys.Up) Then
                ' move the selection in listbox one up
                MoveSelectionInListBox((SelectedIndex - 1))
                ' work is done
                args.Handled = True
                ' on key.down
            ElseIf (args.KeyCode = Keys.Down) Then
                'move one down
                MoveSelectionInListBox((SelectedIndex + 1))
                args.Handled = True
            ElseIf (args.KeyCode = Keys.PageUp) Then
                'move 10 up
                MoveSelectionInListBox((SelectedIndex - 10))
                args.Handled = True
            ElseIf (args.KeyCode = Keys.PageDown) Then
                'move 10 down
                MoveSelectionInListBox((SelectedIndex + 10))
                args.Handled = True
                ' on enter
            ElseIf (args.KeyCode = Keys.Enter) Then
                ' select the item in the ListBox
                SelectItem()
                MyBase.OnKeyDown(args)
                args.Handled = True
            Else
                ' work is not done, maybe the base class will process the event, so call it...
                MyBase.OnKeyDown(args)
            End If
        End Sub

        ' if the user leaves the TextBox, the ListBox and the panel ist hidden
        Protected Overrides Sub OnLostFocus(e As System.EventArgs)
            If Not panel.ContainsFocus Then
                ' call the baseclass event
                MyBase.OnLostFocus(e)
                ' then hide the stuff
                Me.HideSuggestionListBox()
            End If
        End Sub

        ' if the input changes, call ShowSuggests()
        Protected Overrides Sub OnTextChanged(args As EventArgs)
            ' avoids crashing the designer
            If Not Me.DesignMode Then
                ShowSuggests()
            End If
            MyBase.OnTextChanged(args)

            ' remember input
            Me.Text = Me.Text.Replace(vbCrLf, "")
            oldText = Me.Text
        End Sub

        ' event for any key pressed in the ListBox
        Private Sub listBox_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
            ' on enter
            If e.KeyCode = Keys.Enter Then
                ' select the current item
                SelectItem()
                ' work done
                e.Handled = True
            End If
        End Sub

        ' event for MouseClick in the ListBox
        Private Sub listBox_MouseClick(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            ' select the current item
            SelectItem()
        End Sub

        ' event for DoubleClick in the ListBox
        Private Sub listBox_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs)
            ' select the current item
            SelectItem()
        End Sub

        Private Sub MoveSelectionInListBox(Index As Integer)
            ' beginning of list
            If Index <= -1 Then
                Me.SelectedIndex = 0
                ' end of liste
            ElseIf Index > (listBox.Items.Count - 1) Then
                SelectedIndex = (listBox.Items.Count - 1)
            Else
                ' somewhere in the middle
                SelectedIndex = Index
            End If
        End Sub

        ' selects the current item
        Private Function SelectItem() As Boolean
            ' if the ListBox is not empty
            If ((Me.listBox.Items.Count > 0) AndAlso (Me.SelectedIndex > -1)) Then
                ' set the Text of the TextBox to the selected item of the ListBox
                Me.Text = Me.listBox.SelectedItem.ToString()
                ' and hide the ListBox
                Me.HideSuggestionListBox()
            End If
            Return True
        End Function

        ' shows the suggestions in ListBox beneath the TextBox
        ' and fitting it into the ParentForm
        Private Sub ShowSuggests()
            ' show only if MinTypedCharacters have been typed
            If Me.Text.Length >= MinTypedCharacters Then
                ' prevent overlapping problems with other controls
                ' while loading data there is nothing to draw, so suspend layout
                panel.SuspendLayout()
                ' user is typing forward, char has been added at the end of the former input
                If (Me.Text.Length > 0) AndAlso (Me.oldText = Me.Text.Substring(0, Me.Text.Length - 1)) Then
                    'handle forward typing with refresh
                    UpdateCurrentAutoCompleteList()
                    ' user is typing backward - char bas been removed
                ElseIf (Me.oldText.Length > 0) AndAlso (Me.Text = Me.oldText.Substring(0, Me.oldText.Length - 1)) Then
                    'handle backward typing with refresh
                    UpdateCurrentAutoCompleteList()
                Else
                    ' something within has changed
                    ' handle other things like corrections in the middle of the input, clipboard pastes, etc. with refresh
                    UpdateCurrentAutoCompleteList()
                End If

                If ((CurrentAutoCompleteList IsNot Nothing) AndAlso CurrentAutoCompleteList.Count > 0) Then
                    ' finally show Panel and ListBox
                    ' (but after refresh to prevent drawing empty rectangles)
                    panel.Show()
                    ' at the top of all controls
                    panel.BringToFront()
                    ' then give the focuse back to the TextBox (this control)
                    Me.Focus()
                Else
                    ' or hide if no results
                    Me.HideSuggestionListBox()
                End If
                ' prevent overlapping problems with other controls
                panel.ResumeLayout(True)
            Else
                ' hide if typed chars <= MinCharsTyped
                Me.HideSuggestionListBox()
            End If
        End Sub

        ' This is a timecritical part
        ' Fills/ refreshed the CurrentAutoCompleteList with appropreate candidates
        Private Sub UpdateCurrentAutoCompleteList()
            'Me.Text = Me.Text.Replace(vbCrLf, "")
            ' the list of suggestions has to be refreshed so clear it
            CurrentAutoCompleteList.Clear()
            ' an find appropreate candidates for the new CurrentAutoCompleteList in AutoCompleteList
            For Each Str As String In AutoCompleteList
                ' be casesensitive
                If CaseSensitive Then
                    ' search for the substring (equal to SQL Like Command)
                    If (Str.IndexOf(Me.Text) > -1) Then
                        ' Add candidates to new CurrentAutoCompleteList
                        CurrentAutoCompleteList.Add(Str)
                    End If
                Else
                    ' or ignore case
                    ' and search for the substring (equal to SQL Like Command)
                    If (Str.ToLower().IndexOf(Me.Text.ToLower()) > -1) Then
                        ' Add candidates to new CurrentAutoCompleteList
                        CurrentAutoCompleteList.Add(Str)
                    End If
                End If
            Next
            '#Region "Excursus: Performance measuring of Linq queries"
            ' This is a simple example of speedmeasurement for Linq querries
            '
            '            CurrentAutoCompleteList.Clear();
            '            Stopwatch stopWatch = new Stopwatch();
            '            stopWatch.Start();
            '            // using Linq query seems to be slower (twice as slow)
            '            var query =
            '                from expression in this.AutoCompleteList
            '                where expression.ToLower().Contains(this.Text.ToLower())
            '                select expression;
            '            foreach (string searchTerm in query)
            '            {
            '                CurrentAutoCompleteList.Add(searchTerm);
            '            }
            '            stopWatch.Stop();
            '            // method (see below) for printing the performance values to console
            '            PrintStopwatch(stopWatch, "Linq - Contains");
            '            

            '#End Region

            ' countinue to update the ListBox - the visual part
            UpdateListBoxItems()
        End Sub

        ' This is the most timecritical part, adding items to the ListBox
        Private Sub UpdateListBoxItems()
            '#Region "Excursus: DataSource vs. AddRange"
            '
            '                    // This part is not used due to 'listBox.DataSource' approach (see constructor)
            '                    // left for comparison, delete for productive use
            '                    listBox.BeginUpdate();
            '                    listBox.Items.Clear();
            '                    //Fills the ListBox
            '                    listBox.Items.AddRange(this.CurrentAutoCompleteList.ToArray());
            '                    listBox.EndUpdate();
            '                    // to use this method remove the following
            '                    // "((CurrencyManager)listBox.BindingContext[CurrentAutoCompleteList]).Refresh();" line and
            '                    // "listBox.DataSource = CurrentAutoCompleteList;" line from the constructor
            '                    

            '#End Region

            ' if there is a ParentForm
            Try
                If (ParentForm IsNot Nothing) Then
                    ' get its width
                    panel.Width = Me.Width
                    ' calculate the remeining height beneath the TextBox
                    panel.Height = Me.ParentForm.ClientSize.Height - Me.Height - Me.Location.Y
                    ' and the Location to use
                    panel.Location = Me.Location + New Size(0, Me.Height)
                    ' Panel and ListBox have to be added to ParentForm.Controls before calling BingingContext
                    If Not Me.ParentForm.Controls.Contains(panel) Then
                        ' add the Panel and ListBox to the PartenForm
                        Me.ParentForm.Controls.Add(panel)
                    End If
                    ' Update the listBox manually - List<string> dosn't support change events
                    ' this is the DataSource approche, this is a bit tricky and may cause conflicts,
                    ' so in case fall back to AddRange approache (see Excursus)
                    DirectCast(listBox.BindingContext(CurrentAutoCompleteList), CurrencyManager).Refresh()
                End If
            Catch ex As Exception

            End Try
            
        End Sub

#End Region

#Region "Other"

        '
        '        // only needed for performance measuring - delete for productional use
        '        private void PrintStopwatch(Stopwatch stopWatch, string comment)
        '        {
        '            // Get the elapsed time as a TimeSpan value.
        '            TimeSpan ts = stopWatch.Elapsed;
        '            // Format and display the TimeSpan value.
        '            string elapsedTime = String.Format("{0:00}h:{1:00}m:{2:00},{3:000}s \t {4}",
        '                ts.Hours, ts.Minutes, ts.Seconds,
        '                ts.Milliseconds, comment);
        '            Console.WriteLine("RunTime " + elapsedTime);
        '        }
        '        


#End Region
    End Class
End Namespace
