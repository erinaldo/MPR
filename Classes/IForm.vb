Public Interface IForm

    Inherits System.Windows.Forms.IContainerControl

    ''declaration of those function which are applicable to all user controls(playing the role of form)''
    ''this interface is implemented by all user controls used in this project having save, update and delete functionalities''

    Sub NewClick(ByVal sender As Object, ByVal e As EventArgs)
    Sub SaveClick(ByVal sender As Object, ByVal e As EventArgs)
    Sub CloseClick(ByVal sender As Object, ByVal e As EventArgs)
    Sub DeleteClick(ByVal sender As Object, ByVal e As EventArgs)
    Sub ViewClick(ByVal sender As Object, ByVal e As EventArgs)
    Sub RefreshClick(ByVal sender As Object, ByVal e As EventArgs)

End Interface