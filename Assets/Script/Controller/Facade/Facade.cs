public class Facade {

    static FacadeParameters parameters = new HerokuFacadeParameters();

    public static string GetUrl() {
        return parameters.GetUrl();
    }
}