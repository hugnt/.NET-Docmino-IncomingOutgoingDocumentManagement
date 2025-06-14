using Docmino.Domain.Entities;

namespace Docmino.Application.Helpers.Users;
public static class RightHelper
{
    public static bool HasCreateIncomingDocumentRight(this User user)
    {
        return user.CreateIncomingDocumentRight;
    }
    public static bool HasCreateOutgoingDocumentRight(this User user)
    {
        return user.CreateOutgoingDocumentRight;
    }
    public static bool HasCreateInternalDocumentRight(this User user)
    {
        return user.CreateInternalDocumentRight;
    }
    public static bool HasInitialConfirmProcessRight(this User user)
    {
        return user.InitialConfirmProcessRight;
    }
    public static bool HasProcessManagerRight(this User user)
    {
        return user.ProcessManagerRight;
    }
    public static bool HasStoreDocumentRight(this User user)
    {
        return user.StoreDocumentRight;
    }
    public static bool HasManageCategoriesRight(this User user)
    {
        return user.ManageCategories;
    }
    public static bool HasAllRights(this User user)
    {
        return user.HasCreateIncomingDocumentRight() &&
               user.HasCreateOutgoingDocumentRight() &&
               user.HasCreateInternalDocumentRight() &&
               user.HasInitialConfirmProcessRight() &&
               user.HasProcessManagerRight() &&
               user.HasStoreDocumentRight() &&
               user.HasManageCategoriesRight();
    }
}
