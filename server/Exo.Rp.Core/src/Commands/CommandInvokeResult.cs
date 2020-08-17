namespace server.Commands
{
	public enum CommandInvokeResult
	{
		Success = 0,
		NotFound = 1,
		PermissionDenied = 2,
		ParameterCountMissmatch = 3,
	}
}
