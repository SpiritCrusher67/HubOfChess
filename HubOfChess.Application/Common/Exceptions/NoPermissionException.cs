namespace HubOfChess.Application.Common.Exceptions
{
    public class NoPermissionException : Exception
    {
        public NoPermissionException(string fromName,object fromKey, string toName, object toKey)
            : base($"\"{fromName}\" ({fromKey}) fave no permission to access \"{toName}\" ({toKey})") { }
    }
}
