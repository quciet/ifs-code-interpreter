'Namespace IFsWeb.Support
Namespace Support
    Public Class TreeViewManager

        Public Shared Function CountNodes(ByVal Nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection) As Integer
            Dim count As Integer = 0
            GetCount(Nodes, count)
            Return count
        End Function

        Private Shared Sub GetCount(ByVal Nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef count As Integer)
            If Not Nodes Is Nothing Then
                Dim index As Integer

                For index = 0 To Nodes.Count - 1
                    If Not Nodes(index).Nodes Is Nothing Then
                        GetCount(Nodes(index).Nodes, count)
                    End If
                    count = count + 1
                Next
            End If
        End Sub

        Public Shared Sub SetCheckBoxes(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByVal value As Boolean)
            Dim index As Integer
            If Not TreeView Is Nothing Then
                For index = 0 To TreeView.Count - 1
                    If TreeView(index).Nodes Is Nothing Then
                        TreeView(index).CheckBox = value
                    Else
                        TreeView(index).CheckBox = value
                        SetCheckBoxes(TreeView(index).Nodes, value)
                    End If
                Next
            End If
        End Sub

        Public Shared Function GetCheckBoxes(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection) As Boolean
            Dim result As Boolean = False
            If Not TreeView Is Nothing Then
                If TreeView.Count > 0 Then
                    result = TreeView(0).CheckBox()
                End If
            End If
            Return result
        End Function

        Public Shared Sub SetStyle(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeView, ByVal value As Integer)
            If Not TreeView Is Nothing Then
                Select Case value
                    Case 0 To 1
                        TreeView.ShowLines = False
                        TreeView.ShowPlus = False
                    Case 2 To 3
                        TreeView.ShowLines = False
                        TreeView.ShowPlus = True
                    Case 4 To 5
                        TreeView.ShowLines = True
                        TreeView.ShowPlus = False
                    Case 6 To 7
                        TreeView.ShowLines = True
                        TreeView.ShowPlus = True
                End Select
            End If
        End Sub

        Public Shared Sub SetScroll(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeView, ByVal value As Boolean)
            If Not TreeView Is Nothing Then
                Dim unit As System.Web.UI.WebControls.Unit
                If value Then

                    Dim add As Integer

                    If TreeView.Height.Value = 0 Or TreeView.Width.Value = 0 Then
                        add = 200
                    Else
                        add = 1
                    End If

                    unit = New System.Web.UI.WebControls.Unit(TreeView.Height.Value + add)
                    TreeView.Height = unit
                    TreeView.Width = unit

                Else
                    unit = New System.Web.UI.WebControls.Unit()
                    TreeView.Height = unit
                    TreeView.Width = unit
                End If
            End If
        End Sub

        Public Shared Function GetScroll(ByRef TreeView As Microsoft.Web.UI.WebControls.TreeView) As Boolean
            Dim result As Boolean = False

            If Not TreeView Is Nothing Then
                result = Not (TreeView.Height.Value = 0 And TreeView.Width.Value = 0)
            End If

            Return result
        End Function

        Public Shared Function GetSelectedNode(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeView) As Microsoft.Web.UI.WebControls.TreeNode
            Dim node As Microsoft.Web.UI.WebControls.TreeNode = Nothing
            If Not TreeView Is Nothing Then
                node = GetNode(TreeView.Nodes, TreeView.SelectedNodeIndex())
            End If
            Return node
        End Function

        Public Shared Sub SetSelectedNode(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeView, ByVal node As Microsoft.Web.UI.WebControls.TreeNode)
            If Not TreeView Is Nothing And Not node Is Nothing Then
                TreeView.SelectedNodeIndex = node.GetNodeIndex()
            End If
        End Sub

        Public Shared Function GetNode(ByVal TreeViewNodes As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByVal nodeIndex As String) As Microsoft.Web.UI.WebControls.TreeNode
            Dim node As Microsoft.Web.UI.WebControls.TreeNode = Nothing

            If Not TreeViewNodes Is Nothing Then
                If nodeIndex <> "" Then
                    Dim index As Integer
                    Dim nodepos As String = ""
                    Dim indexarray As System.Collections.ArrayList = New System.Collections.ArrayList()

                    For index = 0 To nodeIndex.Length - 1
                        If nodeIndex.Chars(index) <> "." Then
                            nodepos = nodepos + nodeIndex.Chars(index)
                            If index = nodeIndex.Length - 1 Then
                                indexarray.Add(nodepos)
                            End If
                        Else
                            indexarray.Add(nodepos)
                            nodepos = ""
                        End If
                    Next

                    node = SearchNode(TreeViewNodes, indexarray)
                End If
            End If

            Return node
        End Function

        Private Shared Function SearchNode(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef index As System.Collections.ArrayList) As Microsoft.Web.UI.WebControls.TreeNode
            Dim node As Microsoft.Web.UI.WebControls.TreeNode = Nothing

            If Not TreeView Is Nothing Then
                If Not index Is Nothing Then
                    If index.Count = 1 Then
                        node = TreeView(index(0))
                    Else
                        Dim temp As Integer = System.Convert.ToInt32(index(0))
                        index.RemoveAt(0)
                        node = SearchNode(TreeView(temp).Nodes, index)
                    End If
                End If
            End If

            Return node
        End Function

        Public Shared Function SearchNode(ByVal TreeView As Microsoft.Web.UI.WebControls.TreeNodeCollection, ByRef node As System.String) As Microsoft.Web.UI.WebControls.TreeNode
            If Not TreeView Is Nothing And node <> "" Then
                Dim index As Integer
                For index = 0 To TreeView.Count - 1
                    If TreeView(index).Text = node Then
                        Return TreeView(index)
                        Exit For
                    Else
                        If TreeView(index).NodeData = node Then
                            Return TreeView(index)
                            Exit For
                        Else
                            Dim result As Microsoft.Web.UI.WebControls.TreeNode = Nothing
                            result = SearchNode(TreeView(index).Nodes, node)
                            If Not result Is Nothing Then
                                Return result
                            End If
                        End If
                    End If
                Next
            End If
        End Function

        Public Shared Function AddNode(ByRef nodes As Microsoft.Web.UI.WebControls.TreeNodeCollection,
                Optional ByVal PreExistingNode As Object = Nothing,
                Optional ByVal Relationship As Object = Nothing,
                Optional ByVal Key As Object = Nothing,
                Optional ByVal Text As String = "__nothing_text_null__") As Microsoft.Web.UI.WebControls.TreeNode

            Dim node As Microsoft.Web.UI.WebControls.TreeNode = New Microsoft.Web.UI.WebControls.TreeNode()

            If Not nodes Is Nothing Then
                If Text <> "__nothing_text_null__" Then
                    node.Text = Text
                Else
                    node.Text = ""
                End If

                If Key <> Nothing Then
                    node.NodeData = Key
                Else
                    node.NodeData = ""
                End If



                If PreExistingNode Is Nothing And Relationship Is Nothing Then
                    nodes.Add(node)

                ElseIf PreExistingNode Is Nothing And Not Relationship Is Nothing Then
                    Select Case CType(Relationship, Integer)
                        Case 0
                            nodes.AddAt(0, node)
                        Case Else
                            nodes.Add(node)
                    End Select

                ElseIf Not PreExistingNode Is Nothing And Not Relationship Is Nothing Then
                    Dim index As String
                    If PreExistingNode.GetType() Is GetType(Microsoft.Web.UI.WebControls.TreeNode) Then
                        index = CType(PreExistingNode, Microsoft.Web.UI.WebControls.TreeNode).GetNodeIndex()

                        Select Case CType(Relationship, Integer)
                            Case 0
                                nodes.AddAt(0, node)
                            Case 1
                                nodes.Add(node)
                            Case 2
                                nodes.AddAt(index.Replace(index.Substring(index.LastIndexOf(".") + 1), System.Convert.ToString(System.Convert.ToInt32(index.Substring(index.LastIndexOf(".") + 1)) + 1)), node)
                            Case 3
                                nodes.AddAt(index, node)
                            Case 4
                                GetNode(nodes, index).Nodes.Add(node)
                        End Select
                    Else
                        If PreExistingNode.GetType() Is GetType(System.String) Then
                            Dim SearchedNode As Microsoft.Web.UI.WebControls.TreeNode = SearchNode(nodes, PreExistingNode)
                            If (Not SearchedNode Is Nothing) Then
                                index = CType(SearchedNode, Microsoft.Web.UI.WebControls.TreeNode).GetNodeIndex()

                                Select Case CType(Relationship, Integer)
                                    Case 0
                                        nodes.AddAt(0, node)
                                    Case 1
                                        nodes.Add(node)
                                    Case 2
                                        nodes.AddAt(index.Replace(index.Substring(index.LastIndexOf(".") + 1), System.Convert.ToString(System.Convert.ToInt32(index.Substring(index.LastIndexOf(".") + 1)) + 1)), node)
                                    Case 3
                                        nodes.AddAt(index, node)
                                    Case 4
                                        SearchedNode = GetNode(nodes, index)
                                        If Not SearchedNode Is Nothing Then
                                            SearchedNode.Nodes.Add(node)
                                        End If
                                End Select
                            End If
                        End If
                    End If
                End If
            End If
            Return node
        End Function

    End Class
End Namespace
