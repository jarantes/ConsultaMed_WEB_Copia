using System.Web.Optimization;

namespace ConsultaMed_WEB.App_Start
{
    public class BundleConfig
    {
        // Para mais informações sobre Bundling, visite http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cycle.all.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/funcoes").Include(
                "~/Scripts/Funcoes/tooltip*",
                "~/Scripts/Funcoes/dialog*",
                "~/Scripts/Funcoes/application*"));

            bundles.Add(new ScriptBundle("~/bundles/formulario").Include(
                "~/Scripts/Funcoes/mascaras*",
                "~/Scripts/Funcoes/endereco*",
                "~/Scripts/Funcoes/calendario*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.ambiance*"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender com ela. Após isso, quando você estiver
            // pronto para produção, use a ferramenta de compilação em http://modernizr.com para selecionar somente os testes que você precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css*",
                "~/Content/jquery.ambiance.css*"));

            bundles.Add(new StyleBundle("~/Content/themes/overcast/css").Include(
                        "~/Content/themes/overcast/jquery-ui.css",
                        "~/Content/themes/overcast/jquery.ui.dialog.css",
                        "~/Content/themes/overcast/jquery.ui.datepicker.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymask").Include(
                        "~/Scripts/jquery.maskedinput.js*"));
        }
    }
}