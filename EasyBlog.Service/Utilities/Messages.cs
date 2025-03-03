namespace EasyBlog.Service.Utilities;

public static class Messages
{
    public static class General
    {
        public static string ValidationError()
        {
            return "Bir veya daha fazla validasyon hatası ile karşılaşıldı.";
        }
    }

    public static class Article
    {
        public static string NotFound(bool isPlural)
        {
            if (isPlural) return "Hiç bir makale bulunamadı";
            return "Böyle bir makale bulunamadı";
        }

        public static string NotFoundById(Guid articleId)
        {
            return $"{articleId} koduna ait bir makale bulunamadı.";
        }

        public static string Add(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla eklenmiştir.";
        }

        public static string Update(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla güncellenmiştir.";
        }

        public static string Delete(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla silinmiştir.";
        }

        public static string HardDelete(string articleTitle)
        {
            return $"{articleTitle} adlı makale veritabanından silinmiştir.";
        }

        public static string UndoDelete(string articleTitle)
        {
            return $"{articleTitle} adlı makale başarıyla arşivden geri getirilmiştir.";
        }
    }

    public static class Category
    {
        public static string NotFound(bool isPlural)
        {
            if (isPlural) return "Hiç bir makale bulunamadı";
            return "Böyle bir makale bulunamadı";
        }

        public static string NotFoundById(Guid categoryId)
        {
            return $"{categoryId} koduna ait bir başlık bulunamadı.";
        }

        public static string Add(string name)
        {
            return $"{name} adlı başlık başarıyla eklenmiştir.";
        }

        public static string Update(string name)
        {
            return $"{name} adlı başlık başarıyla güncellenmiştir.";
        }

        public static string Delete(string name)
        {
            return $"{name} adlı başlık başarıyla silinmiştir.";
        }

        public static string HardDelete(string name)
        {
            return $"{name} adlı başlık veritabanından silinmiştir.";
        }

        public static string UndoDelete(string name)
        {
            return $"{name} adlı başlık başarıyla arşivden geri getirilmiştir.";
        }
    }

    public static class Auth
    {
        public static string UserInvalid() => "Kullanıcı oturumu geçersiz";
    }
}