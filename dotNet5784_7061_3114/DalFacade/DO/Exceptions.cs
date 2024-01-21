namespace DO;

//A file of exceptions with a class corresponding to each type of exception

/// <summary>
/// Item is not exist
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Item is already exist
/// </summary>
[Serializable]
public class DalDoesExistException : Exception
{
    public DalDoesExistException(string? message) : base(message) { }
}


/// <summary>
/// Can not delete the item
/// </summary>
[Serializable]
public class DalCannotDeleted : Exception
{
    public DalCannotDeleted(string? message) : base(message) { }
}

/// <summary>
/// Fail to create xml file
/// </summary>
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}


/// <summary>
/// Not found ID in the DataBase
/// </summary>
[Serializable]
public class XmlIdException : Exception
{
    public XmlIdException(string? message) : base(message) { }
}