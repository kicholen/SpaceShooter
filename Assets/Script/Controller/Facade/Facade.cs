public class Facade {

    static FacadeParameters parameters = new LocalFacadeParameters();

    public static string GetUrl() {
        return parameters.GetUrl();
    }
}