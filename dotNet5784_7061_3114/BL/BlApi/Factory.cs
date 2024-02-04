

namespace BlApi;

public static class Factory
{
    // Return item with details from type that realizes IBl .
    public static IBl Get() => new BlImplementation.BL();
}
