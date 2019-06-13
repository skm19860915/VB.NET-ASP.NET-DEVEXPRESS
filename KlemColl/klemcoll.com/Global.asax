<%@ Application Language="VB" %>

<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
        RegisterRoutes(RouteTable.Routes)
    End Sub
    
    Sub RegisterRoutes(Routes As RouteCollection)
        Routes.MapPageRoute("Photographers", "photographers/{id}.aspx", "~/ShowPhotographer.aspx")
        Routes.MapPageRoute("Publications", "publications/{id}.aspx", "~/ShowBook.aspx")
        Routes.MapPageRoute("Image", "image/{refid}.aspx", "~/ShowImage.aspx")
        
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
       
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub
       
</script>