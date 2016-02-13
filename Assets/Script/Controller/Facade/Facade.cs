public class Facade {

    static FacadeParameters parameters = new HerokuFacadeParameters();//LocalFacadeParameters();//

    public static string GetUrl() {
        return parameters.GetUrl();
    }
}