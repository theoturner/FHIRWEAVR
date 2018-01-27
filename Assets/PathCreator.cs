// Detect device and set path for save data (before sending to server)

//TODO Alternatively could have developer specify path for their device

public class PathCreator {
    public static string path;
    // e.g. if device = android : path = Application.persistentDataPath.ToString() + "/";
    // MUST APPEND APPROPRIATE / OR \ (FOR WINDOWS) FOR JUST BEFORE FILE ITSELF.
}
