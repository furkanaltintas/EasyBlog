namespace EasyBlog.Web.Helpers;

public static class DashboardTitleHelper
{
    public static string GetTranslatedControllerName(string controllerName, string actionName)
    {
        var controllerMap = new Dictionary<string, string>
        {
            { "Article:Index", "Makale Yönetimi" },
            { "Category:Index", "Kategori Yönetimi" },
            { "User:Index", "Kullanıcı Yönetimi" },
            { "Role:Index", "Rol Yönetimi" },
            { "Profile:Index", "Profil Ayarları" },
            { "User:Profile", "Profil Ayarları" },
            { "Article:DeletedArticle", "Makale Yönetimi" },
            { "Category:DeletedCategory", "Kategori Yönetimi" }
        };
        
        var newPath = $"{controllerName}:{actionName}"; // Örnek: "Category:Index"

        // Eğer tam eşleşen bir değer varsa, döndür
        if (controllerMap.ContainsKey(newPath))
            return controllerMap[newPath];

        // Eğer action yoksa, sadece controller adına göre bir eşleşme arayalım
        var controllerPath = controllerMap.Keys.FirstOrDefault(k => k.StartsWith(controllerName));

        return controllerPath != null ? controllerMap[controllerPath] : $"{controllerName} ({actionName})"; // Varsayılan döndürme
    }

    public static string GetTranslatedActionName(string controllerName, string actionName)
    {
        var newPath = $"{controllerName}:{actionName}"; // Örnek: "Category:Index"

        // Controller ve Action için Türkçe anlamlı işlem metni
        string actionDisplayName = newPath switch
        {
            "Category:Index" => "Kategoriler",
            "Article:Index" => "Makaleler",
            "User:Index" => "Kullanıcılar",
            "User:Profile" => "Profilim",
            "Role:Index" => "Roller",

            // Add, Update, Delete işlemleri için koşul
            var action when action.Contains("Add", StringComparison.OrdinalIgnoreCase) => $"{ChangeActionName(controllerName)} Ekleme",
            var action when action.Contains("Update", StringComparison.OrdinalIgnoreCase) => $"{ChangeActionName(controllerName)} Güncelleme",
            var action when action.Contains("Delete", StringComparison.OrdinalIgnoreCase) => $"{ChangeActionName(controllerName)} Silme",

            // Default
            _ => $"{controllerName}: {actionName}" // Eğer eşleşme yoksa orijinal haliyle döndürür
        };

        return actionDisplayName;
    }

    private static string ChangeActionName(string controllerName)
    {
        // Controller ve Türkçe karşılıklarını eşleyen bir Dictionary
        var controllerMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Category", "Kategori" },
            { "Article", "Makale" },
            { "User", "Kullanıcı" },
            { "Product", "Ürün" },
            { "Role", "Rol" },
        };

        // Dictionary'de controller ismi varsa, Türkçe karşılık döndürülür
        if (controllerMapping.TryGetValue(controllerName, out var translatedName))
            return translatedName;
        else
            // Eğer eşleşme yoksa, orijinal controller ismi döndürülür
            return controllerName;
    }
}