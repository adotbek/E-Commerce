namespace E_Commerce.Endpoints;

public static class AdminEndpoints 
{
    public static void MapAdminEndpoints (this WebApplication app)
    {
        var group = app.MapGroup("/api/admins")
            .WithTags("AdminManagement")
            .RequireAuthorization();

    }
}
