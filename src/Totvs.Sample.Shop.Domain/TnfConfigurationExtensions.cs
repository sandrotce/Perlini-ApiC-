using Tnf.Configuration;

namespace Totvs.Sample.Shop.Domain
{
    public static class TnfConfigurationExtensions
    {
        public static void UseDomainLocalization(this ITnfConfiguration configuration)
        {
            // Incluindo o source de localização
            configuration.Localization.AddJsonEmbeddedLocalizationFile(
                Constants.LocalizationSourceName,
                typeof(Constants).Assembly,
                "Totvs.Sample.Shop.Domain.Localization.SourceFiles");

            // Incluindo suporte as seguintes linguagens
            configuration.Localization.AddLanguage("pt-BR", "Português", isDefault: true);
            configuration.Localization.AddLanguage("en", "English");
        }
    }
}
