public class Facade {

    static FacadeParameters parameters = new LocalFacadeParameters();//HerokuFacadeParameters();

    public static string GetUrl() {
        return parameters.GetUrl();
    }
}